import { Test, TestSuite } from "xunit.ts";
import { shallowMount as mount } from "@vue/test-utils";
import AppViewModel from "../App/AppViewModel";
import FileForm from "../App/file-form.vue";

export default class FileFormTests extends TestSuite {
	@Test()
	async canGetObjectURL() {
		//arrange
		const state = new AppViewModel();
		state.name = "Test";
		global.URL.createObjectURL = () => JSON.stringify(state);

		const component = mount(FileForm, { data: () => ({ state: state }) });

		//act
		const json = component.vm.objectURL();

		//assert
		this.assert.stringContains("\"name\":\"Test\"", json);
	}
}