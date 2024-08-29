import React, { useEffect, useState } from 'react';
import {
	Container,
	Dropdown,
	ImgIcon,
	Overlay,
	SelectedOption
} from './styles';

type SelecteProps = {
	data: any[];
	value: any;
	rounded?: boolean;
	onChangeValue?: (value: any) => void;
	idLabel?: string;
	descriptionLabel?: string;
	imgLabel?: string;
};
function CustomSelect({
	rounded = false,
	value,
	data,
	onChangeValue,
	idLabel = 'id',
	descriptionLabel = 'name',
	imgLabel = 'picture'
}: SelecteProps) {
	const [selected, setSelected] = useState(value);
	const [isOpen, setIsOpen] = useState(false);

	const handleSelect = (value: any) => {
		setSelected(value);
		setIsOpen(false);
		onChangeValue && onChangeValue(value);
	};

	useEffect(() => {
		if (data.length > 0) handleSelect(value ? value : data[0]);
	}, [data]);

	return (
		<Container>
			<SelectedOption onClick={() => setIsOpen(ps => !ps)}>
				{!!selected && (
					<ImgIcon
						rounded={rounded}
						src={
							selected[imgLabel] ||
							'https://via.placeholder.com/40'
						}
						alt={selected[descriptionLabel]}
					/>
				)}
				<span>
					{!!selected ? selected[descriptionLabel] : 'Selecione...'}
				</span>
			</SelectedOption>
			{isOpen && <Overlay onClick={() => setIsOpen(ps => !ps)} />}
			{isOpen && (
				<Dropdown>
					{data.map(option => (
						<li
							key={option[idLabel]}
							onClick={() => handleSelect(option)}
							className={
								option[idLabel] === selected[idLabel]
									? 'active'
									: ''
							}
						>
							<ImgIcon
								rounded={rounded}
								src={
									option[imgLabel] ||
									'https://via.placeholder.com/40'
								}
								alt={option[descriptionLabel]}
							/>
							<span>{option[descriptionLabel]}</span>
						</li>
					))}
				</Dropdown>
			)}
		</Container>
	);
}

export default CustomSelect;
