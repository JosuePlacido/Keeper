import { ETableNames } from '../../ETableNames';
import { EUserColumnNames, IUser } from '../../../models';
import { Knex } from '../../knex';


export const getByLogin = async (login: string): Promise<IUser | Error> => {
	try {
		const result = await Knex(ETableNames.user)
			.select('*')
			.where(EUserColumnNames.login, '=', login)
			.first();

		if (result) return result;

		return new Error('Registro não encontrado');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao consultar o registro');
	}
};
