require('global-jsdom')();
import { Test, TestSuite } from "xunit.ts";
import MainArea from '../WiFiHeatMap.App/main-area.vue';
import { shallowMount as mount } from '@vue/test-utils';
import Reading from "../WiFiHeatMap.App/Reading";
import Point from "../WiFiHeatMap.App/Point";
import Mockito from 'ts-mockito';
import Renderer from "../WiFiHeatMap.App/Renderer";
import MockFactory from "./MockFactory";

export default class MainAreaTests extends TestSuite {
    @Test()
    async showsDataPointForEachReading() {
        //arrange
        const canvas = MockFactory.canvas();
        const readings: Reading[] = [
            new Reading(1, new Point(2, 3), []),
            new Reading(2, new Point(3, 4), [])
        ];

        //act
        const component = mount(MainArea, { propsData: { readings: readings, current: new Reading(0, new Point(0, 0), []), renderer: new Renderer(Mockito.instance(canvas)) } });

        //assert
        this.assert.equal(2, component.findAll('data-point-stub').length);
    }

    @Test()
    async backgroundIsSetFromData() {
        const canvas = MockFactory.canvas();
        const readings: Reading[] = [];

        //act
        const component = mount(MainArea, { propsData: { readings: readings, current: new Reading(0, new Point(0, 0), []), renderer: new Renderer(Mockito.instance(canvas)), background: 'test.png' } });

        //assert
        this.assert.stringContains('background-image: url(test.png);', component.find('.background').attributes('style'));
    }

    @Test()
    async backgroundIsPixelatedWhenFlagSet() {
        const canvas = MockFactory.canvas();
        const readings: Reading[] = [];

        //act
        const component = mount(MainArea, { propsData: { readings: readings, current: new Reading(0, new Point(0, 0), []), renderer: new Renderer(Mockito.instance(canvas)), pixelated: true } });

        //assert
        this.assert.stringContains('pixelated', component.find('.background').attributes('class'));
    }

    @Test()
    async backgroundIsNotPixelatedWhenFlagNotSet() {
        const canvas = MockFactory.canvas();
        const readings: Reading[] = [];

        //act
        const component = mount(MainArea, { propsData: { readings: readings, current: new Reading(0, new Point(0, 0), []), renderer: new Renderer(Mockito.instance(canvas)), pixelated: false } });

        //assert
        this.assert.stringDoesNotContain('pixelated', component.find('.background').attributes('class'));
    }

    @Test()
    async clickingCanvasAddsReading() {
        //arrange
        const canvas = MockFactory.canvas();
        const readings: Reading[] = [];
        const component = mount(MainArea, { propsData: { readings: readings, current: new Reading(0, new Point(0, 0), []), renderer: new Renderer(Mockito.instance(canvas)) } });

        //act
        await component.find('canvas').trigger('click');
        await component.vm.$nextTick();

        //assert
        this.assert.equal(1, readings.length);
    }
}