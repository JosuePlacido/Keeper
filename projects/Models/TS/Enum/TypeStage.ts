export enum TypeStage {
	Knockout,
	League
}
interface Knockout {
	kind: TypeStage.Knockout;
	value: number;
	name: 'Eliminatória';
}

interface League {
	kind: TypeStage.League;
	sideLength: number;
	name: 'Pontos corridos';
}
