import ColorConverter from "./ColorConverter";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";

export default class Triangulation {
    static getCoords(readings: Reading[]): number[] {
        const coords: number[] = [];
        readings.forEach(reading => {
            coords.push(reading.location.x);
            coords.push(reading.location.y);
        });
        return coords;
    }

    static getColors(readings: Reading[], access_point: AccessPoint): number[] {
        const colors: number[] = [];
        readings.forEach(reading => {
            const color = ColorConverter.toColor(reading.signalFor(access_point));
            colors.push(color.red);
            colors.push(color.green);
            colors.push(color.blue);
            colors.push(color.alpha);
        });
        return colors;
    }
}