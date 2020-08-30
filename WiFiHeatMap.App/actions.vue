<template>
  <section class="actions">
    <button type="button" id="reset" @click="$emit('reset')" :disabled="!can_reset">Clear</button>

    <label for="debug">
      <input type="checkbox" id="debug" @change="$emit('debug', $event.target.checked)" />
      Debug
    </label>
  </section>
</template>

<script lang="ts">
  import Vue from 'vue';

  export default Vue.extend({
    props: {
      can_reset: Boolean
    }
  });
</script>