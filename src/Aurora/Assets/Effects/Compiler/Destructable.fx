sampler uImage0 : register(s0);

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
	float4 color = tex2D(uImage0, coords);

	return color;
}

technique Technique1 {
	pass DestructablePass {
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}
