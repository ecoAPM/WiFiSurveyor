import Point from "./Point";
import Signal from "./Signal";

export default class Reading {
    readonly id: number;
    readonly location: Point;
    readonly signals: Signal[];

    constructor(id: number, location: Point, signals: Signal[]) {
        this.id = id;
        this.location = location;
        this.signals = signals;
    }

    signalFor(ssid: string, frequency: number|null = null, mac: string|null = null): number|null {
        const signals = this.signals
            .filter((signal) => (signal.ssid == ssid)
                && (frequency == null || signal.frequency == frequency)
                && (mac == null || signal.mac == mac))
            .map((signal) => signal.strength);
        return signals.length > 0 ? Math.max(...signals) : null;
    }
}