#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  

struct Light {
    vec3 lightPos; 
    vec3 lightColor;
};

uniform vec3 viewPos;
uniform vec3 objectColor;

uniform Light redLight;
uniform Light greenLight;
uniform Light blueLight;

vec3 CalLight(Light light);

void main()
{   
    vec3 result = CalLight(redLight) + CalLight(greenLight) + CalLight(blueLight);
    result = result * objectColor;
    FragColor = vec4(result, 1.0);
} 

vec3 CalLight(Light light)
{
    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * light.lightColor;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * light.lightColor;
    
    // specular
    float specularStrength = 0.1;
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * light.lightColor;  
        
    return ambient + diffuse + specular;
}