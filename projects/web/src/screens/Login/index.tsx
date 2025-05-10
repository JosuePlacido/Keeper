import React, { useState } from 'react';
import { Container, Panel, Title } from './styles';
import { useAuth } from '../../hooks/useAuth';
import Input from '../../components/Input';
import { useNavigate } from 'react-router-dom';
import Button from '../../components/Button';

const Header: React.FC = () => {
	const { singIn } = useAuth();
	const navigate = useNavigate();
	const [login, setLogin] = useState('');
	const [password, setPassword] = useState('');

	async function handleLogin() {
		await singIn(login, password);
		navigate('/');
	}

	return (
		<Container>
			<Panel>
				<Title>Login</Title>
				<fieldset>
					<label htmlFor="login">Login:</label>
					<Input
						name="login"
						value={login}
						onChange={e => setLogin(e.target.value)}
					/>
				</fieldset>
				<fieldset>
					<label htmlFor="password">Senha:</label>
					<Input
						name="password"
						value={password}
						type="password"
						onChange={e => setPassword(e.target.value)}
					/>
				</fieldset>
				<Button onClick={handleLogin}>Login</Button>
			</Panel>
		</Container>
	);
};

export default Header;
