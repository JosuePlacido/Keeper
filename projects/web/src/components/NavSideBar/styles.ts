import { Link, NavLink } from 'react-router-dom';
import styled from 'styled-components';

export const Container = styled.aside`
	margin-top: 20px;
    width: 250px;
    background-color: ${({ theme }) => theme.COLORS.BLUE};
    padding: 20px;
    color: white;
    display: flex;
    flex-direction: column;
    align-items: center;
    height: calc(100vh - 60px);
    border-top-right-radius: 10px;
    border-bottom-right-radius: 10px;
    box-sizing: border-box;
    transition: transform 0.3s ease;
	grid-area: menu;
`;
export const UserInfo = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 20px;
`;

export const UserImg = styled.img`
    border-radius: 50%;
    margin-bottom: 10px;
	background-color: ${({ theme }) => theme.COLORS.GRAY_200};
`;
export const ListOptions = styled.ul`
    list-style-type: none;
    padding: 0;
    width: 100%;
	margin: 1rem 0;
	& li {
		margin: 10px 0;
	}
`;

export const StyledNavLink = styled(NavLink)`
	display: flex;
	gap: 10px;
    color: white;
    text-decoration: none;
    display: flex;
    align-items: center;
    padding: 10px;
    border-radius: 5px;
    transition: background-color 0.3s;

	&.active, &:hover {
		background-color: #165fae;
		color: white;
	}
`;
