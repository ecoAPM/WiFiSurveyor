<template>
	<form>
		<label for="name">Name</label>
		<input id="name" type="text" v-model="state.name"/>

		<section>
			<a class="button" id="save-file" :download="state.name + '.json'" :href="objectURL()">
				<button type="button">Save</button>
			</a>

			<button type="button" @click="$event.target.children[0].click()">
				<label for="load-file" @click.stop>Load</label>
			</button>
			<input v-show="false" id="load-file" type="file" accept="application/json,.json"
						 @change="state.load($event.target.files)"/>
		</section>
	</form>
</template>

<script lang="ts">
import Vue from 'vue';
import SharedState from "./SharedState";

export default Vue.extend({
	data: () => ({
		state: SharedState
	}),
	methods: {
		objectURL(): string {
			const json = JSON.stringify(this.state);
			const blob = new Blob([ json ], { type: "application/json" });
			return URL.createObjectURL(blob);
		}
	}
});
</script>

<style scoped>
label, input {
	margin-bottom: 0.25rem;
}

section {
	display: flex;
	flex-direction: row;
	justify-content: space-between;
}

button {
	padding: 0;
	cursor: pointer;
}

a button, button label {
	padding: 0 1rem;
	cursor: pointer;
}
</style>