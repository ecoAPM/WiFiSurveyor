require('global-jsdom')();
import { Test, TestSuite } from "xunit.ts";
import DataPoint from '../WiFiHeatMap.App/data-point.vue';
import { shallowMount as mount } from '@vue/test-utils';
import Reading from "../WiFiHeatMap.App/Reading";
import Point from "../WiFiHeatMap.App/Point";
import Signal from "../WiFiHeatMap.App/Signal";
import AccessPoint from "../WiFiHeatMap.App/AccessPoint";

export default class DataPointTests extends TestSuite {
    @Test()
    async showsSignalOnDataPoint() {
        //arrange
        const reading = new Reading(1, new Point(0, 0), [new Signal('mac', 'test', 2, -30)]);
        const selected = new AccessPoint('test');

        //act
        const component = mount(DataPoint, { propsData: { reading: reading, selected: selected } });

        //assert
        this.assert.equal('-30 dBm', component.text());
    }

    @Test()
    async positionMatchesReadingLocation() {
        //arrange
        const reading = new Reading(1, new Point(12, 34), [new Signal('mac', 'test', 2, -30)]);
        const selected = new AccessPoint('test');

        //act
        const component = mount(DataPoint, { propsData: { reading: reading, selected: selected } });

        //assert
        this.assert.stringContains('left: 12px;', component.html());
        this.assert.stringContains('top: 34px;', component.html());
    }

    @Test()
    async clickingEmitsDeleteEvent() {
        //arrange
        const reading = new Reading(1, new Point(12, 34), [new Signal('mac', 'test', 2, -30)]);
        const selected = new AccessPoint('test');
        const component = mount(DataPoint, { propsData: { reading: reading, selected: selected } });

        //act
        await component.get('.delete').trigger('click');

        //assert
        this.assert.notNull(component.emitted('delete'));
    }
}