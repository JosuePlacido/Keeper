export enum ETypeEvent {
	Goal,
	AutoGoal,
	YellowCard,
	RedCard,
	MVP/*,
	Injury*/
}

type ETypeEventForSelect = {
	id: number;
	name: string;
	picture: string;
};

export const EventMapped: Record<ETypeEvent, ETypeEventForSelect> = {
	[ETypeEvent.Goal]: { id: 0, name: 'Gol', picture: '/goal.png' },
	[ETypeEvent.AutoGoal]: { id: 1, name: 'Gol contra', picture: '/autogol.png' },
	[ETypeEvent.YellowCard]: { id: 2, name: 'Cartão amarelo', picture: '/yellow-card.png' },
	[ETypeEvent.RedCard]: { id: 3, name: 'Cartão vermelho', picture: '/red-card.png' },
	[ETypeEvent.MVP]: { id: 4, name: 'MVP', picture: '/mvp.png' },
};
