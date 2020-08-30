require('jsdom-global/register');
import { Test, TestSuite } from "xunit.ts";
import Actions from '../WiFiHeatMap.App/actions.vue';
import { shallowMount as mount } from '@vue/test-utils';

export default class ActionsTests extends TestSuite {
    @Test()
    async resetIsDisabledWhenFlagNotSet() {
        //arrange
        const component = mount(Actions);

        //act
        const button = component.find('#reset');

        //assert
        this.assert.notNull(button.attributes('disabled'));
    }

    @Test()
    async resetIsEnabledWhenFlagSet() {
        //arrange
        const component = mount(Actions, { propsData: { can_reset: true } });

        //act
        const button = component.find('#reset');

        //assert
        this.assert.null(button.attributes('disabled'));
    }

    @Test()
    async clickingResetEmitsEvent() {
        //arrange
        const component = mount(Actions, { propsData: { can_reset: true } });

        //act
        const button = component.find('#reset');
        button.trigger('click');

        //assert
        this.assert.notNull(component.emitted('reset'));
    }

    @Test()
    async clickingDebugEmitsEvent() {
        //arrange
        const component = mount(Actions, { propsData: { can_reset: true } });

        //act
        const checkbox = component.find('#debug');
        checkbox.trigger('click');
        checkbox.trigger('click');

        //assert
        const events = component.emitted('debug') as boolean[][];
        this.assert.equal(2, events.length);
        this.assert.true(events[0][0]);
        this.assert.false(events[1][0]);
    }
}