import { Test, TestSuite } from "xunit.ts";
import Reading from "../WiFiHeatMap.App/Reading";
import Point from "../WiFiHeatMap.App/Point";
import Triangulation from "../WiFiHeatMap.App/Triangulation";
import Signal from "../WiFiHeatMap.App/Signal";
import AccessPoint from "../WiFiHeatMap.App/AccessPoint";

export default class TriangulationTests extends TestSuite {
    @Test()
    async canConvertReadingsToVertices() {
        //arrange
        const readings = [
            new Reading(1, new Point(2, 1), []),
            new Reading(2, new Point(4, 2), []),
            new Reading(3, new Point(3, 3), [])
        ];
        const triangulation = new Triangulation(readings);

        //act
        const vertices = triangulation.getCoords();

        //assert
        this.assert.equal([4, 2, 2, 1, 3, 3], vertices);
    }

    @Test()
    async canConvertReadingsToColors() {
        //arrange
        const readings = [
            new Reading(1, new Point(2, 1), [new Signal('12:34:56', 'test', 2, -32)]),
            new Reading(2, new Point(4, 2), [new Signal('12:34:56', 'test', 2, -64)]),
            new Reading(3, new Point(3, 3), [new Signal('12:34:56', 'test', 2, -80)])
        ];
        const triangulation = new Triangulation(readings);

        const access_point = new AccessPoint('test');

        //act
        const colors = triangulation.getColors(access_point);

        //assert
        this.assert.equal([255, 255, 0, 255, 0, 255, 0, 255, 255, 0, 0, 255], colors);
    }
}