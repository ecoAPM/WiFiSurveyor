<template>
	<form>
		<label for="mode-signal">Mode</label>
		<label>
			<input id="mode-signal" v-model="state.mode" type="radio" name="type" :value="Mode.Signal" />
			Signal
		</label>
		<label>
			<input id="mode-snr" v-model="state.mode" type="radio" name="type" :value="Mode.SNR" />
			SNR
		</label>
	</form>
</template>

<script setup lang="ts">
import { defineExpose, ref } from "vue";

import { Mode } from "./Mode";
import SharedState from "./SharedState";

const state = ref(SharedState);

defineExpose({ state });
</script>

<style scoped>
label {
	padding: 0.25rem;
}
</style>