export default class AccessPoint {
    readonly ssid: string;
    readonly frequency: number | null;
    readonly mac: string | null;

    constructor(ssid: string | null, frequency: number | null = null, mac: string | null = null) {
        this.ssid = ssid ?? '[hidden]';
        this.frequency = frequency;
        this.mac = mac;
    }

    label() {
        return this.ssid
            + (this.frequency !== null ? ` @ ${this.frequency} GHz` : '')
            + (this.mac !== null ? ` (${this.mac})` : '');
    }
}