import { Test, TestSuite } from "xunit.ts";
import Renderer from "../App/Renderer";
import Mockito from "ts-mockito";
import Reading from "../App/Reading";
import Point from "../App/Point";
import AccessPoint from "../App/AccessPoint";
import MockFactory from "./MockFactory";

export default class RendererTests extends TestSuite {
	@Test()
	async renderDrawsTrianglesWithCorrectNumberOfVertices() {
		//arrange
		const gl = MockFactory.webGL2RenderingContext();
		const canvas = Mockito.mock<HTMLCanvasElement>();
		Mockito.when(canvas.getContext("webgl2")).thenReturn(Mockito.instance(gl));

		const renderer = new Renderer(Mockito.instance(canvas));

		//act
		const readings = [
			new Reading(1, new Point(2, 1), []),
			new Reading(1, new Point(4, 2), []),
			new Reading(1, new Point(3, 3), []),
			new Reading(1, new Point(4, 4), [])
		];
		const access_point = new AccessPoint("test");
		renderer.render(readings, access_point);

		//assert
		Mockito.verify(gl.drawArrays(Mockito.anything(), Mockito.anything(), 6)).once();
	}

	@Test()
	async canClearCanvas() {
		//arrange
		const gl = MockFactory.webGL2RenderingContext();
		const canvas = Mockito.mock<HTMLCanvasElement>();
		Mockito.when(canvas.getContext("webgl2")).thenReturn(Mockito.instance(gl));

		const renderer = new Renderer(Mockito.instance(canvas));

		//act
		renderer.clear();

		//assert
		Mockito.verify(gl.clear(Mockito.anything())).once();
	}
}