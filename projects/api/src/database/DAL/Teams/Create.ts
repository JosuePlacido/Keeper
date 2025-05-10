import { ETableNames } from '../../ETableNames';
import { ETeamColumnNames, ITeam } from '../../../models';
import { Knex } from '../../knex';


export const create = async (team: Omit<ITeam, ETeamColumnNames.id>): Promise<number> => {
	try {
		const [result] = await Knex(ETableNames.team).insert(team).returning(ETeamColumnNames.id);

		return result.id;
	} catch (error) {
		console.log(error, team);
		throw Error('Erro ao cadastrar o registro');
	}
};
