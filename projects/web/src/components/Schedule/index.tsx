import React, { InputHTMLAttributes, useEffect, useRef, useState } from 'react';
import { PiArrowLeft, PiArrowRight } from 'react-icons/pi';
import { IGroup, IMatch, IStage, IStatistics } from '../../models';
import ButtonIcon from '../ButtonIcon';
import { api } from '../../services/api';
import {
	Container,
	DateText,
	DisplayAwayTeam,
	DisplayHomeTeam,
	GameInfo,
	Header,
	ItemGameContainer,
	Location,
	MainInfo,
	ScoreBox,
	ScorePenalty,
	Title
} from './styles';
import { useTheme } from 'styled-components';
import TeamDisplay from '../TeamDisplay';

interface IScheduleProps {
	data: IMatch[];
	editMode?: boolean;
	onEdit?: (id: number, index: number) => void;
}

const DAYS_WEEK = [
	'Doming',
	'Segunda',
	'Terça',
	'Quarta',
	'Quinta',
	'Sexta',
	'Sabado'
];

const Schedule = ({ data, onEdit, editMode }: IScheduleProps) => {
	const MAX_ROUND = data.reduce((value, m) => {
		return m.round > value ? m.round : value;
	}, 0);
	const { COLORS } = useTheme();
	const [matchList, setMatchList] = useState<IMatch[]>([]);
	const refRound = useRef(1);

	function changeRound(round: number) {
		if (round <= 0 || round > MAX_ROUND) return;
		refRound.current = round;
		setMatchList(data.filter(d => d.round === round));
	}

	useEffect(() => {
		changeRound(1);
	}, []);

	return (
		<Container>
			<Header>
				<ButtonIcon
					icon={
						<PiArrowLeft
							size={32}
							color={COLORS.BLUE_LIGHT}
							onClick={() => changeRound(refRound.current - 1)}
						/>
					}
				/>
				<Title>RODADA {refRound.current}</Title>
				<ButtonIcon
					icon={
						<PiArrowRight
							size={32}
							color={COLORS.BLUE_LIGHT}
							onClick={() => changeRound(refRound.current + 1)}
						/>
					}
				/>
			</Header>
			<ul>
				{matchList.map((m, i) => (
					<ItemGameContainer key={m.id}>
						<GameInfo>
							<Location>{m.local}</Location>
							<DateText>
								{m.date
									? `${m.date.getDay()}/${m.date.getMonth()} · ${
											DAYS_WEEK[m.date.getDay()]
									  } · ${m.date.getHours()}:${m.date.getMinutes()}`
									: undefined}
							</DateText>
						</GameInfo>
						<MainInfo>
							<TeamDisplay
								team={m.homeTeam}
								matchLayout
								variant="bold"
							>
								<ScoreBox>{m.goalsHome}</ScoreBox>
								{m.goalsPenaltyHome && (
									<ScorePenalty>
										{'(' + m.goalsPenaltyHome}
									</ScorePenalty>
								)}
							</TeamDisplay>
							x
							<TeamDisplay
								team={m.awayTeam}
								matchLayout
								reverse
								variant="bold"
							>
								{m.goalsPenaltyAway && (
									<ScorePenalty>
										{'(' + m.goalsPenaltyAway}
									</ScorePenalty>
								)}
								<ScoreBox>{m.goalsAway}</ScoreBox>
							</TeamDisplay>
						</MainInfo>
						{editMode && onEdit !== undefined && (
							<button
								type="button"
								onClick={() => onEdit(m.id, i)}
							>
								{' '}
								Registrar súmula
							</button>
						)}
					</ItemGameContainer>
				))}
			</ul>
		</Container>
	);
};

export default Schedule;
