<template>
  <div class="point" :style="{left: reading.location.x + 'px', top: reading.location.y + 'px', 'background-color': color(reading).toRGBA()}">
    <div class="shadow"></div>
    <div class="value">{{reading.signalFor(selected)}} dBm</div>
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
      reading: Reading,
      selected: AccessPoint
    },
    methods: {
      color(reading: Reading): Color {
        return ColorConverter.toColor(reading.signalFor(this.selected));
      }
    }
  });
</script>

<style scoped>
  .point {
    position: absolute;
    width: 4px;
    height: 4px;
    margin: -4px 0 0 -4px;
    border: 1px solid black;
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
</style>