import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EChampionshipColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.championship, table => {
			table.bigIncrements(EChampionshipColumnNames.id).primary().index();
			table.string(EChampionshipColumnNames.category).notNullable();
			table.string(EChampionshipColumnNames.name).notNullable();
			table.string(EChampionshipColumnNames.season).notNullable();
			table.boolean(EChampionshipColumnNames.isSketch).defaultTo(true);

			table.comment('Tabela usada para armazenar campeonatos.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.championship}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.championship)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.championship}`);
		});
}
