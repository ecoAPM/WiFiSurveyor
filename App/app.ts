import Vue from "vue";
import app from "./app.vue";
import Factory from "./Factory";
import Signal from "./Signal";

Vue.config.devtools = true;

export default new Vue({
    el: 'app',
    render: (r: Vue.CreateElement) => r(app),
    provide: {
        signal_service: (signals: Signal[]) => Factory.signalService(signals),
        renderer: (canvas: HTMLCanvasElement) => Factory.renderer(canvas)
    }
});