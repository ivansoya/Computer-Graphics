#version 400 core
in vec4 FragmentPosition;

uniform vec3 lightPosition;
uniform float farPlane;

void main()
{
    float lightDistance = length(FragmentPosition.xyz - lightPosition);
    
    // map to [0;1] range by dividing by farPlane
    lightDistance /= farPlane;
    
    // write this as modified depth
    gl_FragDepth = lightDistance;
}