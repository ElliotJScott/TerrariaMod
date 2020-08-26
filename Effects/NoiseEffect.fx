sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 NoiseEffect(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(uImage0, coords);
	float rand1 = 0.5f * (1 + sin(218 / (coords.x + 0.15 + (0.05 * sin(uTime)))));
	float rand2 = 0.5f * (1 + cos(173 / (coords.y + 0.2 + (0.1 * sin(uTime)))));
	float rand3 = sqrt(pow(rand1, 2) + pow(rand2, 2));
	colour.rgb *= rand3;
	return colour;
}

technique Technique1
{
	pass NoiseEffect
	{
		PixelShader = compile ps_2_0 NoiseEffect();
	}
}