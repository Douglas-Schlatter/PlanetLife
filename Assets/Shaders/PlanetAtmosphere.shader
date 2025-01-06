Shader "Unlit/PlanetAtmosphere"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (0.5, 0.8, 1, 1)
        _Radius ("Radius", Range(0.1, 1.0)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Back
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 worldPos : TEXCOORD0;
            };

            float4 _MainColor;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float distance = length(i.worldPos);
                float alpha = 1.0 - smoothstep(_Radius, _Radius + 0.1, distance);
                return fixed4(_MainColor.rgb, alpha);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
