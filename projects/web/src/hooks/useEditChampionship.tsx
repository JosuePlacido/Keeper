import { useContext } from 'react';

import { EditChampionshipContext } from '../contexts/editChampionship';

export function useEditChampionship() {
	const context = useContext(EditChampionshipContext);

	return context;
}
