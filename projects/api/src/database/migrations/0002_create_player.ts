import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EPlayerColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.player, table => {
			table.bigIncrements(EPlayerColumnNames.id).primary().index();
			table.string(EPlayerColumnNames.mainPosition, 150).notNullable();
			table.string(EPlayerColumnNames.name).notNullable();
			table.string(EPlayerColumnNames.nickname).notNullable();
			table.string(EPlayerColumnNames.doc).notNullable();
			table.date(EPlayerColumnNames.born).notNullable();
			table.string(EPlayerColumnNames.picture);

			table.comment('Tabela usada para armazenar os jogadores.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.player}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.player)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.player}`);
		});
}
