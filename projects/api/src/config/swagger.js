module.exports = {
	info: {
		version: '1.0.0',
		title: 'Growdev API',
		description: 'Documentação da API da growdev'
	},
	host: 'localhost:3333',
	schemes: ['http', 'https'],
	consumes: ['application/json'],
	produces: ['application/json'],
	securityDefinitions: {
		JWT: {
			description: 'JWT token',
			type: 'apiKey',
			in: 'header',
			name: 'Authorization'
		}
	},
	definitions: {}
};
