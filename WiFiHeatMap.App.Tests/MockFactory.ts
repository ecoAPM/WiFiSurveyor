import Mockito from 'ts-mockito';

export default class MockFactory {
    static WebGL2RenderingContext(): WebGL2RenderingContext {
        const shader = Mockito.mock<WebGLShader>();
        const program = Mockito.mock<WebGLProgram>();
        
        const gl = Mockito.mock<WebGL2RenderingContext>();
        Mockito.when(gl.createShader(Mockito.anything())).thenReturn(Mockito.instance(shader));
        Mockito.when(gl.createProgram()).thenReturn(Mockito.instance(program));
        
        return gl;
    }
}