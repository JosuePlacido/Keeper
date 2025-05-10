import { EClassifieds } from "./index";

export interface IVacancy {
	id: number;
	description: string;
	regulation: EClassifieds;
	fromGroupId: number;
	ownGroupId: number;
	position: number;
}
export enum ETeamPlaceColumnNames {
	id = 'id',
	description = 'description',
	regulation = 'regulation',
	fromGroupId = 'fromGroupId',
	ownGroupId = 'ownGroupId',
	position = 'position',
}
