using System;
using System.Collections;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Helpers {
	public static class CoroutineHelpers {
		//TODO: add documenting comment. Mention that it creates a coroutine on source object + refactor func name
		//TODO: add overloads or some other system to include Vector3's, Vector2's, Colors and potentially other types, or simply generic arguments
		[Obsolete("Doesn't work, WIP", true)]
		public static void LerpBetweenValues(this MonoBehaviour monoBehaviour, float from, float to, float lerpTime,
			out float valueToChange) {
			//TODO
			valueToChange = 0f;
		}
		
		internal static IEnumerator InternalCoroutine(float from, float to, float lerpTime, float valueToChange) {
			//TODO
			//https://forum.unity.com/threads/passing-ref-variable-to-coroutine.379640/
			var timer = 0f;
			while (timer < lerpTime) {
				timer += Time.deltaTime;
				yield return null;
			}
		}
	}
}