Shader "Cg texturing with alpha discard" {
   Properties {
      _MainTex ("RGBA Texture Image", 2D) = "white" {} 
      _Cutoff ("Alpha Cutoff", Float) = 0.5
   }
   SubShader {
      Pass {    
         LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Fog { Mode Off }
		Offset -1, -1
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]
 
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
		 #include "UnityCG.cginc"
 
         uniform sampler2D _MainTex;    
         uniform float _Cutoff;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.tex = input.texcoord;
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float4 textureColor = tex2D(_MainTex, input.tex.xy);  
            if (textureColor.a < _Cutoff)
               // alpha value less than user-specified threshold?
            {
               discard; // yes: discard this fragment
            }
            return textureColor;
         }
 
         ENDCG
      }
   }
 
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Unlit/Transparent Cutout"
}