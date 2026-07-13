<template>
	<aside v-show="state.debug" class="debug">
		<table aria-label="Debug Info">
			<thead>
				<tr>
					<th scope="col"></th>
					<th scope="col">
						SSID
					</th>
					<th scope="col">
						Frequency
					</th>
					<th scope="col">
						Channel
					</th>
					<th scope="col">
						MAC
					</th>
					<th scope="col">
						Strength
					</th>
					<th scope="col">
						SNR
					</th>
				</tr>
			</thead>
			<tbody>
				<tr v-if="state.current.signals.length === 0">
					<td colspan="6" class="center">
						(no signals found)
					</td>
				</tr>
				<tr v-for="signal in sorted_signals" :key="signal.ssid + signal.mac">
					<td :style="{ 'background-color': color(signal) }"></td>
					<td>{{ signal.ssid }}</td>
					<td class="center">
						{{ signal.frequency }} GHz
					</td>
					<td class="center">
						{{ signal.channel }}
					</td>
					<td>{{ signal.mac }}</td>
					<td class="right">
						{{ signal.strength }} dBm
					</td>
					<td class="right">
						{{ signal.snr(state.current.signals) }} dB
					</td>
				</tr>
			</tbody>
		</table>
	</aside>
</template>

<script setup lang="ts">
import { computed, defineExpose, ref } from "vue";

import ColorConverter from "./ColorConverter";
import { Mode } from "./Mode";
import SharedState from "./SharedState";
import Signal from "./Signal";

const state = ref(SharedState);

const sorted_signals = computed(() =>
	state.value.mode == Mode.Signal
		? signals_by_strength.value
		: signals_by_snr.value
);

const signals_by_strength = computed(() =>
	state.value.current.signals.slice()
		.sort((s1, s2) => s1.compareTo(s2))
);

const signals_by_snr = computed(() =>
	state.value.current.signals.slice()
		.sort((s1, s2) => s1.compareTo(s2))
		.sort((s1, s2) => s2.snr(state.value.current.signals) - s1.snr(state.value.current.signals))
);

const color = (signal: Signal) =>
	state.value.mode == Mode.Signal
		? ColorConverter.fromSignal(signal.strength).toRGBA()
		: ColorConverter.fromSNR(signal.snr(state.value.current.signals)).toRGBA();

defineExpose({ state });
</script>

<style scoped>
aside.debug {
	position: absolute;
	bottom: 1rem;
	right: 1rem;
	margin: 1rem;
	padding: 1rem;
	max-height: calc(100vh - 10rem);
	overflow-y: scroll;
	background-color: rgba(255, 255, 255, 0.75);
	border: 2px solid black;
	box-shadow: 0 0 1rem 1rem rgba(0, 0, 0, 0.5);
}

th, td {
	padding: 0.25rem;
}

th:first-child,
td:first-child {
	width: 1rem;
}

.center {
	text-align: center;
}

.right {
	text-align: right;
}
</style>