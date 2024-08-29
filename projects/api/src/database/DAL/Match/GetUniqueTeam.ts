import { EMatchColumnNames, IMatch } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';


export const getById = async (id: number): Promise<IMatch | Error> => {
	try {
		const result = await Knex(ETableNames.match)
			.select('*')
			.where(EMatchColumnNames.id, '=', id)
			.first();

		if (result) return result;

		return new Error('Registro não encontrado');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao consultar o registro');
	}
};
