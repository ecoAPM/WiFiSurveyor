<template>
  <div class="app">
    <status :status="connection_status"></status>
    <header-menu :readings="readings" :current="current" :selected="selected" :last_updated="last_updated" @background="setBackground($event)" @selected="setSelected($event)" @pixelate="setPixelated($event)" @reset="reset()" @debug="setDebug($event)" @undo="deleteDataPoint(readings.length-1)"></header-menu>
    <main-area :enabled="!connection_status" :renderer="renderer" :background="background" :pixelated="pixelated" :readings="readings" :current="current" :selected="selected" @delete="deleteDataPoint($event)"></main-area>
    <debug-panel :enabled="debug" :signals="current.signals" />
  </div>
</template>

<script lang="ts">
  import Vue from "vue";
  import Status from './status.vue';
  import HeaderMenu from './header-menu.vue';
  import MainArea from './main-area.vue';
  import DebugPanel from './debug-panel.vue';
  import AccessPoint from './AccessPoint';
  import AppViewModel from './AppViewModel';

  export default Vue.extend({
    components: {
      'status': Status,
      'header-menu': HeaderMenu,
      'main-area': MainArea,
      'debug-panel': DebugPanel
    },
    inject: {
      signal_service_factory: "signal_service",
      renderer_factory: "renderer",
    },
    data: () => new AppViewModel(),
    computed: {
      connection_status(): string {
        return this.signal_service != null
          ? this.signal_service.status
          : '(loading...)'
      },
      last_updated(): string {
        return this.signal_service != null
          ? this.signal_service.last_updated
          : '';
      }
    },
    mounted(): void {
      const canvas = document.getElementById("webglcanvas") as HTMLCanvasElement;
      this.signal_service = this.signal_service_factory(this.current.signals);
      this.renderer = this.renderer_factory(canvas);
    },
    methods: {
      deleteDataPoint(index: number): void {
        if (confirm('Are you sure you want to delete this reading?')) {
          this.readings.splice(index, 1);
          if (this.readings.length >= 3)
            this.renderer.render(this.readings, this.selected);
          else
            this.renderer.clear();
        }
      },
      reset(): void {
        if (confirm('Are you sure you want to delete all signal readings?')) {
          this.readings = [];
          this.renderer?.clear();
        }
      },
      async setBackground(files: FileList): Promise<void> {
        if (files.length !== 1) {
          this.background = '';
          return;
        }

        const file = files.item(0) as File;
        const reader = new FileReader();
        const file_contents = new Promise<string>((resolve, reject) => {
          reader.onerror = () => reject(new DOMException('sad face'));
          reader.onload = () => resolve(reader.result as string);
          reader.readAsDataURL(file);
        });
        this.background = await file_contents;
      },
      setSelected(selected: AccessPoint): void {
        this.selected = selected;
      },
      setPixelated(enabled: boolean): void {
        this.pixelated = enabled;
      },
      setDebug(enabled: boolean): void {
        this.debug = enabled;
      }
    },
    watch: {
      selected(): void {
        if (this.selected != null)
          this.renderer?.render(this.readings, this.selected);
        else
          this.renderer?.clear();
      }
    }
  });
</script>

<style scoped>
  .app {
    display: flex;
    flex-direction: column;
    height: 100vh;
  }
</style>
