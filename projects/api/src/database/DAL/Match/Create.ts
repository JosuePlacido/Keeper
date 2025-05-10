import { EMatchColumnNames, IMatch } from '../../../models';
import { ETableNames } from '../../ETableNames';
import { Knex } from '../../knex';

export const create = async (match: Omit<IMatch, `${EMatchColumnNames.id}`>): Promise<number | Error> => {
	try {
		const [result] = await Knex(ETableNames.match).insert(match).returning(EMatchColumnNames.id);

		if (typeof result === 'object') {
			return result.id;
		} else if (typeof result === 'number') {
			return result;
		}

		return new Error('Erro ao cadastrar o registro');
	} catch (error) {
		console.log(error);
		return new Error('Erro ao cadastrar o registro');
	}
};

export const createMany = async (matches: Omit<IMatch, `${EMatchColumnNames.id}`>[]): Promise<IMatch[] | Error> => {
	try {
		const result = await Knex(ETableNames.match).insert(matches).returning(EMatchColumnNames.id);
		return result;
	} catch (error) {
		console.log(error);
		return new Error('Erro ao cadastrar o registro');
	}
};
