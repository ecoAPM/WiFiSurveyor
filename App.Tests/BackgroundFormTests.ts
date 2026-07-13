import { Test, TestSuite } from "xunit.ts";

import AppViewModel from "../App/AppViewModel";
import BackgroundForm from "../App/background-form.vue";
import { ComponentDefinition, mount } from "./TestHelpers";

export default class BackgroundFormTests extends TestSuite {
	@Test()
	async selectingBackgroundFileSetsValue() {
		//arrange
		const state = new AppViewModel();
		state.background = "old.png";

		const component = await mount(BackgroundForm as ComponentDefinition, state);

		//act
		await component.get("#background-file").setValue("");
		await component.get("#background-file").trigger("change");

		//assert
		this.assert.equal("", state.background);
	}

	@Test()
	async pixelateCheckboxSetsValue() {
		//arrange
		const state = new AppViewModel();

		const component = await mount(BackgroundForm as ComponentDefinition, state);

		//act
		const checkbox = component.get("#pixelate");
		await checkbox.trigger("click");
		await checkbox.trigger("change");

		//assert
		this.assert.false(state.pixelated);
	}
}