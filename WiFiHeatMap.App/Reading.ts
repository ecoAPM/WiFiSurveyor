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

    signalFor(access_point: AccessPoint): number | null {
        if (access_point == null)
            return null;

        const signals = this.signals
            .filter((signal) => (signal.ssid == access_point.ssid)
                && (access_point.frequency == null || signal.frequency == access_point.frequency)
                && (access_point.mac == null || signal.mac == access_point.mac))
            .map((signal) => signal.strength);

        return signals.length > 0 ? Math.max(...signals) : null;
    }
}