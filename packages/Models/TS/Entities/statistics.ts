import Group from './group';
import IEntidade from './IEntity';
import Register from './register';

export default interface Statistics extends IEntidade {
	GroupId: string;
	Group: Group;
	RegisterId: string;
	Register: Register;
	Games: number;
	Won: number;
	Drowns: number;
	Lost: number;
	GoalsScores: number;
	Position: number;
	GoalsAgainst: number;
	GoalsDifference: number;
	Yellows: number;
	Reds: number;
	Points: number;
}
