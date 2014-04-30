Shader "Custom/greyScale" {
	Properties {
		_MainTex ("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}
	SubShader {
		Tags { 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent"
		 }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			c.r = (c.r + c.b + c.g) / 3;
			c.g = (c.r + c.b + c.g) / 3;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
