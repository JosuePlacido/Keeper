import React, { ChangeEvent, useEffect, useRef, useState } from 'react';
import { api } from '../../services/api';
import {
	ETypeEvent,
	EventMapped,
	IEventGame,
	IMatch,
	IRegisterPlayer
} from '../../models';
import TeamDisplay from '../../components/TeamDisplay';
import { useEditChampionship } from '../../hooks/useEditChampionship';
import CustomSelect from '../../components/Select';
import Input from '../../components/Input';
import { Container, Form, ListEvent, PenaltisPanel, Separator } from './styles';
import Button from '../../components/Button';
import ButtonIcon from '../../components/ButtonIcon';
import { FaArrowDown, FaArrowUp, FaTimes } from 'react-icons/fa';
import { useTheme } from 'styled-components';

interface IScore {
	homeGoals: number;
	awayGoals: number;
	homePenalties?: number;
	awayPenalties?: number;
}
type RegisterResultProps = {
	match: IMatch;
	onClose: () => void;
};
export function RegisterResult({ match, onClose }: RegisterResultProps) {
	const { COLORS } = useTheme();
	const { championship, load, getSquad } = useEditChampionship();
	const refInfoEvent = useRef<IEventGame>({
		matchId: match.id,
		isHomeEvent: true,
		teamRegisterId: match.homeId,
		type: ETypeEvent.Goal
	} as IEventGame);
	const refScore = useRef<IScore>({
		homeGoals: 0,
		awayGoals: 0,
		homePenalties: undefined,
		awayPenalties: undefined
	});
	const refHomeSquad = useRef<IRegisterPlayer[]>([]);
	const refAwaySquad = useRef<IRegisterPlayer[]>([]);
	const [events, setEvents] = useState<IEventGame[]>([]);
	const [squad, setSquad] = useState<IRegisterPlayer[]>([]);
	const [showPenalty, setShowPenalty] = useState(false);

	function handleChangeTeam(newValue: string) {
		refInfoEvent.current.teamRegisterId = Number(newValue);
		refInfoEvent.current.isHomeEvent = match.homeId === Number(newValue);
		setSquad(
			refInfoEvent.current.isHomeEvent
				? refHomeSquad.current
				: refAwaySquad.current
		);
	}

	function handleChangeEvent(id: ETypeEvent) {
		refInfoEvent.current.type = id;
	}

	function handleChangePlayer(id: number) {
		refInfoEvent.current.registerPlayerId = id;
	}

	function handleAddEvent() {
		refInfoEvent.current.order = events.length;
		refInfoEvent.current.player = squad.find(
			pr => pr.id === refInfoEvent.current.registerPlayerId
		)?.player;
		setEvents([...events, { ...refInfoEvent.current }]);

		if (
			(refInfoEvent.current.type === ETypeEvent.Goal &&
				refInfoEvent.current.isHomeEvent) ||
			(refInfoEvent.current.type === ETypeEvent.AutoGoal &&
				!refInfoEvent.current.isHomeEvent)
		)
			refScore.current.homeGoals++;
		else if (
			(refInfoEvent.current.type === ETypeEvent.Goal &&
				!refInfoEvent.current.isHomeEvent) ||
			(refInfoEvent.current.type === ETypeEvent.AutoGoal &&
				refInfoEvent.current.isHomeEvent)
		)
			refScore.current.awayGoals++;
	}

	function handleMoveEventOrder(order: number, diff: 1 | -1) {
		const newEvents = events.map((event, i) => {
			if (i === order + diff) {
				events[order].order = i;
				return events[order];
			}
			if (i === order) {
				events[order + diff].order = i;
				return events[order + diff];
			}
			return event;
		});
		setEvents(newEvents);
	}

	function handleRemoveEvent(index: number) {
		const eventToRemove = events[index];
		if (
			(eventToRemove.type === ETypeEvent.Goal &&
				eventToRemove.isHomeEvent) ||
			(eventToRemove.type === ETypeEvent.AutoGoal &&
				!eventToRemove.isHomeEvent)
		)
			refScore.current.homeGoals--;
		else if (
			(eventToRemove.type === ETypeEvent.Goal &&
				!eventToRemove.isHomeEvent) ||
			(eventToRemove.type === ETypeEvent.AutoGoal &&
				eventToRemove.isHomeEvent)
		)
			refScore.current.awayGoals--;

		events.splice(index, 1);
		setEvents(
			events.map((e, i) => {
				e.order = i;
				return e;
			})
		);
	}

	async function handleFinishGame() {
		if (
			refScore.current.awayGoals === refScore.current.homeGoals &&
			match.penalty &&
			!showPenalty
		) {
			setShowPenalty(true);
			refScore.current.awayPenalties = 0;
			refScore.current.homePenalties = 0;
			return;
		}
		try {
			const { data } = await api.post(`match/${match.id}`, {
				...refScore.current,
				events: events.map(ev => {
					ev.player = undefined;
					return {
						...ev,
						teamRegisterId: undefined,
						player: undefined
					};
				})
			});
			load(championship.id);
			onClose();
		} catch (e) {
			console.error(e);
		}
	}

	useEffect(() => {
		refHomeSquad.current = getSquad(match.homeId!);
		refAwaySquad.current = getSquad(match.awayId!);
		setSquad(refHomeSquad.current);
	}, []);

	return (
		<Container>
			<header>
				<TeamDisplay team={match.homeTeam} size="lg" matchLayout>
					<h1>{refScore.current.homeGoals}</h1>
					{showPenalty && (
						<Input
							value={refScore.current.homePenalties}
							onChange={e => {
								const score = e.target.value.replace(
									/[^0-9]+/g,
									''
								);
								e.target.value = score;
								refScore.current.homePenalties = Number(score);
							}}
						/>
					)}
				</TeamDisplay>
				X
				<TeamDisplay
					team={match.awayTeam}
					size="lg"
					matchLayout
					reverse
				>
					<h1>{refScore.current.awayGoals}</h1>
					{showPenalty && (
						<Input
							value={refScore.current.awayPenalties}
							onChange={e => {
								const score = e.target.value.replace(
									/[^0-9]+/g,
									''
								);
								e.target.value = score;
								refScore.current.awayPenalties = Number(score);
							}}
						/>
					)}
				</TeamDisplay>
			</header>
			<main>
				<Form>
					<fieldset>
						<label>Time</label>
						<CustomSelect
							data={[match.homeTeam, match.awayTeam]}
							imgLabel="logoURL"
							onChangeValue={({ id }) => handleChangeTeam(id)}
							value={
								refInfoEvent.current.isHomeEvent
									? match.homeTeam
									: match.awayTeam
							}
						/>
					</fieldset>
					<fieldset>
						<label>Tipo de evento</label>
						<CustomSelect
							value={EventMapped[refInfoEvent.current.type]}
							data={Object.values(EventMapped)}
							onChangeValue={({ id }) => handleChangeEvent(id)}
						/>
					</fieldset>
					<fieldset>
						<label>Jogador</label>
						<CustomSelect
							value={
								refInfoEvent.current.registerPlayerId
									? squad.find(
											pr =>
												pr.id ===
												refInfoEvent.current
													.registerPlayerId
									  )
									: undefined
							}
							rounded
							data={squad.map(pr => {
								return {
									id: pr.id,
									name: pr.player?.name,
									picture:
										pr.player?.picture ||
										'/user-default.png'
								};
							})}
							onChangeValue={({ id }) => handleChangePlayer(id)}
						/>
					</fieldset>
					<fieldset>
						<label>tempo</label>
						<Input
							placeholder="tempo"
							onChange={e =>
								(refInfoEvent.current.description =
									e.target.value)
							}
						/>
					</fieldset>
					<Button onClick={handleAddEvent}>Adicionar</Button>
				</Form>
				<h2>SÚMULA</h2>
				<ListEvent>
					{events.map(e => (
						<li>
							<span>
								{((e.isHomeEvent &&
									e.type !== ETypeEvent.AutoGoal) ||
									(!e.isHomeEvent &&
										e.type === ETypeEvent.AutoGoal)) && (
									<>
										<img
											src={EventMapped[e.type].picture}
											alt={EventMapped[e.type].name}
										/>
										{`${e.description} ${e.player?.name}`}
										{e.order > 0 && (
											<ButtonIcon
												onClick={() =>
													handleMoveEventOrder(
														e.order,
														-1
													)
												}
												icon={
													<FaArrowUp
														color={
															COLORS.BLUE_LIGHT
														}
													/>
												}
											/>
										)}
										{e.order < events.length - 1 && (
											<ButtonIcon
												onClick={() =>
													handleMoveEventOrder(
														e.order,
														1
													)
												}
												icon={
													<FaArrowDown
														color={
															COLORS.BLUE_LIGHT
														}
													/>
												}
											/>
										)}
										<ButtonIcon
											onClick={() =>
												handleRemoveEvent(e.order)
											}
											icon={<FaTimes color="white" />}
											color="red"
											variant="fill"
										/>
									</>
								)}
							</span>
							<Separator />
							<span>
								{((!e.isHomeEvent &&
									e.type !== ETypeEvent.AutoGoal) ||
									(e.isHomeEvent &&
										e.type === ETypeEvent.AutoGoal)) && (
									<>
										<img
											src={EventMapped[e.type].picture}
											alt={EventMapped[e.type].name}
										/>
										{`${e.description} ${e.player?.name}`}

										{e.order > 0 && (
											<ButtonIcon
												onClick={() =>
													handleMoveEventOrder(
														e.order,
														-1
													)
												}
												icon={
													<FaArrowUp
														color={
															COLORS.BLUE_LIGHT
														}
													/>
												}
											/>
										)}
										{e.order < events.length - 1 && (
											<ButtonIcon
												onClick={() =>
													handleMoveEventOrder(
														e.order,
														1
													)
												}
												icon={
													<FaArrowDown
														color={
															COLORS.BLUE_LIGHT
														}
													/>
												}
											/>
										)}
										<ButtonIcon
											onClick={() =>
												handleRemoveEvent(e.order)
											}
											icon={<FaTimes color="white" />}
											color="red"
											variant="fill"
										/>
									</>
								)}
							</span>
						</li>
					))}
				</ListEvent>
			</main>
			<footer>
				<Button variant="default" onClick={() => onClose()}>
					cancelar
				</Button>
				<Button onClick={handleFinishGame}>Encerrar jogo</Button>
			</footer>
		</Container>
	);
}
