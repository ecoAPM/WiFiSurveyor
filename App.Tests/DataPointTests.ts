import { Test, TestSuite } from "xunit.ts";

import AccessPoint from "../App/AccessPoint";
import AppViewModel from "../App/AppViewModel";
import DataPoint from "../App/data-point.vue";
import Point from "../App/Point";
import Reading from "../App/Reading";
import Signal from "../App/Signal";
import { mount } from "./TestHelpers";
import { Mode } from "../App/Mode";

export default class DataPointTests extends TestSuite {
	@Test()
	async showsSignalOnDataPoint() {
		//arrange
		const reading = new Reading(1, new Point(0, 0), [ new Signal("mac", "test", 2, 1, -30) ]);
		const selected = new AccessPoint("test");
		const props = { mode: Mode.Signal, reading: reading, selected: selected };

		//act
		const component = await mount(DataPoint, null, props);

		//assert
		this.assert.equal("-30 dBm", component.text());
	}

	@Test()
	async showsNoSignalWhenNoSignal() {
		//arrange
		const reading = new Reading(1, new Point(0, 0), []);
		const selected = new AccessPoint("test");
		const props = { mode: Mode.Signal, reading: reading, selected: selected };

		//act
		const component = await mount(DataPoint, null, props);

		//assert
		this.assert.equal("(no signal)", component.text());
	}

	@Test()
	async positionMatchesReadingLocation() {
		//arrange
		const reading = new Reading(1, new Point(12, 34), [ new Signal("mac", "test", 2, 1, -30) ]);
		const selected = new AccessPoint("test");
		const props = { mode: Mode.Signal, reading: reading, selected: selected };

		//act
		const component = await mount(DataPoint, null, props);

		//assert
		this.assert.stringContains("left: 12px;", component.html());
		this.assert.stringContains("top: 34px;", component.html());
	}

	@Test()
	async colorMatchesReadingStrength() {
		//arrange
		const reading = new Reading(1, new Point(12, 34), [ new Signal("mac", "test", 2, 1, -30) ]);
		const selected = new AccessPoint("test");
		const props = { mode: Mode.Signal, reading: reading, selected: selected };

		//act
		const component = await mount(DataPoint, null, props);

		//assert
		this.assert.stringContains("background-color: rgb(0, 127, 0);", component.html());
	}

	@Test()
	async clickingDeletesReading() {
		//arrange
		const state = new AppViewModel();
		state.readings = [
			new Reading(1, new Point(0, 0), []),
			new Reading(2, new Point(0, 0), []),
			new Reading(3, new Point(0, 0), [])
		];

		const reading = new Reading(1, new Point(12, 34), [ new Signal("mac", "test", 2, 1, -30) ]);
		const selected = new AccessPoint("test");
		const props = {mode: Mode.Signal,  index: 1, reading: reading, selected: selected };

		const component = await mount(DataPoint, state, props);
		global.confirm = () => true;

		//act
		await component.get(".delete").trigger("click");

		//assert
		this.assert.count(2, state.readings);
		this.assert.equal(1, state.readings[0].id);
		this.assert.equal(3, state.readings[1].id);
	}

	@Test()
	async cancellingConfirmDoesNotDeleteReading() {
		//arrange
		const state = new AppViewModel();
		state.readings = [
			new Reading(1, new Point(0, 0), []),
			new Reading(2, new Point(0, 0), []),
			new Reading(3, new Point(0, 0), [])
		];

		const reading = new Reading(1, new Point(12, 34), [ new Signal("mac", "test", 2, 1, -30) ]);
		const selected = new AccessPoint("test");
		const props = { mode: Mode.Signal, index: 1, reading: reading, selected: selected };

		const component = await mount(DataPoint, state, props);
		global.confirm = () => false;

		//act
		await component.get(".delete").trigger("click");

		//assert
		this.assert.count(3, state.readings);
	}
}