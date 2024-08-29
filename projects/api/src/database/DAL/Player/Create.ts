import { ETableNames } from '../../ETableNames';
import { EPlayerColumnNames, IPlayer } from '../../../models';
import { Knex } from '../../knex';


export const create = async (player: Omit<IPlayer, EPlayerColumnNames.id>): Promise<number> => {
	try {
		const [result] = await Knex(ETableNames.player).insert(player).returning(EPlayerColumnNames.id);

		if (typeof result === 'object') {
			return result.id;
		} else {
			return result;
		}
	} catch (error) {
		console.log(error);
		throw Error('Erro ao cadastrar o registro');
	}
};
