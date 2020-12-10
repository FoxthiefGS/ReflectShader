Shader "Custom/RefractShader"
{
    Properties
    {
        _Cube("Reflection Map", Cube) = ""{} 
    }
        SubShader
    {
        Pass{
            CGPROGRAM

            #pragma vertex vert 
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform samplerCUBE _Cube; 

            struct vertexInput { 
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                    };
            struct vertexOutput { 
                float4 pos : SV_Position;
                float3 normalDir : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
            };

            vertexOutput vert(vertexInput input) { 
                vertexOutput output;

                float4x4 modelMatrix = unity_ObjectToWorld; 
                float4x4 modelMatrixInverse = unity_WorldToObject; 

                output.viewDir = mul(modelMatrix, input.vertex).xyz - _WorldSpaceCameraPos; 
                output.normalDir = normalize(mul(float4(input.normal, 0.0), modelMatrixInverse).xyz); 
                output.pos = UnityObjectToClipPos(input.vertex); 
                return output; 
            }

            float4 frag(vertexOutput input) : COLOR{ //Reflect
                float3 reflectedDir = reflect(input.viewDir, normalize(input.normalDir)); 
                return texCUBE(_Cube, reflectedDir); 
            }

            //float4 frag(vertexOutput input) : COLOR{ //Refract
            //    float refractiveIndex = 1.5f; 
            //    float3 refractedDir = refract(normalize(input.viewDir), normalize(input.normalDir), 1.0 / refractiveIndex); 
            //    return texCUBE(_Cube, refractedDir); 
            //}

            ENDCG
        }
    }
}
