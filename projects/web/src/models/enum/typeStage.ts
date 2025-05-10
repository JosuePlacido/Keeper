export enum ETypeStage {
	Knockout,
	League
}
interface Knockout {
	kind: ETypeStage.Knockout;
	value: number;
	name: 'Eliminatória';
}

interface League {
	kind: ETypeStage.League;
	sideLength: number;
	name: 'Pontos corridos';
}
