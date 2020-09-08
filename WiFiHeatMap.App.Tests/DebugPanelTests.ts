require('global-jsdom')();
import { Test, TestSuite } from "xunit.ts";
import DebugPanel from '../WiFiHeatMap.App/debug-panel.vue';
import { shallowMount as mount } from '@vue/test-utils';
import Signal from "../WiFiHeatMap.App/Signal";

export default class ActionsTests extends TestSuite {
    @Test()
    async displayedWhenEnabled() {
        //arrange
        const component = mount(DebugPanel, { propsData: { enabled: true, signals: [] } });

        //act
        const wrapper = component.get('aside.debug');

        //assert
        this.assert.null(wrapper.attributes('style'));
    }

    @Test()
    async notDisplayedWhenNotEnabled() {
        //arrange
        const component = mount(DebugPanel, { propsData: { enabled: false, signals: [] } });

        //act
        const wrapper = component.get('aside.debug');

        //assert
        this.assert.stringContains('display: none;', wrapper.attributes('style'));
    }

    @Test()
    async signalsAreSortedByStrength() {
        //arrange
        const signals = [
            new Signal('mac1', 'ssid1', 2, -50),
            new Signal('mac2', 'ssid2', 2, -40)
        ]
        const component = mount(DebugPanel, { propsData: { enabled: false, signals: signals } });

        //act
        const sorted_signals = component.findAll('table tbody tr');

        //assert
        this.assert.stringContains('-40', sorted_signals.at(0).text());
        this.assert.stringContains('-50', sorted_signals.at(1).text());
    }
}