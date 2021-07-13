using System;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Coroutines.Model;

namespace Unvios22_UnityUtils.Runtime.Coroutines.InterpolationTypes {
	public class LerpVector3 : IInterpolationType<Vector3> {
		public Func<Vector3, Vector3, float, Vector3> GetInterpolationFunction() {
			return Vector3.Lerp;
		}
	}
}