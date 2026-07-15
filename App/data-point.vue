<template>
	<div class="point" :style="{ left: reading.location.x + 'px', top: reading.location.y + 'px', 'background-color': color.toRGBA() }">
		<div class="delete" @click="remove()"></div>
		<div class="shadow"></div>
		<div v-if="signal" class="value">
			{{ signal }} {{ mode }}
		</div>
		<div v-if="!signal" class="value">
			(no signal)
		</div>
	</div>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";

import AccessPoint from "./AccessPoint";
import ColorConverter from "./ColorConverter";
import { Mode } from "./Mode";
import Reading from "./Reading";
import SharedState from "./SharedState";

interface Props {
	mode: string
	index: number,
	reading: Reading,
	selected: AccessPoint,
}

const props = defineProps<Props>();
const state = ref(SharedState);

const signal = computed(() =>
	props.mode == Mode.Signal.toString()
		? props.reading.signalFor(props.selected)
		: props.reading.snrFor(props.selected)
);

const color = computed(() =>
	props.mode == Mode.Signal.toString()
		? ColorConverter.fromSignal(signal.value)
		: ColorConverter.fromSNR(signal.value)
);

const remove = () => {
	if (props.index > -1 && confirm("Are you sure you want to remove this reading?"))
		state.value.deleteDataPoint(props.index);
};

defineExpose({ state, props });
</script>

<style scoped>
.point {
	position: absolute;
	width: 0.25rem;
	height: 0.25rem;
	margin: -0.25rem;
	border: 1px solid rgba(0, 0, 0, 0.5);
	border-radius: 50%;
	cursor: default;
}

.point .value {
	margin-top: -1.5rem;
	margin-left: -2rem;
	text-align: center;
	width: 5rem;
}

.point .shadow {
	width: 4rem;
	margin-top: -1rem;
	margin-left: -1.5rem;
	box-shadow: 0 0 1rem 0.5rem rgba(255, 255, 255, 0.5);
}

.delete {
	width: 1.25rem;
	height: 1.25rem;
	margin: -0.5rem;
	border-radius: 50%;
	position: absolute;
	cursor: not-allowed;
}
</style>