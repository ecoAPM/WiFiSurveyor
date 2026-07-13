<template>
	<form>
		<label for="access-point">Access Point</label>
		<section class="access-point">
			<select id="access-point" v-model="state.selected">
				<option v-if="access_points.length === 0" disabled value="">
					(no signals found)
				</option>
				<option v-for="ap in access_points" :key="ap.label()" :value="ap">
					{{ ap.label() }}
				</option>
			</select>
		</section>

		<label>
			<input id="group-by-ssid" v-model="state.group_by.ssid" type="checkbox" @change="state.group_by.update()"/>
			Group by SSID
		</label>

		<label>
			<input
				id="group-by-frequency" v-model="state.group_by.frequency" :disabled="!state.group_by.ssid"
				type="checkbox"
			/>
			Combine 2.4 + 5 GHz
		</label>
	</form>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";

import AccessPoint from "./AccessPoint";
import SharedState from "./SharedState";
import Signal from "./Signal";

const state = ref(SharedState);

const all_signals = computed<Signal[]>(() =>
	state.value.current.signals.concat(state.value.readings.flatMap(r => r.signals))
);

const all_access_points = computed<AccessPoint[]>(() =>
	all_signals.value.map(s =>
		new AccessPoint(s.ssid, state.value.group_by.frequency ? null : s.frequency, state.value.group_by.ssid ? null : s.mac)
	)
);

const access_points = computed<AccessPoint[]>(() => {
	const labels = all_access_points.value.map(a => a.label());
	return all_access_points.value
		.filter((ap, index) => labels.indexOf(ap.label()) === index)
		.sort((ap1, ap2) => ap1.compareTo(ap2));
});

defineExpose({ state });
</script>

<style scoped>
label, select {
	margin-bottom: 0.25rem;
}
</style>