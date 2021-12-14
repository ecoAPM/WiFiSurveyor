import Mockito from "ts-mockito";
import SignalService from "../App/SignalService";
import ImageLoader from "../App/ImageLoader";

export default class MockFactory {
	static signalService(): SignalService {
		const signal_service = Mockito.mock<SignalService>();
		Mockito.when(signal_service.status).thenReturn("");
		Mockito.when(signal_service.last_updated).thenReturn("");
		return signal_service;
	}

	static webGL2RenderingContext(): WebGL2RenderingContext {
		const shader = Mockito.mock<WebGLShader>();
		const program = Mockito.mock<WebGLProgram>();

		const gl = Mockito.mock<WebGL2RenderingContext>();
		Mockito.when(gl.createShader(Mockito.anything())).thenReturn(Mockito.instance(shader));
		Mockito.when(gl.createProgram()).thenReturn(Mockito.instance(program));

		return gl;
	}

	static canvas(): HTMLCanvasElement {
		const gl = MockFactory.webGL2RenderingContext();

		const canvas = Mockito.mock<HTMLCanvasElement>();
		Mockito.when(canvas.getContext("webgl2")).thenReturn(Mockito.instance(gl));

		return canvas;
	}

	static imageLoader(): ImageLoader {
		return Mockito.mock<ImageLoader>();
	}
}