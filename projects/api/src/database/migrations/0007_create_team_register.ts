import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EChampionshipColumnNames, ETeamColumnNames, ETeamRegisterColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.teamRegister, table => {
			table.bigIncrements(ETeamRegisterColumnNames.id).primary().index();
			table.integer(ETeamRegisterColumnNames.status).notNullable();
			table.integer(ETeamRegisterColumnNames.games).notNullable().defaultTo(0);
			table.integer(ETeamRegisterColumnNames.goals).notNullable().defaultTo(0);
			table.integer(ETeamRegisterColumnNames.drowns).notNullable().defaultTo(0);
			table.integer(ETeamRegisterColumnNames.goalsAgainst).notNullable().defaultTo(0);
			table.integer(ETeamRegisterColumnNames.goalsDifference).notNullable().defaultTo(0);
			table.integer(ETeamRegisterColumnNames.lost).notNullable().defaultTo(0);
			table.integer(ETeamRegisterColumnNames.red).defaultTo(0);
			table.integer(ETeamRegisterColumnNames.won).defaultTo(1);
			table.integer(ETeamRegisterColumnNames.yellows).defaultTo(1);

			table
				.bigInteger(ETeamRegisterColumnNames.championshipId)
				.index()
				.notNullable()
				.references(EChampionshipColumnNames.id)
				.inTable(ETableNames.championship)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(ETeamRegisterColumnNames.teamId)
				.index()
				.notNullable()
				.references(ETeamColumnNames.id)
				.inTable(ETableNames.team)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');

			table.comment('Tabela usada para armazenar a inscricao dos times.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.teamRegister}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.teamRegister)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.teamRegister}`);
		});
}
