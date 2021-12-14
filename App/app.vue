<template>
	<div class="app">
		<status :status="connection_status"></status>
		<header-menu :last_updated="last_updated" @background="setBackground($event)"></header-menu>
		<main-area :enabled="!connection_status"></main-area>
		<debug-panel/>
	</div>
</template>

<script lang="ts">
import Vue from "vue";
import Status from "./status.vue";
import HeaderMenu from "./header-menu.vue";
import MainArea from "./main-area.vue";
import DebugPanel from "./debug-panel.vue";
import SharedState from "./SharedState";

export default Vue.extend({
	components: {
		"status": Status,
		"header-menu": HeaderMenu,
		"main-area": MainArea,
		"debug-panel": DebugPanel
	},
	inject: {
		signal_service_factory: "signal_service",
	  renderer_factory: "renderer",
	  image_loader_factory: "image_loader",
	},
	data: () => SharedState,
	computed: {
		connection_status(): string {
			return this.signal_service != null
					? this.signal_service.status
					: "(loading...)"
		},
		last_updated(): string {
			return this.signal_service != null
					? this.signal_service.last_updated
					: "";
		}
	},
	mounted(): void {
		const canvas = document.getElementById("webglcanvas") as HTMLCanvasElement;
		this.signal_service = this.signal_service_factory(this.current.signals);
		this.renderer = this.renderer_factory(canvas);
		this.image_loader = this.image_loader_factory();
	},
	watch: {
		selected(): void {
			if (this.selected != null)
				this.renderer?.render(this.readings, this.selected);
			else
				this.renderer?.clear();
		}
	}
});
</script>

<style scoped>
.app {
	display: flex;
	flex-direction: column;
	height: 100vh;
}
</style>
