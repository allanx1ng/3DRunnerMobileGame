Shader "Custom/TransparentRedOverlay"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RedIntensity ("Red Intensity", Range(0, 1)) = 0.5
        _Opacity ("Opacity", Range(0, 1)) = 0.5
    }
    SubShader
    {

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert

        sampler2D _MainTex;
        float _RedIntensity;
        float _Opacity;

        struct Input {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.uv_MainTex = v.texcoord.xy;
        }

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
            col.r += _RedIntensity;
            o.Albedo = col.rgb;
            o.Alpha = col.a * _Opacity;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
