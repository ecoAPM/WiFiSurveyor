import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from "@vue/test-utils";
import WiFiIcon from "../App/wifi-icon.vue";

export default class WiFiIconTests extends TestSuite {
	@Test()
	async colorGetsPassedToSVG() {
		//arrange
		const color = "rgb(0, 255, 0)";

		//act
		const component = mount(WiFiIcon, { propsData: { color: color } });

		//assert
		this.assert.stringContains("style=\"fill: rgb(0, 255, 0);\"", component.html());
	}
}
