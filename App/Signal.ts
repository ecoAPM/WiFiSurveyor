import Compare from "./Compare";

export default class Signal {
	readonly mac: string;
	readonly ssid: string;
	readonly frequency: number;
	readonly channel: number;
	readonly strength: number;

	private static readonly noiseFloor = -100;

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

	snr(others: Signal[]): number {
		const noise = this.noise(others);
		return this.strength - noise;
	}

	private noise(others: Signal[]): number {
		const neighbors = others.filter(s => s.mac != this.mac
			&& s.frequency == this.frequency
			&& s.channel - 4 < this.channel
			&& s.channel + 4 > this.channel
		);
		const strengths = neighbors.map(s => s.strength);
		return strengths.length > 0
			? Math.max(...strengths)
			: Signal.noiseFloor;
	}
}