sampler TextureSampler : register(s0);

float4 PixelShaderFunction(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{ 
	float4 Color;
	Color = tex2D(TextureSampler, texCoord.xy);
	float min = Color.x;
	float av = Color.y;
	float max = Color.z;
	if (Color.y < min)
		min = Color.y;
	if (Color.z < min)
		min = Color.z;
	if (Color.x > max)
		max = Color.x;
	if (Color.y > max)
		max = Color.y;
	if (Color.x != max && Color.x != min)
		av = Color.x;
	if (Color.z != max && Color.z != min)
		av = Color.z;
	Color.x = av;
	Color.y = av;
	Color.z = av;
	
	return Color;
}

technique Desaturate
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}