export interface ITeam {
	id: number;
	name: string;
	logoURL: string;
	abrev: string;
}

export enum ETeamColumnNames {
	id = 'id',
	name = 'name',
	logoURL = 'logoURL',
	abrev = 'abrev'
}
