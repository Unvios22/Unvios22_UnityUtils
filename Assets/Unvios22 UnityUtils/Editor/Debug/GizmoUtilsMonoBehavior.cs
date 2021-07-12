using System;
using UnityEngine;

namespace Unvios22_UnityUtils.Editor.Debug {
	internal class GizmoUtilsMonoBehavior : MonoBehaviour {
		private Action _methodDefiningDesiredGizmos;
		
		internal void SetGizmoMethodDelegate(Action methodDefiningDesiredGizmos) {
			_methodDefiningDesiredGizmos = methodDefiningDesiredGizmos;
		}

		internal void SetDestroyTime(float time) {
			Destroy(gameObject, time);
		}
		
		private void OnDrawGizmos() {
			_methodDefiningDesiredGizmos?.Invoke();
		}
	}
}