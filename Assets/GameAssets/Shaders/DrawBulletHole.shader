Shader "Hidden/DrawBulletHole"
{
    Properties
    {
        _MainTex ("Source", 2D) = "white" {}
        _BrushTex ("Brush", 2D) = "white" {}

        _BrushPos ("Brush Pos", Vector) = (0.5,0.5,0,0)
        _BrushSize ("Brush Size", Float) = 0.05
        _BrushRotation ("Rotation", Float) = 0
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            ZWrite Off
            Cull Off
            ZTest Always

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_BrushTex);
            SAMPLER(sampler_BrushTex);

            float4 _BrushPos;
            float _BrushSize;
            float _BrushRotation;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;

                half4 baseColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

                float2 local = (uv - _BrushPos.xy) / _BrushSize;

                float s = sin(_BrushRotation);
                float c = cos(_BrushRotation);

                local = float2(
                    local.x * c - local.y * s,
                    local.x * s + local.y * c
                );

                local += 0.5;

                if (local.x < 0 || local.x > 1 ||
                    local.y < 0 || local.y > 1)
                {
                    return baseColor;
                }

                half4 brush = SAMPLE_TEXTURE2D(_BrushTex, sampler_BrushTex, local);

                return lerp(baseColor, brush, brush.a);
            }

            ENDHLSL
        }
    }
}