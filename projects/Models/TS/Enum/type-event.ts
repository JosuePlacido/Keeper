export enum TypeEvent {
	Goal,
	AutoGoal,
	YellowCard,
	RedCard,
	MVP,
	Injury
}
interface Goal {
	kind: TypeEvent.Goal;
	value: number;
	name: 'Gol';
}
interface AutoGoal {
	kind: TypeEvent.AutoGoal;
	value: number;
	name: 'Autogol';
}
interface YellowCard {
	kind: TypeEvent.YellowCard;
	value: number;
	name: 'Amarelo';
}
interface RedCard {
	kind: TypeEvent.RedCard;
	value: number;
	name: 'Vermelho';
}
interface MVP {
	kind: TypeEvent.MVP;
	value: number;
	name: 'MVP';
}
interface Injury {
	kind: TypeEvent.Injury;
	value: number;
	name: 'Injury';
}
