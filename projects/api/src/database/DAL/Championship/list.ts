import { IChampionship } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';

export const getAll = async (page = 1, limit = 20): Promise<IChampionship[] | Error> => {
	try {
		const result = await Knex(ETableNames.championship)
			.select('*')
			.offset((page - 1) * limit)
			.limit(limit);

		return result;
	} catch (error) {
		console.log(error);
		return new Error('Erro ao consultar os registros');
	}
};
/*
export const availableForChampionship = async (page: number, limit: number, filterName: string): Promise<Team[] | Error> => {
	try {
		const result = await Knex(ETableNames.teams)
			.select('*')
			//.where('')
			.whereLike(ETeamColumnNames.name, `%${filterName}%`)
			.offset((page - 1) * limit)
			.limit(limit);

		return result;
	} catch (error) {
		console.log(error);
		return new Error('Erro ao consultar os registros');
	}
};*/
