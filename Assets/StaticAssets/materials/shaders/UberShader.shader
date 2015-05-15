Shader "Nevermore/Uber Shader" { 
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_MainTex ("Base", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_SpecMap ("Specular map(RGB) Gloss (A)", 2D) = "gray" {}
		_Detail ("Diffuse Detail (RGB)", 2D) = "gray" {}
	} 
	SubShader { 
		Tags { 
			"RenderType"="Opaque" 
		} 
		LOD 400 
		CGPROGRAM 
		#pragma surface surf BlinnPhong

		sampler2D _MainTex; 
		sampler2D _BumpMap; 
		sampler2D _SpecMap;
		sampler2D _Detail; 
		fixed4 _Color; 
		half _Shininess;
		half _BumpStrength;

		struct Input { 
			float2 uv_MainTex; 
			float2 uv_BumpMap; 
			float2 uv_SpecMap;
			float2 uv_Detail; 
		};

		void surf (Input IN, inout SurfaceOutput o) { 
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex); 
			fixed4 specTex = tex2D(_SpecMap, IN.uv_SpecMap);
			tex.rgb *= tex2D(_Detail,IN.uv_Detail).rgb*1.375;
			o.Albedo = tex.rgb * _Color.rgb; 
			o.Gloss = specTex.r;
			o.Alpha = tex.a * _Color.a; 
			o.Specular = specTex.a; 
			float3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Normal = normalize(normal); 
		} 
		ENDCG 
	}

	FallBack "Specular" 
}