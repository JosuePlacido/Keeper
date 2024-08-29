import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { ETeamColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.team, table => {
			table.bigIncrements(ETeamColumnNames.id).primary().index();
			table.string(ETeamColumnNames.name).notNullable();
			table.string(ETeamColumnNames.abrev);
			table.string(ETeamColumnNames.logoURL);

			table.comment('Tabela usada para armazenar os times.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.team}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.team)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.team}`);
		});
}
