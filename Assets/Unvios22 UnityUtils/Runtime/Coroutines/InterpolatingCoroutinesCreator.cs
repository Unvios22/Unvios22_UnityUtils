using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Coroutines.Model;

namespace Unvios22_UnityUtils.Runtime.Coroutines {
	internal static class InterpolatingCoroutinesCreator {
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between <paramref name="interpolateFrom"/>
		/// and <paramref name="interpolateTo"/> over the course of <paramref name="lerpTime"/> seconds according to <paramref name="interpolationType"/>
		/// implementation. Invokes the <paramref name="lerpValueConsumer"/> Action every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		internal static Coroutine StartInterpolatingCoroutine<T>(MonoBehaviour coroutineTarget, T interpolateFrom, T interpolateTo,
			[NotNull]Action<T> lerpValueConsumer, float lerpTime, IInterpolationType<T> interpolationType) {
			/*I tried to use ref or out args instead of consumer, but neither anonymous nor enumerating functions (i.e. coroutines) can use them:
			 https://docs.microsoft.com/pl-pl/archive/blogs/ericlippert/iterator-blocks-part-two-why-no-ref-or-out-parameters */
			if (lerpValueConsumer == null) {
				throw new ArgumentNullException(nameof(lerpValueConsumer), "Consumer cannot be null");
			}

			var interpolationFunction = interpolationType.GetInterpolationFunction();
			//define anonymous function that will invoke lerpValueConsumer Action with the result of
			//interpolationFunction Func (that invokes the desired interpolation function)
			void InterpolationPercentageConsumer(float f) {
				lerpValueConsumer.Invoke(interpolationFunction.Invoke(interpolateFrom, interpolateTo, f));
			}

			return coroutineTarget.StartCoroutine(InterpolateWithConsumerActionCoroutine(InterpolationPercentageConsumer, lerpTime));
		}
		
		private static IEnumerator InterpolateWithConsumerActionCoroutine(Action<float> consumer, float interpolationTime) {
			//does standard interpolation coroutine boilerplate code, calculating interpolationPercentage and then
			//passes the result into consumer Action
			var timer = 0f;
			
			while (timer < interpolationTime) {
				yield return null;
				timer += Time.deltaTime;
				var interpolationPercentage = timer / interpolationTime;
				consumer.Invoke(interpolationPercentage);
			}
		}

	}
}