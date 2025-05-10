import React, { useEffect, useState } from 'react';
import { api } from '../../services/api';
import { Link, useNavigate } from 'react-router-dom';
import { Controller, useForm } from 'react-hook-form';
import Input from '../../components/Input';
import Button from '../../components/Button';
import { Container, Footer } from './styles';
import InputImage from '../../components/InputImage';

interface IRegisterProps {
	onCancel: () => void;
}
export function RegisterTeam({ onCancel }: IRegisterProps) {
	const { control, handleSubmit, reset } = useForm();

	function handleCancel() {
		reset();
		onCancel();
	}
	async function handleRegisterTeam(values: any) {
		const input = document.getElementsByName(
			'picture'
		)[0] as HTMLInputElement;
		const formData = new FormData();
		formData.append('name', values.name);
		formData.append('abrev', values.abrev);
		if (input.files && input.files.length > 0) {
			const file = input.files[0];
			formData.append('picture', file);
		}
		const { data } = await api.post('teams', formData, {
			headers: {
				'Content-Type': 'multipart/form-data'
			}
		});
		onCancel();
	}

	return (
		<Container>
			<h1>Criar novo jogador</h1>
			<form onSubmit={handleSubmit(handleRegisterTeam)} method="POST">
				<Controller
					name="logoURL"
					control={control}
					render={({ field: { onChange, value } }) => (
						<InputImage
							value={value}
							onChange={e => onChange(e.target.value)}
						/>
					)}
				/>
				<Controller
					name="name"
					control={control}
					render={({ field: { onChange, value } }) => (
						<fieldset>
							<label>Nome</label>
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
					name="abrev"
					control={control}
					render={({ field: { onChange, value } }) => (
						<fieldset>
							<label>SIGLA</label>
							<Input
								type="text"
								value={value}
								onChange={e => onChange(e.target.value)}
								placeholder="ABC"
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
