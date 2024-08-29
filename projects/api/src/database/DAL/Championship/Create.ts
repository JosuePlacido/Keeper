import { ETableNames } from '../../ETableNames';
import { EChampionshipColumnNames, EGroupColumnNames, EStageColumnNames, EStatus, ETeamPlaceColumnNames, ETeamRegisterColumnNames, IChampionship, IGroup, IMatch, IRegisterPlayer, IStatistics, ITeamRegister, IVacancy, } from '../../../models';
import { Knex } from '../../knex';
import knex from 'knex';

export interface IChampionshipCreateScope {
	name: string;
	category: string;
	season: string;
	status: EStatus;
	isSketch: boolean;
	teams: {
		id: number;
		players: number[];
	}[]
}

export const createScope = async ({ teams, ...championship }: IChampionshipCreateScope): Promise<IChampionship | Error> => {
	try {
		const transaction = await Knex.transaction();
		const playerRegister: IRegisterPlayer[] = [];

		const [championshipId]: number[] = await transaction(ETableNames.championship).insert(championship).returning(EChampionshipColumnNames.id);
		const teamRegisterToInsert: ITeamRegister[] = teams.map(t => {
			return {
				championshipId: championshipId,
				teamId: t.id
			} as ITeamRegister
		});
		const teamRegister: ITeamRegister[] = await transaction(ETableNames.teamRegister).insert(teamRegisterToInsert).returning('*');

		for (let x = 0; x <= teamRegister.length; x++) {
			playerRegister.push(...(teams[x].players.map(p => {
				return {
					playerId: p,
					registerId: teamRegister[x].id
				} as IRegisterPlayer
			})))
		}

		const playersRegistered: IRegisterPlayer[] = await transaction(ETableNames.playerRegister).insert(playerRegister).returning('*');
		transaction.commit();

		for (let x = 0; x <= playersRegistered.length; x++) {
			const indexTeam = teamRegister.findIndex(tr => tr.id === playersRegistered[x].registerId);
			if (!teamRegister[indexTeam].players)
				teamRegister[x].players = [];
			teamRegister[x].players!.push(playersRegistered[x])
		}

		return { ...championship, id: championshipId, teams: teamRegister };
	} catch (error) {
		console.log(error);
		return new Error('Erro ao cadastrar o registro');
	}
};
export const createChampionshipMode = async (championship: IChampionship): Promise<IChampionship | Error> => {
	try {
		const transaction = await Knex.transaction();
		const groups: IGroup[] = [];
		const statistics: IStatistics[] = [];
		const vacancys: IVacancy[] = [];
		const stages = championship.stages!.map(s => {
			s.groups = [];
			return s;
		});

		if (championship.stages) {
			const stagesId = await transaction(ETableNames.stage).insert(championship.stages).returning(EStageColumnNames.id);
			for (let x = 0; x < championship.stages.length; x++) {
				if (championship.stages[x].groups !== undefined) {
					championship.stages[x].id = stagesId[x];
					groups.push(...(championship.stages[x].groups.map(g => { g.stageId = stagesId[x]; return g })));
				}
			}
			const groupsId = await transaction(ETableNames.group).insert(groups).returning(EGroupColumnNames.id);
			for (let x = 0; x < groups.length; x++) {
				groups[x].id = groupsId[x];
				if (groups[x].teams !== undefined) {
					statistics.push(...(groups[x].teams.map(t => {
						return {
							groupId: groups[x].id,
							registerId: t.id
						} as IStatistics
					})));
					vacancys.push(...(groups[x].vacancy.map(s => { s.fromGroupId = groupsId[s.fromGroupIndex], s.ownGroupId = groupsId[s.ownGroupIndex]; return s })));
				}
			}
			const statisticsRegistered: IStatistics[] = await transaction(ETableNames.statistics).insert(statistics).returning('*');
			const teamPlacesRegistered: IVacancy[] = await transaction(ETableNames.teamPlace).insert(vacancys).returning('*');
			for (let x = 0; x < stages.length; x++) {
				stages[x].groups.push(...groups.filter(g => g.stageId === stages[x].id))
				for (let g = 0; g < stages[x].groups.length; g++) {
					stages[x].groups[g].statistics.push(...statisticsRegistered.filter(sr => sr.groupId === stages[x].groups[g].id))
					stages[x].groups[g].vacancy.push(...teamPlacesRegistered.filter(vr => vr.ownGroupId === stages[x].groups[g].id))
				}

			}
			transaction.commit();
		}

		return { ...championship, stages };
	} catch (error) {
		console.log(error);
		return new Error('Erro ao cadastrar o registro');
	}
};
