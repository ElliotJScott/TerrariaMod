sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4 AmpedScreenShader(float2 coords : TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(uImage0, coords);
    float relX = 0.5f - coords.x;
	float relY = 0.5f - coords.y;
	float len = pow(relX, 2) + pow(relY, 2);
	float ext = 0.414 + uProgress + (0.1f * sin(uTime));
    if (len < ext){}
	else
	{
		float diff = 1 - min(1, len-ext);
		colour.rgba *= diff;
	}
    return colour;
}

technique Technique1
{
    pass AmpedScreenShader
    {
        PixelShader = compile ps_2_0 AmpedScreenShader();
    }
}