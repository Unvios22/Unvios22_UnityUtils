using System;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Debug {
	internal class GizmoUtilsMonoBehavior : MonoBehaviour {
		private Action _methodDefiningDesiredGizmos;
		
		internal void SetGizmoMethodDelegate(Action methodDefiningDesiredGizmos) {
			_methodDefiningDesiredGizmos = methodDefiningDesiredGizmos;
		}

		//TODO: Add ability to set a callback destroy, so that the invoking user receives a method to run to destroy the helper obj
		internal void SetDestroyTime(float time) {
			Destroy(gameObject, time);
		}
		
		private void OnDrawGizmos() {
			_methodDefiningDesiredGizmos?.Invoke();
		}
	}
}