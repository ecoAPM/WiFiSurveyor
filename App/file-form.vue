<template>
	<form>
		<label for="name">Name</label>
		<input id="name" v-model="state.name" type="text" />

		<section>
			<a id="save-file" class="button" :download="state.name + '.json'" :href="objectURL()">
				<button type="button">Save</button>
			</a>

			<button type="button" @click="$event.target.children[0].click()">
				<label for="load-file" @click.stop>Load</label>
			</button>
			<input v-show="false" id="load-file" type="file" accept="application/json,.json" @change="load($event.target as HTMLInputElement)" />
		</section>
	</form>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SharedState from "./SharedState";

export default defineComponent({
	data: () => ({
		state: SharedState
	}),
	methods: {
		objectURL(): string {
			const json = JSON.stringify(this.state);
			const blob = new Blob([json], { type: "application/json" });
			return URL.createObjectURL(blob);
		},
		async load(target: HTMLInputElement): Promise<void> {
			if (target.files != null) {
				await this.state.load(target.files);
				target.value = "";
			}
		}
	}
});
</script>

<style scoped>
label,
input {
	margin-bottom: 0.25rem;
}

section {
	display: flex;
	flex-direction: row;
	justify-content: space-between;
}

button {
	padding: 0;
}

a button,
button label {
	padding: 0 1rem;
}
</style>