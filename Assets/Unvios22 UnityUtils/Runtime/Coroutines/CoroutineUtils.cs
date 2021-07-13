using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Coroutines {
	public static class CoroutineUtils {

		private static readonly Func<float, float, float, float> lerpFloat = Mathf.Lerp;
		private static readonly Func<float, float, float, float> slerpFloat = Mathf.SmoothStep;
		private static readonly Func<Vector3, Vector3, float, Vector3> lerpVector3 = Vector3.Lerp;
		private static readonly Func<Vector3, Vector3, float, Vector3> slerpVector3 = Vector3.Slerp;
		private static readonly Func<Color, Color, float, Color> lerpColor = Color.Lerp;

		public enum InterpolationType {
			LerpFloat,
			SlerpFloat,
			LerpVector3,
			SlerpVector3,
			LerpColor
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between <paramref name="from"/>
		/// and <paramref name="to"/> over the course of <paramref name="lerpTime"/> seconds according to <paramref name="type"/>
		/// interpolation type. Invokes the <paramref name="consumer"/> Action every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine Interpolate<T>(MonoBehaviour coroutineTarget, T from, T to,
			[NotNull]Action<T> consumer, float lerpTime, InterpolationType type) {
			/*I tried to use ref or out args instead of consumer, but neither anonymous nor enumerating functions (i.e. coroutines) can use them:
			 https://docs.microsoft.com/pl-pl/archive/blogs/ericlippert/iterator-blocks-part-two-why-no-ref-or-out-parameters */
			ThrowArgumentNullExceptionIfConsumerNull(consumer);

			Func<T, T, float, T> interpolationFunction = type switch {
				InterpolationType.LerpFloat => interpolationFunction = lerpFloat as Func<T,T,float,T>,
				InterpolationType.SlerpFloat => interpolationFunction = slerpFloat as Func<T,T,float,T>,
				InterpolationType.LerpVector3 => interpolationFunction = lerpVector3 as Func<T,T,float,T>,
				InterpolationType.SlerpVector3 => interpolationFunction = slerpVector3 as Func<T,T,float,T>,
				InterpolationType.LerpColor => interpolationFunction = lerpColor as Func<T,T,float,T>,
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
			
			Action<float> interpolationPercentageConsumer = (f) => { consumer.Invoke(interpolationFunction.Invoke(from, to, f));};

			return coroutineTarget.StartCoroutine(InterpolateWithConsumerActionCoroutine(interpolationPercentageConsumer, lerpTime));
		}

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
			
			Action<float> lerpFunction = (f) => { consumer.Invoke(lerpFloat(from, to, f));};
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
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that Lerps between <paramref name="from"/>
		/// and <paramref name="to"/> over the course of <paramref name="lerpTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current Lerp value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine LerpVector3(MonoBehaviour coroutineTarget, Vector3 from, Vector3 to,
			[NotNull]Action<Vector3> consumer, float lerpTime) {
			ThrowArgumentNullExceptionIfConsumerNull(consumer);
			
			Action<float> lerpFunction = (f) => { consumer.Invoke(Vector3.Lerp(from, to, f));};
			return coroutineTarget.StartCoroutine(InterpolateWithConsumerActionCoroutine(lerpFunction, lerpTime));
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that Slerps between <paramref name="from"/>
		/// and <paramref name="to"/> over the course of <paramref name="lerpTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current Slerp value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine SlerpVector3(MonoBehaviour coroutineTarget, Vector3 from, Vector3 to,
			[NotNull]Action<Vector3> consumer, float lerpTime) {
			ThrowArgumentNullExceptionIfConsumerNull(consumer);
			
			Action<float> slerpFunction = (f) => { consumer.Invoke(Vector3.Slerp(from, to, f));};
			return coroutineTarget.StartCoroutine(InterpolateWithConsumerActionCoroutine(slerpFunction, lerpTime));
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that Lerps between <paramref name="from"/>
		/// and <paramref name="to"/> over the course of <paramref name="lerpTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current Lerp value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine LerpColor(MonoBehaviour coroutineTarget, Color from, Color to,
			[NotNull]Action<Color> consumer, float lerpTime) {
			ThrowArgumentNullExceptionIfConsumerNull(consumer);
			
			Action<float> lerpFunction = (f) => { consumer.Invoke(Color.Lerp(from, to, f));};
			return coroutineTarget.StartCoroutine(InterpolateWithConsumerActionCoroutine(lerpFunction, lerpTime));
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