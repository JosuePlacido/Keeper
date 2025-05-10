import { EChampionshipColumnNames, IChampionship } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';


export const updateById = async (id: number, championship: Omit<IChampionship, `${EChampionshipColumnNames.id}`>): Promise<void | Error> => {
	try {
		const result = await Knex(ETableNames.championship)
			.update(championship)
			.where('id', '=', id);

		if (result > 0) return;

		return new Error('Erro ao atualizar o registro');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao atualizar o registro');
	}
};
