export interface IUser {
	id: number;
	login: string;
	password: string;
}

export enum EUserColumnNames {
	id = 'id',
	login = 'login',
	password = 'password'
}
