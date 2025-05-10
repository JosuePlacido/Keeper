import React, { useEffect, useState } from 'react';
import { api } from '../../services/api';
import { useNavigate } from 'react-router-dom';
import { useEditChampionship } from '../../hooks/useEditChampionship';

export function Championship() {
	const navigate = useNavigate();
	const { load } = useEditChampionship();
	const [championshipsList, setChampionshipList] = useState<any[]>([]);

	async function loadChampionships() {
		const { data } = await api.get('championship-rank');
		setChampionshipList(data);
	}
	async function handleOpenMatches(id: number) {
		load(id);
		navigate(`result`);
	}

	useEffect(() => {
		loadChampionships();
	}, []);

	return (
		<div>
			<h1>Tela de campeonatos</h1>
			<table>
				<thead>
					<tr>
						<th>Nome</th>
						<th>Edição</th>
						<th>Categoria</th>
						<th>Ações</th>
					</tr>
				</thead>
				<tbody>
					{championshipsList.map(p => (
						<tr key={p.id}>
							<td>{p.name}</td>
							<td>{p.edition}</td>
							<td>{p.category}</td>
							<td>
								<button onClick={() => handleOpenMatches(p.id)}>
									Registrar resultado
								</button>
							</td>
						</tr>
					))}
				</tbody>
			</table>
		</div>
	);
}
