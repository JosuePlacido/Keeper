import { EStatus } from './enum';
import { IPlayer } from './player';
import { IRegisterPlayer } from './playerRegister';
import { IStage } from './stage';
import { ITeamRegister } from './teamRegister';

export interface IChampionship {
	id: number;
	name: string;
	category: string;
	season: string;
	isSketch: boolean;
	status: EStatus;
	stages?: IStage[];
	teams?: ITeamRegister[];
	scorers?: IRegisterPlayer[];
}

export interface IScorers {
	player: IPlayer;
	team: string;
	goals: number;
}

export enum EChampionshipColumnNames {
	id = 'id',
	name = 'name',
	category = 'category',
	season = 'season',
	isSketch = 'isSketch'
}
