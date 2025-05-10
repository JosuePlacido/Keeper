import React, { InputHTMLAttributes, useRef } from 'react';
import { Article, Container, Header, Title } from './styles';
import { PiArrowLeft, PiArrowRight } from 'react-icons/pi';
import { IStage } from '../../models';
import ButtonIcon from '../ButtonIcon';
import GroupRanking from '../GroupRanking';

interface ISliderStageAttributes {
	stages: IStage[];
	editMode?: boolean;
}
const SliderStage = ({ stages, editMode = false }: ISliderStageAttributes) => {
	const refActiveStage = useRef(0);
	const refContainer = useRef<HTMLDivElement>(null);

	function showSlide(index: number) {
		if (index >= stages.length) {
			refActiveStage.current = 0;
		} else if (index < 0) {
			refActiveStage.current = stages.length - 1;
		} else {
			refActiveStage.current = index;
		}
		const offset = -refActiveStage.current * 100;
		refContainer.current!.style.transform = `translateX(${offset}%)`;
	}

	return (
		<Container ref={refContainer}>
			{stages.map(s => (
				<Article key={s.id}>
					<Header>
						<ButtonIcon
							icon={
								<PiArrowLeft
									size={32}
									color="blue"
									onClick={() =>
										refActiveStage.current > 0 &&
										showSlide(refActiveStage.current - 1)
									}
								/>
							}
						/>
						<Title>{s.name}</Title>
						<ButtonIcon
							icon={
								<PiArrowRight
									size={32}
									color="blue"
									onClick={() =>
										showSlide(refActiveStage.current + 1)
									}
								/>
							}
						/>
					</Header>
					{s.groups &&
						s.groups.map(g => (
							<GroupRanking
								key={g.id}
								group={g}
								editMode={editMode}
							/>
						))}
				</Article>
			))}
		</Container>
	);
};

export default SliderStage;
