Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth("Outline Width", Float) = 1.0
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
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
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _OutlineColor;
                float _OutlineWidth;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    fixed4 color = tex2D(_MainTex, IN.uv);

                    float2 uv1 = IN.uv;
                    uv1.x -= _OutlineWidth / _ScreenParams.x;
                    fixed4 leftPix = tex2D(_MainTex, uv1);

                    float2 uv2 = IN.uv;
                    uv2.x += _OutlineWidth / _ScreenParams.x;
                    fixed4 rightPix = tex2D(_MainTex, uv2);

                    float2 uv3 = IN.uv;
                    uv3.y -= _OutlineWidth / _ScreenParams.y;
                    fixed4 downPix = tex2D(_MainTex, uv3);

                    float2 uv4 = IN.uv;
                    uv4.y += _OutlineWidth / _ScreenParams.y;
                    fixed4 upPix = tex2D(_MainTex, uv4);

                    fixed edge = step(0.5, abs(color.a - leftPix.a) + abs(color.a - rightPix.a) + abs(color.a - upPix.a) + abs(color.a - downPix.a));

                    return lerp(color, _OutlineColor, edge);
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
