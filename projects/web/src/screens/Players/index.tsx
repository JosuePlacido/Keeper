import React, { useEffect, useState } from 'react';
import { api } from '../../services/api';
import ModalWrapper from '../../components/ModalWrapper';
import { RegisterPlayer } from './register';
import TableList from '../../components/TableList';

export function Player() {
	const [playersList, setPlayerList] = useState<any[]>([]);
	const [showModal, setShowModal] = useState(false);

	async function loadPlayers() {
		const { data } = await api.get('player');
		setPlayerList(data);
	}
	async function handleOpenCadastrar() {
		setShowModal(true);
	}

	useEffect(() => {
		loadPlayers();
	}, []);

	return (
		<div className="panel">
			<h1>Tela de jogadores</h1>
			<button onClick={handleOpenCadastrar}>Cadastrar</button>
			<TableList>
				<thead>
					<tr>
						<th>Nome</th>
						<th>CPF</th>
					</tr>
				</thead>
				<tbody>
					{playersList.map(p => (
						<tr key={p.doc}>
							<td>{p.name}</td>
							<td>{p.doc}</td>
						</tr>
					))}
				</tbody>
			</TableList>
			<table>
				<thead>
					<tr>
						<th>Nome</th>
						<th>CPF</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					{playersList.map(p => (
						<tr key={p.doc}>
							<td>{p.name}</td>
							<td>{p.doc}</td>
						</tr>
					))}
				</tbody>
			</table>
			{showModal && (
				<ModalWrapper onClose={() => setShowModal(false)}>
					<RegisterPlayer onCancel={() => setShowModal(false)} />
				</ModalWrapper>
			)}
		</div>
	);
}
