Shader "Custom/BumpMap"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MetallicTex("Metallic (R)", 2D) = "white" {}
        _SpecColor("Specular", Color) = (1,1,1,1)
        _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
        _MainTex("Base Texture", 2D) = "white" {}
        _RampTex ("Ramp Texture", 2D) = "white" {}
        _myBump ("Bump Texture", 2D) = "bump" {}
        _mySlider ("Bump Amount", Range(0,10)) = 1

        [Toggle]_UseDiffuse("Use Diffuse", Float) = 1
        [Toggle]_UseAmbient("Use Ambient", Float) = 1
        [Toggle]_UseSpecular("Use Specular", Float) = 1
        [Toggle]_UseToon("Use Specular", Float) = 1
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
                float3 normalWS : TEXCOORD1;
                float3 tangentWS : TEXCOORD2;
                float2 uv : TEXCOORD0;
                float3 bitangentWS : TEXCOORD3;
                float3 viewDirWS : TEXCOORD4;
            };

            TEXTURE2D(_MetallicTex);
            SAMPLER(sampler_MetallicTex);
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_RampTex);
            SAMPLER(sampler_RampTex);
            TEXTURE2D(_myBump);
            SAMPLER(sampler_myBump);

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float4 _SpecColor;
                float _Smoothness;
                float _UseDiffuse;
                float _UseAmbient;
                float _UseSpecular;
                float _UseToon;
                float _mySlider;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                OUT.tangentWS = normalize(TransformObjectToWorldNormal(IN.tangentOS.xyz));
                OUT.bitangentWS = cross(OUT.normalWS, OUT.tangentWS) * IN.tangentOS.w;
                float3 worldPosWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.viewDirWS = normalize(GetCameraPositionWS() - worldPosWS);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * _Color;
                half3 normalTS = UnpackNormal(SAMPLE_TEXTURE2D(_myBump, sampler_myBump, IN.uv));
                normalTS.xy *= _mySlider;
                half3x3 TBN = half3x3(IN.tangentWS, IN.bitangentWS, IN.normalWS);
                half3 normalWS = normalize(mul(normalTS, TBN));
                
                half metallicTex = SAMPLE_TEXTURE2D(_MetallicTex, sampler_MetallicTex, IN.uv).r;
                half smoothness = _Smoothness;

                Light mainLight = GetMainLight();
                half3 lightDir = normalize(mainLight.direction);
                //half3 normalWS = normalize(IN.normalWS);
                half3 lightColor = mainLight.color;
                
                half NdotL = saturate(dot(normalWS, lightDir));
                half rampValue = SAMPLE_TEXTURE2D(_RampTex, sampler_RampTex, float2(NdotL, 0)).r;

                half3 finalColor = 0;

                //Toon
                if (_UseToon > 0.5)
                {
                    finalColor = _Color.rgb * lightColor * rampValue * 0.5;
                }
                //Ambient
                if (_UseAmbient > 0.5)
                {
                    half3 ambientSH = SampleSH(normalWS);
                    finalColor += ambientSH * texColor.rgb * 0.5;
                }

                //Diffuse
                if (_UseDiffuse > 0.5)
                {
                    half3 diffuse = texColor.rgb * NdotL * 0.5;
                    finalColor += diffuse;
                }

                //Specular 
                if (_UseSpecular > 0.5)
                {
                    half3 viewDir = normalize(GetWorldSpaceViewDir(IN.positionHCS.xyz));
                    half3 halfDir = normalize(lightDir + viewDir);
                    half NdotH = saturate(dot(normalWS, halfDir));

                    half3 specular = _SpecColor.rgb * pow(NdotH, smoothness * 128.0) * 0.5;
                    finalColor += specular * metallicTex;
                }

                return half4(finalColor, 1);
            }
            ENDHLSL
        }
    }
}
