<template>
  <div class="status" v-if="status">
    <pre>{{status}}</pre>
  </div>
</template>

<script lang="ts">
  import Vue from 'vue';

  export default Vue.extend({
    props: {
      status: String
    }
  });
</script>

<style scoped>
  .status {
    position: absolute;
    display: flex;
    width: 100%;
    height: 100%;
    align-items: center;
    justify-content: center;
    background-color: rgba(0, 0, 0, 0.25);
  }

  pre {
    padding: 1rem;
    background: white;
    border: 2px solid black;
    border-radius: 8px;
    box-shadow: 0 0 1rem 1rem rgba(0, 0, 0, 0.5);
  }
</style>