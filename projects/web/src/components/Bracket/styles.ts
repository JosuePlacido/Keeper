import styled, { css } from 'styled-components';

export const Container = styled.span`
	display: flex;
	height: 100%;
	align-items: center;
`;

interface IContainerProps {
	prev?: boolean;
	next?: boolean;
};
export const BracketPanel = styled.div<IContainerProps>`
	display: flex;
	flex-direction: column;
	gap: 5px;
	justify-content: center;
	align-items: stretch;
	min-width: 250px;
	border-radius: 8px;
	border: 1px solid ${({ theme }) => theme.COLORS.GRAY_500};
	background-color: ${({ theme }) => theme.COLORS.GRAY_200};
	padding: 10px;
	${({ prev, next }) => css`margin: 5px ${next ? 0 : 20}px 5px ${prev ? 0 : 20}px;`}
`;

interface IDisplayTeamProps {
	winner?: boolean;
	loser?: boolean;
}
export const DisplayTeam = styled.span<IDisplayTeamProps>`
	display: grid;
	gap: 10px;
	grid-template-columns: auto 1fr auto;
	align-items: center;
	justify-content: space-between;
	background-color: ${({ theme }) => theme.COLORS.GRAY_200};
	font-weight: ${({ winner }) => winner ? 'bold' : 'normal'};
	opacity: ${({ loser }) => loser ? 0.5 : 1};
`;
