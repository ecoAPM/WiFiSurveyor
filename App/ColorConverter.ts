import Color from "./Color";

export default class ColorConverter {
	private static stops: number[] = [-20, -40, -60, -80, -100];

	static toColor(dBm: number | null): Color {
		return dBm != null
			? new Color(this.base(dBm, 1), this.base(dBm, 0), 0)
			: new Color(0, 0, 0, 0);
	}

	private static base(dbm: number, offset: number): number {
		if (dbm > this.stops[0 + offset]) {
			return 0;
		}

		if (dbm > this.stops[1 + offset]) {
			return Math.abs(dbm - this.stops[0 + offset]) * 255 / 20;
		}

		if (dbm > this.stops[2 + offset]) {
			return 255;
		}

		if (dbm > this.stops[3 + offset]) {
			return Math.abs(dbm - this.stops[3 + offset]) * 255 / 20;
		}

		return 0;
	}
}