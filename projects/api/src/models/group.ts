import { IMatch, IStatistics, ITeamRegister, IVacancy } from './index';

export interface IGroup {
	id: number;
	name: string;
	stageId: number;
	teams: ITeamRegister[];
	vacancy: IVacancy[];
	statistics: IStatistics[];
	matchs: IMatch[];
}

export enum EGroupColumnNames {
	id = 'id',
	name = 'name',
	stageId = 'stageId'
}
