
using System;

namespace Unvios22_UnityUtils.Runtime.Coroutines.Model {
	internal interface IInterpolationType<T> {
		public Func<T, T, float, T> GetInterpolationFunction();
	}
}