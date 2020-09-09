<template>
  <form>
    <section class="access-point">
      <label for="access-point">Access Point:</label>
      <select id="access-point" v-model="selected" @change="$emit('selected', selected)">
        <option disabled value="" v-if="access_points.length === 0">(no signals found)</option>
        <option v-for="ap in access_points" :key="ap.label()" :value="ap">
          {{ap.label()}}
        </option>
      </select>
    </section>

    <section class="filters">
      Group by:

      <label>
        <input type="checkbox" id="group-by-ssid" v-model="group_by.ssid" @change="update_frequency">
        SSID
      </label>

      <label>
        <input type="checkbox" id="group-by-frequency" v-model="group_by.frequency" :disabled="!group_by.ssid" />
        2.4 + 5 GHz
      </label>
    </section>

    <section class="background-selector">
      <label for="background-file">Background:</label>
      <input type="file" id="background-file" @change="$emit('background', $event.target.files)" />

      <label>
        <input type="checkbox" id="pixelate" v-model="pixelated" @change="$emit('pixelate', $event.target.checked)" />
        Pixelate
      </label>
    </section>
  </form>
</template>

<script lang="ts">
  import Vue from 'vue';
  import AccessPoint from './AccessPoint';
  import Reading from './Reading';
  import FilterFormViewModel from './FilterFormViewModel';

  export default Vue.extend({
    props: {
      current: Reading
    },
    data: () => new FilterFormViewModel(),
    computed: {
      access_points(): AccessPoint[] {
        return this.current.signals.map(signal =>
          new AccessPoint(signal.ssid,
            this.group_by.frequency ? null : signal.frequency,
            this.group_by.ssid ? null : signal.mac)
        ).sort((ap1, ap2) =>
          ap1.label() > ap2.label() ? 1
            : ap1.label() < ap2.label() ? -1
              : 0)
          .filter((ap, index, array) => array.map(a => a.label()).indexOf(ap.label()) === index);
      }
    },
    methods: {
      update_frequency(): void {
        if (!this.group_by.ssid)
          this.group_by.frequency = false;
      }
    }
  });
</script>