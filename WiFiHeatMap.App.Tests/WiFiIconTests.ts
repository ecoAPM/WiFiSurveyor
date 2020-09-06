require('global-jsdom')();
import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from '@vue/test-utils';
import WiFiIcon from '../WiFiHeatMap.App/wifi-icon.vue';

export default class WiFiIconTests extends TestSuite {
    @Test()
    async colorGetsPassedToSVG() {
        //arrange
        const color = 'rgba(0, 255, 0, 1)';

        //act
        const component = mount(WiFiIcon, { propsData: { color: color } });

        //assert
        this.assert.stringContains('style="fill: rgba(0, 255, 0, 1);"', component.html());
    }

    @Test()
    async colorIsFullByDefault() {
        //arrange
        const color = 'rgba(0, 255, 0, 1)';

        //act
        const component = mount(WiFiIcon, { propsData: { color: color } });
        await component.vm.$nextTick();
        
        //assert
        this.assert.stringDoesNotContain('fading', component.find('svg').html());
    }

    @Test()
    async colorFadesAsReadingGoesStale() {
        //arrange
        const color = 'rgba(0, 255, 0, 1)';
        const component = mount(WiFiIcon, { propsData: { color: color } });
        await component.vm.$nextTick();

        //act
        component.setProps({color: 'rgba(255, 255, 0, 1)'});
        await component.vm.$nextTick();
        await new Promise((resolve) => setTimeout(() => resolve(), 1));
        await component.vm.$nextTick();
        
        //assert
        this.assert.stringContains('fading', component.find('svg').html());
    }
}
