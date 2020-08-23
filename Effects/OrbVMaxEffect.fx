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

float4 OrbVMaxEffect(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(uImage0, coords);
	colour.g = 0;
	float relX = 0.5f - coords.x;
	float relY = 0.5f - coords.y;
	float len = pow(relX, 2) + pow(relY, 2);
	colour.b *= len * 2;
	float angle = atan(relY / relX) + (len*3.6f);
	float waveSpeed = 8;
	float waveNumber = 3;
	float modifier = sin((waveSpeed * uTime) - (waveNumber * angle)) * 0.5f + 0.5f;
	if (relX < 0) {
		modifier = 1 - modifier;
	}
	colour.a *= modifier;
	//colour.a *= factor;
	return colour;

}

technique Technique1
{
	pass OrbVMaxEffect
	{
		PixelShader = compile ps_2_0 OrbVMaxEffect();
	}
}