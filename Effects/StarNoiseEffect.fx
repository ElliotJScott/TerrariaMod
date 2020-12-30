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

float4 StarNoiseEffect(float2 coords : TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(uImage0, coords);
	if (colour.a == 1) {
		float m1 = sin((-0.15f * coords.x) - (7 * uTime));
		float m2 = sin((0.3f * coords.x) + (-0.4f * coords.y) - (2 * uTime));
		float m3 = sin((0.15f * coords.x) + (0.22f * coords.y) - (1.2f * uTime));
		float m4 = sin((-0.073f * coords.x) + (-0.11f * coords.y) - (0.3f * uTime));
		//float m5 = sin((4 * coords.x) + (1.3f * coords.y) - (0.44f * uTime));
		float factor = (m1 + m2 + m3 + m4) / 4.0f;
		colour.gb *= factor;
	}
    return colour;
}

technique Technique1
{
    pass StarNoiseEffect
    {
        PixelShader = compile ps_2_0 StarNoiseEffect();
    }
}