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

float4 FloatingUnderTownEffect(float2 coords : TEXCOORD0) : COLOR0
{
	float pi = 3.14f;
	float4 colour = tex2D(uImage0, coords);
	if (colour.r == colour.g && colour.r == colour.b) {
		float speed = 0.5;
		colour.r *= 0.5 + (0.5 * sin(speed * uTime));
		colour.g *= 0.5 + (0.5 * sin((speed * uTime) - (2 * pi/3)));
		colour.b *= 0.5 + (0.5 * sin((speed * uTime) - (4 * pi / 3)));
	}
	return colour;
}

technique Technique1
{
	pass FloatingUnderTownEffect
	{
		PixelShader = compile ps_2_0 FloatingUnderTownEffect();
	}
}