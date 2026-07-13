<template>
	<form>
		<label for="background-file">Background</label>
		<button type="button" @click="$event.target.children[0].click()">
			<label for="background-file" @click.stop>Import</label>
		</button>
		<input v-show="false" id="background-file" type="file" accept="image/*" @change="state.setBackground($event.target.files)"/>

		<label>
			<input id="pixelate" v-model="state.pixelated" :checked="state.pixelated" :value="state.pixelated" type="checkbox"/>
			Pixelate
		</label>
	</form>
</template>

<script setup lang="ts">
import { ref } from "vue";

import SharedState from "./SharedState";

const state = ref(SharedState);

defineExpose({ state });
</script>

<style scoped>
label, button {
	margin-bottom: 0.25rem;
}
</style>