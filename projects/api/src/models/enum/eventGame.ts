export enum ETypeEvent {
	Goal,
	AutoGoal,
	YellowCard,
	RedCard,
	MVP,
	Injury
}
interface Goal {
	kind: ETypeEvent.Goal;
	value: number;
	name: 'Gol';
}
interface AutoGoal {
	kind: ETypeEvent.AutoGoal;
	value: number;
	name: 'Autogol';
}
interface YellowCard {
	kind: ETypeEvent.YellowCard;
	value: number;
	name: 'Amarelo';
}
interface RedCard {
	kind: ETypeEvent.RedCard;
	value: number;
	name: 'Vermelho';
}
interface MVP {
	kind: ETypeEvent.MVP;
	value: number;
	name: 'MVP';
}
interface Injury {
	kind: ETypeEvent.Injury;
	value: number;
	name: 'Injury';
}
