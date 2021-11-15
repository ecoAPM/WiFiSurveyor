import Shaders from './shaders';

export default class RenderFactory {
    static getContext(canvas: HTMLCanvasElement): WebGL2RenderingContext {
        const gl = canvas.getContext("webgl2");
        if (gl == null)
            throw new Error("WebGL2 not supported");

        gl.enable(gl.BLEND);
        gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
        gl.clearColor(1, 1, 1, 0);

        return gl;
    }

    static getShaderProgram(gl: WebGL2RenderingContext): WebGLProgram {
        const shader_program = gl.createProgram();
        if (shader_program == null)
            throw new Error("Could not create shader program");

        const vertex_shader = RenderFactory.getVertexShader(gl);
        gl.attachShader(shader_program, vertex_shader);

        const fragment_shader = RenderFactory.getFragmentShader(gl);
        gl.attachShader(shader_program, fragment_shader);

        gl.linkProgram(shader_program);
        gl.useProgram(shader_program);

        return shader_program;
    }

    private static getVertexShader(gl: WebGL2RenderingContext): WebGLShader {
        const vertex_shader = gl.createShader(gl.VERTEX_SHADER);
        if (vertex_shader == null)
            throw new Error("Could not create vertex shader");

        gl.shaderSource(vertex_shader, Shaders.vertex);
        gl.compileShader(vertex_shader);

        return vertex_shader;
    }

    private static getFragmentShader(gl: WebGL2RenderingContext): WebGLShader {
        const fragment_shader = gl.createShader(gl.FRAGMENT_SHADER);
        if (fragment_shader == null)
            throw new Error("Could not create fragment shader");

        gl.shaderSource(fragment_shader, Shaders.fragment);
        gl.compileShader(fragment_shader);

        return fragment_shader;
    }
}