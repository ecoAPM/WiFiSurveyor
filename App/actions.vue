<template>
	<section class="actions">
		<button id="undo" :disabled="state.readings.length === 0" type="button" @click="undo()">
			Undo
		</button>
		<button id="reset" :disabled="state.readings.length === 0" type="button" @click="reset()">
			Clear
		</button>

		<label for="debug">
			<input id="debug" v-model="state.debug" type="checkbox" />
			Debug
		</label>
	</section>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SharedState from "./SharedState";

export default defineComponent({
	data: () => ({
		state: SharedState
	}),
	methods: {
		undo(): void {
			if (confirm("Are you sure you want to remove your most recent reading?"))
				this.state.deleteDataPoint(this.state.readings.length - 1);
		},
		reset(): void {
			if (confirm("Are you sure you want to delete all signal readings?")) {
				this.state.clearAllDataPoints();
			}
		}
	}
});
</script>

<style scoped>
button {
	margin-bottom: 0.5rem;
}
</style>