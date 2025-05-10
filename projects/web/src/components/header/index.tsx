import React from 'react';
import { Container, StyledLink, Title } from './styles';
import { useAuth } from '../../hooks/useAuth';
import { Link } from 'react-router-dom';
import { FaUser, FaSignOutAlt } from 'react-icons/fa';
import { useTheme } from 'styled-components';

const Header: React.FC = () => {
	const { user, signOut } = useAuth();
	const { COLORS } = useTheme();

	return (
		<Container>
			<StyledLink to="/">
				<img
					width="20"
					height="20"
					alt="logo torneios FC"
					src="/logo192.png"
				/>
			</StyledLink>
			<Title>Torneios FC</Title>
			{!user ? (
				<StyledLink to="signin">
					<FaUser color={COLORS.WHITE} /> Entrar
				</StyledLink>
			) : (
				<StyledLink onClick={signOut} to="signin">
					<FaSignOutAlt color={COLORS.WHITE} /> Sair
				</StyledLink>
			)}
		</Container>
	);
};

export default Header;
