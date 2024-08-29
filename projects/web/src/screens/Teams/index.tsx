import React, { useEffect, useState } from 'react';
import { api } from '../../services/api';
import ModalWrapper from '../../components/ModalWrapper';
import { RegisterTeam } from './register';

export function Teams() {
	const [teamsList, setTeamsList] = useState<any[]>([]);
	const [showModal, setShowModal] = useState(false);

	async function loadTeams() {
		const { data } = await api.get('teams');
		setTeamsList(data);
	}
	async function handleOpenCadastrar() {
		setShowModal(true);
	}

	useEffect(() => {
		loadTeams();
	}, []);

	return (
		<div>
			<h1>Tela de times</h1>
			<button onClick={handleOpenCadastrar}>Cadastrar</button>
			<table>
				<thead>
					<tr>
						<th>Nome</th>
						<th>Sigla</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					{teamsList.map(t => (
						<tr key={t.id}>
							<td>{t.name}</td>
							<td>{t.abrev}</td>
						</tr>
					))}
				</tbody>
			</table>
			{showModal && (
				<ModalWrapper onClose={() => setShowModal(false)}>
					<RegisterTeam onCancel={() => setShowModal(false)} />
				</ModalWrapper>
			)}
		</div>
	);
}
