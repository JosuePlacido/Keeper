import Jimp from 'jimp';

async function resizeImage(filePath: string, outputPath: string, width: number, height: number) {
	try {
		const file = await Jimp.read(filePath);
		file.resize(width, height).write(outputPath);
	} catch (error) {
		console.error('Erro ao redimensionar a imagem:', error);
	}
}

export const ImageService = { resizeImage };
