import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from "@vue/test-utils";
import BackgroundForm from '../App/background-form.vue';
import AppViewModel from "../App/AppViewModel";

export default class BackgroundFormTests extends TestSuite {
    @Test()
    async selectingBackgroundFileSetsValue() {
        //arrange
        const state = new AppViewModel();
        state.background = 'old.png';
        const component = mount(BackgroundForm,{ data: () => ({state: state})});

        //act
        await component.get('#background-file').setValue('');
        await component.get('#background-file').trigger('change');

        //assert
        this.assert.equal('', state.background);
    }

    @Test()
    async pixelateCheckboxSetsValue() {
        //arrange
        const state = new AppViewModel();
        const component = mount(BackgroundForm, { data: () => ({ state: state})});

        //act
        await component.get('#pixelate').trigger('click');
        await component.get('#pixelate').trigger('change');

        //assert
        this.assert.false(state.pixelated);
    }
}