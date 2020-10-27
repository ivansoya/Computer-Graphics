#version 400 core

#define MAX_FLOAT 3.402823466e+38
#define MAX_SIZE 10

struct Material {
    sampler2D diffuseTexture;
    sampler2D specularTexture;

    float shininess; 
};

struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    vec3 color;

    float constant;
    float linear;
    float quadratic;

    float farPlane;
    float intensive;
};

uniform int lightNumber;

uniform Light light[MAX_SIZE];
uniform Material material;

uniform samplerCube shadowMap[MAX_SIZE];
uniform vec3 viewPos;

layout (location = 0) out vec4 FragColor;
layout (location = 1) out vec4 BrightColor;

in vec3 Normal;
in vec3 FragmentPosition;
in vec2 TexCoords;

vec3 gridSamplingDisk[20] = vec3[]
(
   vec3(1, 1,  1), vec3( 1, -1,  1), vec3(-1, -1,  1), vec3(-1, 1,  1), 
   vec3(1, 1, -1), vec3( 1, -1, -1), vec3(-1, -1, -1), vec3(-1, 1, -1),
   vec3(1, 1,  0), vec3( 1, -1,  0), vec3(-1, -1,  0), vec3(-1, 1,  0),
   vec3(1, 0,  1), vec3(-1,  0,  1), vec3( 1,  0, -1), vec3(-1, 0, -1),
   vec3(0, 1,  1), vec3( 0, -1,  1), vec3( 0, -1, -1), vec3( 0, 1, -1)
);

float AttunetionCalculation(Light light, vec3 fragPos)
{
    float distance    = length(light.position - fragPos);
    float attunetion = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

    return attunetion;
}

float ShadowCalculation(Light light, vec3 fragPos, int j)
{
    vec3 fragToLight = fragPos - light.position;
    float currentDepth = length(fragToLight);
    float shadow = 0.0;
    float bias = 0.2;
    int samples = 20;
    float viewDistance = length(viewPos - fragPos);
    float diskRadius = (1.0 + (viewDistance / light.farPlane)) / 25.0;
    float offset = light.farPlane * (1 / 5);
    float koeff = pow(currentDepth - offset, light.intensive) / pow(light.farPlane - offset, light.intensive);
    if (currentDepth - bias >= light.farPlane)
        return 0.0;
    for(int i = 0; i < samples; i++)
    {
        float closestDepth = texture(shadowMap[j], fragToLight + gridSamplingDisk[i] * diskRadius).r;
        closestDepth *= light.farPlane;   // undo mapping [0;1]
        if(currentDepth - bias > closestDepth)
            shadow += 1.0;
    }
    shadow /= float(samples);
    shadow *= 1.0 - koeff;    
    return shadow;
}

vec3 AmbientCalculation(Light light)
{
    vec3 ambient = light.ambient * light.color * vec3(texture(material.diffuseTexture, TexCoords));

    return ambient;
}

vec3 LightCalculation(Light light, vec3 fragPos, vec3 normal)
{
    vec3 lightDir = normalize(light.position - fragPos);

    //diffuse
    float diff = max(dot(normal, lightDir), 0.0);
    vec3 diffuse = light.diffuse * light.color * (diff * vec3(texture(material.diffuseTexture, TexCoords)));

    //specular
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 reflectDir = reflect(-lightDir, normal);
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    float spec = pow(max(dot(normal, halfwayDir), 0.0), material.shininess);
    //float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * light.color * (spec * vec3(texture(material.specularTexture, TexCoords)));

    float attenuation = AttunetionCalculation(light, fragPos);

    diffuse  *= attenuation;
    specular *= attenuation;

    return (diffuse + specular);
}

void main()
{
    vec3 ambient = vec3(0.0);
    vec3 lighting = vec3(0.0);
    float shadow = 0.0;
    vec3 normal = normalize(Normal);
    for (int i = 0; i < lightNumber; i++) {
        ambient += AmbientCalculation(light[i]);
        lighting += LightCalculation(light[i], FragmentPosition, normal);
        shadow += ShadowCalculation(light[i], FragmentPosition, i);
    }
    lighting *= (1.0 - shadow);
    vec3 result = ambient + lighting;
    FragColor = vec4(result, 1.0);
    float brightness = dot(FragColor.rgb, vec3(0.2126, 0.7152, 0.0722));
    if(brightness > 1.0)
        BrightColor = vec4(FragColor.rgb, 1.0);
    else
        BrightColor = vec4(0.0, 0.0, 0.0, 1.0);
}