using System;
using UnityEngine;

namespace Unvios22_UnityUtils.Runtime.Debug {
	public static class GizmoUtils {
		private const string CreatedHelperObjName = "GizmoHelper";
		//TODO: possibly add more overloads that take a transform to follow, and follow it automatically in the scene
		
		/// <summary>
		/// Draws a gizmo ray starting at <paramref name="position"/>, going in (and with the magnitude of) <paramref name="direction"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks>
		/// Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.
		/// </remarks>
		public static void DrawVector(Vector3 position, Vector3 direction, Color gizmoColor, float displayTime) {
			//TODO: possibly redundant because of Unity Debug.DrawRay?
			//TODO: display some arrow mesh (or use some different method) to mark the direction of the vector.
			var delegateMethod = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawRay(position, direction);
			});
			InitializeGizmoHelperAndInsertDelegate(delegateMethod, displayTime);
		}

		/// <summary>
		/// Draws a gizmo sphere at <paramref name="position"/>, with given <paramref name="radius"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks>
		/// Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.
		/// </remarks>
		public static void DrawSphere(Vector3 position, float radius, Color gizmoColor, float displayTime) {
			var delegateMethod = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawSphere(position, radius);
			});
			InitializeGizmoHelperAndInsertDelegate(delegateMethod, displayTime);
		}

		/// <summary>
		/// Draws a gizmo wiresphere at <paramref name="position"/>, with given <paramref name="radius"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks>
		/// Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.
		/// </remarks>
		public static void DrawWireSphere(Vector3 position, float radius, Color gizmoColor, float displayTime) {
			var delegateMethod = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawWireSphere(position, radius);
			});
			InitializeGizmoHelperAndInsertDelegate(delegateMethod, displayTime);
		}

		private static void InitializeGizmoHelperAndInsertDelegate(Action delegateMethod, float displayTime) {
			//possible performance upgrade -> possibly creating redundant GC troubles with object creation and anonymous functions
			//though it doesn't have to be optimized - it's for debug only
			
			var instantiatedGizmosHelper = new GameObject(CreatedHelperObjName);
			var gizmoHelperMonoBehavior = instantiatedGizmosHelper.AddComponent<GizmoUtilsMonoBehavior>();
			
			gizmoHelperMonoBehavior.SetGizmoMethodDelegate(delegateMethod);
			gizmoHelperMonoBehavior.SetDestroyTime(displayTime);
		}
	}
}