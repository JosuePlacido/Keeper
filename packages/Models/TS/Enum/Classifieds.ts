export enum Classifieds {
	Random,
	BestVsWorst,
	Configured
}
interface Random {
	kind: Classifieds.Random;
	value: number;
	name: 'Sorteio';
}
interface BestVsWorst {
	kind: Classifieds.BestVsWorst;
	value: number;
	name: 'Melhores vs Piores';
}
interface Configured {
	kind: Classifieds.Configured;
	value: number;
	name: 'Pré-configurado';
}
