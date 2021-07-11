using System;
using UnityEngine;

namespace Unvios22_UnityUtils.Debug {
	public static class GizmoUtils {
		private const string CreatedHelperObjName = "GizmoHelper";
		//TODO: add documenting comments. Remark when it draws a line, a cube or other stuff, so the user knows what to expect
		//TODO: move gizmo utils to editor folder && Add separate assembly definition marked as editor only
		
		public static void DisplayVectorGizmo(Vector3 position, Vector3 direction, Color gizmoColor, float displayTime) {
			
			
			var delegateMethod = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawRay(position, direction);
			});
			
			InitializeGizmoHelperAndInsertDelegate(delegateMethod, displayTime);
		}

		private static void InitializeGizmoHelperAndInsertDelegate(Action delegateMethod, float displayTime) {
			//possible performance upgrade -> possible GC troubles with object creation and anonymous functions
			
			var instantiatedGizmosHelper = new GameObject(CreatedHelperObjName);
			var gizmoHelperMonoBehavior = instantiatedGizmosHelper.AddComponent<GizmoUtilsMonoBehavior>();
			
			gizmoHelperMonoBehavior.SetGizmoMethodDelegate(delegateMethod);
			gizmoHelperMonoBehavior.SetDestroyTime(displayTime);
		}
	}
}