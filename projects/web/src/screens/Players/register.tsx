import React from 'react';
import { api } from '../../services/api';
import { Controller, useForm } from 'react-hook-form';
import Input from '../../components/Input';
import Button from '../../components/Button';
import { Container, Footer } from './styles';
import InputImage from '../../components/InputImage';

interface IRegisterProps {
	onCancel: () => void;
}

export function RegisterPlayer({ onCancel }: IRegisterProps) {
	const { control, handleSubmit, reset } = useForm();

	function handleCancel() {
		reset();
		onCancel();
	}

	async function handleRegisterPlayer(values: any) {
		const input = document.getElementsByName(
			'picture'
		)[0] as HTMLInputElement;
		const formData = new FormData();
		formData.append('name', values.name);
		formData.append('born', values.born);
		formData.append('mainPosition', values.mainPosition);
		formData.append('nickname', values.nickname);
		formData.append('doc', values.doc);

		if (input.files && input.files.length > 0) {
			const file = input.files[0];
			formData.append('picture', file);
		}
		const { data } = await api.post('player', formData, {
			headers: {
				'Content-Type': 'multipart/form-data'
			}
		});
		onCancel();
	}

	return (
		<Container>
			<h1>Criar novo jogador</h1>
			<form onSubmit={handleSubmit(handleRegisterPlayer)}>
				<Controller
					name="picture"
					control={control}
					render={({ field: { onChange, value } }) => (
						<InputImage
							value={value}
							onChange={e => onChange(e.target.value)}
							defaultImg="user-default.png"
						/>
					)}
				/>
				<Controller
					name="name"
					control={control}
					render={({ field: { onChange, value } }) => (
						<fieldset>
							<label>Nome:</label>
							<Input
								type="text"
								value={value}
								onChange={e => onChange(e.target.value)}
								placeholder="Nome"
							/>
						</fieldset>
					)}
				/>
				<Controller
					name="born"
					control={control}
					render={({ field: { onChange, value } }) => (
						<fieldset>
							<label>Data de nascimento:</label>
							<Input
								type="date"
								value={value}
								onChange={e => onChange(e.target.value)}
								placeholder="Nascimento"
							/>
						</fieldset>
					)}
				/>
				<Controller
					name="mainPosition"
					control={control}
					render={({ field: { onChange, value } }) => (
						<fieldset>
							<label>Posição:</label>
							<Input
								type="text"
								value={value}
								onChange={e => onChange(e.target.value)}
								placeholder="Posição"
							/>
						</fieldset>
					)}
				/>
				<Controller
					name="nickname"
					control={control}
					render={({ field: { onChange, value } }) => (
						<fieldset>
							<label>Apelido/Nome na camisa:</label>
							<Input
								type="text"
								value={value}
								onChange={e => onChange(e.target.value)}
								placeholder="Apelido"
							/>
						</fieldset>
					)}
				/>
				<Controller
					name="doc"
					control={control}
					render={({ field: { onChange, value } }) => (
						<fieldset>
							<label>CPF:</label>
							<Input
								type="text"
								value={value}
								onChange={e => onChange(e.target.value)}
								placeholder="CPF"
							/>
						</fieldset>
					)}
				/>
				<Footer>
					<Button>Cadastrar</Button>
					<Button
						type="button"
						variant="default"
						onClick={handleCancel}
					>
						Cancelar
					</Button>
				</Footer>
			</form>
		</Container>
	);
}
