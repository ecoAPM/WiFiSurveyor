<template>
  <header>
    <h1>WiFiHeatMap</h1>
    <filter-form :current="current" @selected="$emit('selected', $event)" @background="$emit('background', $event)" @pixelate="$emit('pixelate', $event)"></filter-form>
    <wifi-status :signal="current_signal"></wifi-status>
    <actions :readings="readings" @reset="$emit('reset')" @debug="$emit('debug', $event)"></actions>
  </header>
</template>

<script lang="ts">
  import Vue from 'vue';
  import FilterForm from './filter-form.vue';
  import WiFiStatus from './wifi-status.vue';
  import Actions from './actions.vue';
  import AccessPoint from './AccessPoint';
  import Reading from './Reading';

  export default Vue.extend({
    components: {
      'filter-form': FilterForm,
      'wifi-status': WiFiStatus,
      'actions': Actions
    },
    props: {
      readings: Array,
      current: Reading,
      selected: AccessPoint
    },
    computed: {
      current_signal(): number | null {
        return this.current.signalFor(this.selected);
      }
    }
  });
</script>

<style scoped>
  header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  header > * {
    margin: 1rem;
    display: flex;
    flex-direction: column;
  }
</style>