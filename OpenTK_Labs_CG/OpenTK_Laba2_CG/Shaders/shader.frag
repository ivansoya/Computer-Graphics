#version 330

in vec3 color;

out vec4 fragment;

void main(void)
{
    fragment = vec4(color, 1);
}