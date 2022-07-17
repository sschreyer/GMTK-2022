// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// https://github.com/Elringus/BlendModeExample/blob/master/Assets/Shaders/GrabDarken.shader
Shader "Custom/Overlay"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
        _Opacity ("Opacity", float) = 1
	}

	SubShader
	{
		Tags
		{ 
			"Queue" = "Transparent" 
			"RenderType" = "Transparent" 	
		}
		
		Blend SrcAlpha OneMinusSrcAlpha

		GrabPass { }

		Pass
		{
			CGPROGRAM
			
			#include "UnityCG.cginc"
			
			#pragma vertex ComputeVertex
			#pragma fragment ComputeFragment
			
			sampler2D _MainTex;
			sampler2D _GrabTexture;
			fixed4 _Color;
			float _Opacity;
			
			struct VertexInput
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct VertexOutput
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
			};
			
			VertexOutput ComputeVertex (VertexInput vertexInput)
			{
				VertexOutput vertexOutput;
				
				vertexOutput.vertex = UnityObjectToClipPos(vertexInput.vertex);
				vertexOutput.screenPos = vertexOutput.vertex;	
				vertexOutput.texcoord = vertexInput.texcoord;
				vertexOutput.color = vertexInput.color * _Color;
							
				return vertexOutput;
			}
			
			fixed4 Overlay (fixed4 target, fixed4 blend)
			{ 
                fixed4 r = (target > 0.5) * (1 - (1-2*(target-0.5))*(1-blend)) + (target <= 0.5) * ((2*target)*blend);
				r.a = blend.a;
				return r;
			} 

			fixed4 ComputeFragment (VertexOutput vertexOutput) : SV_Target
			{
				half4 color = tex2D(_MainTex, vertexOutput.texcoord) * vertexOutput.color;
                color.a = _Opacity * color.a;
				
				float2 grabTexcoord = vertexOutput.screenPos.xy / vertexOutput.screenPos.w; 
				grabTexcoord.x = (grabTexcoord.x + 1.0) * .5;
				grabTexcoord.y = (grabTexcoord.y + 1.0) * .5; 
				#if UNITY_UV_STARTS_AT_TOP
				grabTexcoord.y = 1.0 - grabTexcoord.y;
				#endif
				
				fixed4 grabColor = tex2D(_GrabTexture, grabTexcoord);

				return Overlay(grabColor, color);
			}
			
			ENDCG
		}
	}

	Fallback "UI/Default"
}