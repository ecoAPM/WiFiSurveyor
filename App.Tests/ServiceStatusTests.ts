import { shallowMount as mount } from "@vue/test-utils";
import { Test, TestSuite } from "xunit.ts";

import ServiceStatus from "../App/service-status.vue";

export default class ServiceStatusTests extends TestSuite {
	@Test()
	showsStatusWhenOneExists() {
		//arrange
		const status = "test message";

		//act
		const component = mount(ServiceStatus, { propsData: { status: status } });

		//assert
		this.assert.equal("test message", component.text());
	}

	@Test()
	componentDoesNotDisplayWhenNoStatus() {
		//arrange
		const status = "";

		//act
		const component = mount(ServiceStatus, { propsData: { status: status } });

		//assert
		const text = component.text().split("");
		this.assert.empty(text);
	}
}