using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Coroutines.Model;

namespace Unvios22_UnityUtils.Runtime.Coroutines {
	internal static class InterpolationsInternal {
		//TODO: add fixed update coroutines as well
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between <paramref name="interpolateFrom"/>
		/// and <paramref name="interpolateTo"/> over the course of <paramref name="interpolationTime"/> seconds according to
		/// <paramref name="interpolationType"/> implementation. Invokes the <paramref name="lerpValueConsumer"/> Action
		/// every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		internal static Coroutine StartInterpolatingCoroutine<T>(MonoBehaviour coroutineTarget, T interpolateFrom, T interpolateTo,
			[NotNull]Action<T> lerpValueConsumer, float interpolationTime, IInterpolationType<T> interpolationType) {
			//I tried to use ref or out args instead of consumer, but neither anonymous nor enumerating functions (i.e. coroutines) can use them:
			//https://docs.microsoft.com/pl-pl/archive/blogs/ericlippert/iterator-blocks-part-two-why-no-ref-or-out-parameters
			
			//possible performance upgrade -> the anonymous delegates and abstraction may prob be optimized to reduce the created coroutine overhead
			
			if (lerpValueConsumer == null) {
				throw new ArgumentNullException(nameof(lerpValueConsumer), "Consumer cannot be null!");
			}
			var interpolationFunction = interpolationType.GetInterpolationFunction();

			return coroutineTarget.StartCoroutine(
				InterpolateWithConsumerActionCoroutine(InterpolationPercentageConsumer, interpolationTime));
			
			void InterpolationPercentageConsumer(float interpolationPercentage) {
				var interpolationFuncResult = interpolationFunction.Invoke(interpolateFrom, interpolateTo, interpolationPercentage);
				lerpValueConsumer.Invoke(interpolationFuncResult);
			}
		}
		
		private static IEnumerator InterpolateWithConsumerActionCoroutine(Action<float> consumer, float interpolationTime) {
			//does standard interpolation coroutine boilerplate code calculating interpolationPercentage and
			//passes the result into consumer Action every frame
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