import axios, { AxiosError } from 'axios';
import Cookies from 'js-cookie';
import { redirect } from 'react-router-dom';

export const api = axios.create({
	baseURL: 'http://localhost:3333/'
});

api.interceptors.response.use(successResponse => successResponse, (errorResponse: AxiosError<{ errors: { default: string } }>) => {
	if (errorResponse.response?.status === 401) {
		if (errorResponse.response.data?.errors!.default === 'Não autenticado') {
			Cookies.remove('token');
			redirect('signin');
		};
	}
})
