Shader "Custom/NeonShader"
{
    Properties
    {
        [HideInInspector] _MainTex("MainTexture", 2D) = "white" {}
        _BaseTex("Base Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _OuterTex("Outer Texture", 2D) = "white" {}
        _OuterColor("Outer Color", Color) = (1,1,1,1)
        _InnerTex("Inner Texture", 2D) = "white" {}
        _InnerColor("Inner Color", Color) = (1,1,1,1)
        [HDR]_EmissionColor("Emission Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Standard alpha:blend vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BaseTex;
        sampler2D _OuterTex;
        sampler2D _InnerTex;
        fixed4 _BaseColor;
        fixed4 _OuterColor;
        fixed4 _InnerColor;
        fixed4 _EmissionColor;

        struct Input
        {
            float2 uv_BaseTex;
            float2 texcoord;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        fixed4 blendColor(fixed4 top, fixed4 bottom) {
            fixed3 col = top.rgb * top.a + bottom.rgb * (1.0 - top.a);
            fixed alpha = top.a + bottom.a * (1.0 - top.a);
            return fixed4(col.rgb, alpha);
        }

        fixed4 generateNeon(Input i, inout float2 neonUV) {
            fixed4 c = fixed4(_BaseColor.rgb, 0);
            neonUV = i.texcoord.xy;
            neonUV.y = abs(neonUV.y * 2.0 - 1.0);
            fixed4 baseCol = tex2D(_BaseTex, neonUV) * _BaseColor;
            fixed4 innerCol = tex2D(_InnerTex, neonUV) * _InnerColor;
            fixed4 outerCol = tex2D(_OuterTex, neonUV) * _OuterColor;
            c = blendColor(outerCol, c);
            c = blendColor(baseCol, c);
            c = blendColor(innerCol, c);
            return c;
        }

        fixed3 calcEmission(float2 neonUV, float alpha) {
            alpha = (1.0 - alpha) + tex2D(_InnerTex, neonUV).r;
            return alpha * _EmissionColor;
        }

        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.texcoord = v.texcoord;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 neonUV;
            fixed4 c = generateNeon(IN, neonUV);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Emission = calcEmission(neonUV, c.a);
            o.Smoothness = 0;
            o.Metallic = 0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
