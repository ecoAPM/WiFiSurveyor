import { Test, TestSuite } from "xunit.ts";
import HeaderMenu from '../App/header-menu.vue';
import { shallowMount as mount } from '@vue/test-utils';
import AppViewModel from "../App/AppViewModel";
import AccessPoint from "../App/AccessPoint";
import Reading from "../App/Reading";
import Point from "../App/Point";
import Signal from "../App/Signal";

export default class HeaderMenuTests extends TestSuite {
    @Test()
    async canGetCurrentSignal() {
        //arrange
        const signals = [
            new Signal('123abc', 'test', 2.4, -10)
        ];
        const state = new AppViewModel();
        state.current = new Reading(0, new Point(0, 0), signals);
        state.selected = new AccessPoint('test');
        const component = mount(HeaderMenu, { data: () => ({ state: state }) });
       
        //act
        const current_signal = component.vm.current_signal;
        
        //assert
        this.assert.equal(-10, current_signal);
    }
}