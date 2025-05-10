import { Status } from '../Enum/status';
import EventGame from './event-game';
import Group from './group';
import Vacancy from './hook-places';
import IEntidade from './IEntity';
import Register from './register';

export default interface Match extends IEntidade {
	Name: string;
	Local: string;
	Date: Date;
	GroupId: string;
	Group: Group;
	HookHome: Vacancy;
	HookHomeId: string;
	HookAway: Vacancy;
	HookAwayId: string;
	HomeId: string;
	Home: Register;
	AwayId: string;
	Away: Register;
	Round: number;
	GoalsHome: number;
	GoalsAway: number;
	GoalsPenaltyHome: number;
	GoalsPenaltyVisitante: number;
	FinalGame: boolean;
	AggregateGame: boolean;
	Penalty: boolean;
	AggregateGoalsAway?: number;
	AggregateGoalsHome?: number;
	Status: Status;
	EventGame: EventGame;
}
