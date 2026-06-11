import js from "@eslint/js";
import stylistic from "@stylistic/eslint-plugin";
import { defineConfig } from "eslint/config";
import sort from "eslint-plugin-simple-import-sort";
import tseslint from "typescript-eslint";

export default defineConfig([
	{
		ignores: ["dist"]
	},
	{
		languageOptions: {
			parser: tseslint.parser,
			parserOptions: { projectService: true }
		},
		plugins: {
			"@stylistic": stylistic,
			"simple-import-sort": sort
		},
		files: ["**/*.ts"],
		extends: [
			js.configs.recommended,
			tseslint.configs.recommended,
			tseslint.configs.strictTypeChecked,
			tseslint.configs.stylisticTypeChecked
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
			"@typescript-eslint/no-unsafe-function-type": "off",
			"@typescript-eslint/restrict-template-expressions": "off"
		}
	}
]);