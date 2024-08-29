import { IMatch } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';

export const getAll = async (page = 1, limit = 20): Promise<IMatch[] | Error> => {
	try {
		const result = await Knex(ETableNames.match)
			.select('*')
			.offset((page - 1) * limit)
			.limit(limit);

		return result;
	} catch (error) {
		console.log(error);
		return new Error('Erro ao consultar os registros');
	}
};
