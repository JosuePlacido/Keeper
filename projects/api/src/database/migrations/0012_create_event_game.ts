import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EEventGameColumnNames, EMatchColumnNames, ERegisterPlayer } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.eventGame, table => {
			table.bigIncrements(EEventGameColumnNames.id).primary().index();
			table.string(EEventGameColumnNames.description).notNullable();
			table.boolean(EEventGameColumnNames.isHomeEvent);
			table.integer(EEventGameColumnNames.order);
			table.integer(EEventGameColumnNames.type);

			table
				.bigInteger(EEventGameColumnNames.registerPlayerId)
				.index()
				.references(ERegisterPlayer.id)
				.inTable(ETableNames.playerRegister)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(EEventGameColumnNames.matchId)
				.index()
				.references(EMatchColumnNames.id)
				.inTable(ETableNames.match)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');

			table.comment('Tabela usada para armazenar os grupos das fazes.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.eventGame}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.eventGame)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.eventGame}`);
		});
}
