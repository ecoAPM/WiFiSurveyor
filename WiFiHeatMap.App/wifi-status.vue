<template>
  <figure :class="{'full': !fading, 'fading': fading }">
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
      signal: Number,
      last_updated: String
    },
    data(): object {
      return {
        fading: false
      };
    },
    computed: {
      color(): string {
        return this.signal != null
          ? ColorConverter.toColor(this.signal).toRGBA()
          : 'rgba(0, 0, 0, 0.5)';
      }
    },
    watch: {
      last_updated() {
        this.fading = false;
        setTimeout(() => this.fading = true, 1);
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

  .fading {
    opacity: 0.25;
    transition-delay: 5s;
    transition-duration: 10s;
  }

  .full {
    opacity: 1;
    transition: all 0s;
  }
</style>