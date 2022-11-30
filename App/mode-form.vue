<template>
	<form>
		<label>Mode</label>
		<label>
			<input id="mode-signal" v-model="state.mode" type="radio" name="type" :value="mode.Signal" />
			Signal
		</label>
		<label>
			<input id="mode-snr" v-model="state.mode" type="radio" name="type" :value="mode.SNR" />
			SNR
		</label>
	</form>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import AppViewModel from "./AppViewModel";
import { Mode } from "./Mode";
import SharedState from "./SharedState";

export default defineComponent({
	data: function (): {state: AppViewModel, mode: typeof Mode } {
		return {
			state: SharedState,
			mode: Mode
		};
	}
});
</script>

<style scoped>
label {
	padding: 0.25rem;
}
</style>