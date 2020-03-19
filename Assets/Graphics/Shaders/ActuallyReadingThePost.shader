Shader "Custom/ActuallyReadingThePost"
{
    Properties
    {

        // color of the water
		_Color("Color", Color) = (1, 1, 1, 1)
		// color of the edge effect
		_EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
		// width of the edge effect
		_DepthFactor("Depth Factor", float) = 1.0

    }

    SubShader
    {
        Pass
		{

			CGPROGRAM
			// required to use ComputeScreenPos()
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag
 
			// Unity built-in - NOT required in Properties
			sampler2D _CameraDepthTexture;

			struct vertexInput
			{
				float4 vertex : POSITION;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 screenPos : TEXCOORD1;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				// convert obj-space position to camera clip space
				output.pos = UnityObjectToClipPos(input.vertex);

				// compute depth (screenPos is a float4)
				output.screenPos = ComputeScreenPos(output.pos);

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, input.screenPos);
				float depth = LinearEyeDepth(depthSample).r;
  
				// apply the DepthFactor to be able to tune at what depth values
				// the foam line actually starts
				float foamLine = 1 - saturate(_DepthFactor * (depth - input.screenPos.w));
  
				// multiply the edge color by the foam factor to get the edge,
				// then add that to the color of the water
				float4 col = _Color + foamLine * _EdgeColor;
			}

			ENDCG

		}
    }
}
