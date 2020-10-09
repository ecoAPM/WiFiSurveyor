<template>
  <div class="point" :style="{left: reading.location.x + 'px', top: reading.location.y + 'px', 'background-color': color(reading).toRGBA()}">
    <div class="delete" @click="$emit('delete')"></div>
    <div class="shadow"></div>
    <div class="value" v-if="signal">{{signal}} dBm</div>
    <div class="value" v-if="!signal">(no signal)</div>
  </div>
</template>

<script lang="ts">
  import Vue from 'vue';
  import Color from './Color';
  import ColorConverter from './ColorConverter';
  import AccessPoint from './AccessPoint';
  import Reading from './Reading';

  export default Vue.extend({
    props: {
      index: Number,
      reading: Reading,
      selected: AccessPoint
    },
    methods: {
      color(reading: Reading): Color {
        return ColorConverter.toColor(reading.signalFor(this.selected));
      }
    },
    computed: {
      signal(): number | null {
        return this.reading.signalFor(this.selected);
      }
    }
  });
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