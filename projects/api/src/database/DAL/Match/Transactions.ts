import { Knex as TKnex } from 'knex';
import { Knex } from '../../knex';

export const getTransaction = async (): Promise<TKnex.Transaction> => {
	return await Knex.transaction();
};
export const commit = async (transaction: TKnex.Transaction) => {
	await transaction.commit();
};
