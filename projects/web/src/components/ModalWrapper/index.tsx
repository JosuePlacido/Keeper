import React, { useRef } from 'react';
import { OverLay, Content, CloseButton } from './styles';

type ModalProps = {
	onClose: () => void;
	children: React.ReactNode;
};

export default function ModalWrapper({ onClose, children }: ModalProps) {
	const refOverlay = useRef<HTMLDivElement>(null);
	function handleClose(e: any) {
		if (e.target === refOverlay.current) onClose();
	}
	return (
		<OverLay ref={refOverlay} onClick={handleClose}>
			<Content>
				<CloseButton onClick={onClose}>&times;</CloseButton>
				{children}
			</Content>
		</OverLay>
	);
}
