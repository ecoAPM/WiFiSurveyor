<template>
	<div class="app">
		<status :status="connection_status" />
		<header-menu :last_updated="last_updated" @background="setBackground($event)" />
		<main-area :enabled="!connection_status" />
		<debug-panel />
	</div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Status from "./status.vue";
import HeaderMenu from "./header-menu.vue";
import MainArea from "./main-area.vue";
import DebugPanel from "./debug-panel.vue";
import SharedState from "./SharedState";

export default defineComponent({
	components: {
		"status": Status,
		"header-menu": HeaderMenu,
		"main-area": MainArea,
		"debug-panel": DebugPanel
	},
	inject: {
		signal_service_factory: "signal_service",
		renderer_factory: "renderer",
		file_loader_factory: "file_loader",
	},
	data: () => SharedState,
	computed: {
		connection_status(): string {
			return this.signal_service != null
				? this.signal_service.status
				: "(loading...)";
		},
		last_updated(): string {
			return this.signal_service != null
				? this.signal_service.last_updated
				: "";
		}
	},
	watch: {
		mode(): void {
			this.refresh();
		},
		selected(): void {
			this.refresh();
		}
	},
	async mounted(): void {
		this.signal_service = this.signal_service_factory(this.current.signals);
		const start = this.signal_service?.start();

		const canvas = document.getElementById("webglcanvas") as HTMLCanvasElement;
		this.renderer = this.renderer_factory(canvas);
		this.file_loader = this.file_loader_factory();

		await start;
	},
	methods: {
		refresh(): void {
			this.renderer?.render(this.mode, this.readings, this.selected);
		}
	}
});
</script>

<style scoped>
@import url("@fontsource/roboto/300.css");

.app {
	display: flex;
	flex-direction: column;
	height: 100vh;
}

button:not([disabled]),
a button:not([disabled]),
button label:not([disabled]) {
	cursor: pointer;
}
</style>