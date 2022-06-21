using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Extensions {
	public static class TransformExtensions {
	
		/// <summary>Applies Pose position and rotation to this transform.</summary>
		/// <remarks>Directly modifies the position and rotation.</remarks>
		public static void ApplyPose(this Transform transform, Pose pose) {
			transform.position = pose.position;
			transform.rotation = pose.rotation;
		}
		
		/// <summary>Parses this transform position and rotation into a Pose struct</summary>
		public static Pose ToPose(this Transform transform) {
			return new Pose(transform.position, transform.rotation);
		}
	}
}
