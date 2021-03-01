export default class Shaders {
    static fragment: string = `
precision highp float;

varying vec4 v_color;

void main()
{
    gl_FragColor = v_color / vec4(255);
}
`;

    static vertex: string = `
precision highp float;

uniform vec2 u_offset;
attribute vec2 a_coords;
attribute vec4 a_color;
varying vec4 v_color;

void main()
{
    v_color = a_color;

    mat4 scale_and_translate = mat4
    (
        2.0 / u_offset.x, 0, 0, -1.0,
        0, -2.0 / u_offset.y, 0, 1.0,
        0, 0, 1.0, 0,
        0, 0, 0, 1.0
    );

    gl_Position = vec4(a_coords, 0, 1) * scale_and_translate;
}
`;
}