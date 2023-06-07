Shader "Custom/FlatShadingVertexColor" 
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM   
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR0;
                fixed3 normal : NORMAL;
            };

            fixed4 _LightColor0;
            float4 _LightDirection0;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 normal = normalize(i.normal);
                fixed3 lightDir = normalize(_LightDirection0.xyz);
                fixed3 diff = _LightColor0.rgb * i.color.rgb * max(0.0, dot(normal, lightDir));
                fixed4 col = fixed4(diff, i.color.a);
                return col;
            }
            ENDCG
        }
    }
}