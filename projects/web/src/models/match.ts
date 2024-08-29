import { EStatus, IEventGame, ITeam } from './index';

export interface IMatch {
	id: number;
	name: string;
	local?: string;
	date?: Date;
	groupId: number;
	hookHomeId?: number;
	hookAwayId?: number;
	homeId?: number;
	awayId?: number;
	round: number;
	goalsHome?: number;
	goalsAway?: number;
	goalsPenaltyHome?: number;
	goalsPenaltyAway?: number;
	finalGame: boolean;
	aggregateGame: boolean;
	penalty: boolean;
	aggregateGoalsAway?: number;
	aggregateGoalsHome?: number;
	status: EStatus;
	eventgame?: IEventGame[];
	homeTeam: ITeam;
	awayTeam: ITeam;
}

export enum EMatchColumnNames {
	id = 'id',
	name = 'name',
	local = 'local',
	date = 'date',
	groupId = 'groupId',
	hookHomeId = 'hookHomeId',
	hookAwayId = 'hookAwayId',
	homeId = 'homeId',
	awayId = 'awayId',
	round = 'round',
	goalsHome = 'goalsHome',
	goalsAway = 'goalsAway',
	goalsPenaltyHome = 'goalsPenaltyHome',
	goalsPenaltyAway = 'goalsPenaltyAway',
	finalGame = 'finalGame',
	aggregateGame = 'aggregateGame',
	penalty = 'penalty',
	aggregateGoalsAway = 'aggregateGoalsAway',
	aggregateGoalsHome = 'aggregateGoalsHome',
	status = 'status'
}
