import js from "@eslint/js";
import stylistic from "@stylistic/eslint-plugin";
import { defineConfig } from "eslint/config";
import sort from "eslint-plugin-simple-import-sort";
import vue from "eslint-plugin-vue";
import tseslint from "typescript-eslint";

export default defineConfig(
	{
		ignores: ["dist"]
	},
	{
		languageOptions: {
			globals: {
				confirm: "readonly",
				document: "readonly",
				process: "readonly",
				setTimeout: "readonly",
				window: "readonly"
			},
			parserOptions: {
				parser: tseslint.parser,
				projectService: true,
				extraFileExtensions: ["vue"]
			}
		},
		plugins: {
			"@stylistic": stylistic,
			"simple-import-sort": sort,
			"vue": vue
		},
		files: ["**/*.{ts,vue}"],
		extends: [
			js.configs.recommended,
			tseslint.configs.recommended,
			tseslint.configs.strictTypeChecked,
			tseslint.configs.stylisticTypeChecked,
			vue.configs["flat/recommended"]
		],
		rules: {
			"indent": ["warn", "tab"],
			"quotes": ["warn", "double"],

			"simple-import-sort/imports": "warn",
			"simple-import-sort/exports": "warn",
			"sort-imports": "off",

			"@stylistic/object-curly-spacing": ["warn", "always"],
			"@stylistic/semi": ["error", "always"],

			"@typescript-eslint/no-confusing-void-expression": "off",
			"@typescript-eslint/no-extraneous-class": "off",
			"@typescript-eslint/no-inferrable-types": "off",
			"@typescript-eslint/no-unsafe-function-type": "off",
			"@typescript-eslint/restrict-template-expressions": "off",

			"vue/html-indent": ["warn", "tab"],
			"vue/html-self-closing": "off",
			"vue/max-attributes-per-line": "off"
		}
	}
);