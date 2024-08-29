import { EChampionshipColumnNames, EStatisticsColumnNames, IChampionship, IEventGame, IStatistics } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';


export const createMany = async (events: IEventGame[], transaction: any): Promise<void | Error> => {
	try {
		await transaction(ETableNames.eventGame).insert(events).returning('*');
	} catch (error) {
		console.log(error);
		throw new Error('Erro ao atualizar o registro');
	}
};
