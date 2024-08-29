const swaggerAutogen = require('swagger-autogen')();
const doc = require('./config/swagger');

const outputFile = './swagger_documentation.json';
const endpoints = ['src/routes/index.ts'];

swaggerAutogen(outputFile, endpoints, doc);
