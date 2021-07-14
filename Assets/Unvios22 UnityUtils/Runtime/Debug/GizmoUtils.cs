using System;
using UnityEngine;
using Unvios22_UnityUtils.Runtime.Math;

namespace Unvios22_UnityUtils.Runtime.Debug {
	public static class GizmoUtils {
		
		/// <summary>
		/// Draws a gizmo ray starting at <paramref name="position"/>, going in (and with the magnitude of) <paramref name="direction"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		[Obsolete("Works, but mind that Debug.DrawRay has the same functionality")]
		public static void DrawRay(Vector3 position, Vector3 direction, Color gizmoColor, float displayTime) {
			GizmoUtilsInternal.DrawRay(() => position, direction, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo ray that follows the position of <paramref name="transformToFollow"/>, going in (and with the magnitude of)
		/// <paramref name="direction"/>, coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/>
		/// seconds.</summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawFollowingRay(Transform transformToFollow, Vector3 direction, Color gizmoColor,
			float displayTime) {
			GizmoUtilsInternal.DrawRay(() => transformToFollow.position, direction, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo sphere at <paramref name="position"/>, with given <paramref name="radius"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawSphere(Vector3 position, float radius, Color gizmoColor, float displayTime) {
			GizmoUtilsInternal.DrawSphere(() => position, radius, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo sphere that follows <paramref name="transformToFollow"/> position, with given <paramref name="radius"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawFollowingSphere(Transform transformToFollow, float radius, Color gizmoColor,
			float displayTime) {
			GizmoUtilsInternal.DrawSphere(() => transformToFollow.position, radius, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo wiresphere at <paramref name="position"/>, with given <paramref name="radius"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawWireSphere(Vector3 position, float radius, Color gizmoColor, float displayTime) {
			GizmoUtilsInternal.DrawWireSphere(() => position, radius, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo wiresphere that follows <paramref name="transformToFollow"/> position, with given <paramref name="radius"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawFollowingWireSphere(Transform transformToFollow, float radius, Color gizmoColor,
			float displayTime) {
			GizmoUtilsInternal.DrawWireSphere(() => transformToFollow.position, radius, gizmoColor, displayTime);
		}
		
		//TODO: simplify DrawVector... API calls
		
		/// <summary>
		/// Draws a gizmo arrow mesh, starting at <paramref name="startingPosition"/>, pointing in (and with the magnitude of)
		/// <paramref name="direction"/>, coloring it with <paramref name="gizmoColor"/> and displaying for
		/// <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawVector(Vector3 startingPosition, Vector3 direction, Color gizmoColor,
			float displayTime) {
			GizmoUtilsInternal.DrawVector(() => startingPosition,() => direction, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo arrow mesh, starting at <paramref name="startingPosition"/> and ending at <paramref name="endPosition"/>,
		/// coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawVectorToPosition(Vector3 startingPosition, Vector3 endPosition, Color gizmoColor,
			float displayTime) {
			//TODO: test
			var vectorToDrawDirection = VectorUtils.VectorFromTo(startingPosition, endPosition);
			GizmoUtilsInternal.DrawVector(() => startingPosition,() => vectorToDrawDirection, gizmoColor, displayTime);
		}
		
		/// <summary>
		/// Draws a gizmo arrow mesh, following the position of <paramref name="transformToFollow"/>, pointing in
		/// (and with the magnitude of) <paramref name="direction"/>, coloring it with <paramref name="gizmoColor"/>
		/// and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawFollowingVector(Transform transformToFollow, Vector3 direction, Color gizmoColor,
			float displayTime) {
			GizmoUtilsInternal.DrawVector(() => transformToFollow.position,() => direction, gizmoColor, displayTime);
		}

		/// <summary>
		/// Draws a gizmo arrow mesh, following the position of <paramref name="transformToFollow"/> and ending at
		/// <paramref name="endPosition"/>, coloring it with <paramref name="gizmoColor"/> and displaying for
		/// <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawFollowingVectorToPosition(Transform transformToFollow, Vector3 endPosition,
			Color gizmoColor, float displayTime) {
			Vector3 DirectionFunc() => VectorUtils.VectorFromTo(transformToFollow.position, endPosition);
			GizmoUtilsInternal.DrawVector(() => transformToFollow.position, DirectionFunc, gizmoColor, 
			displayTime);
		}
		
		/// <summary>
		/// Draws a gizmo arrow mesh, starting at <paramref name="startingPosition"/> and following <paramref name="targetTransform"/> as
		/// target, coloring it with <paramref name="gizmoColor"/> and displaying for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawVectorToFollowedTransform(Vector3 startingPosition, Transform targetTransform,
			Color gizmoColor, float displayTime) {		
			Vector3 DirectionFunc() => VectorUtils.VectorFromTo(startingPosition, targetTransform.position);
			GizmoUtilsInternal.DrawVector(() => startingPosition,DirectionFunc, gizmoColor, 
				displayTime);
		}
		
		/// <summary>
		/// Draws a gizmo arrow mesh, following the position of <paramref name="transformToFollow"/> and following
		/// <paramref name="targetTransform"/> as target, coloring it with <paramref name="gizmoColor"/> and displaying
		/// for <paramref name="displayTime"/> seconds.
		/// </summary>
		/// <remarks> Creates a temp GizmoHelper object in the scene. Uses OnDrawGizmos() internally.</remarks>
		public static void DrawFollowingVectorToFollowedTransform(Transform transformToFollow, Transform targetTransform,
			Color gizmoColor, float displayTime) {
			Vector3 DirectionFunc() => VectorUtils.VectorFromTo(transformToFollow.position, targetTransform.position);
			GizmoUtilsInternal.DrawVector(() => transformToFollow.position, DirectionFunc, gizmoColor, 
				displayTime);
		}
	}
}