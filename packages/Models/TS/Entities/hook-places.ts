import { Classifieds } from '../Enum/Classifieds';
import Group from './group';
import IEntidade from './IEntity';

export default interface HookPlaces extends IEntidade {
	Id: string;
	Description: string;
	Regulation: Classifieds;
	FromGroup: Group;
	FromGroupId: string;
	OwnGroup: Group;
	OwnGroupId: string;
	Position: number;
}
