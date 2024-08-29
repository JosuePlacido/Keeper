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
	team?: ITeam;
	teamString?: string;
}

export enum ETeamRegisterColumnNames {
	id = 'id',
	championshipId = 'championshipId',
	teamId = 'teamId',
	status = 'status',
	games = 'games',
	won = 'won',
	drowns = 'drowns',
	lost = 'lost',
	goals = 'goals',
	goalsAgainst = 'goalsAgainst',
	goalsDifference = 'goalsDifference',
	yellows = 'yellows',
	red = 'red',
}
