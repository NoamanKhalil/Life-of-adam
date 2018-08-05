
Shader "Unlit/Outline" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_OutlineSize("Outline Size", Float) = 1.5
	}
	SubShader
		{
			Tags{ "RenderType" = "Opaque" }
			LOD 100




			Pass
		   {

		   Cull Front
		   CGPROGRAM
		   #pragma vertex vert
		   #pragma fragment frag
		   #include "UnityCG.cginc"


			float _OutlineSize;
			fixed4 _OutlineColor;

				struct v2f
				{
					float4 pos : SV_POSITION;
				};

				v2f vert(appdata_base v)
				{
					v2f o;
					v.vertex.xyz *= _OutlineSize;
					v.normal *= -1;
					o.pos = UnityObjectToClipPos(v.vertex);
					return o;
				}

				half4 frag(v2f i) : SV_Target
				{
					return _OutlineColor;
				}

				ENDCG
		   }

			Cull Back

			CGPROGRAM
					// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

					// Use shader model 3.0 target, to get nicer looking lighting
			//#pragma target 3.0

					sampler2D _MainTex;

					struct Input
					{
						float2 uv_MainTex;
					};


				fixed4 _Color;

				void surf(Input IN, inout SurfaceOutputStandard o)
				{
					fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					o.Albedo = c.rgb;
					o.Alpha = c.a;
				}
				ENDCG
		}
		//FallBack "Diffuse"
}