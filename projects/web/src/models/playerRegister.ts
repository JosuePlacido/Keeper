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
	team: ITeam;
}
