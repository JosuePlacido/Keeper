import { Classifieds } from '../Enum/Classifieds';
import { TypeStage } from '../Enum/TypeStage';
import Championship from './championship';
import Group from './group';
import IEntidade from './IEntity';

export default interface Stage extends IEntidade {
	Order: number;
	Name: string;
	Championship?: Championship;
	ChampionshipId: string;
	TypeStage: TypeStage;
	IsDoubleTurn: boolean;
	Criterias: string;
	Regulation: Classifieds;
	Group: Group[];
}
