<template>
	<header>
		<wifi-status :last_updated="last_updated" :signal="current_signal" />
		<ap-form />
		<file-form />
		<background-form />
		<actions />
	</header>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import APForm from "./ap-form.vue";
import BackgroundForm from "./background-form.vue";
import FileForm from "./file-form.vue";
import WiFiStatus from "./wifi-status.vue";
import Actions from "./actions.vue";
import SharedState from "./SharedState";

export default defineComponent({
	components: {
		"ap-form": APForm,
		"background-form": BackgroundForm,
		"file-form": FileForm,
		"wifi-status": WiFiStatus,
		"actions": Actions
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
			return this.state.current.signalFor(this.state.selected);
		}
	}
});
</script>

<style scoped>
header {
	background: linear-gradient(#fff, #00000005);
	display: flex;
	justify-content: space-between;
	align-items: flex-start;
	border-bottom: 1px solid rgba(0, 0, 0, 0.25);
	margin-bottom: 0.5rem;
}

header > * {
	margin: 0.5rem 1rem;
	display: flex;
	flex-direction: column;
}
</style>