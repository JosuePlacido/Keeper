import { IGroup, ETypeStage, EClassifieds } from './index';

export interface IStage {
	id: number;
	order: number;
	name: string;
	championshipId: number;
	typeStage: ETypeStage;
	isDoubleTurn: boolean;
	criterias: string;
	regulation: EClassifieds;
	groups: IGroup[];
}

export enum EStageColumnNames {
	id = 'id',
	order = 'order',
	name = 'name',
	championshipId = 'championshipId',
	typeStage = 'typeStage',
	isDoubleTurn = 'isDoubleTurn',
	criterias = 'criterias',
	regulation = 'regulation',
}

