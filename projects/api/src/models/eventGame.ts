import { ETypeEvent } from './index';

export interface IEventGame {
	id: number;
	order: number;
	type: ETypeEvent;
	matchId: number;
	registerPlayerId: number;
	description: string;
	isHomeEvent: boolean;
	teamRegisterId: number;
}

export enum EEventGameColumnNames {
	id = 'id',
	order = 'order',
	type = 'type',
	matchId = 'matchId',
	registerPlayerId = 'registerPlayerId',
	description = 'description',
	isHomeEvent = 'isHomeEvent',
	teamRegisterId = 'teamRegisterId',
}
