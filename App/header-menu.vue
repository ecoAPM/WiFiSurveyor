<template>
	<header>
		<wifi-status :last_updated="last_updated" :value="current_signal" :units="state.mode"/>
		<mode-form/>
		<ap-form/>
		<file-form/>
		<background-form/>
		<actions/>
	</header>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Actions from "./actions.vue";
import APForm from "./ap-form.vue";
import BackgroundForm from "./background-form.vue";
import FileForm from "./file-form.vue";
import ModeForm from "./mode-form.vue";
import SharedState from "./SharedState";
import WiFiStatus from "./wifi-status.vue";
import { Mode } from "./Mode";

export default defineComponent({
	components: {
		"actions": Actions,
		"ap-form": APForm,
		"background-form": BackgroundForm,
		"file-form": FileForm,
		"mode-form": ModeForm,
		"wifi-status": WiFiStatus
	},
	props: {
		last_updated: {
			type: String,
			default: ""
		}
	},
	data: () => ({
		state: SharedState
	}),
	computed: {
		current_signal(): number | null {
			return this.state.mode == Mode.Signal
				? this.state.current.signalFor(this.state.selected)
				: this.state.current.snrFor(this.state.selected);
		}
	}
});
</script>

<style scoped>
header {
	background: linear-gradient(#fff, #00000005);
	display: flex;
	justify-content: space-between;
	border-bottom: 1px solid rgba(0, 0, 0, 0.25);
	margin-bottom: 0.5rem;
}

header > * {
	margin: 0.5rem 1rem;
	display: flex;
	flex-direction: column;
}
</style>