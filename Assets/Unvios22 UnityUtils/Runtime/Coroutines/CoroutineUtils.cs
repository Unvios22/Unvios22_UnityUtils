using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Coroutines {
	public static class CoroutineUtils {
		
		/// <summary>
		/// Creates a coroutine on <paramref name="coroutineTarget"/> object, and lerps between <paramref name="fromValue"/>
		/// and <paramref name="toValue"/> over the course of <paramref name="lerpTime"/> seconds.
		/// Invokes the <paramref name="valueSetterAction"/> Action every frame with current lerp value.
		/// </summary>
		/// <returns>Reference to the created coroutine</returns>
		public static Coroutine LerpFloat(MonoBehaviour coroutineTarget, float fromValue, float toValue, float lerpTime,
			[NotNull]Action<float> valueSetterAction) {
			//I tried to use ref or out args, but neither anonymous functions nor enumerating functions (i.e. coroutines) can use them
			//https://docs.microsoft.com/pl-pl/archive/blogs/ericlippert/iterator-blocks-part-two-why-no-ref-or-out-parameters
			if (valueSetterAction == null) {
				throw new ArgumentNullException();
			}
			return coroutineTarget.StartCoroutine(InternalLerpCoroutine(fromValue, toValue, lerpTime, valueSetterAction));
		}

		private static IEnumerator InternalLerpCoroutine(float fromValue, float toValue, float lerpTime,
			Action<float> updateValueAction) {
			var timer = 0f;
			var lerpPercentage = 0f;
			while (timer < lerpTime) {
				yield return null;
				timer += Time.deltaTime;
				lerpPercentage = timer / lerpTime;
				updateValueAction(Mathf.Lerp(fromValue, toValue, lerpPercentage));
			}
		}
	}
}