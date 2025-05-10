import { Knex } from 'knex';

import { ETableNames } from '../ETableNames';
import { EGroupColumnNames, EMatchColumnNames, EStageColumnNames, ETeamPlaceColumnNames, ETeamRegisterColumnNames } from '../../models';


export async function up(knex: Knex) {
	return knex
		.schema
		.createTable(ETableNames.match, table => {
			table.bigIncrements(EMatchColumnNames.id).primary().index();
			table.string(EMatchColumnNames.name).notNullable();
			table.integer(EMatchColumnNames.aggregateGame);
			table.integer(EMatchColumnNames.aggregateGoalsAway);
			table.integer(EMatchColumnNames.aggregateGoalsHome);
			table.date(EMatchColumnNames.date);
			table.boolean(EMatchColumnNames.finalGame);
			table.integer(EMatchColumnNames.goalsAway);
			table.integer(EMatchColumnNames.goalsHome);
			table.integer(EMatchColumnNames.goalsPenaltyHome);
			table.integer(EMatchColumnNames.goalsPenaltyAway);
			table.string(EMatchColumnNames.local);
			table.integer(EMatchColumnNames.round);
			table.integer(EMatchColumnNames.status);
			table.boolean(EMatchColumnNames.penalty);

			table
				.bigInteger(EMatchColumnNames.awayId)
				.index()
				.references(ETeamRegisterColumnNames.id)
				.inTable(ETableNames.teamRegister)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(EMatchColumnNames.homeId)
				.index()
				.references(ETeamRegisterColumnNames.id)
				.inTable(ETableNames.teamRegister)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(EMatchColumnNames.hookHomeId)
				.index()
				.references(ETeamPlaceColumnNames.id)
				.inTable(ETableNames.teamPlace)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(EMatchColumnNames.hookAwayId)
				.index()
				.references(ETeamPlaceColumnNames.id)
				.inTable(ETableNames.teamPlace)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');
			table
				.bigInteger(EMatchColumnNames.groupId)
				.index()
				.references(EGroupColumnNames.id)
				.inTable(ETableNames.group)
				.onUpdate('CASCADE')
				.onDelete('CASCADE');

			table.comment('Tabela usada para armazenar os grupos das fazes.');
		})
		.then(() => {
			console.log(`# Created table ${ETableNames.match}`);
		});
}

export async function down(knex: Knex) {
	return knex
		.schema
		.dropTable(ETableNames.match)
		.then(() => {
			console.log(`# Dropped table ${ETableNames.match}`);
		});
}
