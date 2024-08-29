import React from 'react';
import { Outlet } from 'react-router-dom';
import Header from '../components/header';
import { useTheme } from 'styled-components';
import { useAuth } from '../hooks/useAuth';
import NavSideBar from '../components/NavSideBar';
import { Body, Container } from './styles';
import { EditChampionshipContextProvider } from '../contexts/editChampionship';

export function LayoutContextChampionship() {
	return (
		<EditChampionshipContextProvider>
			<Outlet />
		</EditChampionshipContextProvider>
	);
}
