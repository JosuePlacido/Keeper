import { Knex as TKnex } from 'knex';
import { EMatchColumnNames, EStatus, IMatch } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';

export const updateById = async (id: number, match: Omit<IMatch, `${EMatchColumnNames.id}`>): Promise<void | Error> => {
	try {
		const result = await Knex(ETableNames.match)
			.update(match)
			.where('id', '=', id);

		if (result > 0) return;

		return new Error('Erro ao atualizar o registro');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao atualizar o registro');
	}
};
interface IRegisterResult {
	id: number;
	homeGoals: number;
	awayGoals: number;
	homePenalties?: number;
	awayPenalties?: number;
}
export const registerResult = async (result: IRegisterResult, transaction: TKnex.Transaction): Promise<number[]> => {
	try {
		const [match]: IMatch[] = await transaction(ETableNames.match).select('*').where(EMatchColumnNames.id, '=', result.id);
		match.status = EStatus.Finish;
		match.goalsHome = result.homeGoals;
		match.goalsAway = result.awayGoals;
		if (match.aggregateGame) {
			match.aggregateGoalsHome = result.homeGoals + (match.aggregateGoalsHome == null ? 0 : match.aggregateGoalsHome);
			match.aggregateGoalsAway = result.awayGoals + (match.aggregateGoalsAway == null ? 0 : match.aggregateGoalsAway);
		}
		if (match.penalty && ((match.aggregateGame && match.aggregateGoalsAway == match.aggregateGoalsHome) ||
			match.goalsPenaltyHome == match.goalsPenaltyAway)) {
			match.goalsPenaltyHome = result.homePenalties;
			match.goalsPenaltyAway = result.awayPenalties;
		}
		await transaction(ETableNames.match)
			.update({
				status: match.status,
				goalsHome: match.goalsHome,
				goalsAway: match.goalsAway,
				aggregateGoalsHome: match.aggregateGoalsHome,
				aggregateGoalsAway: match.aggregateGoalsAway,
				goalsPenaltyHome: match.goalsPenaltyHome,
				goalsPenaltyAway: match.goalsPenaltyAway
			} as IMatch)
			.where('id', '=', result.id);

		return [match.groupId, match.homeId!, match.awayId!];
	} catch (error) {
		console.log(error);
		throw Error('Erro ao atualizar o registro');
	}
};
