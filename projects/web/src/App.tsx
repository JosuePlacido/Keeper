import React from 'react';
import { ThemeProvider } from 'styled-components';
import theme from './styles/theme';
import { AuthContextProvider } from './contexts/auth';
import { BrowserRouter } from 'react-router-dom';
import { AppRoutes } from './routes';

function App() {
	return (
		<ThemeProvider theme={theme}>
			<BrowserRouter>
				<AuthContextProvider>
					<AppRoutes />
				</AuthContextProvider>
			</BrowserRouter>
		</ThemeProvider>
	);
}

export default App;
