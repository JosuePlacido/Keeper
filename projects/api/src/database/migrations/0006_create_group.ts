import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EGroupColumnNames, EStageColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.group, table => {
			table.bigIncrements(EGroupColumnNames.id).primary().index();
			table.string(EGroupColumnNames.name).notNullable();
			table
				.bigInteger(EGroupColumnNames.stageId)
				.index()
				.notNullable()
				.references(EStageColumnNames.id)
				.inTable(ETableNames.stage)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');

			table.comment('Tabela usada para armazenar os grupos das fazes.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.group}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.group)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.group}`);
		});
}
