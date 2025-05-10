import { Route, Routes } from 'react-router-dom';
import Login from '../screens/Login';
import { Home } from '../screens/Home';
import { useAuth } from '../hooks/useAuth';
import { Teams } from '../screens/Teams';
import { Player } from '../screens/Players';
import Layout from '../Layout';
import { Championship } from '../screens/Championship';
import { MatchListEdit } from '../screens/Championship/MatchListEdit';
import { LayoutContextChampionship } from '../Layout/LayoutContextChampionship';

export function AppRoutes() {
	const { user } = useAuth();
	return (
		<Routes>
			<Route path="/" element={<Layout />}>
				<Route index element={<Home />} />
				<Route path="/signin" element={<Login />} />
				{user && (
					<>
						<Route
							path="/championship"
							element={<LayoutContextChampionship />}
						>
							<Route index element={<Championship />} />
							<Route path="result" element={<MatchListEdit />} />
						</Route>
						<Route path="/teams">
							<Route index element={<Teams />} />
						</Route>
						<Route path="/players/">
							<Route index element={<Player />} />
						</Route>
					</>
				)}
			</Route>
		</Routes>
	);
}
