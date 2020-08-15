import Delaunator from 'delaunator';
import ColorConverter from "./ColorConverter";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";
import Point from './Point';

export default class Triangulation {
    private readonly readings: Reading[];
    private readonly output: Delaunator<Point>;

    constructor(readings: Reading[]) {
        this.readings = readings;

        const points = readings.map(reading => reading.location);
        this.output = Delaunator.from(points, p => p.x, p => p.y);
    }
    getCoords(): number[] {
        const coords: number[] = [];
        this.output.triangles.forEach(index => {
            coords.push(this.readings[index].location.x);
            coords.push(this.readings[index].location.y);
        });
        return coords;
    }

    getColors(access_point: AccessPoint): number[] {
        const colors: number[] = [];
        this.output.triangles.forEach(index => {
            const reading = this.readings[index];
            const signal = reading.signalFor(access_point);
            const color = ColorConverter.toColor(signal);
            colors.push(color.red);
            colors.push(color.green);
            colors.push(color.blue);
            colors.push(color.alpha);
        });
        return colors;
    }
}