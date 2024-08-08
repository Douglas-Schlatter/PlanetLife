Shader "Custom/PlanetShaderURP"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque"}
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            CBUFFER_START(UnityPerMaterial)
                half _Glossiness;
                half _Metallic;
                float4 _Color;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * IN.color;
                half3 normal = half3(0, 0, 1);
                half metallic = _Metallic;
                half smoothness = _Glossiness;
                InputData inputData;
                InitializeInputData(IN.positionHCS.xy, normal, inputData);
                SurfaceData surfaceData;
                surfaceData.color = albedo.rgb;
                surfaceData.metallic = metallic;
                surfaceData.smoothness = smoothness;
                half3 fresnel = FresnelSchlick(0.04, inputData.fresnel);
                half3 color = SurfaceShading(inputData, surfaceData, fresnel);
                return half4(color, albedo.a);
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
