import { createContext, ReactNode, useEffect, useRef, useState } from 'react';
import { api } from '../services/api';
import Cookies from 'js-cookie';

export type AuthContextDataProps = {
	user: string | undefined;
	singIn: (email: string, password: string) => void;
	signOut: () => Promise<void>;
};
type AuthContextProviderProps = {
	children: ReactNode;
};

export const AuthContext = createContext<AuthContextDataProps>(
	{} as AuthContextDataProps
);

export function AuthContextProvider({ children }: AuthContextProviderProps) {
	const [user, setUser] = useState<string | undefined>(undefined);

	function userAndTokenUpdate(token: string) {
		api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
		setUser(`Bearer ${token}`);
		Cookies.set('token', token);
	}

	async function singIn(login: string, password: string) {
		try {
			const { data } = await api.post('/user/', { login, password });
			if (data.token) {
				userAndTokenUpdate(data.token);
			}
		} catch (error) {
			throw error;
		}
	}

	async function signOut() {
		try {
			api.defaults.headers.common['Authorization'] = '';
			Cookies.remove('token');
			setUser(undefined);
		} catch (error) {
			throw error;
		}
	}

	async function loadUserData() {
		const token = Cookies.get('token');
		if (token) {
			userAndTokenUpdate(token);
		}
	}

	useEffect(() => {
		loadUserData();
	}, []);

	return (
		<AuthContext.Provider
			value={{
				user,
				singIn,
				signOut
			}}
		>
			{children}
		</AuthContext.Provider>
	);
}
