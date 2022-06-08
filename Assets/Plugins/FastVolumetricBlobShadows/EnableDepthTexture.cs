using UnityEngine;

namespace PixelTea.BlobShadow
{
	[ExecuteInEditMode]
	public class EnableDepthTexture : MonoBehaviour
	{
		void OnEnable()
		{
			GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
		}
	}
}