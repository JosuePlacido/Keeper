import { EChampionshipColumnNames, EStatisticsColumnNames, IChampionship, IStatistics } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';


export const updateRank = async (statistics: IStatistics[], transaction: any): Promise<void | Error> => {
	try {
		for (let i = 0; i < statistics.length; i++) {
			await transaction(ETableNames.statistics)
				.update({ position: statistics[i].position } as IStatistics)
				.where(EStatisticsColumnNames.id, '=', statistics[i].id)
		}
	} catch (error) {
		console.log(error);
		throw new Error('Erro ao atualizar o registro');
	}
};
