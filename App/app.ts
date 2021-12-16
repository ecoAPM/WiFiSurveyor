import Vue from "vue";
import app from "./app.vue";
import Factory from "./Factory";
import Signal from "./Signal";

Vue.config.devtools = true;

const local = process.env.NODE_ENV === "production";
const server = local ? "" : "http://localhost:5000";

export default new Vue({
	el: "app",
	render: (r: Vue.CreateElement) => r(app),
	provide: {
		signal_service: (signals: Signal[]) => Factory.signalService(server, signals),
		renderer: Factory.renderer,
		image_loader: Factory.imageLoader
	}
});