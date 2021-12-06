import { Test, TestSuite } from "xunit.ts";
import Reading from "../App/Reading";
import Point from "../App/Point";
import Triangulation from "../App/Triangulation";
import Signal from "../App/Signal";
import AccessPoint from "../App/AccessPoint";

export default class TriangulationTests extends TestSuite {
	@Test()
	async canConvertReadingsToVertexCoordinates() {
		//arrange
		const readings = [
			new Reading(1, new Point(2, 1), []),
			new Reading(2, new Point(4, 2), []),
			new Reading(3, new Point(3, 3), [])
		];

		//act
		const triangulation = new Triangulation(readings, new AccessPoint("test"));

		//assert
		this.assert.equal([ 4, 2, 2, 1, 3, 3 ], triangulation.vertex_coordinates);
	}

	@Test()
	async canConvertReadingsToColorParts() {
		//arrange
		const readings = [
			new Reading(1, new Point(2, 1), [ new Signal("12:34:56", "test", 2, -40) ]),
			new Reading(2, new Point(4, 2), [ new Signal("12:34:56", "test", 2, -60) ]),
			new Reading(3, new Point(3, 3), [ new Signal("12:34:56", "test", 2, -80) ])
		];
		const access_point = new AccessPoint("test");

		//act
		const triangulation = new Triangulation(readings, access_point);

		//assert
		this.assert.equal([ 255, 255, 0, 191, 0, 255, 0, 191, 255, 0, 0, 191 ], triangulation.vertex_color_parts);
	}
}