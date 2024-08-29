import { Link } from 'react-router-dom';
import styled from 'styled-components';

export const Container = styled.header`
  	background-color: ${({ theme }) => theme.COLORS.BLUE};
  	color: ${({ theme }) => theme.COLORS.WHITE};
	width: 100vw;
	padding: 10px;
	display: flex;
	flex-direction: row;
	justify-content: space-between;
	grid-area: header;
`;
export const StyledLink = styled(Link)`
    display: flex;
    align-items: center;
    cursor: pointer;
    text-decoration: none;
    color: white;
	gap: 8px;
`;

export const Title = styled.span`
	font-weight: bold;
    display: flex;
    align-items: center;
    color: white;
`;
