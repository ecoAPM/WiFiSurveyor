import vue from "@vitejs/plugin-vue";
import fs from "fs";
import path from "path";
import {defineConfig} from "vite";

const files = fs.readdirSync(__dirname)
	.filter(file => /\.ts$/.exec(file))
	.map(file => path.resolve(__dirname, file));

export default defineConfig({
	root: "App.Tests",
	plugins: [vue()],
	logLevel: "warn",
	build: {
		lib: {
			entry: "",
			formats: ["cjs"],
			fileName: "[name]"
		},
		outDir: "../dist/tests",
		emptyOutDir: true,
		minify: false,
		sourcemap: true,
		rolldownOptions: {
			input: files,
			output: {
				intro: "require('global-jsdom/register');"
			},
			external: ["xunit.ts", "ts-mockito", "vue", "@vue/test-utils"]
		}
	}
});