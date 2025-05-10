import { createContext, ReactNode, useEffect, useRef, useState } from 'react';
import { api } from '../services/api';
import { IChampionship, IRegisterPlayer } from '../models';

export type EditChampionshipContextDataProps = {
	championship: IChampionship;
	load: (id: number) => Promise<void>;
	getSquad: (teamRegisterId: number) => IRegisterPlayer[];
};
type EditChampionshipContextProviderProps = {
	children: ReactNode;
};

export const EditChampionshipContext = createContext<
	EditChampionshipContextDataProps
>({} as EditChampionshipContextDataProps);

export function EditChampionshipContextProvider({
	children
}: EditChampionshipContextProviderProps) {
	const [championship, setChampionship] = useState<IChampionship>(
		{} as IChampionship
	);

	async function load(id: number) {
		const { data } = await api.get(`championship-rank/${id}`);
		setChampionship(data);
	}

	function getSquad(teamRegisterId: number) {
		if (championship.teams) {
			const teamSubscription = championship.teams.find(
				t => t.id === teamRegisterId
			);
			if (teamSubscription) {
				return teamSubscription.players || [];
			}
		}
		return [];
	}

	return (
		<EditChampionshipContext.Provider
			value={{
				championship,
				load,
				getSquad
			}}
		>
			{children}
		</EditChampionshipContext.Provider>
	);
}
