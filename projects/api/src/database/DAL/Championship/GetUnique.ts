import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';
import { EChampionshipColumnNames, EGroupColumnNames, EMatchColumnNames, EPlayerColumnNames, ERegisterPlayer, EStageColumnNames, EStatisticsColumnNames, ETeamColumnNames, ETeamRegisterColumnNames, IChampionship, IGroup, IStage } from '../../../models';

const idIndexesDictionary: { [key: number]: number } = {};
export const getByIdWithMatchesAndRanking = async (id: number): Promise<IChampionship> => {
	try {
		const result: IChampionship = await Knex(ETableNames.championship)
			.select('*')
			.where(EChampionshipColumnNames.id, '=', id)
			.first();

		result.teams = await Knex(`${ETableNames.teamRegister} as tr`)
			.select('tr.*')
			.select(Knex.raw("json_object('id', t.id,'name',t.name,'abrev',t.abrev,'logoURL',t.logoURL) as teamString"))
			.leftJoin(`${ETableNames.team} as t`, `tr.${ETeamRegisterColumnNames.teamId}`, `t.${ETeamColumnNames.id}`)
			.where(`tr.${ETeamRegisterColumnNames.championshipId}`, '=', id);
		result.scorers = [];
		for (let t = 0; t < result.teams.length; t++) {
			idIndexesDictionary[result.teams[t].teamId] = t;
			result.teams[t].team = JSON.parse(result.teams[t].teamString!);
			result.teams[t].teamString = undefined;
			result.teams[t].players = await Knex(`${ETableNames.playerRegister} as pr`)
				.select('pr.*')
				.select(Knex.raw(`(?) as playerString`,
					Knex(`${ETableNames.player} as p`)
						.select(Knex.raw("json_object('id', p.id,'name',p.name,'mainPosition',p.mainPosition,'nickname',p.nickname)"))
						.where(EPlayerColumnNames.id, '=', Knex.ref(`pr.${ERegisterPlayer.playerId}`))))
				.where(ERegisterPlayer.registerId, '=', result.teams[t].id);
			result.teams[t].players = result.teams[t].players?.map(p => {
				if (p.playerString) {
					p.player = JSON.parse(p.playerString)
					p.playerString = undefined;
				}
				return p;
			}) || [];
			if (result.teams[t].players !== undefined) {
				result.scorers.push(...result.teams[t].players!.filter(pr => pr.goals > 0).map(pr => {
					pr.team = result.teams![t].team;
					return pr;
				}));
			}
		}
		result.scorers = result.scorers.sort((a, b) => b.goals - a.goals).slice(0, 10);

		result.stages = await Knex(ETableNames.stage)
			.select(`*`)
			.where(EStageColumnNames.championshipId, '=', result.id);
		const groups: IGroup[] = await await Knex(ETableNames.group)
			.select(`*`)
			.whereIn(EGroupColumnNames.stageId, result.stages.map((s: IStage) => s.id));

		for (let g = 0; g < groups.length; g++) {
			groups[g].statistics = await Knex(ETableNames.statistics)
				.select('*')
				.where(EStatisticsColumnNames.groupId, '=', groups[g].id)
				.orderBy(EStatisticsColumnNames.position);
			groups[g].statistics = groups[g].statistics.map(s => {
				s.teamRegister = result.teams![idIndexesDictionary[s.registerId]];
				return s;
			});

			groups[g].matchs = await Knex(ETableNames.match)
				.select('*')
				.where(EMatchColumnNames.groupId, '=', groups[g].id);
			groups[g].matchs = groups[g].matchs.map(m => {
				m.homeTeam = m.homeId ? result.teams![idIndexesDictionary[m.homeId]].team : undefined;
				m.awayTeam = m.awayId ? result.teams![idIndexesDictionary[m.awayId]].team : undefined;
				return m;
			});
		}
		result.stages.forEach((s: IStage) => s.groups = groups.filter(g => g.stageId === s.id));

		return result;
	} catch (error) {
		console.log(error);
		throw new Error('Erro ao consultar o registro');
	}
};

export const getScopeById = async (id: number): Promise<IChampionship> => {
	try {
		const result: IChampionship = await Knex(ETableNames.championship)
			.select(`*`)
			.where(EChampionshipColumnNames.id, '=', id)
			.first();
		result.stages = await await Knex(ETableNames.stage)
			.select(`*`)
			.where(EStageColumnNames.championshipId, '=', result.id);
		const groups: IGroup[] = await await Knex(ETableNames.group)
			.select(`*`)
			.whereIn(EGroupColumnNames.stageId, result.stages.map(s => s.id));
		result.stages.forEach(s => s.groups = groups.filter(g => g.stageId === s.id));
		return result;
	} catch (error) {
		console.log(error);
		throw new Error('Erro ao consultar o registro');
	}
};
