using System;
using System.Collections;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Extensions {
	public static class ExtensionMethods {
		
		/// <summary>Invokes an Action after given <paramref name="delay"/>.</summary>
		/// <remarks>Starts a coroutine on the invoking MonoBehavior object. </remarks>
		/// <returns>A reference to the started coroutine.</returns>
		public static Coroutine InvokeWithDelay(this MonoBehaviour monoBehaviour, Action methodToInvoke, float delay) {
			return monoBehaviour.StartCoroutine(InvokeWithDelayCoroutine(methodToInvoke, delay));
		}
	
		private static IEnumerator InvokeWithDelayCoroutine(Action methodToInvoke, float delay) {
			yield return new WaitForSeconds(delay);
			methodToInvoke.Invoke();
		}
		
		/// <summary>Invokes an Action every <paramref name="interval"/> seconds. Has optional <paramref name="afterDelay"/>
		/// argument to star repeating after delay, otherwise will start right away</summary>
		/// <remarks>Starts a coroutine on the invoking MonoBehavior object.</remarks>
		/// <returns>A reference to the started coroutine.</returns>
		public static Coroutine InvokeRepeating(this MonoBehaviour monoBehaviour, Action methodToInvoke, float interval, 
		float afterDelay = 0f) {
			if (afterDelay > 0) {
				return monoBehaviour.StartCoroutine(InvokeRepeatingCoroutineAfterDelay(methodToInvoke, interval, 
				afterDelay));
			}
			return monoBehaviour.StartCoroutine(InvokeRepeatingCoroutine(methodToInvoke, interval));
		}
		
		private static IEnumerator InvokeRepeatingCoroutineAfterDelay(Action methodToInvoke, float interval, float delay) {
			yield return new WaitForSeconds(delay);
			yield return InvokeRepeatingCoroutine(methodToInvoke, interval);
		}

		private static IEnumerator InvokeRepeatingCoroutine(Action methodToInvoke, float interval) {
			for (;;) {
				yield return new WaitForSeconds(interval);
				methodToInvoke.Invoke();
			}
		}
	}
}