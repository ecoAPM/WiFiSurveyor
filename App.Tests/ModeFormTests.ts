import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from "@vue/test-utils";
import ModeForm from "../App/mode-form.vue";
import AppViewModel from "../App/AppViewModel";
import { Mode } from "../App/Mode";

export default class modeFormTests extends TestSuite {
	@Test()
	async canSetModeToSignal() {
		//arrange
		const state = new AppViewModel();
		const component = mount(ModeForm, { data: () => ({ state: state }) });

		//act
		await component.get("#mode-signal").setChecked();

		//assert
		this.assert.equal(Mode.Signal, state.mode);
	}
	
	@Test()
	async canSetModeToSNR() {
		//arrange
		const state = new AppViewModel();
		const component = mount(ModeForm, { data: () => ({ state: state }) });

		//act
		await component.get("#mode-snr").setChecked()

		//assert
		this.assert.equal(Mode.SNR, state.mode);
	}
}