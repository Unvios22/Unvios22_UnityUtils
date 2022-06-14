using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Extensions {
	public static class PoseExtensions {
		
		/// <summary>Applies Pose position & rotation to a given <paramref name="transform"/>.</summary>
		/// <remarks>Directly modifies the <paramref name="transform"/>.</remarks>
		public static void ApplyToTransform(this Pose pose, Transform transform) {
			transform.position = pose.position;
			transform.rotation = pose.rotation;
		}
	}
}
