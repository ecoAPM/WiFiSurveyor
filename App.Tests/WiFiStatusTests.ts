import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from "@vue/test-utils";
import WiFiStatus from "../App/wifi-status.vue";
import { Mode } from "../App/Mode";

export default class WiFiStatusTests extends TestSuite {
	@Test()
	async showsIndicatorWhenSignalFound() {
		//arrange
		const signal = -40;
		const units = Mode.Signal;

		//act
		const component = mount(WiFiStatus, { propsData: { value: signal, units: units } });

		//assert
		this.assert.stringContains("rgba(0, 255, 0, 1)", component.html());
		this.assert.equal("-40 dBm", component.text());
	}

	@Test()
	async showsCorrectUnits() {
		//arrange
		const value = 50;
		const units = Mode.SNR;

		//act
		const component = mount(WiFiStatus, { propsData: { value: value, units: units } });

		//assert
		this.assert.stringContains("rgba(0, 255, 0, 1)", component.html());
		this.assert.equal("50 dB", component.text());
	}

	@Test()
	async showsIndicatorWhenNoSignal() {
		//arrange
		const signal = null;
		const units = Mode.Signal;

		//act
		const component = mount(WiFiStatus, { propsData: { value: signal, units: units } });

		//assert
		this.assert.stringContains("rgba(0, 0, 0, 0.5)", component.html());
		this.assert.stringContains("no signal", component.text());
	}

	@Test()
	async colorIsFullByDefault() {
		//arrange
		const color = "rgba(0, 255, 0, 1)";

		//act
		const component = mount(WiFiStatus, { propsData: { color: color } });

		//assert
		this.assert.stringDoesNotContain("fading", component.html());
	}

	@Test()
	async colorFadesAsReadingGoesStale() {
		//arrange
		const component = mount(WiFiStatus, { propsData: { color: "rgba(0, 255, 0, 1)", last_updated: "earlier" } });

		//act
		await component.setProps({ last_updated: "now" });
		await component.vm.$nextTick();
		await new Promise<void>((resolve) => setTimeout(() => resolve(), 100));

		//assert
		this.assert.stringContains("fading", component.html());
	}
}