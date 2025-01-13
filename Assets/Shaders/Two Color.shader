Shader "Custom/TwoColorSprite"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1, 0, 0, 1)  // Primeira cor (vermelho)
        _Color2 ("Color 2", Color) = (0, 0, 1, 1)  // Segunda cor (azul)
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            uniform float4 _Color1;
            uniform float4 _Color2;
            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                // Divida o sprite ao meio com base na coordenada X
                if (i.uv.x < 0.5)
                    return _Color1;  // Aplica a cor 1 à metade esquerda
                else
                    return _Color2;  // Aplica a cor 2 à metade direita
            }
            ENDCG
        }
    }
    Fallback "Sprites/Default"
}
