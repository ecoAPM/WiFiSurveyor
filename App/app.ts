import { createApp, h } from "vue";

import App from "./app.vue";
import Factory from "./Factory";
import Signal from "./Signal";

interface node_env { NODE_ENV: string }
const local = (process.env as unknown as node_env).NODE_ENV === "production";
const server = local ? "" : "http://localhost:5000";

const app = createApp({
	provide: () => ({
		signal_service: (signals: Signal[]) => Factory.signalService(server, signals),
		renderer: Factory.renderer.bind(this)
	}),
	render: () => h(App)
});
app.mount("app");