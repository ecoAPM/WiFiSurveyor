<template>
  <header>
    <wifi-status :signal="current_signal" :last_updated="last_updated"></wifi-status>
    <ap-form :current="current" @selected="$emit('selected', $event)"></ap-form>
    <background-form @background="$emit('background', $event)" @pixelate="$emit('pixelate', $event)"></background-form>
    <actions :readings="readings" @undo="$emit('undo')" @reset="$emit('reset')" @debug="$emit('debug', $event)"></actions>
  </header>
</template>

<script lang="ts">
  import Vue from 'vue';
  import APForm from './ap-form.vue';
  import BackgroundForm from './background-form.vue';
  import WiFiStatus from './wifi-status.vue';
  import Actions from './actions.vue';
  import AccessPoint from './AccessPoint';
  import Reading from './Reading';

  export default Vue.extend({
    components: {
      'ap-form': APForm,
      'background-form': BackgroundForm,
      'wifi-status': WiFiStatus,
      'actions': Actions
    },
    props: {
      readings: Array,
      current: Reading,
      selected: AccessPoint,
      last_updated: String
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
    align-items: top;
    border-bottom: 1px solid rgba(0, 0, 0, 0.25);
    margin-bottom: 0.5rem;
  }

  header > * {
    margin: 0.5rem;
    display: flex;
    flex-direction: column;
  }
</style>