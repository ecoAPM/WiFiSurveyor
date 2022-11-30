import { Test, TestSuite } from "xunit.ts";
import Signal from "../App/Signal";

export default class SignalTests extends TestSuite {
	@Test()
	async canCreateSignal() {
		//arrange
		const mac = "12:34:56:78:90:ab", ssid = "test", frequency = 2, channel = 1, strength = -50;

		//act
		const signal = new Signal(mac, ssid, frequency, channel, strength);

		//assert
		this.assert.equal("12:34:56:78:90:ab", signal.mac);
		this.assert.equal("test", signal.ssid);
		this.assert.equal(2, signal.frequency);
		this.assert.equal(-50, signal.strength);
	}

	@Test()
	async canCompareSignals() {
		//arrange
		const s1 = new Signal("12:34:56:78:90:ab", "test1", 2, 1, -20);
		const s2 = new Signal("12:34:56:78:90:ac", "test1", 2, 11, -20);
		const s3 = new Signal("12:34:56:78:90:ad", "test2", 2, 6, -20);
		const s4 = new Signal("12:34:56:78:90:ae", "test2", 2, 11, -40);
		const s5 = new Signal("12:34:56:78:90:af", "test2", 5, 36, -40);

		//act
		const less1 = s1.compareTo(s2);
		const less2 = s1.compareTo(s3);
		const less3 = s1.compareTo(s4);
		const less4 = s4.compareTo(s5);
		const greater1 = s2.compareTo(s1);
		const greater2 = s3.compareTo(s1);
		const greater3 = s4.compareTo(s1);
		const greater4 = s5.compareTo(s1);

		//assert
		this.assert.equal(-1, less1);
		this.assert.equal(-1, less2);
		this.assert.equal(-1, less3);
		this.assert.equal(-1, less4);
		this.assert.equal(1, greater1);
		this.assert.equal(1, greater2);
		this.assert.equal(1, greater3);
		this.assert.equal(1, greater4);
	}
}