import React, { useEffect, useState } from 'react';
import { IGroup, IMatch } from '../../models';
import Schedule from '../../components/Schedule';
import ModalWrapper from '../../components/ModalWrapper';
import { RegisterResult } from './RegisterResult';
import { useEditChampionship } from '../../hooks/useEditChampionship';

export function MatchListEdit() {
	const { championship } = useEditChampionship();
	const [groups, setGroups] = useState<IGroup[]>([]);
	const [groupSelected, setGroupSelected] = useState<IGroup>();
	const [match, setMatch] = useState<IMatch | undefined>();

	function changeStage(idStage: number) {
		if (championship && championship.stages) {
			const index = championship.stages.findIndex(s => s.id === idStage);
			setGroups(championship.stages[index].groups);
			setGroupSelected(
				championship.stages[index].groups.length > 0
					? championship.stages[index].groups[0]
					: undefined
			);
		}
	}

	function handleOpenModalRegisterResult(matchId: number) {
		const match = groupSelected!.matchs.find(m => m.id === matchId);
		setMatch(match);
	}

	useEffect(() => {
		championship.stages && changeStage(championship.stages[0].id);
	}, [championship]);
	return (
		<div>
			<h1>tela de registro de resultado</h1>
			<h2>{championship.name}</h2>
			<article>
				<aside>
					<select onChange={e => changeStage(Number(e.target.value))}>
						{championship.stages?.map(s => (
							<option value={s.id}>{s.name}</option>
						))}
					</select>
					<select>
						{groups.map(g => (
							<option value={g.id}>{g.name}</option>
						))}
					</select>
				</aside>
				<main>
					{groupSelected && groupSelected.matchs.length > 0 ? (
						<Schedule
							data={groupSelected.matchs}
							editMode
							onEdit={handleOpenModalRegisterResult}
						/>
					) : (
						<p>nenhuma partida cadastrada para esse grupo</p>
					)}
				</main>
			</article>
			{match && (
				<ModalWrapper onClose={() => setMatch(undefined)}>
					<RegisterResult
						onClose={() => setMatch(undefined)}
						match={match}
					/>
				</ModalWrapper>
			)}
		</div>
	);
}
