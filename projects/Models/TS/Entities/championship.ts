import IEntidade from './IEntity';
import Stage from './stage';

export default interface Championship extends IEntidade {
	Name: string;
	Category: string;
	Season: string;
	CategoryId: string;
	Stages: Stage[];
	Status: string;
}
