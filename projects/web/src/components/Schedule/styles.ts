import styled from 'styled-components';

export const Container = styled.div`
	min-width: 300px;
	max-width: 400px;
`;
export const ItemGameContainer = styled.li`
	list-style: none;
	display: flex;
	flex-direction: column;
	gap: 5px;
	padding: 10px 0;
    border-bottom: 1px solid ${({ theme }) => theme.COLORS.GRAY_300};
    text-align: center;
`;
export const MainInfo = styled.span`
	display: grid;
	grid-template-columns: 1fr auto 1fr;
	flex-direction: row;
	align-items: center;
`;
export const DisplayHomeTeam = styled.span`
	display: flex;
	gap: 10px;
	align-items: center;
	justify-content: flex-end;
	font-size: 1.1rem;
	font-weight: bold;
	text-align: right;
	margin-right: 5px;
`;
export const DisplayAwayTeam = styled(DisplayHomeTeam)`
	text-align: left;
	justify-content: flex-start;
	margin-right: 0px;
	margin-left: 5px;
`;

export const ScoreBox = styled.span`
	font-size: 1.1rem;
	font-weight: bold;
	min-width: 20px;
`;
export const ScorePenalty = styled.span`
	font-size: 70%;
`;
export const GameInfo = styled.span`
	min-height: 15px;
    display: flex;
    justify-content: center;
    gap: 10px;
`;
export const Location = styled.span`
	font-size: 0.8rem;
    font-style: italic;
    color: ${({ theme }) => theme.COLORS.GRAY_700};
    flex-shrink: 0;
`;
export const DateText = styled.span`
	font-size: 0.8em;
    font-weight: bold;
    color: ${({ theme }) => theme.COLORS.GRAY_800};
    flex-shrink: 0;
`;
export const Header = styled.header`
	display: flex;
	justify-content: space-between;
	align-items: center;
    background-color: ${({ theme }) => theme.COLORS.GRAY_200};
`;
export const Title = styled.h3`

`;

