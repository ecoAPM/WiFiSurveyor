<template>
	<form>
		<label for="background-file">Background</label>
		<input id="background-file" type="file" @change="state.setBackground($event.target.files)"/>

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