import React from 'react';
import { Outlet } from 'react-router-dom';
import Header from '../components/header';
import { useTheme } from 'styled-components';
import { useAuth } from '../hooks/useAuth';
import NavSideBar from '../components/NavSideBar';
import { Body, Container } from './styles';

function Layout() {
	const { user } = useAuth();
	return (
		<Container>
			<Header />
			{user && <NavSideBar />}
			<Body>
				<Outlet />
			</Body>
		</Container>
	);
}

export default Layout;
