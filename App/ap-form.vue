<template>
  <form>
    <label for="access-point">Access Point</label>
    <section class="access-point">
      <select id="access-point" v-model="selected" @change="$emit('selected', selected)">
        <option disabled value="" v-if="access_points.length === 0">(no signals found)</option>
        <option v-for="ap in access_points" :key="ap.label()" :value="ap">
          {{ap.label()}}
        </option>
      </select>
    </section>

    <label>
      <input type="checkbox" id="group-by-ssid" v-model="group_by.ssid" @change="update_frequency">
      Group by SSID
    </label>

    <label>
      <input type="checkbox" id="group-by-frequency" v-model="group_by.frequency" :disabled="!group_by.ssid" />
      Combine 2.4 + 5 GHz
    </label>
  </form>
</template>

<script lang="ts">
  import Vue from 'vue';
  import AccessPoint from './AccessPoint';
  import Reading from './Reading';
  import APFormViewModel from './APFormViewModel';

  export default Vue.extend({
    props: {
      current: Reading
    },
    data: () => new APFormViewModel(),
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