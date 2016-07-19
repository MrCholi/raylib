#version 330

// Input vertex attributes (from vertex shader)
in vec2 fragTexCoord;
in vec4 fragColor;

// Input uniform values
uniform sampler2D texture0;
uniform vec4 fragTintColor;

// Output fragment color
out vec4 finalColor;

// NOTE: Add here your custom variables

const float renderWidth = 800.0;      // HARDCODED for example!
const float renderHeight = 480.0;     // Use uniforms instead...

float radius = 250.0;
float angle = 0.8;

uniform vec2 center = vec2(200.0, 200.0);

void main()
{
    vec2 texSize = vec2(renderWidth, renderHeight);
    vec2 tc = fragTexCoord*texSize;
    tc -= center;
    
    float dist = length(tc);

    if (dist < radius) 
    {
        float percent = (radius - dist)/radius;
        float theta = percent*percent*angle*8.0;
        float s = sin(theta);
        float c = cos(theta);
        
        tc = vec2(dot(tc, vec2(c, -s)), dot(tc, vec2(s, c)));
    }

    tc += center;
    vec4 color = texture2D(texture0, tc/texSize)*colDiffuse*fragColor;;

    finalColor = vec4(color.rgb, 1.0);;
}