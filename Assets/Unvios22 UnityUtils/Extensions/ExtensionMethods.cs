using System;
using System.Collections;
using UnityEngine;

namespace Unvios22_UnityUtils.Extensions {
	public static class ExtensionMethods {
		
		//TODO: add documenting comment. Remark that this uses a coroutine created on the monobehavior obj.
		public static void InvokeWithDelay(this MonoBehaviour monoBehaviour, Action methodToInvoke, float delay) {
			monoBehaviour.StartCoroutine(InvokeWithDelayCoroutine(methodToInvoke, delay));
		}
	
		private static IEnumerator InvokeWithDelayCoroutine(Action methodToInvoke, float delay) {
			yield return new WaitForSeconds(delay);
			methodToInvoke.Invoke();
		}
	}
}