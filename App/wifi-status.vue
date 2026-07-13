<template>
	<figure :class="{ 'full': !fading, 'fading': fading }">
		<WiFiIcon :color="color"/>
		<figcaption v-if="value != null">
			{{ value }} {{ units }}
		</figcaption>
		<figcaption v-else>
			(no signal)
		</figcaption>
	</figure>
</template>

<script setup lang="ts">
import { computed, nextTick, ref, watch } from "vue";

import ColorConverter from "./ColorConverter";
import { Mode } from "./Mode";
import WiFiIcon from "./wifi-icon.vue";

interface Props {
	value: number | null,
	units: string,
	lastUpdated: string
};

const { value, units, lastUpdated } = defineProps<Props>();

const fading = ref(false);
const color = computed(() =>
	units == Mode.Signal.toString() && value != null
		? ColorConverter.fromSignal(value).toRGBA()
		: ColorConverter.fromSNR(value).toRGBA()
);

watch(() => lastUpdated, async () => {
	fading.value = false;
	await nextTick();
	await new Promise<void>((resolve) => setTimeout(() => {
		fading.value = true;
		resolve();
	}, 100));
});
</script>

<style scoped>
figure {
	width: 5rem;
	padding: 0.5rem;
}

figcaption {
	text-align: center;
	margin: 0.5rem 0;
}

.fading {
	opacity: 0.5;
	transition-delay: 1s;
	transition-duration: 5s;
}

.full {
	opacity: 1;
	transition-delay: 0s;
	transition-duration: 0s;
}
</style>