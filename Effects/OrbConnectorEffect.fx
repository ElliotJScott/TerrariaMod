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

float4 OrbConnectorEffect(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(uImage0, coords);
	float pos = coords.x;
	float waveSpeed = 8;
	float waveNumber = 3;
	float modifier = sin((waveSpeed * uTime) - (waveNumber * pos)) * 0.4f + 0.6f;
	colour.a *= modifier;
	//colour.a *= factor;
	return colour;

}

technique Technique1
{
	pass OrbConnectorEffect
	{
		PixelShader = compile ps_2_0 OrbConnectorEffect();
	}
}