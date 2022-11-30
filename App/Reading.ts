import Point from "./Point";
import Signal from "./Signal";
import AccessPoint from "./AccessPoint";

export default class Reading {
	readonly id: number;
	readonly location: Point;
	readonly signals: Signal[];

	constructor(id: number, location: Point, signals: Signal[]) {
		this.id = id;
		this.location = location;
		this.signals = signals;
	}

	signalFor(access_point: AccessPoint | null): number | null {
		return this.mapValue(s => s.strength, access_point);
	}

	snrFor(access_point: AccessPoint | null): number | null {
		return this.mapValue(s => s.snr(this.signals), access_point);
	}

	private mapValue(mapper: (signal: Signal) => number, access_point: AccessPoint | null): number | null {
		if (access_point == null)
			return null;

		const values = this.signals
			.filter(signal => (signal.ssid == access_point.ssid)
				&& (access_point.frequency == null || signal.frequency == access_point.frequency)
				&& (access_point.mac == null || signal.mac == access_point.mac))
			.map(mapper);

		return values.length > 0 ? Math.max(...values) : null;
	}
}