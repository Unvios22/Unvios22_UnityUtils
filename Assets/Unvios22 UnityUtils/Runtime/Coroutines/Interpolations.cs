using System;
using JetBrains.Annotations;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Coroutines.InterpolationTypes;

namespace Unvios22_UnityUtils.Runtime.Coroutines {
	public class Interpolations {
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between 
		/// <paramref name="from"/> and <paramref name="to"/> over the course of <paramref name="interpolationTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine LerpFloat(MonoBehaviour coroutineTarget, float from, float to,
			[NotNull]Action<float> consumer, float interpolationTime) {
			return InterpolationsInternal.StartInterpolatingCoroutine(coroutineTarget, from, to,
				consumer, interpolationTime, new LerpFloat());
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between 
		/// <paramref name="from"/> and <paramref name="to"/> over the course of <paramref name="interpolationTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine SlerpFloat(MonoBehaviour coroutineTarget, float from, float to,
			[NotNull]Action<float> consumer, float interpolationTime) {
			return InterpolationsInternal.StartInterpolatingCoroutine(coroutineTarget, from, to,
				consumer, interpolationTime, new SlerpFloat());
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between 
		/// <paramref name="from"/> and <paramref name="to"/> over the course of <paramref name="interpolationTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine LerpVector3(MonoBehaviour coroutineTarget, Vector3 from, Vector3 to,
			[NotNull]Action<Vector3> consumer, float interpolationTime) {
			return InterpolationsInternal.StartInterpolatingCoroutine(coroutineTarget, from, to, 
				consumer, interpolationTime, new LerpVector3());
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between 
		/// <paramref name="from"/> and <paramref name="to"/> over the course of <paramref name="interpolationTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine SlerpVector3(MonoBehaviour coroutineTarget, Vector3 from, Vector3 to,
			[NotNull]Action<Vector3> consumer, float interpolationTime) {
			return InterpolationsInternal.StartInterpolatingCoroutine(coroutineTarget, from, to,
				consumer, interpolationTime, new SlerpVector3());
		}
		
		/// <summary>
		/// Starts a coroutine on <paramref name="coroutineTarget"/> object that interpolates between 
		/// <paramref name="from"/> and <paramref name="to"/> over the course of <paramref name="interpolationTime"/> seconds.
		/// Invokes the <paramref name="consumer"/> Action every frame with current interpolation value.
		/// </summary>
		/// <returns>Reference to the started coroutine</returns>
		public static Coroutine LerpColor(MonoBehaviour coroutineTarget, Color from, Color to,
			[NotNull]Action<Color> consumer, float interpolationTime) {
			return InterpolationsInternal.StartInterpolatingCoroutine(coroutineTarget, from, to,
				consumer, interpolationTime, new LerpColor());
		}
	}
}