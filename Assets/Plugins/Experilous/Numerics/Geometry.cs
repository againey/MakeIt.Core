/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using GeneralUtility = Experilous.Core.GeneralUtility;

namespace Experilous.Numerics
{
	/// <summary>
	/// A static utility class related to working with geometry.
	/// </summary>
	public static class Geometry
	{
		#region Vector Operations

		/// <summary>
		/// Performs a linear interpolation from <paramref name="lhs"/> to <paramref name="rhs"/> using the interpolation parameter <paramref name="t"/>, which does not get clamped to the range [0, 1] first.
		/// </summary>
		/// <param name="lhs">The starting value of the interpolation, when <paramref name="t"/> is zero.</param>
		/// <param name="rhs">The ending value of the interpolation, when <paramref name="t"/> is one.</param>
		/// <param name="t">The position at which the interpolation is done, determining the weighting applied to <paramref name="lhs"/> and <paramref name="rhs"/>.  Typically in the range [0, 1], but all values are valid.</param>
		/// <returns>The result of linearly interpolating from <paramref name="lhs"/> to <paramref name="rhs"/> at position <paramref name="t"/>.</returns>
		/// <remarks><para>This functionality was added to the <see cref="UnityEngine.Vector2"/> class in Unity 5.2, and is only included
		/// in this class for use in earlier versions of Unity which do not have it built in to the Unity library.</para></remarks>
		public static Vector2 LerpUnclamped(Vector2 lhs, Vector2 rhs, float t)
		{
			return lhs * (1f - t) + rhs * t;
		}

		/// <summary>
		/// Performs a linear interpolation from <paramref name="lhs"/> to <paramref name="rhs"/> using the interpolation parameter <paramref name="t"/>, which does not get clamped to the range [0, 1] first.
		/// </summary>
		/// <param name="lhs">The starting value of the interpolation, when <paramref name="t"/> is zero.</param>
		/// <param name="rhs">The ending value of the interpolation, when <paramref name="t"/> is one.</param>
		/// <param name="t">The position at which the interpolation is done, determining the weighting applied to <paramref name="lhs"/> and <paramref name="rhs"/>.  Typically in the range [0, 1], but all values are valid.</param>
		/// <returns>The result of linearly interpolating from <paramref name="lhs"/> to <paramref name="rhs"/> at position <paramref name="t"/>.</returns>
		/// <remarks><para>This functionality was added to the <see cref="UnityEngine.Vector3"/> class in Unity 5.2, and is only included
		/// in this class for use in earlier versions of Unity which do not have it built in to the Unity library.</para></remarks>
		public static Vector3 LerpUnclamped(Vector3 lhs, Vector3 rhs, float t)
		{
			return lhs * (1f - t) + rhs * t;
		}

		/// <summary>
		/// Performs a linear interpolation from <paramref name="lhs"/> to <paramref name="rhs"/> using the interpolation parameter <paramref name="t"/>, which does not get clamped to the range [0, 1] first.
		/// </summary>
		/// <param name="lhs">The starting value of the interpolation, when <paramref name="t"/> is zero.</param>
		/// <param name="rhs">The ending value of the interpolation, when <paramref name="t"/> is one.</param>
		/// <param name="t">The position at which the interpolation is done, determining the weighting applied to <paramref name="lhs"/> and <paramref name="rhs"/>.  Typically in the range [0, 1], but all values are valid.</param>
		/// <returns>The result of linearly interpolating from <paramref name="lhs"/> to <paramref name="rhs"/> at position <paramref name="t"/>.</returns>
		/// <remarks><para>This functionality was added to the <see cref="UnityEngine.Vector4"/> class in Unity 5.2, and is only included
		/// in this class for use in earlier versions of Unity which do not have it built in to the Unity library.</para></remarks>
		public static Vector4 LerpUnclamped(Vector4 lhs, Vector4 rhs, float t)
		{
			return lhs * (1f - t) + rhs * t;
		}

		/// <summary>
		/// Performs a spherical linear interpolation from <paramref name="p0"/> to <paramref name="p1"/> using the interpolation parameter <paramref name="t"/>, which does not get clamped to the range [0, 1] first.
		/// </summary>
		/// <param name="p0">The starting vector of the interpolation, when <paramref name="t"/> is zero.</param>
		/// <param name="p1">The ending vector of the interpolation, when <paramref name="t"/> is one.</param>
		/// <param name="t">The position at which the interpolation is done.  Typically in the range [0, 1], but all values are valid.</param>
		/// <returns>The result of performing a spherical linear interpolation from <paramref name="p0"/> to <paramref name="p1"/> at position <paramref name="t"/>.</returns>
		public static Vector3 SlerpUnitVectors(Vector3 p0, Vector3 p1, float t)
		{
			var omega = Mathf.Acos(Vector3.Dot(p0, p1));
			var d = Mathf.Sin(omega);
			var s0 = Mathf.Sin((1f - t) * omega);
			var s1 = Mathf.Sin(t * omega);
			return (p0 * s0 + p1 * s1) / d;
		}

		/// <summary>
		/// Calculates the angle, in radians, of the smallest arc between the given unit vectors.
		/// </summary>
		/// <param name="lhs">The first vector defining the arc.  Must have a magnitude of one for accurate results.</param>
		/// <param name="rhs">The second vector defining the arc.  Must have a magnitude of one for accurate results.</param>
		/// <returns>The angle, in radians, of the arc between the vectors.</returns>
		public static float AngleBetweenUnitVectors(Vector3 lhs, Vector3 rhs)
		{
			return Mathf.Atan2(Vector3.Cross(lhs, rhs).magnitude, Vector3.Dot(lhs, rhs));
		}

		/// <summary>
		/// Calculates the angle, in radians, of the smallest arc between the given vectors.
		/// </summary>
		/// <param name="lhs">The first vector defining the arc.  Must have a magnitude equal to <paramref name="sphereRadius"/> for accurate results.</param>
		/// <param name="rhs">The second vector defining the arc.  Must have a magnitude equal to <paramref name="sphereRadius"/> for accurate results.</param>
		/// <param name="sphereRadius">The radius of the sphere to which the vectors belong.</param>
		/// <returns>The angle, in radians, of the arc between the vectors.</returns>
		public static float AngleBetweenSphericalVectors(Vector3 lhs, Vector3 rhs, float sphereRadius)
		{
			lhs /= sphereRadius;
			rhs /= sphereRadius;
			return Mathf.Atan2(Vector3.Cross(lhs, rhs).magnitude, Vector3.Dot(lhs, rhs));
		}

		/// <summary>
		/// Calculates the angle, in radians, of the smallest arc between the given vectors.
		/// </summary>
		/// <param name="lhs">The first vector defining the arc.</param>
		/// <param name="rhs">The second vector defining the arc.</param>
		/// <returns>The angle, in radians, of the arc between the vectors.</returns>
		public static float AngleBetweenVectors(Vector3 lhs, Vector3 rhs)
		{
			lhs.Normalize();
			rhs.Normalize();
			return Mathf.Atan2(Vector3.Cross(lhs, rhs).magnitude, Vector3.Dot(lhs, rhs));
		}

		/// <summary>
		/// The spherical length of the shortest great circle arc between the given vectors.
		/// </summary>
		/// <param name="lhs">The first vector defining the arc.</param>
		/// <param name="rhs">The second vector defining the arc.</param>
		/// <param name="sphereRadius">The radius of the sphere to which the vectors belong.</param>
		/// <returns>The spherical arc length between the vectors</returns>
		public static float SphericalArcLength(Vector3 lhs, Vector3 rhs, float sphereRadius)
		{
			return AngleBetweenSphericalVectors(lhs, rhs, sphereRadius) * sphereRadius;
		}

		/// <summary>
		/// Adjusts the magnitude of the given vector without changing its direction.
		/// </summary>
		/// <param name="v">The vector to adjust.</param>
		/// <param name="newMagnitude">The new magnitude for the vector to acquire.</param>
		/// <returns>A vector with the same direction as the given vector, but with the specified magnitude.</returns>
		public static Vector2 WithMagnitude(this Vector2 v, float newMagnitude)
		{
			var originalMagnitude = v.magnitude;
			return v * (newMagnitude / originalMagnitude);
		}

		/// <summary>
		/// Adjusts the magnitude of the given vector without changing its direction.
		/// </summary>
		/// <param name="v">The vector to adjust.</param>
		/// <param name="newMagnitude">The new magnitude for the vector to acquire.</param>
		/// <returns>A vector with the same direction as the given vector, but with the specified magnitude.</returns>
		public static Vector3 WithMagnitude(this Vector3 v, float newMagnitude)
		{
			var originalMagnitude = v.magnitude;
			return v * (newMagnitude / originalMagnitude);
		}

		/// <summary>
		/// Adjusts the magnitude of the given vector without changing its direction.
		/// </summary>
		/// <param name="v">The vector to adjust.</param>
		/// <param name="newMagnitude">The new magnitude for the vector to acquire.</param>
		/// <returns>A vector with the same direction as the given vector, but with the specified magnitude.</returns>
		public static Vector4 WithMagnitude(this Vector4 v, float newMagnitude)
		{
			var originalMagnitude = v.magnitude;
			return v * (newMagnitude / originalMagnitude);
		}

		/// <summary>
		/// Creates a new vector by multiplying the components of the given vector by the specified scale.
		/// </summary>
		/// <param name="v">The vector whose components are to be multiplied.</param>
		/// <param name="s">The scale by which to multiply the components of the given vector.</param>
		/// <returns>A new vector which is the given vector multiplied by the amount specified.</returns>
		public static Vector3 MultiplyComponents(this Vector2 v, Vector2 s)
		{
			v.Scale(s);
			return v;
		}

		/// <summary>
		/// Creates a new vector by multiplying the components of the given vector by the specified scale.
		/// </summary>
		/// <param name="v">The vector whose components are to be multiplied.</param>
		/// <param name="s">The scale by which to multiply the components of the given vector.</param>
		/// <returns>A new vector which is the given vector multiplied by the amount specified.</returns>
		public static Vector3 MultiplyComponents(this Vector3 v, Vector3 s)
		{
			v.Scale(s);
			return v;
		}

		/// <summary>
		/// Creates a new vector by multiplying the components of the given vector by the specified scale.
		/// </summary>
		/// <param name="v">The vector whose components are to be multiplied.</param>
		/// <param name="s">The scale by which to multiply the components of the given vector.</param>
		/// <returns>A new vector which is the given vector multiplied by the amount specified.</returns>
		public static Vector3 MultiplyComponents(this Vector4 v, Vector4 s)
		{
			v.Scale(s);
			return v;
		}

		/// <summary>
		/// Creates a new vector by dividing the components of the given vector by the specified scale.
		/// </summary>
		/// <param name="v">The vector whose components are to be divided.</param>
		/// <param name="d">The divisor by which to divide the components of the given vector.</param>
		/// <returns>A new vector which is the given vector divided by the amount specified.</returns>
		public static Vector3 DivideComponents(this Vector2 v, Vector2 d)
		{
			v.x /= d.x;
			v.y /= d.y;
			return v;
		}

		/// <summary>
		/// Creates a new vector by dividing the components of the given vector by the specified scale.
		/// </summary>
		/// <param name="v">The vector whose components are to be divided.</param>
		/// <param name="d">The divisor by which to divide the components of the given vector.</param>
		/// <returns>A new vector which is the given vector divided by the amount specified.</returns>
		public static Vector3 DivideComponents(this Vector3 v, Vector3 d)
		{
			v.x /= d.x;
			v.y /= d.y;
			v.z /= d.z;
			return v;
		}

		/// <summary>
		/// Creates a new vector by dividing the components of the given vector by the specified scale.
		/// </summary>
		/// <param name="v">The vector whose components are to be divided.</param>
		/// <param name="d">The divisor by which to divide the components of the given vector.</param>
		/// <returns>A new vector which is the given vector divided by the amount specified.</returns>
		public static Vector3 DivideComponents(this Vector4 v, Vector4 d)
		{
			v.x /= d.x;
			v.y /= d.y;
			v.z /= d.z;
			v.w /= d.w;
			return v;
		}

		/// <summary>
		/// Determines the minimum component value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the minimum value of all the component values.</returns>
		public static float MinComponent(this Vector2 v)
		{
			return Mathf.Min(v.x, v.y);
		}

		/// <summary>
		/// Determines the minimum component value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the minimum value of all the component values.</returns>
		public static float MinComponent(this Vector3 v)
		{
			return Mathf.Min(Mathf.Min(v.x, v.y), v.z);
		}

		/// <summary>
		/// Determines the minimum component value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the minimum value of all the component values.</returns>
		public static float MinComponent(this Vector4 v)
		{
			return Mathf.Min(Mathf.Min(Mathf.Min(v.x, v.y), v.z), v.w);
		}

		/// <summary>
		/// Determines the maximum component value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the maximum value of all the component values.</returns>
		public static float MaxComponent(this Vector2 v)
		{
			return Mathf.Max(v.x, v.y);
		}

		/// <summary>
		/// Determines the maximum component value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the maximum value of all the component values.</returns>
		public static float MaxComponent(this Vector3 v)
		{
			return Mathf.Max(Mathf.Max(v.x, v.y), v.z);
		}

		/// <summary>
		/// Determines the maximum component value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the maximum value of all the component values.</returns>
		public static float MaxComponent(this Vector4 v)
		{
			return Mathf.Max(Mathf.Max(Mathf.Max(v.x, v.y), v.z), v.w);
		}

		/// <summary>
		/// Determines the minimum component absolute value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the minimum absolute value of all the component values.</returns>
		public static float MinAbsComponent(this Vector2 v)
		{
			return Mathf.Min(Mathf.Abs(v.x), Mathf.Abs(v.y));
		}

		/// <summary>
		/// Determines the minimum component absolute value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the minimum absolute value of all the component values.</returns>
		public static float MinAbsComponent(this Vector3 v)
		{
			return Mathf.Min(Mathf.Min(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z));
		}

		/// <summary>
		/// Determines the minimum component absolute value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the minimum absolute value of all the component values.</returns>
		public static float MinAbsComponent(this Vector4 v)
		{
			return Mathf.Min(Mathf.Min(Mathf.Min(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z)), Mathf.Abs(v.w));
		}

		/// <summary>
		/// Determines the maximum component absolute value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the maximum absolute value of all the component values.</returns>
		public static float MaxAbsComponent(this Vector2 v)
		{
			return Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
		}

		/// <summary>
		/// Determines the maximum component absolute value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the maximum absolute value of all the component values.</returns>
		public static float MaxAbsComponent(this Vector3 v)
		{
			return Mathf.Max(Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z));
		}

		/// <summary>
		/// Determines the maximum component absolute value of the given vector.
		/// </summary>
		/// <param name="v">The vector whose components are to be examined.</param>
		/// <returns>The value of the vector component with the maximum absolute value of all the component values.</returns>
		public static float MaxAbsComponent(this Vector4 v)
		{
			return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z)), Mathf.Abs(v.w));
		}

		/// <summary>
		/// Calculates the magnitude of the two-dimensional cross product of the given vectors, which is equal to the sine of the angle between them multiplied by their magnitudes.
		/// </summary>
		/// <param name="lhs">The first vector.</param>
		/// <param name="rhs">The second vector.</param>
		/// <returns>The sine of the angle between the two vectors multiplied by their magnitudes.</returns>
		public static float SinMagnitude(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.y - lhs.y * rhs.x;
		}

		/// <summary>
		/// Projects the first vector onto the second target vector.
		/// </summary>
		/// <param name="v">The vector to be projected.</param>
		/// <param name="target">The target vector onto which the first will be projected.</param>
		/// <returns>The projection of the first vector onto the second.</returns>
		public static Vector2 ProjectOnto(this Vector2 v, Vector2 target)
		{
			return Vector2.Dot(v, target) / Vector2.Dot(target, target) * target;
		}

		/// <summary>
		/// Projects the first vector onto the second target unit vector.
		/// </summary>
		/// <param name="v">The vector to be projected.</param>
		/// <param name="target">The target unit vector onto which the first will be projected.  Must have a magnitude of one for accurate results.</param>
		/// <returns>The projection of the first vector onto the second.</returns>
		public static Vector2 ProjectOntoUnit(this Vector2 v, Vector2 target)
		{
			return Vector2.Dot(v, target) * target;
		}

		/// <summary>
		/// Projects the first vector onto the second target vector.
		/// </summary>
		/// <param name="v">The vector to be projected.</param>
		/// <param name="target">The target vector onto which the first will be projected.</param>
		/// <returns>The projection of the first vector onto the second.</returns>
		public static Vector3 ProjectOnto(this Vector3 v, Vector3 target)
		{
			return Vector3.Dot(v, target) / Vector3.Dot(target, target) * target;
		}

		/// <summary>
		/// Projects the first vector onto the second target vector.
		/// </summary>
		/// <param name="v">The vector to be projected.</param>
		/// <param name="target">The target vector onto which the first will be projected.</param>
		/// <returns>The projection of the first vector onto the second.</returns>
		public static Vector3 ProjectOntoUnit(this Vector3 v, Vector3 target)
		{
			return Vector3.Dot(v, target) * target;
		}

		/// <summary>
		/// Projects the first vector onto the second target vector.
		/// </summary>
		/// <param name="v">The vector to be projected.</param>
		/// <param name="target">The target vector onto which the first will be projected.</param>
		/// <returns>The projection of the first vector onto the second.</returns>
		public static Vector4 ProjectOnto(this Vector4 v, Vector4 target)
		{
			return Vector4.Dot(v, target) / Vector4.Dot(target, target) * target;
		}

		/// <summary>
		/// Projects the first vector onto the second target vector.
		/// </summary>
		/// <param name="v">The vector to be projected.</param>
		/// <param name="target">The target vector onto which the first will be projected.</param>
		/// <returns>The projection of the first vector onto the second.</returns>
		public static Vector4 ProjectOntoUnit(this Vector4 v, Vector4 target)
		{
			return Vector4.Dot(v, target) * target;
		}

		/// <summary>
		/// Checks if the two vectors are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>True if the two vectors are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Vector2 lhs, Vector2 rhs)
		{
			return Mathf.Approximately(lhs.x, rhs.x) && Mathf.Approximately(lhs.y, rhs.y);
		}

		/// <summary>
		/// Checks if the two vectors are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="x">The first component of the second vector to compare.</param>
		/// <param name="y">The second component of the second vector to compare.</param>
		/// <returns>True if the two vectors are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Vector2 lhs, float x, float y)
		{
			return Mathf.Approximately(lhs.x, x) && Mathf.Approximately(lhs.y, y);
		}

		/// <summary>
		/// Checks if the two vectors are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>True if the two vectors are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Vector3 lhs, Vector3 rhs)
		{
			return Mathf.Approximately(lhs.x, rhs.x) && Mathf.Approximately(lhs.y, rhs.y) && Mathf.Approximately(lhs.z, rhs.z);
		}

		/// <summary>
		/// Checks if the two vectors are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="x">The first component of the second vector to compare.</param>
		/// <param name="y">The second component of the second vector to compare.</param>
		/// <param name="z">The third component of the second vector to compare.</param>
		/// <returns>True if the two vectors are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Vector3 lhs, float x, float y, float z)
		{
			return Mathf.Approximately(lhs.x, x) && Mathf.Approximately(lhs.y, y) && Mathf.Approximately(lhs.z, z);
		}

		/// <summary>
		/// Checks if the two vectors are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>True if the two vectors are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Vector4 lhs, Vector4 rhs)
		{
			return Mathf.Approximately(lhs.x, rhs.x) && Mathf.Approximately(lhs.y, rhs.y) && Mathf.Approximately(lhs.z, rhs.z) && Mathf.Approximately(lhs.w, rhs.w);
		}

		/// <summary>
		/// Checks if the two vectors are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="x">The first component of the second vector to compare.</param>
		/// <param name="y">The second component of the second vector to compare.</param>
		/// <param name="z">The third component of the second vector to compare.</param>
		/// <param name="w">The fourth component of the second vector to compare.</param>
		/// <returns>True if the two vectors are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Vector4 lhs, float x, float y, float z, float w)
		{
			return Mathf.Approximately(lhs.x, x) && Mathf.Approximately(lhs.y, y) && Mathf.Approximately(lhs.z, z) && Mathf.Approximately(lhs.w, w);
		}

		/// <summary>
		/// Checks if the two quaternions are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first quaternions to compare.</param>
		/// <param name="rhs">The second quaternions to compare.</param>
		/// <returns>True if the two quaternions are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Quaternion lhs, Quaternion rhs)
		{
			return Mathf.Approximately(lhs.x, rhs.x) && Mathf.Approximately(lhs.y, rhs.y) && Mathf.Approximately(lhs.z, rhs.z) && Mathf.Approximately(lhs.w, rhs.w);
		}

		/// <summary>
		/// Checks if the two quaternions are approximately equal, accounting for floating point imprecision.
		/// </summary>
		/// <param name="lhs">The first quaternions to compare.</param>
		/// <param name="x">The first component of the second quaternions to compare.</param>
		/// <param name="y">The second component of the second quaternions to compare.</param>
		/// <param name="z">The third component of the second quaternions to compare.</param>
		/// <param name="w">The fourth component of the second quaternions to compare.</param>
		/// <returns>True if the two quaternions are approximately equal, and false if they are sufficiently different.</returns>
		/// <seealso cref="Mathf.Approximately(float, float)"/>
		public static bool Approximately(this Quaternion lhs, float x, float y, float z, float w)
		{
			return Mathf.Approximately(lhs.x, x) && Mathf.Approximately(lhs.y, y) && Mathf.Approximately(lhs.z, z) && Mathf.Approximately(lhs.w, w);
		}

		#endregion

		#region Plane/Line Intersection

		/// <summary>
		/// Gets the t value of the given line at which it intersects the given plane.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <returns>The t value of the line at which it intersects the plane.</returns>
		/// <remarks><para>The position of intersection can be determined by multiplying the line's
		/// direction by the returned t value, and adding that to the line's origin.</para>
		/// <note type="caution">If the ray does not actually intersect the surface, the result of this function is undefined.
		/// Use <see cref="GetIntersectionParameter(Plane, Ray, out float)"/> instead, if this is a possibility.</note></remarks>
		public static float GetIntersectionParameter(Plane plane, Ray line)
		{
			return -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / Vector3.Dot(line.direction, plane.normal);
		}

		/// <summary>
		/// Determines if the given line intersects the given plane, and if so gets the t value of the line where that intersection occurs.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <param name="intersectionParameter">The t value of the line at which it intersects the plane.</param>
		/// <returns>True if the line intersects the plane, and false if it does not.</returns>
		/// <remarks><para>The position of intersection can be determined by multiplying the line's
		/// direction by the returned t value, and adding that to the line's origin.</para></remarks>
		public static bool GetIntersectionParameter(Plane plane, Ray line, out float intersectionParameter)
		{
			var denominator = Vector3.Dot(line.direction, plane.normal);
			if (denominator != 0f)
			{
				intersectionParameter = -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / denominator;
				return true;
			}
			else
			{
				intersectionParameter = float.NaN;
				return false;
			}
		}

		/// <summary>
		/// Gets the t value of the given line at which it intersects the given plane.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <returns>The t value of the line at which it intersects the plane.</returns>
		/// <remarks><para>The position of intersection can be determined by multiplying the line's
		/// scaled direction by the returned t value, and adding that to the line's origin.</para>
		/// <note type="caution">If the ray does not actually intersect the surface, the result of this function is undefined.
		/// Use <see cref="GetIntersectionParameter(Plane, ScaledRay, out float)"/> instead, if this is a possibility.</note></remarks>
		public static float GetIntersectionParameter(Plane plane, ScaledRay line)
		{
			return -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / Vector3.Dot(line.direction, plane.normal);
		}

		/// <summary>
		/// Determines if the given line intersects the given plane, and if so gets the t value of the line where that intersection occurs.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <param name="intersectionParameter">The t value of the line at which it intersects the plane.</param>
		/// <returns>True if the line intersects the plane, and false if it does not.</returns>
		/// <remarks><para>The position of intersection can be determined by multiplying the line's
		/// scaled direction by the returned t value, and adding that to the line's origin.</para></remarks>
		public static bool GetIntersectionParameter(Plane plane, ScaledRay line, out float intersectionParameter)
		{
			var denominator = Vector3.Dot(line.direction, plane.normal);
			if (denominator != 0f)
			{
				intersectionParameter = -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / denominator;
				return true;
			}
			else
			{
				intersectionParameter = float.NaN;
				return false;
			}
		}

		/// <summary>
		/// Gets the position at which the given line intersects the given plane.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <returns>The position at which the given line intersects the given plane.</returns>
		/// <remarks><note type="caution">If the line does not actually intersect the surface, the return value is
		/// undefined.  Use <see cref="Intersect(Plane, Ray, out Vector3)"/> instead, if this is a possibility.</note>
		/// <note type="caution">If the line points away from the plane, the return value is the intersection position
		/// of the line in the opposite direction.  To prevent this case, use <see cref="IntersectForward(Plane, Ray, out Vector3)"/>
		/// instead.</note></remarks>
		/// <seealso cref="Intersect(Plane, Ray, out Vector3)"/>
		/// <seealso cref="IntersectForward(Plane, Ray, out Vector3)"/>
		public static Vector3 Intersect(Plane plane, Ray line)
		{
			return line.direction * GetIntersectionParameter(plane, line) + line.origin;
		}

		/// <summary>
		/// Determines if the given line intersects the given plane, and if so gets the position where that intersection occurs.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <param name="intersectionPoint">The position at which the given line intersects the given plane.</param>
		/// <returns>True if the line intersects the plane, and false if it does not.</returns>
		/// <remarks><note type="caution">If the line points away from the plane, the return value is the intersection position
		/// of the line in the opposite direction.  To prevent this case, use <see cref="IntersectForward(Plane, Ray, out Vector3)"/>
		/// instead.</note></remarks>
		/// <seealso cref="IntersectForward(Plane, Ray, out Vector3)"/>
		public static bool Intersect(Plane plane, Ray line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return true;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Gets the position at which the given line intersects the given plane.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <returns>The position at which the given line intersects the given plane.</returns>
		/// <remarks><note type="caution">If the line does not actually intersect the surface, the return value is
		/// undefined.  Use <see cref="Intersect(Plane, ScaledRay, out Vector3)"/> instead, if this is a possibility.</note>
		/// <note type="caution">If the line points away from the plane, the return value is the intersection position
		/// of the line in the opposite direction.  To prevent this case, use <see cref="IntersectForward(Plane, ScaledRay, out Vector3)"/>
		/// instead.</note></remarks>
		/// <seealso cref="Intersect(Plane, ScaledRay, out Vector3)"/>
		/// <seealso cref="IntersectForward(Plane, ScaledRay, out Vector3)"/>
		public static Vector3 Intersect(Plane plane, ScaledRay line)
		{
			return line.direction * GetIntersectionParameter(plane, line) + line.origin;
		}

		/// <summary>
		/// Determines if the given line intersects the given plane, and if so gets the position where that intersection occurs.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <param name="intersectionPoint">The position at which the given line intersects the given plane.</param>
		/// <returns>True if the line intersects the plane, and false if it does not.</returns>
		/// <remarks><note type="caution">If the line points away from the plane, the return value is the intersection position
		/// of the line in the opposite direction.  To prevent this case, use <see cref="IntersectForward(Plane, ScaledRay, out Vector3)"/>
		/// instead.</note></remarks>
		/// <seealso cref="IntersectForward(Plane, ScaledRay, out Vector3)"/>
		public static bool Intersect(Plane plane, ScaledRay line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return true;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given line intersects the given plane in the forward direction, and if so gets the position where that intersection occurs.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <param name="intersectionPoint">The position at which the given line intersects the given plane.</param>
		/// <returns>True if the line intersects the plane in the forward direction of the line, and false if it does not.</returns>
		public static bool IntersectForward(Plane plane, Ray line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return intersectionParameter >= 0f;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given line intersects the given plane in the forward direction, and if so gets the position where that intersection occurs.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="line">The line to be intersected.</param>
		/// <param name="intersectionPoint">The position at which the given line intersects the given plane.</param>
		/// <returns>True if the line intersects the plane in the forward direction of the line, and false if it does not.</returns>
		public static bool IntersectForward(Plane plane, ScaledRay line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return intersectionParameter >= 0f;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Gets the position at which the line defined by the given end points intersects the given plane.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="endPoint0">The first endpoint of the line to be intersected.</param>
		/// <param name="endPoint1">The second endpoint of the line to be intersected.</param>
		/// <returns>The position at which the line intersects the plane.</returns>
		/// <remarks><note type="caution">If the line does not actually intersect the surface, the return value is
		/// undefined.  Use <see cref="Intersect(Plane, Vector3, Vector3, out Vector3)"/> instead, if this is a possibility.</note>
		/// <note type="caution">The intersection point will be returned even if it is beyond either of the end points.  To distinguish
		/// this case, use <see cref="GetIntersectionParameter(Plane, ScaledRay)"/> with an appropriately constructed ray and check for
		/// a t value outside the range [0, 1].</note></remarks>
		/// <seealso cref="Intersect(Plane, Vector3, Vector3, out Vector3)"/>
		/// <seealso cref="GetIntersectionParameter(Plane, ScaledRay)"/>
		public static Vector3 Intersect(Plane plane, Vector3 endPoint0, Vector3 endPoint1)
		{
			return Intersect(plane, new ScaledRay(endPoint0, endPoint1 - endPoint0));
		}

		/// <summary>
		/// Determines if the line defined by the given end points intersects the given plane, and if so gets the position where that intersection occurs.
		/// Gets the position at which the line defined by the given end points intersects the given plane.
		/// </summary>
		/// <param name="plane">The plane to be intersected.</param>
		/// <param name="endPoint0">The first endpoint of the line to be intersected.</param>
		/// <param name="endPoint1">The second endpoint of the line to be intersected.</param>
		/// <param name="intersectionPoint">The position at which the line intersects the plane.</param>
		/// <returns>True if the line intersects the plane, and false if it does not.</returns>
		/// <remarks><note type="caution">The intersection point will be returned even if it is beyond either of the end points.  To distinguish
		/// this case, use <see cref="GetIntersectionParameter(Plane, ScaledRay, out float)"/> with an appropriately constructed ray and check for
		/// a t value outside the range [0, 1].</note></remarks>
		/// <seealso cref="GetIntersectionParameter(Plane, ScaledRay, out float)"/>
		public static bool Intersect(Plane plane, Vector3 endPoint0, Vector3 endPoint1, out Vector3 intersectionPoint)
		{
			return Intersect(plane, new ScaledRay(endPoint0, endPoint1 - endPoint0), out intersectionPoint);
		}

		#endregion

		#region Sphere/Line Intersection

		/// <summary>
		/// Determines if the given ray intersects the given sphere, and if so gets the two t values of the line where the intersections occur.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="t0">The first t value of the line at which it intersects the plane.</param>
		/// <param name="t1">The second t value of the line at which it intersects the plane.</param>
		/// <returns>True if the line intersects the plane, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; <paramref name="t0"/>
		/// will always be less than <paramref name="t1"/> in this case.  If the ray only touches the
		/// sphere at a single tangent point, then both <paramref name="t0"/> and <paramref name="t1"/>
		/// will be equal.</para>
		/// <para>The position of intersection can be determined by multiplying the ray's direction
		/// vector by either of the returned t values, and adding that to the ray's origin.</para>
		/// </remarks>
		public static bool GetIntersectionParameters(Sphere sphere, Ray ray, out float t0, out float t1)
		{
			// Derived from the basic equation Length(d*t + p - q) = r, where d is the direction of the ray,
			// p is the origin of the ray, r is the radius of the sphere, and q is the origin of the sphere.
			// This turns into the quadratic formula, with:
			//     a = Length(d)^2 = Dot(d, d) = 1   (since a Ray direction is a unit vector)
			//     b = 2*Dot(d, p - q)
			//     c = Dot(p - q, p - q) - r^2
			// The twos and fours of the above and the quadratic formula all end up cancelling out.

			var delta = ray.origin - sphere.center;
			var directionDeltaDot = Vector3.Dot(ray.direction, delta);
			var directionDeltaSquared = directionDeltaDot * directionDeltaDot;
			var deltaLengthSquared = Vector3.Dot(delta, delta);
			var radiusSquared = sphere.radius * sphere.radius;

			var square = directionDeltaSquared - deltaLengthSquared + radiusSquared;

			if (square > 0f)
			{
				var squareRoot = Mathf.Sqrt(square);
				t0 = -directionDeltaDot - squareRoot;
				t1 = -directionDeltaDot + squareRoot;
				return true;
			}
			else if (square == 0f)
			{
				t0 = t1 = -directionDeltaDot;
				return true;
			}
			else
			{
				t0 = t1 = float.NaN;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given ray intersects the given sphere, and if so gets the two t values of the line where the intersections occur.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="t0">The first t value of the line at which it intersects the plane.</param>
		/// <param name="t1">The second t value of the line at which it intersects the plane.</param>
		/// <returns>True if the line intersects the plane, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; <paramref name="t0"/>
		/// will always be less than <paramref name="t1"/> in this case.  If the ray only touches the
		/// sphere at a single tangent point, then both <paramref name="t0"/> and <paramref name="t1"/>
		/// will be equal.</para>
		/// <para>The position of intersection can be determined by multiplying the ray's scaled direction
		/// vector by either of the returned t values, and adding that to the ray's origin.</para>
		/// </remarks>
		public static bool GetIntersectionParameters(Sphere sphere, ScaledRay ray, out float t0, out float t1)
		{
			// Derived from the basic equation Length(d*t + p - q) = r, where d is the direction of the ray,
			// p is the origin of the ray, r is the radius of the sphere, and q is the origin of the sphere.
			// This turns into the quadratic formula, with:
			//     a = Length(d)^2 = Dot(d, d)
			//     b = 2*Dot(d, p - q)
			//     c = Dot(p - q, p - q) - r^2
			// The twos and fours of the above and the quadratic formula all end up cancelling out.

			var delta = ray.origin - sphere.center;
			var directionDeltaDot = Vector3.Dot(ray.direction, delta);
			var directionDeltaSquared = directionDeltaDot * directionDeltaDot;
			var directionLengthSquared = Vector3.Dot(ray.direction, ray.direction);
			var deltaLengthSquared = Vector3.Dot(delta, delta);
			var radiusSquared = sphere.radius * sphere.radius;

			var square = directionDeltaSquared - directionLengthSquared * (deltaLengthSquared - radiusSquared);

			if (square > 0f)
			{
				var squareRoot = Mathf.Sqrt(square);
				t0 = (-directionDeltaDot - squareRoot) / directionLengthSquared;
				t1 = (-directionDeltaDot + squareRoot) / directionLengthSquared;
				return true;
			}
			else if (square == 0f)
			{
				t0 = t1 = -directionDeltaDot / directionLengthSquared;
				return true;
			}
			else
			{
				t0 = t1 = float.NaN;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given ray intersects the given sphere, and if so gets the primary position where that intersection occurs.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="intersection">The primary position at which the given ray intersects the given sphere.</param>
		/// <returns>True if the ray intersects the sphere in the ray's positive direction, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; this function will
		/// only return the first intersection in ray's positive direction.  If the ray's origin is
		/// inside the sphere, then the first intersection will in a sense be on the inside of the sphere,
		/// pointing away from it, rather than the more conventional situation of intersecting the outside
		/// of the sphere pointing into it.  If neither of the intersection points are in the positive
		/// direction, or if the ray never intersects the sphere in either direction at all, then the
		/// intersection position is undefined, and the function will return false.</para></remarks>
		/// <seealso cref="IntersectForwardExternal(Sphere, Ray, out Vector3)"/>
		/// <seealso cref="IntersectForwardInternal(Sphere, Ray, out Vector3)"/>
		public static bool Intersect(Sphere sphere, Ray ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t0 >= 0f)
				{
					intersection = ray.origin + ray.direction * t0;
					return true;
				}
				else if (t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * t1;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given ray intersects the given sphere, and if so gets the primary position where that intersection occurs.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="intersection">The primary position at which the given ray intersects the given sphere.</param>
		/// <returns>True if the ray intersects the sphere in the ray's positive direction, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; this function will
		/// only return the first intersection in ray's positive direction.  If the ray's origin is
		/// inside the sphere, then the first intersection will in a sense be on the inside of the sphere,
		/// pointing away from it, rather than the more conventional situation of intersecting the outside
		/// of the sphere pointing into it.  If neither of the intersection points are in the positive
		/// direction, or if the ray never intersects the sphere in either direction at all, then the
		/// intersection position is undefined, and the function will return false.</para></remarks>
		/// <seealso cref="IntersectForwardExternal(Sphere, ScaledRay, out Vector3)"/>
		/// <seealso cref="IntersectForwardInternal(Sphere, ScaledRay, out Vector3)"/>
		public static bool Intersect(Sphere sphere, ScaledRay ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t0 >= 0f)
				{
					intersection = ray.origin + ray.direction * t0;
					return true;
				}
				else if (t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * t1;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given ray intersects the given sphere from the outside, and if so gets the primary position where that intersection occurs.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="intersection">The primary position at which the given ray intersects the given sphere.</param>
		/// <returns>True if the ray externally intersects the sphere in the ray's positive direction, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; this function will
		/// only return the first intersection in ray's positive direction, and only if the ray's
		/// origin is is not within the sphere.  If neither of the intersection points are in the positive
		/// direction, or if the ray never intersects the sphere in either direction at all, then the
		/// intersection position is undefined, and the function will return false.</para></remarks>
		/// <seealso cref="Intersect(Sphere, Ray, out Vector3)"/>
		/// <seealso cref="IntersectForwardInternal(Sphere, Ray, out Vector3)"/>
		public static bool IntersectForwardExternal(Sphere sphere, Ray ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t0 >= 0f)
				{
					intersection = ray.origin + ray.direction * t0;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given ray intersects the given sphere from the outside, and if so gets the primary position where that intersection occurs.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="intersection">The primary position at which the given ray intersects the given sphere.</param>
		/// <returns>True if the ray externally intersects the sphere in the ray's positive direction, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; this function will
		/// only return the first intersection in ray's positive direction, and only if the ray's
		/// origin is is not within the sphere.  If neither of the intersection points are in the positive
		/// direction, or if the ray never intersects the sphere in either direction at all, then the
		/// intersection position is undefined, and the function will return false.</para></remarks>
		/// <seealso cref="Intersect(Sphere, ScaledRay, out Vector3)"/>
		/// <seealso cref="IntersectForwardInternal(Sphere, ScaledRay, out Vector3)"/>
		public static bool IntersectForwardExternal(Sphere sphere, ScaledRay ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t0 >= 0f)
				{
					intersection = ray.origin + ray.direction * t0;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given ray intersects the given sphere from the inside, and if so gets the position where that intersection occurs.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="intersection">The position at which the given ray internally intersects the given sphere.</param>
		/// <returns>True if the ray internally intersects the sphere in the ray's positive direction, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; this function will
		/// only return the last intersection in ray's positive direction, from which the ray will
		/// be pointing out of the sphere.  If neither of the intersection points are in the positive
		/// direction, or if the ray never intersects the sphere in either direction at all, then the
		/// intersection position is undefined, and the function will return false.</para></remarks>
		/// <seealso cref="Intersect(Sphere, Ray, out Vector3)"/>
		/// <seealso cref="IntersectForwardExternal(Sphere, Ray, out Vector3)"/>
		public static bool IntersectForwardInternal(Sphere sphere, Ray ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * t1;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		/// <summary>
		/// Determines if the given ray intersects the given sphere from the inside, and if so gets the position where that intersection occurs.
		/// </summary>
		/// <param name="sphere">The sphere to be intersected.</param>
		/// <param name="ray">The ray to be intersected.</param>
		/// <param name="intersection">The position at which the given ray internally intersects the given sphere.</param>
		/// <returns>True if the ray internally intersects the sphere in the ray's positive direction, and false if it does not.</returns>
		/// <remarks><para>Most of the time, there will be two intersection points along the ray,
		/// one on the front side of the sphere, and the other on the back side; this function will
		/// only return the last intersection in ray's positive direction, from which the ray will
		/// be pointing out of the sphere.  If neither of the intersection points are in the positive
		/// direction, or if the ray never intersects the sphere in either direction at all, then the
		/// intersection position is undefined, and the function will return false.</para></remarks>
		/// <seealso cref="Intersect(Sphere, ScaledRay, out Vector3)"/>
		/// <seealso cref="IntersectForwardExternal(Sphere, ScaledRay, out Vector3)"/>
		public static bool IntersectForwardInternal(Sphere sphere, ScaledRay ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * t1;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		#endregion

		#region Plane Operations

		/// <summary>
		/// Calculates the line of intersection of two planes.
		/// </summary>
		/// <param name="plane0">The first plane to intersect.  Cannot be coplanar with <paramref name="plane1"/>.</param>
		/// <param name="plane1">The second plane to intersect.  Cannot be coplanar with <paramref name="plane0"/>.</param>
		/// <returns>A line along which the two planes intersect.</returns>
		public static Ray Intersect(Plane plane0, Plane plane1)
		{
			var lineVector = Vector3.Cross(plane0.normal, plane1.normal);
			var angleCosine = Vector3.Dot(plane0.normal, plane1.normal);
			var angleSineSquared = Vector3.Dot(lineVector, lineVector);
			var component0 = (angleCosine * plane1.distance - plane0.distance) * plane0.normal;
			var component1 = (angleCosine * plane0.distance - plane1.distance) * plane1.normal;
			var lineOrigin = (component0 + component1) / angleSineSquared;
			return new Ray(lineOrigin, lineVector);
		}

		/// <summary>
		/// Calculates the point at which three planes intersect each other simultaneously.
		/// </summary>
		/// <param name="plane0">The first plane to intersect.  Cannot be coplanar with either of the other two planes.</param>
		/// <param name="plane1">The second plane to intersect.  Cannot be coplanar with either of the other two planes.</param>
		/// <param name="plane2">The third plane to intersect.  Cannot be coplanar with either of the other two planes.</param>
		/// <returns>The point at which all three planes intersect.</returns>
		public static Vector3 Intersect(Plane plane0, Plane plane1, Plane plane2)
		{
			var line = Intersect(plane0, plane1);
			return Intersect(plane2, line);
		}

		/// <summary>
		/// Creates a flipped plane that has the exact same position as the given plane, but with an opposite surface normal.
		/// </summary>
		/// <param name="plane">The plane to be flipped.</param>
		/// <returns>A flipped variant of the original plane.</returns>
		public static Plane Flip(this Plane plane)
		{
			return new Plane(-plane.normal, -plane.distance);
		}

		/// <summary>
		/// Flips the plane to have the exact same position as before, but with an opposite surface normal.
		/// </summary>
		/// <param name="plane">The plane to be flipped in place.</param>
		public static void Flip(ref Plane plane)
		{
			plane.normal = -plane.normal;
			plane.distance = -plane.distance;
		}

		/// <summary>
		/// Creates a flipped plane that has the exact same position as the given plane, but with an opposite surface normal.
		/// </summary>
		/// <param name="plane">The plane to be flipped.</param>
		/// <returns>A flipped variant of the original plane.</returns>
		public static SerializablePlane Flip(this SerializablePlane plane)
		{
			return new SerializablePlane(-plane.normal, -plane.distance);
		}

		/// <summary>
		/// Flips the plane to have the exact same position as before, but with an opposite surface normal.
		/// </summary>
		/// <param name="plane">The plane to be flipped in place.</param>
		public static void Flip(ref SerializablePlane plane)
		{
			plane.normal = -plane.normal;
			plane.distance = -plane.distance;
		}

		/// <summary>
		/// Creates a shifted plane, with the same surface normal but offset from the original plane by a given distance.
		/// </summary>
		/// <param name="plane">The plane to be shifted.</param>
		/// <param name="distance">The distance by which to shift the plane.</param>
		/// <returns>A shifted variant of the original plane.</returns>
		public static Plane Shift(this Plane plane, float distance)
		{
			return new Plane(plane.normal, plane.distance - distance);
		}

		/// <summary>
		/// Creates a shifted plane, with the same surface normal but offset from the original plane by a given distance.
		/// </summary>
		/// <param name="plane">The plane to be shifted.</param>
		/// <param name="distance">The distance by which to shift the plane.</param>
		/// <returns>A shifted variant of the original plane.</returns>
		public static SerializablePlane Shift(this SerializablePlane plane, float distance)
		{
			return new SerializablePlane(plane.normal, plane.distance - distance);
		}

		#endregion

		#region Ray Operations

		/// <summary>
		/// Transforms a ray according to the scale, translation, and orientation properties of the transform object.
		/// </summary>
		/// <param name="transform">The transform whose scale, translation, and orientation properties are to be used to transform the ray.</param>
		/// <param name="ray">The ray to be transformed.</param>
		/// <returns>A transformed ray.</returns>
		public static Ray TransformRay(this Transform transform, Ray ray)
		{
			return new Ray(
				transform.TransformPoint(ray.origin),
				transform.TransformDirection(ray.direction));
		}

		/// <summary>
		/// Transforms a ray according to the scale, translation, and orientation properties of the transform object.
		/// </summary>
		/// <param name="transform">The transform whose scale, translation, and orientation properties are to be used to transform the ray.</param>
		/// <param name="ray">The ray to be transformed.</param>
		/// <returns>A transformed ray.</returns>
		public static ScaledRay TransformRay(this Transform transform, ScaledRay ray)
		{
			return new ScaledRay(
				transform.TransformPoint(ray.origin),
				transform.TransformVector(ray.direction));
		}

		/// <summary>
		/// Inversely transforms a ray according to the scale, translation, and orientation properties of the transform object.
		/// </summary>
		/// <param name="transform">The transform whose scale, translation, and orientation properties are to be used to transform the ray.</param>
		/// <param name="ray">The ray to be transformed.</param>
		/// <returns>A transformed ray.</returns>
		public static Ray InverseTransformRay(this Transform transform, Ray ray)
		{
			return new Ray(
				transform.InverseTransformPoint(ray.origin),
				transform.InverseTransformDirection(ray.direction));
		}

		/// <summary>
		/// Inversely transforms a ray according to the scale, translation, and orientation properties of the transform object.
		/// </summary>
		/// <param name="transform">The transform whose scale, translation, and orientation properties are to be used to transform the ray.</param>
		/// <param name="ray">The ray to be transformed.</param>
		/// <returns>A transformed ray.</returns>
		public static ScaledRay InverseTransformRay(this Transform transform, ScaledRay ray)
		{
			return new ScaledRay(
				transform.InverseTransformPoint(ray.origin),
				transform.InverseTransformVector(ray.direction));
		}

		#endregion

		#region Cuboid Operations

		/// <summary>
		/// Finds the plane whose surface normal most closely matches the normal vector specified.
		/// </summary>
		/// <param name="normal">The normal vector to compare to the surface normals of the provided planes.</param>
		/// <param name="planes">The planes whose surface normals are to be compared to the provided normal vector.</param>
		/// <returns>The array index of the first plane whose surface normal most clostly matches the normal vector.</returns>
		public static int FindMatchingPlane(Vector3 normal, Plane[] planes)
		{
			int bestIndex = -1;
			float bestCosine = float.NegativeInfinity;

			for (int i = 0; i < planes.Length; ++i)
			{
				var cosine = Vector3.Dot(normal, planes[i].normal);
				if (cosine > bestCosine)
				{
					bestIndex = i;
					bestCosine = cosine;
				}
			}

			return bestIndex;
		}

		/// <summary>
		/// Finds the eight corners of the cuboid defined by the planes provided, and organizes them according to the orthonormal basis defined by the three given vectors.
		/// </summary>
		/// <param name="right">The right vector of the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="up">The up vector of the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="forward">The forward vector of the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="planes">The planes defining the cuboid.  Must be an array of six planes.</param>
		/// <returns>An array of the eight corners of the cuboid.</returns>
		/// <remarks>
		/// <para>Relative to the orthonormal basis provided, the corners returned are as follows:</para>
		/// <list type="bullet">
		///     <item>[0] = near-left-lower</item>
		///     <item>[1] = near-right-lower</item>
		///     <item>[2] = near-left-upper</item>
		///     <item>[3] = near-right-upper</item>
		///     <item>[4] = far-left-lower</item>
		///     <item>[5] = far-right-lower</item>
		///     <item>[6] = far-left-upper</item>
		///     <item>[7] = far-right-upper</item>
		/// </list>
		/// </remarks>
		public static Vector3[] FindCuboidCorners(Vector3 right, Vector3 up, Vector3 forward, Plane[] planes)
		{
			return FindCuboidCorners(right, up, forward, planes, new Vector3[8]);
		}

		/// <summary>
		/// Finds the eight corners of the cuboid defined by the planes provided, and organizes them according to the orthonormal basis defined by the three given vectors.
		/// </summary>
		/// <param name="right">The right vector of the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="up">The up vector of the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="forward">The forward vector of the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="planes">The planes defining the cuboid.  Must be an array of six planes.</param>
		/// <param name="corners">A pre-allocated array to which the eight corners will be stored.</param>
		/// <returns>The array of eight corners of the cuboid.</returns>
		/// <remarks>
		/// <para>Relative to the orthonormal basis provided, the corners returned are as follows:</para>
		/// <list type="bullet">
		///     <item>[0] = near-left-lower</item>
		///     <item>[1] = near-right-lower</item>
		///     <item>[2] = near-left-upper</item>
		///     <item>[3] = near-right-upper</item>
		///     <item>[4] = far-left-lower</item>
		///     <item>[5] = far-right-lower</item>
		///     <item>[6] = far-left-upper</item>
		///     <item>[7] = far-right-upper</item>
		/// </list>
		/// </remarks>
		public static Vector3[] FindCuboidCorners(Vector3 right, Vector3 up, Vector3 forward, Plane[] planes, Vector3[] corners)
		{
			var rightPlane = planes[FindMatchingPlane(-right, planes)];
			var leftPlane = planes[FindMatchingPlane(right, planes)];
			var upperPlane = planes[FindMatchingPlane(-up, planes)];
			var lowerPlane = planes[FindMatchingPlane(up, planes)];
			var farPlane = planes[FindMatchingPlane(-forward, planes)];
			var nearPlane = planes[FindMatchingPlane(forward, planes)];

			var lineLeftLower = Intersect(lowerPlane, leftPlane);
			var lineRightLower = Intersect(lowerPlane, rightPlane);
			var lineLeftUpper = Intersect(upperPlane, leftPlane);
			var lineRightUpper = Intersect(upperPlane, rightPlane);

			corners[0] = Intersect(nearPlane, lineLeftLower);
			corners[1] = Intersect(nearPlane, lineRightLower);
			corners[2] = Intersect(nearPlane, lineLeftUpper);
			corners[3] = Intersect(nearPlane, lineRightUpper);
			corners[4] = Intersect(farPlane, lineLeftLower);
			corners[5] = Intersect(farPlane, lineRightLower);
			corners[6] = Intersect(farPlane, lineLeftUpper);
			corners[7] = Intersect(farPlane, lineRightUpper);

			return corners;
		}

		/// <summary>
		/// Finds the eight corners of the cuboid defined by the planes provided, and organizes them according to the orthonormal basis of the given camera.
		/// </summary>
		/// <param name="camera">The camera defining the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="planes">The planes defining the cuboid.  Must be an array of six planes.</param>
		/// <returns>An array of the eight corners of the cuboid.</returns>
		/// <remarks>
		/// <para>Relative to the orthonormal basis of the given camera, the corners returned are as follows:</para>
		/// <list type="bullet">
		///     <item>[0] = near-left-lower</item>
		///     <item>[1] = near-right-lower</item>
		///     <item>[2] = near-left-upper</item>
		///     <item>[3] = near-right-upper</item>
		///     <item>[4] = far-left-lower</item>
		///     <item>[5] = far-right-lower</item>
		///     <item>[6] = far-left-upper</item>
		///     <item>[7] = far-right-upper</item>
		/// </list>
		/// </remarks>
		public static Vector3[] FindFrustumCorners(Camera camera, Plane[] planes)
		{
			return FindFrustumCorners(camera, planes, new Vector3[8]);
		}

		/// <summary>
		/// Finds the eight corners of the cuboid defined by the planes provided, and organizes them according to the orthonormal basis of the given camera.
		/// </summary>
		/// <param name="camera">The camera defining the orthonormal basis used for organizing the returned corners.</param>
		/// <param name="planes">The planes defining the cuboid.  Must be an array of six planes.</param>
		/// <param name="corners">A pre-allocated array to which the eight corners will be stored.</param>
		/// <returns>The array of eight corners of the cuboid.</returns>
		/// <remarks>
		/// <para>Relative to the orthonormal basis of the given camera, the corners returned are as follows:</para>
		/// <list type="bullet">
		///     <item>[0] = near-left-lower</item>
		///     <item>[1] = near-right-lower</item>
		///     <item>[2] = near-left-upper</item>
		///     <item>[3] = near-right-upper</item>
		///     <item>[4] = far-left-lower</item>
		///     <item>[5] = far-right-lower</item>
		///     <item>[6] = far-left-upper</item>
		///     <item>[7] = far-right-upper</item>
		/// </list>
		/// </remarks>
		public static Vector3[] FindFrustumCorners(Camera camera, Plane[] planes, Vector3[] corners)
		{
			return FindCuboidCorners(camera.transform.right, camera.transform.up, camera.transform.forward, planes, corners);
		}

		#endregion

		#region Bounding Operations

		/// <summary>
		/// Finds the approximate minimum bounding sphere around the given point cloud.
		/// </summary>
		/// <param name="points">The points around which a bounding sphere will be computed.</param>
		/// <returns>A bounding sphere around the points, approximating the smallest bounding sphere possible.</returns>
		public static Sphere FindBoundingSphere(Vector3[] points)
		{
			var p0 = points[0];
			var p1 = points[1];
			var center = (p0 + p1) * 0.5f;
			var delta = p0 - p1;
			var radiusSquared = delta.sqrMagnitude * 0.25f;
			var radius = Mathf.Sqrt(radiusSquared);

			for (int i = 2; i < points.Length; ++i)
			{
				delta = points[i] - center;
				var distanceSquared = delta.sqrMagnitude;
				if (distanceSquared > radiusSquared)
				{
					var distance = Mathf.Sqrt(distanceSquared);
					var excess = distance - radius;
					center += delta * (excess * 0.5f / distance);
				}
			}

			return new Sphere(center, radius);
		}

		/// <summary>
		/// Returns a vector composed of the per-component minimum values of the input vectors.
		/// </summary>
		/// <param name="lhs">The first vector from which minimum components will be derived.</param>
		/// <param name="rhs">The second vector from which minimum components will be derived.</param>
		/// <returns>A vector composed of the per-component minimum values of the input vectors.</returns>
		public static Vector2 AxisAlignedMin(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(
				Mathf.Min(lhs.x, rhs.x),
				Mathf.Min(lhs.y, rhs.y));
		}

		/// <summary>
		/// Returns a vector composed of the per-component minimum values of the input vectors.
		/// </summary>
		/// <param name="lhs">The first vector from which minimum components will be derived.</param>
		/// <param name="rhs">The second vector from which minimum components will be derived.</param>
		/// <returns>A vector composed of the per-component minimum values of the input vectors.</returns>
		public static Vector3 AxisAlignedMin(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				Mathf.Min(lhs.x, rhs.x),
				Mathf.Min(lhs.y, rhs.y),
				Mathf.Min(lhs.z, rhs.z));
		}

		/// <summary>
		/// Returns a vector composed of the per-component minimum values of the input vectors.
		/// </summary>
		/// <param name="lhs">The first vector from which minimum components will be derived.</param>
		/// <param name="rhs">The second vector from which minimum components will be derived.</param>
		/// <returns>A vector composed of the per-component minimum values of the input vectors.</returns>
		public static Vector4 AxisAlignedMin(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(
				Mathf.Min(lhs.x, rhs.x),
				Mathf.Min(lhs.y, rhs.y),
				Mathf.Min(lhs.z, rhs.z),
				Mathf.Min(lhs.w, rhs.w));
		}

		/// <summary>
		/// Returns a vector composed of the per-component minimum values of the input vectors.
		/// </summary>
		/// <param name="vectors">The array of vectors from which minimum components will be derived.</param>
		/// <returns>A vector composed of the per-component minimum values of the input vectors.</returns>
		public static Vector2 AxisAlignedMin(params Vector2[] vectors)
		{
			var min = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				min = AxisAlignedMin(min, vectors[i]);
			}
			return min;
		}

		/// <summary>
		/// Returns a vector composed of the per-component minimum values of the input vectors.
		/// </summary>
		/// <param name="vectors">The array of vectors from which minimum components will be derived.</param>
		/// <returns>A vector composed of the per-component minimum values of the input vectors.</returns>
		public static Vector3 AxisAlignedMin(params Vector3[] vectors)
		{
			var min = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				min = AxisAlignedMin(min, vectors[i]);
			}
			return min;
		}

		/// <summary>
		/// Returns a vector composed of the per-component minimum values of the input vectors.
		/// </summary>
		/// <param name="vectors">The array of vectors from which minimum components will be derived.</param>
		/// <returns>A vector composed of the per-component minimum values of the input vectors.</returns>
		public static Vector4 AxisAlignedMin(params Vector4[] vectors)
		{
			var min = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				min = AxisAlignedMin(min, vectors[i]);
			}
			return min;
		}

		/// <summary>
		/// Returns a vector composed of the per-component maximum values of the input vectors.
		/// </summary>
		/// <param name="lhs">The first vector from which maximum components will be derived.</param>
		/// <param name="rhs">The second vector from which maximum components will be derived.</param>
		/// <returns>A vector composed of the per-component maximum values of the input vectors.</returns>
		public static Vector2 AxisAlignedMax(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(
				Mathf.Max(lhs.x, rhs.x),
				Mathf.Max(lhs.y, rhs.y));
		}

		/// <summary>
		/// Returns a vector composed of the per-component maximum values of the input vectors.
		/// </summary>
		/// <param name="lhs">The first vector from which maximum components will be derived.</param>
		/// <param name="rhs">The second vector from which maximum components will be derived.</param>
		/// <returns>A vector composed of the per-component maximum values of the input vectors.</returns>
		public static Vector3 AxisAlignedMax(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				Mathf.Max(lhs.x, rhs.x),
				Mathf.Max(lhs.y, rhs.y),
				Mathf.Max(lhs.z, rhs.z));
		}

		/// <summary>
		/// Returns a vector composed of the per-component maximum values of the input vectors.
		/// </summary>
		/// <param name="lhs">The first vector from which maximum components will be derived.</param>
		/// <param name="rhs">The second vector from which maximum components will be derived.</param>
		/// <returns>A vector composed of the per-component maximum values of the input vectors.</returns>
		public static Vector4 AxisAlignedMax(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(
				Mathf.Max(lhs.x, rhs.x),
				Mathf.Max(lhs.y, rhs.y),
				Mathf.Max(lhs.z, rhs.z),
				Mathf.Max(lhs.w, rhs.w));
		}

		/// <summary>
		/// Returns a vector composed of the per-component maximum values of the input vectors.
		/// </summary>
		/// <param name="vectors">The array of vectors from which maximum components will be derived.</param>
		/// <returns>A vector composed of the per-component maximum values of the input vectors.</returns>
		public static Vector2 AxisAlignedMax(params Vector2[] vectors)
		{
			var max = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				max = AxisAlignedMax(max, vectors[i]);
			}
			return max;
		}

		/// <summary>
		/// Returns a vector composed of the per-component maximum values of the input vectors.
		/// </summary>
		/// <param name="vectors">The array of vectors from which maximum components will be derived.</param>
		/// <returns>A vector composed of the per-component maximum values of the input vectors.</returns>
		public static Vector3 AxisAlignedMax(params Vector3[] vectors)
		{
			var max = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				max = AxisAlignedMax(max, vectors[i]);
			}
			return max;
		}

		/// <summary>
		/// Returns a vector composed of the per-component maximum values of the input vectors.
		/// </summary>
		/// <param name="vectors">The array of vectors from which maximum components will be derived.</param>
		/// <returns>A vector composed of the per-component maximum values of the input vectors.</returns>
		public static Vector4 AxisAlignedMax(params Vector4[] vectors)
		{
			var max = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				max = AxisAlignedMax(max, vectors[i]);
			}
			return max;
		}

		/// <summary>
		/// Truncates a line segment by subtracting the given plane.
		/// </summary>
		/// <param name="ray">The ray defining the overall line.</param>
		/// <param name="plane">The plane to subtract from the line segment.</param>
		/// <param name="minT">The t value of the first end point of the line segment.</param>
		/// <param name="maxT">The t value of the second end point of the line segment.</param>
		/// <returns>The parameterized length of the line segment after the truncation.</returns>
		public static float TruncateLineSegment(ScaledRay ray, Plane plane, ref float minT, ref float maxT)
		{
			var denominator = Vector3.Dot(plane.normal, ray.direction);
			if (denominator > 0f)
			{
				var intersectionT = (Vector3.Dot(plane.normal, ray.origin) - plane.distance) / denominator;
				minT = Mathf.Min(minT, intersectionT);
			}
			else if (denominator < 0f)
			{
				var intersectionT = (Vector3.Dot(plane.normal, ray.origin) - plane.distance) / denominator;
				maxT = Mathf.Min(maxT, intersectionT);
			}
			else if (!plane.GetSide(ray.origin))
			{
				maxT = minT;
			}

			return maxT - minT;
		}

		/// <summary>
		/// Determines if all the points provided are above the specified plane.
		/// </summary>
		/// <param name="points">The points to compare to the plane.</param>
		/// <param name="plane">The plane to which the points will be compared.</param>
		/// <returns>True if all points are strictly above the plane, and false if at least one is exactly on the surface of or below the plane.</returns>
		public static bool AllAreAbove(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) <= 0f) return false;
			}
			return true;
		}

		/// <summary>
		/// Determines if any of the points provided are above the specified plane.
		/// </summary>
		/// <param name="points">The points to compare to the plane.</param>
		/// <param name="plane">The plane to which the points will be compared.</param>
		/// <returns>True if any point is strictly above the plane, and false if all are exactly on the surface of or below the plane.</returns>
		public static bool AnyAreAbove(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) > 0f) return true;
			}
			return false;
		}

		/// <summary>
		/// Determines if all the points provided are below the specified plane.
		/// </summary>
		/// <param name="points">The points to compare to the plane.</param>
		/// <param name="plane">The plane to which the points will be compared.</param>
		/// <returns>True if all points are strictly below the plane, and false if at least one is exactly on the surface of or above the plane.</returns>
		public static bool AllAreBelow(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) >= 0f) return false;
			}
			return true;
		}

		/// <summary>
		/// Determines if any of the points provided are below the specified plane.
		/// </summary>
		/// <param name="points">The points to compare to the plane.</param>
		/// <param name="plane">The plane to which the points will be compared.</param>
		/// <returns>True if any point is strictly below the plane, and false if all are exactly on the surface of or above the plane.</returns>
		public static bool AnyAreBelow(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) < 0f) return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the minimum corner of the given axis-aligned bounding box, relative to the given normal vector.
		/// </summary>
		/// <param name="box">The bounding box whose relative minimum corner is to be determined.</param>
		/// <param name="normal">The normal vector that points roughly in the opposite direction of the minimum corner from the bounding box center.</param>
		/// <returns>The relative minimum corner of the bounding box.</returns>
		public static Vector3 GetMinRelativeToNormal(this Bounds box, Vector3 normal)
		{
			return new Vector3(
				(normal.x >= 0) ? box.min.x : box.max.x,
				(normal.y >= 0) ? box.min.y : box.max.y,
				(normal.z >= 0) ? box.min.z : box.max.z);
		}

		/// <summary>
		/// Gets the maximum corner of the given axis-aligned bounding box, relative to the given normal vector.
		/// </summary>
		/// <param name="box">The bounding box whose relative maximum corner is to be determined.</param>
		/// <param name="normal">The normal vector that points roughly in the same direction as the maximum corner from the bounding box center.</param>
		/// <returns>The relative maximum corner of the bounding box.</returns>
		public static Vector3 GetMaxRelativeToNormal(this Bounds box, Vector3 normal)
		{
			return new Vector3(
				(normal.x >= 0) ? box.max.x : box.min.x,
				(normal.y >= 0) ? box.max.y : box.min.y,
				(normal.z >= 0) ? box.max.z : box.min.z);
		}

		/// <summary>
		/// Gets both the minimum and maximum corners of the given axis-aligned bounding box, relative to the given normal vector.
		/// </summary>
		/// <param name="box">The bounding box whose relative minimum and maximum corners are to be determined.</param>
		/// <param name="normal">The normal vector that points roughly in the same direction as the maximum corner from the bounding box center.</param>
		/// <param name="min">The relative minimum corner of the bounding box.</param>
		/// <param name="max">The relative maximum corner of the bounding box.</param>
		public static void GetMinMaxRelativeToNormal(this Bounds box, Vector3 normal, out Vector3 min, out Vector3 max)
		{
			min = box.min;
			max = box.max;
			if (normal.x < 0) GeneralUtility.Swap(ref min.x, ref max.x);
			if (normal.y < 0) GeneralUtility.Swap(ref min.y, ref max.y);
			if (normal.z < 0) GeneralUtility.Swap(ref min.z, ref max.z);
		}

		/// <summary>
		/// Determines if the given bounding box and plane intersect or at least touch.
		/// </summary>
		/// <param name="box">The bounding box to check for intersection.</param>
		/// <param name="plane">The plane to check for intersection.</param>
		/// <returns>True if the bounding box and plane intersect or touch each other, and false if they do not.</returns>
		/// <remarks><para>This function treats the plane as a true plane of zero thickness, not as a volume half space.
		/// Thus, a bounding box that is entirely below the plane is not considered to be intersecting the plane, just as
		/// a bounding box entirely above it would not be either.</para></remarks>
		public static bool IntersectsOrTouches(this Bounds box, Plane plane)
		{
			Vector3 min, max;
			box.GetMinMaxRelativeToNormal(plane.normal, out min, out max);
			return plane.GetDistanceToPoint(min) <= 0f && plane.GetDistanceToPoint(max) >= 0f;
		}

		/// <summary>
		/// Determines if the given bounding box and plane fully intersect each other, and do not merely touch.
		/// </summary>
		/// <param name="box">The bounding box to check for intersection.</param>
		/// <param name="plane">The plane to check for intersection.</param>
		/// <returns>True if the bounding box and plane fully intersect each other, and false if they do not intersect at all or merely touch each other.</returns>
		/// <remarks><para>This function treats the plane as a true plane of zero thickness, not as a volume half space.
		/// Thus, a bounding box that is entirely below the plane is not considered to be intersecting the plane, just as
		/// a bounding box entirely above it would not be either.</para>
		/// <para>By not allowing a mere touch between the two objects, this function is essentially requiring that the
		/// plane divide the bounding box into two segments, both of which have non-zero volumes.</para></remarks>
		public static bool Intersects(this Bounds box, Plane plane)
		{
			Vector3 min, max;
			box.GetMinMaxRelativeToNormal(plane.normal, out min, out max);
			return plane.GetDistanceToPoint(min) < 0f && plane.GetDistanceToPoint(max) > 0f;
		}

		/// <summary>
		/// Determines if the given bounding box is entirely above and does not even touch the given plane.
		/// </summary>
		/// <param name="box">The bounding box to compare against the plane.</param>
		/// <param name="plane">The plane to which the bounding box will be compared.</param>
		/// <returns>True if the bounding box is entirely above the plane, and false if they touch or if any part of the bounding box is below the plane.</returns>
		public static bool IsAbove(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMinRelativeToNormal(plane.normal)) > 0f;
		}

		/// <summary>
		/// Determines if the given bounding box is entirely above or at most touches the given plane from the upper side.
		/// </summary>
		/// <param name="box">The bounding box to compare against the plane.</param>
		/// <param name="plane">The plane to which the bounding box will be compared.</param>
		/// <returns>True if the bounding box is entirely above or at most touches the plane from the upper side, and false if any part of the bounding box is below the plane.</returns>
		public static bool IsAboveOrTouches(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMinRelativeToNormal(plane.normal)) >= 0f;
		}

		/// <summary>
		/// Determines if the given bounding box is at least partially above the given plane.
		/// </summary>
		/// <param name="box">The bounding box to compare against the plane.</param>
		/// <param name="plane">The plane to which the bounding box will be compared.</param>
		/// <returns>True if any part of the bounding box is above the plane, and false if the bounding box is entirely below or at most touches the plane from the lower side.</returns>
		public static bool IsAboveOrIntersects(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMaxRelativeToNormal(plane.normal)) > 0f;
		}

		/// <summary>
		/// Determines if the given bounding box is entirely below and does not even touch the given plane.
		/// </summary>
		/// <param name="box">The bounding box to compare against the plane.</param>
		/// <param name="plane">The plane to which the bounding box will be compared.</param>
		/// <returns>True if the bounding box is entirely below the plane, and false if they touch or if any part of the bounding box is above the plane.</returns>
		public static bool IsBelow(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMaxRelativeToNormal(plane.normal)) < 0f;
		}

		/// <summary>
		/// Determines if the given bounding box is entirely below or at most touches the given plane from the lower side.
		/// </summary>
		/// <param name="box">The bounding box to compare against the plane.</param>
		/// <param name="plane">The plane to which the bounding box will be compared.</param>
		/// <returns>True if the bounding box is entirely below or at most touches the plane from the lower side, and false if any part of the bounding box is above the plane.</returns>
		public static bool IsBelowOrTouches(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMaxRelativeToNormal(plane.normal)) <= 0f;
		}

		/// <summary>
		/// Determines if the given bounding box is at least partially below the given plane.
		/// </summary>
		/// <param name="box">The bounding box to compare against the plane.</param>
		/// <param name="plane">The plane to which the bounding box will be compared.</param>
		/// <returns>True if any part of the bounding box is below the plane, and false if the bounding box is entirely above or at most touches the plane from the upper side.</returns>
		public static bool IsBelowOrIntersects(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMinRelativeToNormal(plane.normal)) < 0f;
		}

		#endregion
	}
}
