export enum Status {
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
	kind: Status.Matching;
	value: number;
	name: 'Disputando';
}

interface Scheduled {
	kind: Status.Scheduled;
	sideLength: number;
	name: 'Marcado';
}
interface Finish {
	kind: Status.Finish;
	sideLength: number;
	name: 'Encerrado';
}
interface Canceled {
	kind: Status.Canceled;
	sideLength: number;
	name: 'Cancelado';
}
interface Eliminated {
	kind: Status.Eliminated;
	sideLength: number;
	name: 'Eliminado';
}
interface Classified {
	kind: Status.Classified;
	sideLength: number;
	name: 'Classificado';
}
interface Champion {
	kind: Status.Champion;
	sideLength: number;
	name: 'Campeão';
}
interface Created {
	kind: Status.Created;
	sideLength: number;
	name: 'Criado';
}
