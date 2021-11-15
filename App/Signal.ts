import ColorConverter from "./ColorConverter";
import Color from "./Color";
import Compare from "./Compare";

export default class Signal {
	readonly mac: string;
	readonly ssid: string;
	readonly frequency: number;
	readonly strength: number;

	constructor(mac: string, ssid: string, frequency: number, strength: number) {
		this.mac = mac;
		this.ssid = ssid || '[hidden]';
		this.frequency = frequency;
		this.strength = strength;
	}

	color(): Color {
		return ColorConverter.toColor(this.strength);
	}

	compareTo(other: Signal): number {
		return Compare.numbers(other.strength, this.strength)
			|| Compare.strings(this.ssid, other.ssid)
			|| Compare.numbers(this.frequency, other.frequency)
			|| Compare.strings(this.mac, other.mac);
	}
}