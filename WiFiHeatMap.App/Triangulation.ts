import Delaunator from 'delaunator';
import ColorConverter from "./ColorConverter";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";
import Point from './Point';

export default class Triangulation {
    readonly vertex_coordinates: number[] = [];
    readonly vertex_color_parts: number[] = [];

    constructor(readings: Reading[], access_point: AccessPoint) {
        const points = readings.map(reading => reading.location);
        const delauney = Delaunator.from(points, p => p.x, p => p.y);

        delauney.triangles.forEach(index => {
            const reading = readings[index];
            this.vertex_coordinates.push(reading.location.x);
            this.vertex_coordinates.push(reading.location.y);

            const signal = reading.signalFor(access_point);
            const color = ColorConverter.toColor(signal);
            this.vertex_color_parts.push(color.red);
            this.vertex_color_parts.push(color.green);
            this.vertex_color_parts.push(color.blue);
            this.vertex_color_parts.push(color.alpha);
        });
    }
}