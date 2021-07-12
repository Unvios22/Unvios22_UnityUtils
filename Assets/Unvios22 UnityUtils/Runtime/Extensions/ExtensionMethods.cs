using System;
using System.Collections;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Extensions {
	public static class ExtensionMethods {
		
		/// <summary>
		/// Invokes an Action after given delay.
		/// </summary>
		/// <remarks>
		/// Starts a coroutine on the invoking MonoBehavior object.
		/// </remarks>
		public static void InvokeWithDelay(this MonoBehaviour monoBehaviour, Action methodToInvoke, float delay) {
			monoBehaviour.StartCoroutine(InvokeWithDelayCoroutine(methodToInvoke, delay));
		}
	
		private static IEnumerator InvokeWithDelayCoroutine(Action methodToInvoke, float delay) {
			yield return new WaitForSeconds(delay);
			methodToInvoke.Invoke();
		}
	}
}