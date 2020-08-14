import { Test, TestSuite } from "xunit.ts";
import Renderer from "../WiFiHeatMap.App/Renderer";
import Mockito from 'ts-mockito';
import Reading from "../WiFiHeatMap.App/Reading";
import Point from "../WiFiHeatMap.App/Point";
import AccessPoint from "../WiFiHeatMap.App/AccessPoint";
import MockFactory from "./MockFactory";

export default class RendererTests extends TestSuite
{
    @Test()
    async renderDrawsTriangles()
    {
        //arrange
        const gl = MockFactory.WebGL2RenderingContext();
        const canvas = Mockito.mock<HTMLCanvasElement>();
        Mockito.when(canvas.getContext('webgl2')).thenReturn(Mockito.instance(gl));

        const renderer = new Renderer(Mockito.instance(canvas));

        //act
        const readings = [new Reading(1, new Point(2, 3), [])];
        const access_point = new AccessPoint('test');
        renderer.render(readings, access_point);

        //assert
        Mockito.verify(gl.drawArrays(Mockito.anything(), Mockito.anything(), Mockito.anything()));
    }
}