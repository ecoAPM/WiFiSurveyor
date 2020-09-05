require('global-jsdom')();
import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from '@vue/test-utils';
import WiFiStatus from '../WiFiHeatMap.App/wifi-status.vue';

export default class WiFiStatusTests extends TestSuite {
    @Test()
    async showsIndicatorWhenSignalFound() {
        //arrange
        const signal = -32;

        //act
        const component = mount(WiFiStatus, { propsData: { signal: signal } });

        //assert
        this.assert.stringContains('rgba(0, 255, 0, 1)', component.html())
        this.assert.stringContains('-32 dBm', component.text())
    }

    @Test()
    async showsIndicatorWhenNoSignal() {
        //arrange
        const signal = null;

        //act
        const component = mount(WiFiStatus, { propsData: { signal: signal } });

        //assert
        this.assert.stringContains('rgba(0, 0, 0, 0.5)', component.html())
        this.assert.stringContains('no signal', component.text())
    }
}