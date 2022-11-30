import ColorConverter from "./ColorConverter";
import Color from "./Color";
import Compare from "./Compare";

export default class Signal {
	readonly mac: string;
	readonly ssid: string;
	readonly frequency: number;
	readonly channel: number;
	readonly strength: number;

	constructor(mac: string, ssid: string, frequency: number, channel: number, strength: number) {
		this.mac = mac;
		this.ssid = ssid || "[hidden]";
		this.frequency = frequency;
		this.channel = channel;
		this.strength = strength;
	}

	compareTo(other: Signal): number {
		return Compare.numbers(other.strength, this.strength)
			|| Compare.strings(this.ssid, other.ssid)
			|| Compare.numbers(this.frequency, other.frequency)
			|| Compare.strings(this.mac, other.mac);
	}
}