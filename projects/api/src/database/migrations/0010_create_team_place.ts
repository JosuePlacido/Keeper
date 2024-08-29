import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EGroupColumnNames, EStatisticsColumnNames, ETeamPlaceColumnNames, ETeamRegisterColumnNames } from '../../models';

export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.teamPlace, table => {
			table.bigIncrements(ETeamPlaceColumnNames.id).primary().index();
			table.integer(ETeamPlaceColumnNames.position).notNullable().defaultTo(0);
			table.integer(ETeamPlaceColumnNames.description).notNullable().defaultTo(0);
			table.integer(ETeamPlaceColumnNames.regulation).notNullable().defaultTo(0);

			table
				.bigInteger(ETeamPlaceColumnNames.fromGroupId)
				.references(EGroupColumnNames.id)
				.inTable(ETableNames.group)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(ETeamPlaceColumnNames.ownGroupId)
				.references(EGroupColumnNames.id)
				.inTable(ETableNames.group);

			table.comment('Tabela usada para armazenar as vagas da fase para os classificados.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.teamPlace}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.teamPlace)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.teamPlace}`);
		});
}
