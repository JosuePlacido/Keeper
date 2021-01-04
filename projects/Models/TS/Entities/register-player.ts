import Championship from './championship';
import IEntidade from './IEntity';
import Player from './player';
import Register from './register';

export default interface RegisterPlayer extends IEntidade {
	Id: string;
	RegisterId: string;
	Register: Register;
	PlayerId: string;
	Player: Player;
	Games: number;
	Goals: number;
	YellowCard: number;
	RedCard: number;
	MVPs: number;
	ChampionshipId: string;
	Championship: Championship;
}
