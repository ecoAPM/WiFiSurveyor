import { createApp, h } from "vue";
import App from "./app.vue";
import Factory from "./Factory";
import Signal from "./Signal";

const local = process.env.NODE_ENV === "production";
const server = local ? "" : "http://localhost:5000";

const app = createApp({
	provide: () => ({
		signal_service: (signals: Signal[]) => Factory.signalService(server, signals),
		renderer: Factory.renderer,
		file_loader: Factory.fileLoader
	}),
	render: () => h(App)
});
app.mount("app");