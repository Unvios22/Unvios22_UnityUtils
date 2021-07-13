using System;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Coroutines.Model;

namespace Unvios22_UnityUtils.Runtime.Coroutines.InterpolationTypes {
	public class LerpFloat : IInterpolationType<float> {
		public Func<float, float, float, float> GetInterpolationFunction() {
			return Mathf.Lerp;
		}
	}
}