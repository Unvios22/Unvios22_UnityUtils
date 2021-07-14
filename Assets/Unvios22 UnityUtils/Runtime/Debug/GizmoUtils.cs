using System;
using System.IO;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Math;

namespace Unvios22_UnityUtils.Runtime.Debug {
	public static class GizmoUtils {
		private const string CreatedHelperObjName = "GizmoHelper";
		private const string ResourcesGizmoArrowMeshPath = "UtilModels/Arrow/UtilGizmoArrow";
		
		//TODO: possibly add more overloads that take a transform to follow, and follow it automatically in the scene
		
		/// <summary>
		/// Draws a gizmo ray starting at <paramref name="position"/>, going in (and with the magnitude of) <paramref name="direction"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		[Obsolete("Works, but mind that Debug.DrawRay has the same functionality")]
		public static void DrawRay(Vector3 position, Vector3 direction, Color gizmoColor, float displayTime) {
			var delegateMethod = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawRay(position, direction);
			});
			InitializeGizmoHelperAndInsertDelegate(delegateMethod, displayTime);
		}
		
		/// <summary>
		/// Draws a gizmo arrow mesh, starting at <paramref name="startingPosition"/>, pointing in (and with the magnitude of) <paramref name="direction"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawVector(Vector3 startingPosition, Vector3 direction, Color gizmoColor, float displayTime) {
			DrawVectorInternal(startingPosition, direction, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo arrow mesh, starting at <paramref name="startingPosition"/> and ending at <paramref name="endPosition"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawVectorToPosition(Vector3 startingPosition, Vector3 endPosition, Color gizmoColor,
			float displayTime) {
			//TODO: test if works properly

			var vectorToDrawDirection = VectorUtils.VectorFromTo(startingPosition, endPosition);
			DrawVectorInternal(startingPosition, vectorToDrawDirection, gizmoColor, displayTime);
		}

		private static void DrawVectorInternal(Vector3 position, Vector3 direction, Color gizmoColor, float displayTime) {
			var arrowMesh = LoadArrowMeshOrThrowException();
			var arrowRotation = Quaternion.LookRotation(direction);
			var arrowScale = Vector3.one * direction.magnitude;
			
			var delegateMethod = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawMesh(arrowMesh, position, arrowRotation, arrowScale);
			});
			InitializeGizmoHelperAndInsertDelegate(delegateMethod, displayTime);
		}

		private static Mesh LoadArrowMeshOrThrowException() {
			var arrowMesh = Resources.Load<Mesh>(ResourcesGizmoArrowMeshPath);
			if (arrowMesh == null) {
				throw new FileNotFoundException("Gizmo arrow mesh asset is missing! Expected at Resources path: " +
				                                ResourcesGizmoArrowMeshPath);
			}
			return arrowMesh;
		}

		/// <summary>
		/// Draws a gizmo sphere at <paramref name="position"/>, with given <paramref name="radius"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
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
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
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
			
			//TODO: possibly add GizmoHelper pooling?
			var instantiatedGizmosHelper = new GameObject(CreatedHelperObjName);
			var gizmoHelperMonoBehavior = instantiatedGizmosHelper.AddComponent<GizmoUtilsMonoBehavior>();
			
			gizmoHelperMonoBehavior.SetGizmoMethodDelegate(delegateMethod);
			gizmoHelperMonoBehavior.SetDestroyTime(displayTime);
		}
	}
}