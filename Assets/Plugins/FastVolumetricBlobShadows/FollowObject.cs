using UnityEngine;

namespace PixelTea.BlobShadow
{
	[ExecuteInEditMode]
	public class FollowObject : MonoBehaviour
	{
		public Transform ObjectToFollow;
		public Vector3 WorldOffset;
		public Vector3 LocalOffset;
		public bool MatchRotation;

		// Update is called once per frame
		void LateUpdate()
		{
			if (ObjectToFollow == null)
				return;

			if (MatchRotation)
			{
				transform.SetPositionAndRotation(ObjectToFollow.position + WorldOffset + LocalOffset.x * ObjectToFollow.right + LocalOffset.y * ObjectToFollow.up + LocalOffset.z * ObjectToFollow.forward, ObjectToFollow.localRotation);
			}
			else
			{
				transform.position = ObjectToFollow.position + WorldOffset + LocalOffset.x * ObjectToFollow.right + LocalOffset.y * ObjectToFollow.up + LocalOffset.z * ObjectToFollow.forward;
			}
		}
	}
}