sampler TextureSampler : register(s0);

#define INV_SQRT_2PI_X3 1.1968268412042980338198381798031

float4 PixelShaderFunction(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{ 
	float4 Color;
	Color = tex2D(TextureSampler, texCoord.xy);
	int n = 3;
	for (int i = 1; i < n + 1; i++)
	{
		float2 up = float2(0.007 * i, 0);
		float2 right = float2(0, 0.007 * i);
		Color += (tex2D(TextureSampler, texCoord.xy + up) +
				  tex2D(TextureSampler, texCoord.xy - up) +
				  tex2D(TextureSampler, texCoord.xy + right) +
			      tex2D(TextureSampler, texCoord.xy - right)) / 4;
	}
	Color = Color / (n + 1);
	
	return Color;
}

technique Desaturate
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}