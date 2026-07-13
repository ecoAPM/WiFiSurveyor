<template>
	<section class="actions">
		<button id="undo" :disabled="state.readings.length === 0" type="button" @click="undo()">
			Undo
		</button>
		<button id="reset" :disabled="state.readings.length === 0" type="button" @click="reset()">
			Clear
		</button>

		<label for="debug">
			<input id="debug" v-model="state.debug" type="checkbox"/>
			Debug
		</label>
	</section>
</template>

<script setup lang="ts">
import { ref } from "vue";

import SharedState from "./SharedState";

const state = ref(SharedState);

const undo = () => {
	if (confirm("Are you sure you want to remove your most recent reading?"))
		state.value.deleteDataPoint(state.value.readings.length - 1);
};

const reset = () => {
	if (confirm("Are you sure you want to delete all signal readings?"))
		state.value.clearAllDataPoints();
};

defineExpose({ state });
</script>

<style scoped>
button {
	margin-bottom: 0.5rem;
}
</style>