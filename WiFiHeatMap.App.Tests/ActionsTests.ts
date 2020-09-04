require('jsdom-global/register');
import { Test, TestSuite } from "xunit.ts";
import Actions from '../WiFiHeatMap.App/actions.vue';
import { shallowMount as mount } from '@vue/test-utils';

export default class ActionsTests extends TestSuite {
    @Test()
    async resetIsEnabledWhenReadingsExist() {
        //arrange
        const component = mount(Actions, { propsData: { readings: [{}] } });

        //act
        const button = component.find('#reset');

        //assert
        this.assert.null(button.attributes('disabled'));
    }

    @Test()
    async resetIsDisabledWhenReadingsAreEmpty() {
        //arrange
        const component = mount(Actions, { propsData: { readings: [] } });

        //act
        const button = component.find('#reset');

        //assert
        this.assert.notNull(button.attributes('disabled'));
    }

    @Test()
    async clickingResetEmitsEvent() {
        //arrange
        const component = mount(Actions, { propsData: { readings: [{}] } });

        //act
        const button = component.find('#reset');
        await button.trigger('click');

        //assert
        this.assert.notNull(component.emitted('reset'));
    }

    @Test()
    async clickingDebugEmitsEvent() {
        //arrange
        const component = mount(Actions, { propsData: { readings: [{}] } });

        //act
        const checkbox = component.find('#debug');
        await checkbox.trigger('click');
        await checkbox.trigger('click');

        //assert
        const events = component.emitted('debug') as boolean[][];
        this.assert.equal(2, events.length);
        this.assert.true(events[0][0]);
        this.assert.false(events[1][0]);
    }
}