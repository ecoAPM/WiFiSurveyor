import Vue from "vue";
import app from "./app.vue";

Vue.config.devtools = true;

export default new Vue({
    el: 'app',
    render: (r: Vue.CreateElement) => r(app)
});