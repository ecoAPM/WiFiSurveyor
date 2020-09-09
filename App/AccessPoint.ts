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
            + (this.frequency !== null ? ` @ ${this.frequency} GHz` : '')
            + (this.mac !== null ? ` (${this.mac})` : '');
    }
}