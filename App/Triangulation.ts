import Delaunator from "delaunator";
import ColorConverter from "./ColorConverter";
import Reading from "./Reading";
import AccessPoint from "./AccessPoint";
import {Mode} from "./Mode";

export default class Triangulation {
	readonly vertex_coordinates: number[] = [];
	readonly vertex_color_parts: number[] = [];

	constructor(mode: Mode, readings: Reading[], access_point: AccessPoint | null) {
		const points = readings.map(reading => reading.location);
		const delauney = Delaunator.from(points, p => p.x, p => p.y);

		delauney.triangles.forEach(index => {
			const reading = readings[index];

			const signal = mode == Mode.Signal
				? reading.signalFor(access_point)
				: reading.snrFor(access_point);

			const color = mode == Mode.Signal
				? ColorConverter.fromSignal(signal)
				: ColorConverter.fromSNR(signal);

			this.vertex_coordinates.push(
				reading.location.x,
				reading.location.y
			);

			const alpha = (color.alpha > 0 ? color.alpha + 1 : color.alpha) * 0.75 - (color.alpha > 0 ? 1 : 0);
			this.vertex_color_parts.push(
				color.red,
				color.green,
				color.blue,
				alpha
			);
		});
	}
}