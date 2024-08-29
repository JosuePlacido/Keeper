import React, { useEffect, useRef, useState } from 'react';
import { Container, Panel, Title } from './styles';
import Header from '../../components/header';
import { useAuth } from '../../hooks/useAuth';
import { useNavigate } from 'react-router-dom';
import { api } from '../../services/api';
import { IChampionship } from '../../models';
import GroupRanking from '../../components/GroupRanking';
import Loading from '../../components/Loading';
import Ranking from '../../components/Ranking';
import Schedule from '../../components/Schedule';
import Scorers from '../../components/Scorer';
import Bracket from '../../components/Bracket';
import { Before, GoDown, GoUp } from '../../components/Bracket/BracketDraw';
import { StagePanel } from '../../components/KnockoutStage/styles';
import { KnockoutStageView } from '../../components/KnockoutStage';
import CustomSelect from '../../components/Select';

export function Home() {
	const { user } = useAuth();
	const navigate = useNavigate();
	const [championship, setChampionship] = useState<IChampionship>();

	async function loadChampionships() {
		const listResponse = await api.get('championship-rank');
		const { data } = await api.get(
			`championship-rank/${listResponse.data[0].id}`
		);
		setChampionship(data);
	}

	useEffect(() => {
		loadChampionships();
	}, []);

	if (!championship) {
		return <Loading />;
	}

	return (
		<Container>
			<Panel>
				<Title>TABELA</Title>
				<Ranking data={championship!.stages![0].groups[0].statistics} />
			</Panel>
			<Panel>
				<Title>JOGOS</Title>
				<Schedule data={championship!.stages![0].groups[0].matchs} />
			</Panel>
			<Panel>
				<Title>ARTILHEIROS</Title>
				<Scorers data={championship!.scorers!} />
			</Panel>
			<Panel>
				<KnockoutStageView />
			</Panel>
		</Container>
	);
}
