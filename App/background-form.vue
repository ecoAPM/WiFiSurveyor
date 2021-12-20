<template>
	<form>
		<label>Background</label>
	  <button type="button" @click="$event.target.children[0].click()">
		  <label for="background-file" @click.stop>Import</label>
	  </button>
		<input v-show="false" id="background-file" type="file" accept="image/*" @change="state.setBackground($event.target.files)"/>

		<label>
			<input id="pixelate" v-model="state.pixelated" type="checkbox"/>
			Pixelate
		</label>
	</form>
</template>

<script lang="ts">
import Vue from 'vue';
import SharedState from "./SharedState";

export default Vue.extend({
	data: () => ({
		state: SharedState
	})
});
</script>

<style scoped>
label, button {
	margin-bottom: 0.25rem;
}
</style>