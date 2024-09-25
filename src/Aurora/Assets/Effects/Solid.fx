sampler uImage0 : register(s0);

float uIntensity;
float3 uColor;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
	float4 color = tex2D(uImage0, coords);

	if (!any(color)) {
		return color;
	}

	return lerp(color, float4(uColor, color.a), uIntensity);
}

technique Technique1 {
	pass SolidPass {
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}
