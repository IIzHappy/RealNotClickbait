Shader "Custom/RimLighting"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MetallicTex("Metallic (R)", 2D) = "white" {}
        _SpecColor("Specular", Color) = (1,1,1,1)
        _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
        _MainTex("Base Texture", 2D) = "white" {}
        _RimColor ("Rim Color", Color) = (0, 0.5, 0.5, 1)
        _RimPower ("Rim Power", Range(0.5, 8.0)) = 3.0

        [Toggle]_UseDiffuse("Use Diffuse", Float) = 1
        [Toggle]_UseAmbient("Use Ambient", Float) = 1
        [Toggle]_UseSpecular("Use Specular", Float) = 1
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque" }

        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
                float4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 viewDirWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float2 uv : TEXCOORD3;
            };

            TEXTURE2D(_MetallicTex);
            SAMPLER(sampler_MetallicTex);
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float4 _SpecColor;
                float _Smoothness;
                float _UseDiffuse;
                float _UseAmbient;
                float _UseSpecular;
                float4 _RimColor;
                float _RimPower;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                float3 worldPosWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.viewDirWS = normalize(GetCameraPositionWS() - worldPosWS);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * _Color;
                
                half metallicTex = SAMPLE_TEXTURE2D(_MetallicTex, sampler_MetallicTex, IN.uv).r;
                half smoothness = _Smoothness;

                Light mainLight = GetMainLight();
                half3 lightDir = normalize(mainLight.direction);
                half3 normalWS = normalize(IN.normalWS);
                half3 viewDirWS = normalize(IN.viewDirWS);
                half rimFactor = 1.0 - saturate(dot(viewDirWS, normalWS));
                half rimLighting = pow(rimFactor, _RimPower);

                half NdotL = saturate(dot(normalWS, lightDir));

                half3 finalColor = 0;

                //Ambient
                if (_UseAmbient > 0.5)
                {
                    half3 ambientSH = SampleSH(normalWS);
                    finalColor += ambientSH * texColor.rgb;
                }

                //Diffuse
                if (_UseDiffuse > 0.5)
                {
                    half3 diffuse = texColor.rgb * NdotL;
                    finalColor += diffuse;
                }

                //Specular 
                if (_UseSpecular > 0.5)
                {
                    half3 viewDir = normalize(GetWorldSpaceViewDir(IN.positionHCS.xyz));
                    half3 halfDir = normalize(lightDir + viewDir);
                    half NdotH = saturate(dot(normalWS, halfDir));

                    half3 specular = _SpecColor.rgb * pow(NdotH, smoothness * 128.0) + _RimColor.rgb * rimLighting;
                    finalColor += specular * metallicTex;
                }

                return half4(finalColor, 1);
            }
            ENDHLSL
        }
    }
}
