import { Test, TestSuite } from "xunit.ts"
import RenderFactory from "../WiFiHeatMap.App/RenderFactory";
import Mockito from 'ts-mockito';
import MockFactory from "./MockFactory";

export default class RenderFactoryTests extends TestSuite {
    @Test()
    async canGetContextFromCanvas() {
        //arrange
        const context = Mockito.mock<WebGL2RenderingContext>();
        const instance = Mockito.instance(context);

        const canvas = Mockito.mock<HTMLCanvasElement>();
        Mockito.when(canvas.getContext('webgl2')).thenReturn(instance);

        //act
        const gl = RenderFactory.getContext(Mockito.instance(canvas));

        //assert
        this.assert.equal(instance, gl);
    }

    @Test()
    async shaderProgramIsCompiled() {
        //arrange
        const gl = MockFactory.webGL2RenderingContext();

        //act
        const shader_program = RenderFactory.getShaderProgram(Mockito.instance(gl));

        //assert
        Mockito.verify(gl.linkProgram(shader_program)).once();
        Mockito.verify(gl.useProgram(shader_program)).once();
    }
}