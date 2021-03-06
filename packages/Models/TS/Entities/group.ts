import HookPlaces from './hook-places';
import IEntidade from './IEntity';
import Match from './match';
import Register from './register';
import Stage from './stage';
import Statistics from './statistics';

export default interface Group extends IEntidade {
	Name: string;
	StageId: string;
	Stage: Stage;
	Teams: Register[];
	HookPlaces: HookPlaces[];
	Statistics: Statistics[];
	Matchs: Match;
}
