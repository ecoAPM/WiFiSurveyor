<template>
	<form>
		<label for="access-point">Access Point</label>
		<section class="access-point">
			<select id="access-point" v-model="state.selected">
				<option v-if="access_points.length === 0" disabled value="">(no signals found)</option>
				<option v-for="ap in access_points" :key="ap.label()" :value="ap">
					{{ ap.label() }}
				</option>
			</select>
		</section>

		<label>
			<input id="group-by-ssid" v-model="state.group_by.ssid" type="checkbox" @change="state.group_by.update()">
			Group by SSID
		</label>

		<label>
			<input id="group-by-frequency" v-model="state.group_by.frequency" :disabled="!state.group_by.ssid"
						 type="checkbox"/>
			Combine 2.4 + 5 GHz
		</label>
	</form>
</template>

<script lang="ts">
import Vue from 'vue';
import AccessPoint from './AccessPoint';
import SharedState from "./SharedState";
import Signal from "./Signal";

export default Vue.extend({
	data: () => ({
		state: SharedState,
	}),
	computed: {
		all_signals(): Signal[] {
			return [ ...this.state.current.signals, ...this.state.readings.flatMap(r => r.signals) ];
		},
		all_access_points(): AccessPoint[] {
			return this.all_signals.map(s =>
					new AccessPoint(s.ssid,
							this.state.group_by.frequency ? null : s.frequency,
							this.state.group_by.ssid ? null : s.mac)
			)
		},
		access_points(): AccessPoint[] {
			const labels = this.all_access_points.map(a => a.label());
			return this.all_access_points
					.filter((ap, index) => labels.indexOf(ap.label()) === index)
					.sort((ap1, ap2) => ap1.compareTo(ap2));
		}
	}
});
</script>