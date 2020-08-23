export default class Signal {
    readonly mac: string;
    readonly ssid: string;
    readonly frequency: number;
    readonly strength: number;

    constructor(mac: string, ssid: string, frequency: number, strength: number) {
        this.mac = mac;
        this.ssid = ssid || '[hidden]';
        this.frequency = frequency;
        this.strength = strength;
    }
}