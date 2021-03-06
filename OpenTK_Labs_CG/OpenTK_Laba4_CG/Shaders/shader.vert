﻿#version 400 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexture;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform bool isReversed;

out vec3 Normal;
out vec3 FragmentPosition;
out vec2 TexCoords;


void main()
{
    FragmentPosition = vec3(vec4(aPosition, 1.0) * model);
    Normal = aNormal * transpose(inverse(mat3(model)));
    if (isReversed == true)
        Normal *= -1;
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
    TexCoords = aTexture;
}