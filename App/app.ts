import Vue from "vue";
import app from "./app.vue";
import Factory from "./Factory";

Vue.config.devtools = true;

export default new Vue({
	el: "app",
	render: (r: Vue.CreateElement) => r(app),
	provide: {
		signal_service: Factory.signalService,
		renderer: Factory.renderer,
		image_loader: Factory.imageLoader
	}
});