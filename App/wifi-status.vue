<template>
	<figure :class="{ 'full': !fading, 'fading': fading }">
		<wifi-icon :color="color" />
		<figcaption v-if="value != null">
			{{ value }} {{ units }}
		</figcaption>
		<figcaption v-else>
			(no signal)
		</figcaption>
	</figure>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import wifi_icon from "./wifi-icon.vue";
import ColorConverter from "./ColorConverter";
import { Mode } from "./Mode";

export default defineComponent({
	components: {
		"wifi-icon": wifi_icon
	},
	props: {
		value: {
			type: Number,
			default: 0
		},
		units: {
			type: String,
			default: Mode.Signal
		},
		last_updated: {
			type: String,
			default: ""
		}
	},
	data(): { fading: boolean } {
		return {
			fading: false
		};
	},
	computed: {
		color(): string {
			return this.units == Mode.Signal && this.value != null
				? ColorConverter.fromSignal(this.value).toRGBA()
				: ColorConverter.fromSNR(this.value).toRGBA();
		}
	},
	watch: {
		async last_updated() {
			this.fading = false;
			await this.$nextTick();
			await new Promise<void>((resolve) => setTimeout(() => {
				this.fading = true;
				resolve();
			}, 100));
		}
	}
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