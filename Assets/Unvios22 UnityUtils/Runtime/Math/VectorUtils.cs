using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Math {
	public static class VectorUtils {
		
		/// <summary>
		/// Returns Vector3 going between two Vectors 
		/// </summary>
		// I mean, c'mon, how many times does one have to recall whether it's "target - source" or "source - target"? ;)
		public static Vector3 VectorFromTo(Vector3 from, Vector3 to) {
			return to - from;
		}
		
		/// <summary>
		/// Returns Vector2 going between two Vectors 
		/// </summary>
		public static Vector2 VectorFromTo(Vector2 from, Vector2 to) {
			return to - from;
		}

		/// <summary>
		/// Returns average of Vector3s input in <paramref name="vectorCollection"/>
		/// </summary>
		public static Vector3 GetAverageVector(ICollection<Vector3> vectorCollection) {
			var sum = vectorCollection.Aggregate(Vector3.zero, (current, vector) => current + vector);
			return sum / vectorCollection.Count;
		}
		
		/// <summary>
		/// Returns average of Vector2s input in <paramref name="vectorCollection"/>
		/// </summary>
		public static Vector2 GetAverageVector(ICollection<Vector2> vectorCollection) {
			var sum = vectorCollection.Aggregate(Vector2.zero, (current, vector) => current + vector);
			return sum / vectorCollection.Count;
		}
	}
}