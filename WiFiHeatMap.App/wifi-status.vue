<template>
  <figure>
    <wifi-icon :color="color"></wifi-icon>
    <figcaption v-if="signal">{{signal}} dBm</figcaption>
    <figcaption v-else>(no signal)</figcaption>
  </figure>
</template>

<script lang="ts">
  import Vue from 'vue';
  import wifi_icon from './wifi-icon.vue';
  import Color from './Color';
  import ColorConverter from './ColorConverter';

  export default Vue.extend({
    components: {
      'wifi-icon': wifi_icon
    },
    props: {
      signal: Number
    },
    computed: {
      color(): string {
        return this.signal != null
          ? ColorConverter.toColor(this.signal).toRGBA()
          : 'rgba(0, 0, 0, 0.5)';
      }
    }
  });
</script>

<style scoped>
  figure {
    width: 5rem;
  }

  figcaption {
    text-align: center;
  }
</style>