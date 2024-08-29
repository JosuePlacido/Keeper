import { IPlayer } from "./player";
import { ITeam } from "./team";

export interface IRegisterPlayer {
	id: number;
	registerId: number;
	playerId: number;
	games: number;
	goals: number;
	yellowCard: number;
	redCard: number;
	mvps: number;
	championshipId: number;
	player?: IPlayer;
	playerString?: string;
	team?: ITeam;
}

export enum ERegisterPlayer {
	id = 'id',
	registerId = 'registerId',
	playerId = 'playerId',
	games = 'games',
	goals = 'goals',
	yellowCard = 'yellowCard',
	redCard = 'redCard',
	mvps = 'mvps',
	championshipId = 'championshipId'
}
