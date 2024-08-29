import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EUserColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.user, table => {
			table.bigIncrements(EUserColumnNames.id).primary().index();
			table.string(EUserColumnNames.login, 150).checkLength('<=', 150).index().notNullable();
			table.string(EUserColumnNames.password).notNullable();

			table.comment('Tabela usada para armazenar os usuários.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.user}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.user)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.user}`);
		});
}
