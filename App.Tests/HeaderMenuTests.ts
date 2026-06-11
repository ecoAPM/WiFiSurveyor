import { shallowMount as mount } from "@vue/test-utils";
import { Test, TestSuite } from "xunit.ts";

import AccessPoint from "../App/AccessPoint";
import AppViewModel from "../App/AppViewModel";
import HeaderMenu from "../App/header-menu.vue";
import Point from "../App/Point";
import Reading from "../App/Reading";
import Signal from "../App/Signal";

export default class HeaderMenuTests extends TestSuite {
	@Test()
	async canGetCurrentSignal() {
		//arrange
		const signals = [
			new Signal("123abc", "test", 2, 1, -10)
		];
		const state = new AppViewModel();
		state.current = new Reading(0, new Point(0, 0), signals);
		state.selected = new AccessPoint("test");
		const component = mount(HeaderMenu, { data: () => ({ state: state }) });

		//act
		const current_signal = component.vm.current_signal;

		//assert
		this.assert.equal(-10, current_signal);
	}
}