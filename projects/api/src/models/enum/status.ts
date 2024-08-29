export enum EStatus {
	Matching,
	Scheduled,
	Finish,
	Canceled,
	Eliminated,
	Classified,
	Champion,
	Created
}
interface Matching {
	kind: EStatus.Matching;
	value: number;
	name: 'Disputando';
}

interface Scheduled {
	kind: EStatus.Scheduled;
	sideLength: number;
	name: 'Marcado';
}
interface Finish {
	kind: EStatus.Finish;
	sideLength: number;
	name: 'Encerrado';
}
interface Canceled {
	kind: EStatus.Canceled;
	sideLength: number;
	name: 'Cancelado';
}
interface Eliminated {
	kind: EStatus.Eliminated;
	sideLength: number;
	name: 'Eliminado';
}
interface Classified {
	kind: EStatus.Classified;
	sideLength: number;
	name: 'Classificado';
}
interface Champion {
	kind: EStatus.Champion;
	sideLength: number;
	name: 'Campeão';
}
interface Created {
	kind: EStatus.Created;
	sideLength: number;
	name: 'Criado';
}
