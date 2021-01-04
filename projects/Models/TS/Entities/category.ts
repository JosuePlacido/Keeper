import IEntidade from './IEntity';

export default interface CategoryBase extends IEntidade {
	Name: string;
	Description: string;
}
