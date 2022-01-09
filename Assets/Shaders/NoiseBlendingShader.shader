Shader "Custom/NoiseBlendingShader"
{
    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        _NoiseTex("Noise", 2D) = "" {}
        _NoiseColor("Noise Color", Color) = (1,1,1,1)
        _Tint("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _NoiseTex;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            fixed4 _Tint;
            fixed4 _NoiseColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 blendColor(fixed4 top, fixed4 bottom) {
                fixed3 col = top.rgb * top.a + bottom.rgb * (1.0 - top.a);
                fixed alpha = top.a + bottom.a * (1.0 - top.a);
                return fixed4(col.rgb, alpha);
            }
            fixed4 sampleTexture(sampler2D tex, float2 uv) {
                fixed4 col = tex2D(tex, uv);
                col.a = tex2D(tex, uv).r;
                return col;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Tint;
                fixed4 noise = sampleTexture(_NoiseTex, TRANSFORM_TEX(i.uv, _NoiseTex)) * _NoiseColor;
                return blendColor(noise, col);
            }
            ENDCG
        }
    }
}
