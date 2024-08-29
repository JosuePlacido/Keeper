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
	teamRegister?: ITeamRegister;
	teamRegisterString?: string;
}

export enum EStatisticsColumnNames {
	id = 'id',
	groupId = 'groupId',
	registerId = 'registerId',
	games = 'games',
	won = 'won',
	drowns = 'drowns',
	lost = 'lost',
	goals = 'goals',
	position = 'position',
	goalsAgainst = 'goalsAgainst',
	goalsDifference = 'goalsDifference',
	yellows = 'yellows',
	reds = 'reds',
	points = 'points',
}
export enum ECritrion {
	points = EStatisticsColumnNames.points,
	win = EStatisticsColumnNames.won,
	goalsDiff = EStatisticsColumnNames.goalsDifference,
	goals = EStatisticsColumnNames.goals,
	reds = EStatisticsColumnNames.reds,
	yellows = EStatisticsColumnNames.yellows,
	goalsAgainst = EStatisticsColumnNames.goalsAgainst,
	direct = 'confront direto',
	awayGoals = 'gols marcados como visitante',
	TiebreakGame = 'jogo desempate',
	penalty = 'disputa de penalty',
	random = 'sorteio'
}
