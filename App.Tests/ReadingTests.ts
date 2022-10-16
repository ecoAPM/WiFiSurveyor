import { Test, TestSuite } from "xunit.ts";
import Reading from "../App/Reading";
import Point from "../App/Point";
import Signal from "../App/Signal";
import AccessPoint from "../App/AccessPoint";

export default class ReadingTests extends TestSuite {
	private static signals: Signal[] = [
		new Signal("mac1", "ssid1", 2, 1, -35),
		new Signal("mac2", "ssid1", 2, 11, -30),
		new Signal("mac3", "ssid1", 5, 36, -45),
		new Signal("mac4", "ssid1", 5, 157, -40),
		new Signal("mac5", "ssid2", 2, 1, -55),
		new Signal("mac6", "ssid2", 2, 11, -50),
		new Signal("mac7", "ssid2", 5, 36, -65),
		new Signal("mac8", "ssid2", 5, 157, -60)
	];

	@Test()
	async canGetSignalForSingleAP() {
		//arrange
		const reading = new Reading(1, new Point(2, 3), ReadingTests.signals);
		const access_point = new AccessPoint("ssid1", 5, "mac3");

		//act
		const strength = reading.signalFor(access_point);

		//assert
		this.assert.equal(-45, strength);
	}

	@Test()
	async canGetSignalForSSIDOnSingleFrequency() {
		//arrange
		const reading = new Reading(1, new Point(2, 3), ReadingTests.signals);
		const access_point = new AccessPoint("ssid1", 5);

		//act
		const strength = reading.signalFor(access_point);

		//assert
		this.assert.equal(-40, strength);
	}

	@Test()
	async canGetSignalForSSID() {
		//arrange
		const reading = new Reading(1, new Point(2, 3), ReadingTests.signals);
		const access_point = new AccessPoint("ssid2");

		//act
		const strength = reading.signalFor(access_point);

		//assert
		this.assert.equal(-50, strength);
	}
}