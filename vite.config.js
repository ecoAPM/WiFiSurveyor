import { defineConfig } from "vite";
import { createVuePlugin as vue } from "vite-plugin-vue2";
import { createHtmlPlugin as html } from "vite-plugin-html";

export default defineConfig({
	root: "App",
	plugins: [
		html({ minify: true }),
		vue()
	]
});