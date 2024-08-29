import { ETableNames } from '../../ETableNames';
import { ETeamColumnNames, ITeam } from '../../../models/team';
import { Knex } from '../../knex';


export const getById = async (id: number): Promise<ITeam | Error> => {
	try {
		const result = await Knex(ETableNames.team)
			.select('*')
			.where(ETeamColumnNames.id, '=', id)
			.first();

		if (result) return result;

		return new Error('Registro não encontrado');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao consultar o registro');
	}
};
