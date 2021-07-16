using System;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Coroutines.Model;

namespace Unvios22_UnityUtils.Runtime.Coroutines.InterpolationTypes {
	public class SmoothStepFloat : IInterpolationType<float> {
		public Func<float, float, float, float> GetInterpolationFunction() {
			return Mathf.SmoothStep;
		}
	}
}