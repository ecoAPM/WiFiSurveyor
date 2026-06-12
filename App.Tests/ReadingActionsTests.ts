import { shallowMount as mount } from "@vue/test-utils";
import Mockito from "ts-mockito";
import { Test, TestSuite } from "xunit.ts";

import AppViewModel from "../App/AppViewModel";
import Reading from "../App/Reading";
import ReadingActions from "../App/reading-actions.vue";

export default class ReadingActionsTests extends TestSuite {
	@Test()
	undoIsEnabledWhenReadingsExist() {
		//arrange
		const state = new AppViewModel();
		state.readings = [ Mockito.instance(Mockito.mock(Reading)) ];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });

		//act
		const button = component.get("#undo");

		//assert
		this.assert.undefined(button.attributes("disabled"));
	}

	@Test()
	undoIsDisabledWhenReadingsAreEmpty() {
		//arrange
		const state = new AppViewModel();
		state.readings = [];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });

		//act
		const button = component.get("#undo");

		//assert
		this.assert.notNull(button.attributes("disabled"));
	}

	@Test()
	async clickingUndoRemovesLastReading() {
		//arrange
		const state = new AppViewModel();
		state.readings = [ Mockito.instance(Mockito.mock(Reading)), Mockito.instance(Mockito.mock(Reading)) ];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });
		global.confirm = () => true;

		//act
		await component.get("#undo").trigger("click");

		//assert
		this.assert.count(1, state.readings);
	}

	@Test()
	async cancellingUndoDoesNotRemoveLastReading() {
		//arrange
		const state = new AppViewModel();
		state.readings = [ Mockito.instance(Mockito.mock(Reading)), Mockito.instance(Mockito.mock(Reading)) ];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });
		global.confirm = () => false;

		//act
		await component.get("#undo").trigger("click");

		//assert
		this.assert.count(2, state.readings);
	}

	@Test()
	resetIsEnabledWhenReadingsExist() {
		//arrange
		const state = new AppViewModel();
		state.readings = [ Mockito.instance(Mockito.mock(Reading)) ];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });

		//act
		const button = component.get("#reset");

		//assert
		this.assert.undefined(button.attributes("disabled"));
	}

	@Test()
	resetIsDisabledWhenReadingsAreEmpty() {
		//arrange
		const state = new AppViewModel();
		state.readings = [];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });

		//act
		const button = component.get("#reset");

		//assert
		this.assert.notNull(button.attributes("disabled"));
	}

	@Test()
	async clickingResetClearsReadings() {
		//arrange
		const state = new AppViewModel();
		state.readings = [ Mockito.instance(Mockito.mock(Reading)) ];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });
		global.confirm = () => true;

		//act
		await component.get("#reset").trigger("click");

		//assert
		this.assert.empty(state.readings);
	}

	@Test()
	async cancellingResetDoesNotClearReadings() {
		//arrange
		const state = new AppViewModel();
		state.readings = [ Mockito.instance(Mockito.mock(Reading)) ];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });
		global.confirm = () => false;

		//act
		await component.get("#reset").trigger("click");

		//assert
		this.assert.notEmpty(state.readings);
	}

	@Test()
	async clickingDebugSetsFlag() {
		//arrange
		const state = new AppViewModel();
		state.readings = [ Mockito.instance(Mockito.mock(Reading)) ];
		const component = mount(ReadingActions, { data: () => ({ state: state }) });

		//act
		const checkbox = component.get("#debug");
		await checkbox.trigger("click");
		await checkbox.trigger("change");

		//assert
		this.assert.true(state.debug);
	}
}