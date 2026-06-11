import { shallowMount as mount } from "@vue/test-utils";
import { Test, TestSuite } from "xunit.ts";

import WiFiIcon from "../App/wifi-icon.vue";

export default class WiFiIconTests extends TestSuite {
	@Test()
	colorGetsPassedToSVG() {
		//arrange
		const color = "rgba(0, 255, 0, 0.9)";

		//act
		const component = mount(WiFiIcon, { propsData: { color: color } });

		//assert
		this.assert.stringContains("style=\"fill: rgba(0, 255, 0, 0.9);\"", component.html());
	}
}
