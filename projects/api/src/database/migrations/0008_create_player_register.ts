import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EChampionshipColumnNames, EPlayerColumnNames, ERegisterPlayer, ETeamColumnNames, ETeamRegisterColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.playerRegister, table => {
			table.bigIncrements(ERegisterPlayer.id).primary().index();
			table.integer(ERegisterPlayer.games).notNullable().defaultTo(0);
			table.integer(ERegisterPlayer.goals).notNullable().defaultTo(0);
			table.integer(ERegisterPlayer.mvps).notNullable().defaultTo(0);
			table.integer(ERegisterPlayer.redCard).notNullable().defaultTo(0);
			table.integer(ERegisterPlayer.yellowCard).notNullable().defaultTo(0);

			table
				.bigInteger(ERegisterPlayer.championshipId)
				.index()
				.notNullable()
				.references(EChampionshipColumnNames.id)
				.inTable(ETableNames.championship)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(ERegisterPlayer.playerId)
				.index()
				.notNullable()
				.references(EPlayerColumnNames.id)
				.inTable(ETableNames.player);
			table
				.bigInteger(ERegisterPlayer.registerId)
				.index()
				.notNullable()
				.references(ETeamRegisterColumnNames.id)
				.inTable(ETableNames.teamRegister)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');

			table.comment('Tabela usada para armazenar a inscricao e estatisticas dos times.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.playerRegister}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.playerRegister)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.playerRegister}`);
		});
}
