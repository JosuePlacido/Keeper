import { ITeamRegister } from "./teamRegister";

export interface IStatistics {
	id: number;
	groupId: number;
	registerId: number;
	games: number;
	won: number;
	drowns: number;
	lost: number;
	goals: number;
	position: number;
	goalsAgainst: number;
	goalsDifference: number;
	yellows: number;
	reds: number;
	points: number;
	teamRegister: ITeamRegister;
	teamRegisterString: string;
}

export enum ECritrion {
	points = 'points',
	win = 'won',
	goalsDiff = 'goalsDifference',
	goals = 'goals',
	reds = 'reds',
	yellows = 'yellows',
	goalsAgainst = 'goalsAgainst',
	direct = 'confront direto',
	awayGoals = 'gols marcados como visitante',
	TiebreakGame = 'jogo desempate',
	penalty = 'disputa de penalty',
	random = 'sorteio'
}
