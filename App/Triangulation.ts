import Delaunator from "delaunator";
import ColorConverter from "./ColorConverter";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";
import { Mode } from "./Mode";

export default class Triangulation {
	readonly vertex_coordinates: number[] = [];
	readonly vertex_color_parts: number[] = [];

	constructor(mode: Mode, readings: Reading[], access_point: AccessPoint | null) {
		const points = readings.map(reading => reading.location);
		const delauney = Delaunator.from(points, p => p.x, p => p.y);

		delauney.triangles.forEach(index => {
			const reading = readings[index];
			this.vertex_coordinates.push(reading.location.x);
			this.vertex_coordinates.push(reading.location.y);

			const signal = mode == Mode.Signal
				? reading.signalFor(access_point)
				: reading.snrFor(access_point);

			const color = mode == Mode.Signal
				? ColorConverter.fromSignal(signal)
				: ColorConverter.fromSNR(signal);

			this.vertex_color_parts.push(color.red);
			this.vertex_color_parts.push(color.green);
			this.vertex_color_parts.push(color.blue);
			this.vertex_color_parts.push((color.alpha > 0 ? color.alpha + 1 : color.alpha) * 0.75 - (color.alpha > 0 ? 1 : 0));
		});
	}
}