// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UI Cutoff"
{
	Properties
	{
		[PerRendererData] _MainTex( "Base Texture", 2D ) = "white" {}
		_Color( "Tint", Color ) = ( 1, 1, 1, 1 )
		_Cutoff( "Cutoff", Float ) = 0
		_Softness( "Softness", Float ) = 0
		_FinalTex( "Final Texture", 2D ) = "white" {}
		_FinalTint( "Final Tint", Color ) = ( 1, 1, 1, 1 )
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]
		
		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#pragma multi_compile __ UNITY_UI_ALPHACLIP

				struct appdata_t
				{
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					half2 texcoord : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					fixed4 color : COLOR;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed4 _Color;
				sampler2D _FinalTex;
				fixed4 _FinalTint;
				float _Cutoff;
				float _Softness;
				fixed4 _TextureSampleAdd;
				float4 _ClipRect;
				
				v2f vert( appdata_t v )
				{
					v2f o;
					o.vertex = UnityObjectToClipPos( v.vertex );
					o.texcoord = TRANSFORM_TEX( v.texcoord, _MainTex );
					o.worldPosition = v.vertex;
					o.vertex = UnityObjectToClipPos( o.worldPosition );
					
					#ifdef UNITY_HALF_TEXEL_OFFSET
						o.vertex.xy += ( _ScreenParams.zw - 1 ) * float2( -1, 1 );
					#endif
					
					o.color = v.color * _Color;
					return o;
				}
				
				fixed4 frag( v2f i ) : SV_Target
				{
					float tempR = ( tex2D( _MainTex, i.texcoord ) * i.color ).r;
					fixed4 tempColor = ( tex2D( _FinalTex, i.texcoord ) + _TextureSampleAdd ) * _FinalTint;
					
					if ( tempR > _Cutoff )
					{
						tempColor.a = 0;
					}
					else if ( tempR > ( _Cutoff - _Softness ) )
					{
						tempColor.a *= ( _Cutoff - tempR ) / _Softness;
					}
					
					tempColor.a *= UnityGet2DClipping( i.worldPosition.xy, _ClipRect );
					#ifdef UNITY_UI_ALPHACLIP
						clip( tempColor.a - 0.001 );
					#endif
					
					return tempColor;
				}
			ENDCG
		}
	}
}