import Color from "./Color";

export default class ColorConverter {
	private static signalStops: number[] = [ -20, -40, -60, -80, -100 ];
	private static snrStops: number[] = [ 90, 50, 20, 0, -10 ];

	static fromSignal(dBm: number | null): Color {
		return dBm != null
			? new Color(this.base(dBm, 1, this.signalStops), this.base(dBm, 0, this.signalStops), 0)
			: new Color(0, 0, 0, 127);
	}

	static fromSNR(dB: number | null): Color {
		return dB != null
			? new Color(this.base(dB, 1, this.snrStops), this.base(dB, 0, this.snrStops), 0)
			: new Color(0, 0, 0, 127);
	}

	private static base(value: number, offset: number, stops: number[]): number {
		if (value > stops[offset]) {
			return 0;
		}

		if (value > stops[1 + offset]) {
			return Math.abs(value - stops[offset]) * 255 / (stops[offset] - stops[1 + offset]);
		}

		if (value > stops[2 + offset]) {
			return 255;
		}

		if (value > stops[3 + offset]) {
			return Math.abs(value - stops[3 + offset]) * 255 / (stops[2 + offset] - stops[3 + offset]);
		}

		return 0;
	}
}