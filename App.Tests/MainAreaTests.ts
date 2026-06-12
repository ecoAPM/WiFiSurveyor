import { shallowMount as mount } from "@vue/test-utils";
import { Test, TestSuite } from "xunit.ts";

import AppViewModel from "../App/AppViewModel";
import MainArea from "../App/main-area.vue";
import Point from "../App/Point";
import Reading from "../App/Reading";

export default class MainAreaTests extends TestSuite {
	@Test()
	showsDataPointForEachReading() {
		//arrange
		const state = new AppViewModel();
		state.readings = [
			new Reading(1, new Point(2, 3), []),
			new Reading(2, new Point(3, 4), [])
		];

		//act
		const component = mount(MainArea, { data: () => ({ state: state }) });

		//assert
		this.assert.equal(2, component.findAll("data-point-stub").length);
	}

	@Test()
	backgroundIsSetFromData() {
		//arrange
		const state = new AppViewModel();
		state.background = "test.png";

		//act
		const component = mount(MainArea, { data: () => ({ state: state }) });

		//assert
		this.assert.stringContains("background-image: url(\"test.png\");", component.get(".background").attributes("style"));
	}

	@Test()
	backgroundIsPixelatedWhenFlagSet() {
		//arrange
		const state = new AppViewModel();
		state.pixelated = true;

		//act
		const component = mount(MainArea, { data: () => ({ state: state }) });

		//assert
		this.assert.stringContains("pixelated", component.get(".background").attributes("class"));
	}

	@Test()
	backgroundIsNotPixelatedWhenFlagNotSet() {
		//arrange
		const state = new AppViewModel();
		state.pixelated = false;

		//act
		const component = mount(MainArea, { data: () => ({ state: state }) });

		//assert
		this.assert.stringDoesNotContain("pixelated", component.get(".background").attributes("class"));
	}

	@Test()
	async clickingCanvasAddsReading() {
		//arrange
		const state = new AppViewModel();
		const component = mount(MainArea, { data: () => ({ state: state }), propsData: { enabled: true } });

		//act
		await component.get("canvas").trigger("click");

		//assert
		this.assert.equal(1, state.readings.length);
	}

	@Test()
	async clickingCanvasDoesNotAddReadingWhenDisabled() {
		//arrange
		const state = new AppViewModel();
		const component = mount(MainArea, { data: () => ({ state: state }) });

		//act
		await component.get("canvas").trigger("click");

		//assert
		this.assert.empty(state.readings);
	}
}