using System;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Coroutines.Model;

namespace Unvios22_UnityUtils.Runtime.Coroutines.InterpolationTypes {
	public class LerpColor : IInterpolationType<Color> {
		public Func<Color, Color, float, Color> GetInterpolationFunction() {
			return Color.Lerp;
		}
	}
}