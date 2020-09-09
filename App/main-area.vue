<template>
  <main>
    <div class="background" :style="{'background-image': `url(${background})`}" :class="{'pixelated': pixelated}"></div>
    <canvas id="webglcanvas" @click="add($event)" :disabled="!enabled"></canvas>
    <data-point v-for="(reading, index) in readings" :key="index" :reading="reading" :selected="selected" @delete="$emit('delete', index)"></data-point>
  </main>
</template>

<script lang="ts">
  import Vue from 'vue';
  import DataPoint from './data-point.vue';
  import Renderer from './Renderer';
  import Point from './Point';
  import Reading from './Reading';
  import AccessPoint from './AccessPoint';

  export default Vue.extend({
    components: {
      'data-point': DataPoint
    },
    props: {
      enabled: Boolean,
      renderer: Renderer,
      background: String,
      pixelated: Boolean,
      readings: Array,
      current: Reading,
      selected: AccessPoint
    },
    mounted(): void {
      const canvas = document.getElementById("webglcanvas") as HTMLCanvasElement;
      if (canvas != null) {
        canvas.width = canvas.clientWidth;
        canvas.height = canvas.clientHeight;
      }
      window.addEventListener('resize', this.refresh);
    },
    methods: {
      add(e: MouseEvent): void {
        if (!this.enabled)
          return;

        const location = new Point(e.offsetX, e.offsetY);
        const reading = new Reading(this.readings.length, location, this.current.signals.concat());
        this.readings.push(reading);
        this.refresh();
      },
      refresh(): void {
        this.renderer.render(this.readings, this.selected);
      }
    }
  });
</script>

<style>
  main {
    flex-grow: 1;
    position: relative;
    overflow-x: hidden;
  }

  div.background,
  canvas {
    position: absolute;
    width: 100%;
    height: 100%;
  }

  div.background {
    background-size: contain;
    background-position: center;
    background-repeat: no-repeat;
  }

  canvas:not([disabled]) {
    cursor: pointer;
  }

  .pixelated {
    image-rendering: crisp-edges;
    image-rendering: pixelated;
  }
</style>