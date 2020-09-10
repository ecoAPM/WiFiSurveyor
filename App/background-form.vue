<template>
  <form>
    <label for="background-file">Background</label>
    <input type="file" id="background-file" @change="$emit('background', $event.target.files)" />

    <label>
      <input type="checkbox" id="pixelate" checked @change="$emit('pixelate', $event.target.checked)" />
      Pixelate
    </label>
  </form>
</template>

<script lang="ts">
  import Vue from 'vue';

  export default Vue.extend({
    data(): object {
      return {
        pixelated: true
      };
    }
  });
</script>