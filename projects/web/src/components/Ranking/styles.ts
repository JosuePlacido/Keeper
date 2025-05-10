import styled from 'styled-components';

export const Container = styled.div`
	display: flex;
	flex-direction: column;
	flex: 1;
	position: relative;
    transition: transform 0.5s ease-in-out;
	align-items: center;
`;
export const Cell = styled.td`
    padding: 10px;
    text-align: left;
    vertical-align: middle;
	font-family: Arial, sans-serif;
`;
export const CellHeader = styled(Cell)`
	background-color: ${({ theme }) => theme.COLORS.GRAY_300};
	font-weight: bold;
`;

interface ILineProps {
	stripe?: boolean;
}
export const Line = styled.tr<ILineProps>`
	background-color: ${({ theme, stripe }) => stripe ? theme.COLORS.GRAY_100 : theme.COLORS.GRAY_200};
    border-bottom: 1px solid ${({ theme }) => theme.COLORS.GRAY_300};
`;

export const TeamDisplayFull = styled.span`
	display: flex;
	gap: 10px;
	align-items: center;
`;

export const Title = styled.h2`
	margin-bottom: 0.83rem;
`;
