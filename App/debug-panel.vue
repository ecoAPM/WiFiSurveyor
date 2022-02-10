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
						MAC
					</th>
					<th scope="col">
						Strength
					</th>
				</tr>
			</thead>
			<tbody>
				<tr v-if="state.current.signals.length === 0">
					<td colspan="5">
						(no signals found)
					</td>
				</tr>
				<tr v-for="signal in signals_by_strength" :key="signal.ssid + signal.mac">
					<td :style="{ 'background-color': signal.color().toRGBA() }"></td>
					<td>{{ signal.ssid }}</td>
					<td>{{ signal.frequency }} GHz</td>
					<td>{{ signal.mac }}</td>
					<td>{{ signal.strength }} dBm</td>
				</tr>
			</tbody>
		</table>
	</aside>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Signal from "./Signal";
import SharedState from "./SharedState";

export default defineComponent({
	data: () => ({
		state: SharedState
	}),
	computed: {
		signals_by_strength(): Signal[] {
			return this.state.current.signals.slice()
				.sort((s1, s2) => s1.compareTo(s2));
		}
	}
});
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

table th:first-child,
table td:first-child {
	width: 1rem;
}
</style>