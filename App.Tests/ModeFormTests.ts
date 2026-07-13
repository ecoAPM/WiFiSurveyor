import { Test, TestSuite } from "xunit.ts";

import AppViewModel from "../App/AppViewModel";
import { Mode } from "../App/Mode";
import ModeForm from "../App/mode-form.vue";
import { ComponentDefinition, mount } from "./TestHelpers";

export default class ModeFormTests extends TestSuite {
	@Test()
	async canSetModeToSignal() {
		//arrange
		const state = new AppViewModel();
		const component = await mount(ModeForm as ComponentDefinition, state);

		//act
		await component.get("#mode-signal").setValue(true);

		//assert
		this.assert.equal(Mode.Signal, state.mode);
	}

	@Test()
	async canSetModeToSNR() {
		//arrange
		const state = new AppViewModel();
		const component = await mount(ModeForm as ComponentDefinition, state);

		//act
		await component.get("#mode-snr").setValue(true);

		//assert
		this.assert.equal(Mode.SNR, state.mode);
	}
}