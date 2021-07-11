using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unvios22_UnityUtils.Math {
	public static class VectorUtils {
		//TODO: add documenting comments to methods
		
		public static Vector3 VectorFromTo(Vector3 from, Vector3 to) {
			return to - from;
		}
		
		public static Vector2 VectorFromTo(Vector2 from, Vector2 to) {
			return to - from;
		}

		public static Vector3 GetAverageVector(ICollection<Vector3> vectorCollection) {
			var sum = vectorCollection.Aggregate(Vector3.zero, (current, vector) => current + vector);
			return sum / vectorCollection.Count;
		}
		
		public static Vector2 GetAverageVector(ICollection<Vector2> vectorCollection) {
			var sum = vectorCollection.Aggregate(Vector2.zero, (current, vector) => current + vector);
			return sum / vectorCollection.Count;
		}
	}
}