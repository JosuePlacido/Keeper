import js from '@eslint/js';
import * as parser from '@typescript-eslint/parser';
import prettier from 'eslint-config-prettier';
import prettierPlugin from 'eslint-plugin-prettier';
import reactPlugin from 'eslint-plugin-react';
import reactHooks from 'eslint-plugin-react-hooks';
import reactRefresh from 'eslint-plugin-react-refresh';
import simpleImportSort from 'eslint-plugin-simple-import-sort';
import unusedImports from 'eslint-plugin-unused-imports';
import globals from 'globals';
import tseslint from 'typescript-eslint';

export default tseslint.config(
	{ ignores: ['node_modules', '.expo'] },
	{
		extends: [js.configs.recommended, prettier],
		files: ['**/*.{ts,tsx,js,tsx}'],
		languageOptions: {
			ecmaVersion: 2020,
			parser: parser,
			parserOptions: {
				ecmaFeatures: {
					jsx: true
				}
			},
			sourceType: 'module',
			globals: {
				...globals.browser,
				...globals.jest,
				...globals.node,
				...globals.es2021
			}
		},
		plugins: {
			'react-hooks': reactHooks,
			'react-refresh': reactRefresh,
			'simple-import-sort': simpleImportSort,
			'unused-imports': unusedImports,
			'react-plugin': reactPlugin,
			prettier: prettierPlugin // Plugin do Prettier
		},
		rules: {
			...reactHooks.configs.recommended.rules,
			'react-refresh/only-export-components': ['warn', { allowConstantExport: true }],
			'simple-import-sort/imports': 'error',
			'simple-import-sort/exports': 'error',
			'no-unused-vars': 'off',
			'unused-imports/no-unused-imports': 'error',
			'prettier/prettier': 'error',
			quotes: ['error', 'single', { avoidEscape: true, allowTemplateLiterals: false }],
			'unused-imports/no-unused-vars': [
				'warn',
				{
					vars: 'all',
					varsIgnorePattern: '^_',
					args: 'after-used',
					argsIgnorePattern: '^_'
				}
			]
		},
		settings: {
			react: {
				version: 'detect'
			}
		}
	}
);
