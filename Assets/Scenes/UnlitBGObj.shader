// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "flyugel/UnlitBGObj"
{
	Properties
	{
		// _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Color2("ColorObj", Color) = (1.0, 1.0, 1.0, 1.0)
		_DepthRate("DepthRate", float) = 1.0
	}

SubShader {
    Tags { "RenderType"="Opaque" }
    Pass {
        Fog { Mode Off }
		ZTest Less
CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

struct appdata 
{
	half4 vertex : POSITION;
	// float2 uv : TEXCOORD0;
	UNITY_VERTEX_INPUT_INSTANCE_ID 
};

struct v2f {
    half4 pos : SV_POSITION;
    // float2 depth : TEXCOORD0;
};

v2f vert (appdata v) {
	UNITY_SETUP_INSTANCE_ID(v);
    v2f o;
    o.pos = UnityObjectToClipPos (v.vertex);
    return o;
}

fixed4 _Color;
fixed4 _Color2;
float _DepthRate;

fixed4 frag(v2f i) : COLOR {
#ifdef SHADER_API_MOBILE 
	fixed depth = min(1.0, ((i.pos.z - 1) * -0.5) * _DepthRate);
#else
	fixed depth = min(1.0, i.pos.z * _DepthRate);
#endif
	//return half4(depth, depth, depth, 1);
	return _Color * fixed4(1.0 - depth, 1.0 - depth, 1.0 - depth, 1) + _Color2 * fixed4(depth, depth, depth, 1);
}
ENDCG
    }
}
}

//// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Shader "flyugel/UnlitBGObj"
//{
//	Properties
//	{
//		// _MainTex ("Texture", 2D) = "white" {}
//		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
//		//

//		_LineColor ("Line Color", Color) = (1,1,1,1)
//		_GridColor ("Grid Color", Color) = (1,1,1,0)
//		_LineWidth ("Line Width", float) = 0.05
//	}
	
//	SubShader
//	{
//		Tags 
//		{
//			"Queue" = "Geometry"
//			"RenderType" = "Opaque"
//			"PreviewType" = "Sphere"
//		}

//		Pass
//		{
//			Tags { "LightMode" = "Always"}
//			// Lighting Off
//			ZTest Less
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			#pragma multi_compile_instancing
//			#include "UnityCG.cginc"
			
//			struct appdata 
//			{
//				half4 vertex : POSITION;
//				// float2 uv : TEXCOORD0;
//				UNITY_VERTEX_INPUT_INSTANCE_ID 
//			};

//			struct v2f { 
//				// float2 uv : TEXCOORD0;
//				half4 vertex : SV_POSITION;
//			};

//			v2f vert (appdata v)
//			{
//				UNITY_SETUP_INSTANCE_ID(v);
//				v2f o;
//				o.vertex = UnityObjectToClipPos(v.vertex);
				
//				return o;
//			}

//			fixed4 _Color;
//			fixed4 frag() : COLOR 
//			{
//                return _Color;
//            }			

//			ENDCG
//		}

//		//
//		GrabPass{}

//		//

//	Pass
//    {
//    Cull off
//    Tags { "RenderType" = "Transparent" }
//    Blend SrcAlpha OneMinusSrcAlpha
//    AlphaTest Greater 0.5
     
//    CGPROGRAM
//    #pragma vertex vert
//    #pragma fragment frag
     
//    uniform float4 _LineColor;
//    uniform float4 _GridColor;
//    uniform float _LineWidth;
     
//    // vertex input: position, uv1, uv2
//    struct appdata
//    {
//    half4 vertex : POSITION;
//    half4 texcoord1 : TEXCOORD0;
//    half4 color : COLOR;
//    };
     
//    struct v2f
//    {
//    half4 pos : POSITION;
//    half4 texcoord1 : TEXCOORD0;
//    half4 color : COLOR;
//    };
     
//    v2f vert (appdata v)
//    {
//    v2f o;
//    o.pos = UnityObjectToClipPos( v.vertex);
//    o.texcoord1 = v.texcoord1;
//    o.color = v.color;
//    return o;
//    }
     
//    fixed4 frag(v2f i) : COLOR
//    {
//    fixed4 answer;
     
//    float lx = step(_LineWidth, i.texcoord1.x);
//    float ly = step(_LineWidth, i.texcoord1.y);
//    float hx = step(i.texcoord1.x, 1 - _LineWidth);
//    float hy = step(i.texcoord1.y, 1 - _LineWidth);
     
//    answer = lerp(_LineColor, _GridColor, lx*ly*hx*hy);
     
//    return answer;
//    }
//    ENDCG
//    }
//	}
//}
