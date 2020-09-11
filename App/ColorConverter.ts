import Color from "./Color";

export default class ColorConverter {
    private static stops: number[] = [-20, -40, -60, -80, -100];

    static toColor(dBm: number | null): Color {
        return dBm != null
            ? new Color(this.base(dBm, 1), this.base(dBm, 0), 0)
            : new Color(0, 0, 0, 0);
    }

    private static base(dbm: number, offset: number): number {
        return dbm > this.stops[0 + offset] ? 0
            : dbm > this.stops[1 + offset] ? Math.abs(dbm - this.stops[0 + offset]) * 12.75
                : dbm > this.stops[2 + offset] ? 255
                    : dbm > this.stops[3 + offset] ? Math.abs(dbm - this.stops[3 + offset]) * 12.75
                        : 0;
    }
}