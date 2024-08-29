import React, {
	ChangeEvent,
	ChangeEventHandler,
	InputHTMLAttributes,
	useRef,
	useState
} from 'react';
import { ButtonRemove, Container, InputStyled, Preview } from './styles';

type Props = InputHTMLAttributes<HTMLInputElement> & {
	defaultImg?: 'team-default.png' | 'user-default.png';
};

const Input: React.FC<Props> = ({
	value,
	defaultImg = 'team-default.png',
	onChange,
	...rest
}: Props) => {
	const refInput = useRef<HTMLInputElement>(null);
	const [imgPreview, setImgPreview] = useState(
		(value as string) || defaultImg
	);
	function handleCleanImage() {
		setImgPreview(defaultImg);
		if (refInput.current) refInput.current.value = '';
	}
	function handleImageChange(event: ChangeEvent<HTMLInputElement>) {
		if (
			event.target.files &&
			event.target.files.length > 0 &&
			event.target.files[0]
		) {
			const file = event.target.files[0];
			const reader = new FileReader();
			reader.onload = e => {
				if (e.target && e.target.result)
					setImgPreview(e.target!.result as string);
			};
			reader.readAsDataURL(file);
		}
	}

	function handleSelectImage() {
		refInput.current?.click();
	}

	return (
		<Container>
			<Preview
				onClick={handleSelectImage}
				width="100"
				height="100"
				src={imgPreview}
				alt="preview de imagem upada"
			/>
			<InputStyled
				ref={refInput}
				{...rest}
				accept="image/*"
				onChange={handleImageChange}
				value={value}
				type="file"
				name="picture"
			/>
			{imgPreview !== defaultImg && (
				<ButtonRemove type="button" onClick={handleCleanImage}>
					&times;
				</ButtonRemove>
			)}
		</Container>
	);
};

export default Input;
