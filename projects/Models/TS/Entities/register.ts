import { Status } from '../Enum/status';
import IEntidade from './IEntity';
import RegisterPlayer from './register-player';
import Statistics from './statistics';
import Team from './team';

export default interface Register extends IEntidade {
	ChampionshipId: string;
	TeamId: string;
	Status: Status;
	Team: Team;
	Players: RegisterPlayer[];
	Statistics: Statistics[];
}
