﻿#version 400 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D hdrBuffer;
uniform sampler2D bloom;
//uniform float exposure;

void main()
{             
    //const float gamma = 2.2;
    vec3 hdrColor = texture(hdrBuffer, TexCoords).rgb;
    vec3 bloomColor = texture(bloom, TexCoords).rgb;
    hdrColor += bloomColor;
    // exposure
    //vec3 result = vec3(1.0) - exp(-hdrColor * exposure);
    // also gamma correct while we're at it       
    //result = pow(result, vec3(1.0 / gamma));
    FragColor = vec4(hdrColor, 1.0);
}