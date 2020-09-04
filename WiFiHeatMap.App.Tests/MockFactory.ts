import Mockito from 'ts-mockito';

export default class MockFactory {
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
        Mockito.when(canvas.getContext('webgl2')).thenReturn(Mockito.instance(gl));

        return canvas;
    }
}