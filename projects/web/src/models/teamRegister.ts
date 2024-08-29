import { EStatus } from "./enum";
import { IRegisterPlayer } from "./playerRegister";
import { ITeam } from "./team";

export interface ITeamRegister {
	id: number;
	championshipId: number;
	teamId: number;
	status: EStatus;
	players?: IRegisterPlayer[];
	games: number;
	won: number;
	drowns: number;
	lost: number;
	goals: number;
	goalsAgainst: number;
	goalsDifference: number;
	yellows: number;
	red: number;
	team: ITeam;
}
