import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EChampionshipColumnNames, EStageColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.stage, table => {
			table.bigIncrements(EStageColumnNames.id).primary().index();
			table.integer(EStageColumnNames.order).notNullable();
			table.string(EStageColumnNames.name).notNullable();
			table.boolean(EStageColumnNames.isDoubleTurn).notNullable();
			table.string(EStageColumnNames.criterias).notNullable();
			table.integer(EStageColumnNames.regulation).notNullable();
			table.integer(EStageColumnNames.typeStage).notNullable();
			table
				.bigInteger(EStageColumnNames.championshipId)
				.index()
				.notNullable()
				.references(EChampionshipColumnNames.id)
				.inTable(ETableNames.championship)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');

			table.comment('Tabela usada para armazenar as fases.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.stage}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.stage)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.stage}`);
		});
}
