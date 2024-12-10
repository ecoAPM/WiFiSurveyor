import Compare from "./Compare";

export default class AccessPoint {
	readonly ssid: string;
	readonly frequency: number | null;
	readonly mac: string | null;

	constructor(ssid: string, frequency: number | null = null, mac: string | null = null) {
		this.ssid = ssid;
		this.frequency = frequency;
		this.mac = mac;
	}

	label(): string {
		return this.ssid
			+ (this.frequency ? ` @ ${this.frequency} GHz` : "")
			+ (this.mac ? ` (${this.mac})` : "");
	}

	compareTo(other: AccessPoint): number {
		return Compare.strings(this.ssid, other.ssid)
			|| Compare.numbers(this.frequency ?? 0, other.frequency ?? 0)
			|| Compare.strings(this.mac ?? "", other.mac ?? "");
	}
}