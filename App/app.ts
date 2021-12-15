import Vue from "vue";
import app from "./app.vue";
import Factory from "./Factory";

Vue.config.devtools = true;

const local = process.env.NODE_ENV === "production";
const factory = new Factory(local);

export default new Vue({
	el: "app",
	render: (r: Vue.CreateElement) => r(app),
	provide: {
		signal_service: factory.signalService,
		renderer: factory.renderer,
		image_loader: factory.imageLoader
	}
});