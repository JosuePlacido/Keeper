import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EGroupColumnNames, EStatisticsColumnNames, ETeamRegisterColumnNames } from '../../models';

export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.statistics, table => {
			table.bigIncrements(EStatisticsColumnNames.id).primary().index();
			table.integer(EStatisticsColumnNames.games).notNullable().defaultTo(0);
			table.integer(EStatisticsColumnNames.goals).notNullable().defaultTo(0);
			table.integer(EStatisticsColumnNames.drowns).notNullable().defaultTo(0);
			table.integer(EStatisticsColumnNames.goalsAgainst).notNullable().defaultTo(0);
			table.integer(EStatisticsColumnNames.goalsDifference).notNullable().defaultTo(0);
			table.integer(EStatisticsColumnNames.lost).notNullable().defaultTo(0);
			table.integer(EStatisticsColumnNames.points).notNullable().defaultTo(0);
			table.integer(EStatisticsColumnNames.position).defaultTo(1);
			table.integer(EStatisticsColumnNames.reds).defaultTo(0);
			table.integer(EStatisticsColumnNames.won).defaultTo(1);
			table.integer(EStatisticsColumnNames.yellows).defaultTo(1);

			table
				.bigInteger(EStatisticsColumnNames.groupId)
				.index()
				.notNullable()
				.references(EGroupColumnNames.id)
				.inTable(ETableNames.group)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(EStatisticsColumnNames.registerId)
				.index()
				.notNullable()
				.references(ETeamRegisterColumnNames.id)
				.inTable(ETableNames.teamRegister);

			table.comment('Tabela usada para armazenar as estatisticas do time no grupo inscrito.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.statistics}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.statistics)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.statistics}`);
		});
}
