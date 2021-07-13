using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Coroutines {
	public static class CoroutineUtils {
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that Lerps between <paramref name="from"/>
		/// and <paramref name="to"/> over the course of <paramref name="lerpTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current Lerp value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine LerpFloat(MonoBehaviour coroutineTarget, float from, float to,
			[NotNull]Action<float> consumer, float lerpTime) {
			/*I tried to use ref or out args, but neither anonymous nor enumerating functions (i.e. coroutines) can use them:
			 https://docs.microsoft.com/pl-pl/archive/blogs/ericlippert/iterator-blocks-part-two-why-no-ref-or-out-parameters */
			ThrowArgumentNullExceptionIfConsumerNull(consumer);
			
			Action<float> lerpFunction = (f) => { consumer.Invoke(Mathf.Lerp(from, to, f));};
			return coroutineTarget.StartCoroutine(InterpolateWithConsumerActionCoroutine(lerpFunction, lerpTime));
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that Slerps between <paramref name="from"/>
		/// and <paramref name="to"/> over the course of <paramref name="lerpTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current Slerp value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine SlerpFloat(MonoBehaviour coroutineTarget, float from, float to,
			[NotNull]Action<float> consumer, float slerpTime) {
			ThrowArgumentNullExceptionIfConsumerNull(consumer);

			Action<float> slerpFunction = (f) => { consumer.Invoke(Mathf.SmoothStep(from, to, f));};
			return coroutineTarget.StartCoroutine(InterpolateWithConsumerActionCoroutine(slerpFunction, slerpTime));
		}

		private static void ThrowArgumentNullExceptionIfConsumerNull<T>(Action<T> consumer) {
			if (consumer == null) {
				throw new ArgumentNullException(nameof(consumer), "Consumer cannot be null");
			}
		}
		
		private static IEnumerator InterpolateWithConsumerActionCoroutine(Action<float> consumer, float interpolationTime) {
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