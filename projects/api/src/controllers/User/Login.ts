import { Request, Response } from 'express';
import { StatusCodes } from 'http-status-codes';
import { JWTService, PasswordCrypto } from '../../services';
import { validation } from '../../middlewares';
import { UserRepository } from '../../database';
import * as yup from 'yup';


interface IBodyProps {
	login: string;
	password: string;
}

export const signInValidation = validation((getSchema) => ({
	body: getSchema<IBodyProps>(yup.object().shape({
		password: yup.string().required().min(5),
		login: yup.string().required().min(5),
	})),
}));

export const signIn = async (req: Request<{}, {}, IBodyProps>, res: Response) => {
	const { login, password } = req.body;
	const user = await UserRepository.getByLogin(login);
	if (user instanceof Error) {
		return res.status(StatusCodes.UNAUTHORIZED).json({
			errors: {
				default: 'Email ou senha são inválidos'
			}
		});
	}

	const passwordMatch = await PasswordCrypto.verifyPassword(password, user.password);
	if (!passwordMatch) {
		return res.status(StatusCodes.UNAUTHORIZED).json({
			errors: {
				default: 'Email ou senha são inválidos'
			}
		});
	} else {
		const token = JWTService.sign({ uid: user.id });
		if (token === 'JWT_SECRET_NOT_FOUND') {
			return res.status(StatusCodes.INTERNAL_SERVER_ERROR).json({
				errors: {
					default: 'Erro ao gerar o token de acesso'
				}
			});
		}

		return res.status(StatusCodes.OK).json({ token });
	}
};
