import { defineConfig} from "vite";
import { createVuePlugin as vue } from 'vite-plugin-vue2';
import { minifyHtml as minify } from 'vite-plugin-html';

export default defineConfig({
  root: 'App',
  plugins: [minify(), vue()]
});