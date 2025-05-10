export interface IPlayer {
	id: number;
	name: string;
	nickname: string;
	mainPosition: string;
	born: Date;
	doc: string;
	picture?: string;
}

export enum EPlayerColumnNames {
	id = 'id',
	name = 'name',
	nickname = 'nickname',
	mainPosition = 'mainPosition',
	born = 'born',
	doc = 'doc',
}
