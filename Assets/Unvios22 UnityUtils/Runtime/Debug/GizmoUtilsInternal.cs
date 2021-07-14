using System;
using System.IO;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Math;

namespace Unvios22_UnityUtils.Runtime.Debug {
	internal class GizmoUtilsInternal {
		private const string CreatedHelperObjName = "GizmoHelper";
		private const string ResourcesGizmoArrowMeshPath = "UtilModels/Arrow/UtilGizmoArrow";
		
		internal static void DrawRay(Func<Vector3> positionFunc, Vector3 direction, Color gizmoColor, float displayTime) {
			var gizmoContentsDelegate = new Action(() => {
				Gizmos.color = gizmoColor;	
				Gizmos.DrawRay(positionFunc.Invoke(), direction);
			});
			InitializeGizmoHelperAndInsertDelegate(gizmoContentsDelegate, displayTime);
		}
		
		internal static void DrawSphere(Func<Vector3> positionFunc, float radius, Color gizmoColor, float displayTime) {
			var gizmoContentsDelegate = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawSphere(positionFunc.Invoke(), radius);
			});
			InitializeGizmoHelperAndInsertDelegate(gizmoContentsDelegate, displayTime);
		}

		internal static void DrawWireSphere(Func<Vector3> positionFunc, float radius, Color gizmoColor, float displayTime) {
			var gizmoContentsDelegate = new Action(() => {
				Gizmos.color = gizmoColor;
				Gizmos.DrawWireSphere(positionFunc.Invoke(), radius);
			});
			InitializeGizmoHelperAndInsertDelegate(gizmoContentsDelegate, displayTime);
		}

		internal static void DrawVector(Func<Vector3> positionFunc, Func<Vector3> directionFunc, Color gizmoColor, float 
		displayTime) {
			var arrowMesh = LoadArrowMeshOrThrowException();
			
			var gizmoContentsDelegate = new Action(() => {
				Gizmos.color = gizmoColor;
				var direction = directionFunc.Invoke();
				
				var arrowRotation = Quaternion.LookRotation(direction);
				var arrowScale = Vector3.one * direction.magnitude;
				Gizmos.DrawMesh(arrowMesh, positionFunc.Invoke(), arrowRotation, arrowScale);
			});
			InitializeGizmoHelperAndInsertDelegate(gizmoContentsDelegate, displayTime);
		}

		private static Mesh LoadArrowMeshOrThrowException() {
			var arrowMesh = Resources.Load<Mesh>(ResourcesGizmoArrowMeshPath);
			if (arrowMesh == null) {
				throw new FileNotFoundException("Gizmo arrow mesh asset is missing! Expected at Resources path: " +
				                                ResourcesGizmoArrowMeshPath);
			}
			return arrowMesh;
		}

		private static void InitializeGizmoHelperAndInsertDelegate(Action gizmoContentsDelegate, float displayTime) {
			//possible performance upgrade -> possibly creating redundant GC troubles with object creation and anonymous functions
			//though it doesn't have to be optimized - it's for debug only
			
			//TODO: possibly add GizmoHelper pooling?
			var instantiatedGizmosHelper = new GameObject(CreatedHelperObjName);
			var gizmoHelperMonoBehavior = instantiatedGizmosHelper.AddComponent<GizmoUtilsMonoBehavior>();
			
			gizmoHelperMonoBehavior.SetGizmoMethodDelegate(gizmoContentsDelegate);
			gizmoHelperMonoBehavior.SetDestroyTime(displayTime);
		}
	}
}