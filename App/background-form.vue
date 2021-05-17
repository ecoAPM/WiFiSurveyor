<template>
  <form>
    <label for="background-file">Background</label>
    <input type="file" id="background-file" @change="state.setBackground($event.target.files)"/>

    <label>
      <input type="checkbox" id="pixelate" v-model="state.pixelated"/>
      Pixelate
    </label>
  </form>
</template>

<script lang="ts">
import Vue from 'vue';
import SharedState from "./SharedState";

export default Vue.extend({
  data: () => ({
    state: SharedState
  })
});
</script>