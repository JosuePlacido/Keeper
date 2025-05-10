import { ETableNames } from '../../ETableNames';
import { EStatisticsColumnNames, ETeamColumnNames, ETypeEvent, IEventGame, IStatistics, ITeam, ITeamRegister } from '../../../models';
import { Knex } from '../../knex';

export const updateById = async (id: number, team: Partial<ITeam>): Promise<void | Error> => {
	try {
		const result = await Knex(ETableNames.team)
			.update(team)
			.where('id', '=', id);

		if (result > 0) return;

		return new Error('Erro ao atualizar o registro');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao atualizar o registro');
	}
};

export const registerResultStatistics = async (id: number, goalsScored: number, goalsSuffered: number, groupId: number, events: IEventGame[], transaction: any): Promise<void | Error> => {
	try {
		const goalsDiff = goalsScored - goalsSuffered;

		let { goals, goalsAgainst, goalsDifference, drowns, won, lost, yellows, red }: ITeamRegister = await transaction(ETableNames.teamRegister)
			.select('*')
			.where(ETeamColumnNames.id, '=', id)
			.first();
		const statistics: IStatistics = await transaction(ETableNames.statistics)
			.select('*')
			.where(EStatisticsColumnNames.registerId, '=', id)
			.where(EStatisticsColumnNames.groupId, '=', groupId)
			.first();

		statistics.games++;
		statistics.goals += goalsScored;
		statistics.goalsAgainst += goalsSuffered;
		statistics.goalsDifference += goalsDiff;
		goals += goalsScored;
		goalsAgainst += goalsSuffered;
		goalsDifference += goalsDiff;

		if (goalsDiff === 0) {
			drowns++;
			statistics.drowns++;
			statistics.points += 1;
		} else if (goalsDiff < 0) {
			lost++;
			statistics.lost++;
		} else if (goalsDiff > 0) {
			won++;
			statistics.won++;
			statistics.points += 3;
		}

		yellows += events.filter(e => e.type === ETypeEvent.YellowCard && e.teamRegisterId === id).length;
		red += events.filter(e => e.type === ETypeEvent.RedCard && e.teamRegisterId === id).length;
		statistics.yellows += yellows;
		statistics.reds += red;

		await transaction(ETableNames.teamRegister)
			.update({
				goals, goalsAgainst, goalsDifference, drowns, won, lost, yellows, red
			})
			.where('id', '=', id);
		await transaction(ETableNames.statistics)
			.update({
				games: statistics.games,
				drowns: statistics.drowns,
				goals: statistics.goals,
				goalsAgainst: statistics.goalsAgainst,
				goalsDifference: statistics.goalsDifference,
				lost: statistics.lost,
				points: statistics.points,
				reds: statistics.reds,
				won: statistics.won,
				yellows: statistics.yellows
			} as IStatistics)
			.where(EStatisticsColumnNames.id, '=', statistics.id);
	} catch (error) {
		console.log(error);
		throw new Error('Erro ao atualizar o registro');
	}
};
