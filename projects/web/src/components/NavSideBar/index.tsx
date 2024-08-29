import React from 'react';
import {
	Container,
	ListOptions,
	StyledNavLink,
	UserImg,
	UserInfo
} from './styles';
import {
	FaCogs,
	FaMoneyCheckAlt,
	FaTrophy,
	FaUser,
	FaUsers
} from 'react-icons/fa';

export default function NavSideBar() {
	return (
		<Container>
			<UserInfo>
				<UserImg
					src="/user-default.png"
					alt="User Photo"
					width="50"
					height="50"
				/>
				<span>Nome Usuário</span>
			</UserInfo>
			<nav>
				<ListOptions>
					<li>
						<StyledNavLink to="/championship">
							<FaTrophy /> Meus Torneios
						</StyledNavLink>
					</li>
					<li>
						<StyledNavLink to="teams">
							<FaUsers /> Registro de Times
						</StyledNavLink>
					</li>
					<li>
						<StyledNavLink to="players">
							<FaUser /> Registro de Jogadores
						</StyledNavLink>
					</li>
					{/*<li>
						<StyledNavLink to="#">
							<FaMoneyCheckAlt />
							Patrocinadores
						</StyledNavLink>
					</li>
					<li>
						<StyledNavLink to="#">
							<FaCogs /> Configurações
						</StyledNavLink>
					</li>*/}
				</ListOptions>
			</nav>
		</Container>
	);
}
