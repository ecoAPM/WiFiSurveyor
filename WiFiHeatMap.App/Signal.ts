import ColorConverter from "./ColorConverter";
import Color from "./Color";

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

    color(): Color {
        return ColorConverter.toColor(this.strength);
    }
}