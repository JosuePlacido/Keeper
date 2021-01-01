import { TypeEvent } from '../Enum/type-event';
import IEntidade from './IEntity';
import Match from './match';
import RegisterPlayer from './register-player';

export default interface EventGame extends IEntidade {
	Id: string;
	Order: number;
	Type: TypeEvent;
	MatchId: string;
	Match: Match;
	RegisterPlayerId: string;
	RegisterPlayer: RegisterPlayer;
	Description: string;
	IsHomeEvent: boolean;
}
