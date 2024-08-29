export enum EClassifieds {
	Random,
	BestVsWorst,
	Configured
}
interface Random {
	kind: EClassifieds.Random;
	value: number;
	name: 'Sorteio';
}
interface BestVsWorst {
	kind: EClassifieds.BestVsWorst;
	value: number;
	name: 'Melhores vs Piores';
}
interface Configured {
	kind: EClassifieds.Configured;
	value: number;
	name: 'Pré-configurado';
}
