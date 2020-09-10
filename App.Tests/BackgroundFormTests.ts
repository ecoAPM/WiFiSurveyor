import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from "@vue/test-utils";
import BackgroundForm from '../App/background-form.vue';

export default class BackgroundTests extends TestSuite {
    @Test()
    async selectingBackgroundFileEmitsEvent() {
        //arrange
        const component = mount(BackgroundForm);

        //act
        await component.get('#background-file').trigger('change');

        //assert
        this.assert.notNull(component.emitted('background'));
    }

    @Test()
    async pixelateCheckboxEmitsEvent() {
        //arrange
        const component = mount(BackgroundForm);

        //act
        await component.get('#pixelate').trigger('change');

        //assert
        this.assert.notNull(component.emitted('pixelate'));
    }
}