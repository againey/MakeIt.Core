/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.Numerics
{
	/// <summary>
	/// A ray with a direction vector that does not need to be a unit vector.
	/// </summary>
	/// <remarks>
	/// <para><see cref="UnityEngine.Ray2D"/> requires that its direction vector be a unit vector.  Not only does
	/// this result in a performance cost when constructing a ray (the provided direction must be normalized, just
	/// in case), but it also precludes the use of a ray to represent a ray that propagates at a particular velocity,
	/// or a parametric line that has endpoints at t = 0 and t = 1.  Additionally, it is not marked as serializable,
	/// and thus cannot be used directly as a serialized field in a class.  The scaled ray solves all of these
	/// problems, at the expense of not guaranteeing a unit vector direction.</para>
	/// </remarks>
	[Serializable]
	public struct ScaledRay2D : IEquatable<ScaledRay2D>, IComparable<ScaledRay2D>
	{
		/// <summary>
		/// The origin point from which the ray is emitted.
		/// </summary>
		public Vector2 origin;

		/// <summary>
		/// The direction vector in which the ray points, and which can be something other than a unit vector.
		/// </summary>
		public Vector2 direction;

		/// <summary>
		/// Constructs a scaled ray with the given direction vector, emitted from the world origin at (0, 0).
		/// </summary>
		/// <param name="direction">The direction of the scaled ray.  Will not be scaled to become a unit vector.</param>
		public ScaledRay2D(Vector2 direction)
		{
			origin = Vector2.zero;
			this.direction = direction;
		}

		/// <summary>
		/// Constructs a scaled ray with the given direction vector, emitted from the given origin.
		/// </summary>
		/// <param name="origin">The origin of the scaled ray.</param>
		/// <param name="direction">The direction of the scaled ray.  Will not be scaled to become a unit vector.</param>
		public ScaledRay2D(Vector2 origin, Vector2 direction)
		{
			this.origin = origin;
			this.direction = direction;
		}

		/// <summary>
		/// Converts the given ray to a scaled ray.
		/// </summary>
		/// <param name="ray">The ray to be converted.</param>
		/// <returns>The converted scaled ray.</returns>
		/// <remarks><para>Since the ray to be converted has more rigid requirements on its state,
		/// the conversion is nothing more than a simple assignment of fields.</para></remarks>
		public static implicit operator ScaledRay2D(Ray2D ray)
		{
			return new ScaledRay2D(ray.origin, ray.direction);
		}

		/// <summary>
		/// Converts the given scaled ray to a ray with a unit vector direction.
		/// </summary>
		/// <param name="ray">The scaled ray to be converted.</param>
		/// <returns>The converted Ray.</returns>
		/// <remarks><para>Since the ray to be converted has less rigid requirements on its state,
		/// the conversion might include a normalizatio of the ray direction.</para></remarks>
		public static explicit operator Ray2D(ScaledRay2D ray)
		{
			return new Ray2D(ray.origin, ray.direction);
		}

		/// <summary>
		/// Get a point that lies a given scaled distance along a ray.
		/// </summary>
		/// <param name="scaledDistance">Scaled distance of the desired point along the path of the ray.</param>
		/// <returns>The point that is <paramref name="scaledDistance"/> units of the ray's scaled direction offset from the ray's origin.</returns>
		public Vector2 GetPoint(float scaledDistance)
		{
			return origin + direction * scaledDistance;
		}

		/// <summary>
		/// Get a point that lies a given distance along a ray.
		/// </summary>
		/// <param name="scaledDistance">Distance of the desired point along the path of the ray.</param>
		/// <returns>The point that is <paramref name="distance"/> away from the the ray's origin along the ray's normalized direction.</returns>
		public Vector2 GetPointUnscaled(float distance)
		{
			return origin + direction.normalized * distance;
		}

		/// <summary>
		/// Compares the current scaled ray to the supplied scaled ray, using a lexicographical ordering.
		/// </summary>
		/// <param name="other">The scaled ray to be compared to the current scaled ray.</param>
		/// <returns>Returns -1 if the current scaled ray comes before the supplied scaled ray in the lexicographical ordering,
		/// +1 if it comes after the supplied scaled ray, and 0 if they are equal.</returns>
		/// <remarks><para>The lexicographical ordering proceeds starting with the later components and moving toward
		/// the earlier components last, and checks origin before direction.</para></remarks>
		public int CompareTo(ScaledRay2D other)
		{
			if (origin.y != other.origin.y) return (origin.y < other.origin.y) ? -1 : +1;
			if (origin.x != other.origin.x) return (origin.x < other.origin.x) ? -1 : +1;
			if (direction.y != other.direction.y) return (direction.y < other.direction.y) ? -1 : +1;
			if (direction.x != other.direction.x) return (direction.x < other.direction.x) ? -1 : +1;
			return 0;
		}

		/// <summary>
		/// Compares the current scaled ray to the supplied scaled ray to find if they are equal.
		/// </summary>
		/// <param name="other">The scaled ray to be compared to the current scaled ray.</param>
		/// <returns>Returns true if the two scaled rays are equal, and false otherwise.</returns>
		public bool Equals(ScaledRay2D other)
		{
			return this == other;
		}

		/// <summary>
		/// Compares the current scaled ray to the supplied object to find if they are equal.
		/// </summary>
		/// <param name="obj">The object to be compared to the current scaled ray.</param>
		/// <returns>Returns true if the supplied object is an instance of ScaledRay and equal to the current scaled ray, and false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return obj is ScaledRay2D && this == (ScaledRay2D)obj;
		}

		/// <summary>
		/// Calculates a 32-bit integer hash code for the current scaled ray.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code based on the component values of the current scaled ray.</returns>
		public override int GetHashCode()
		{
			return origin.GetHashCode() ^ direction.GetHashCode();
		}

		/// <summary>
		/// Compares two scaled rays for equality.
		/// </summary>
		/// <param name="lhs">The first scaled ray to compare.</param>
		/// <param name="rhs">The second scaled ray to compare.</param>
		/// <returns>Returns true if the two scaled rays are equal, and false otherwise.</returns>
		public static bool operator ==(ScaledRay2D lhs, ScaledRay2D rhs) { return lhs.origin == rhs.origin && lhs.direction == rhs.direction; }

		/// <summary>
		/// Compares two scaled rays for inequality.
		/// </summary>
		/// <param name="lhs">The first scaled ray to compare.</param>
		/// <param name="rhs">The second scaled ray to compare.</param>
		/// <returns>Returns true if the two scaled rays are not equal, and false otherwise.</returns>
		public static bool operator !=(ScaledRay2D lhs, ScaledRay2D rhs) { return lhs.origin != rhs.origin || lhs.direction != rhs.direction; }

		/// <summary>
		/// Compares two scaled rays to find if the first is lexicographically less than the second scaled ray.
		/// </summary>
		/// <param name="lhs">The first scaled ray to compare.</param>
		/// <param name="rhs">The second scaled ray to compare.</param>
		/// <returns>True if the first scaled ray is lexicographically less than the second scaled ray, and false otherwise.</returns>
		/// <seealso cref="CompareTo(ScaledRay2D)"/>
		public static bool operator < (ScaledRay2D lhs, ScaledRay2D rhs)
		{
			if (lhs.origin.y != rhs.origin.y) return lhs.origin.y < rhs.origin.y;
			if (lhs.origin.x != rhs.origin.x) return lhs.origin.x < rhs.origin.x;
			if (lhs.direction.y != rhs.direction.y) return lhs.direction.y < rhs.direction.y;
			return lhs.direction.x < rhs.direction.x;
		}

		/// <summary>
		/// Compares two scaled rays to find if the first is lexicographically less than or equal to the second scaled ray.
		/// </summary>
		/// <param name="lhs">The first scaled ray to compare.</param>
		/// <param name="rhs">The second scaled ray to compare.</param>
		/// <returns>True if the first scaled ray is lexicographically less than or equal to the second scaled ray, and false otherwise.</returns>
		/// <seealso cref="CompareTo(ScaledRay2D)"/>
		public static bool operator <=(ScaledRay2D lhs, ScaledRay2D rhs)
		{
			if (lhs.origin.y != rhs.origin.y) return lhs.origin.y < rhs.origin.y;
			if (lhs.origin.x != rhs.origin.x) return lhs.origin.x < rhs.origin.x;
			if (lhs.direction.y != rhs.direction.y) return lhs.direction.y < rhs.direction.y;
			return lhs.direction.x <= rhs.direction.x;
		}

		/// <summary>
		/// Compares two scaled rays to find if the first is lexicographically greater than the second scaled ray.
		/// </summary>
		/// <param name="lhs">The first scaled ray to compare.</param>
		/// <param name="rhs">The second scaled ray to compare.</param>
		/// <returns>True if the first scaled ray is lexicographically greater than the second scaled ray, and false otherwise.</returns>
		/// <seealso cref="CompareTo(ScaledRay2D)"/>
		public static bool operator > (ScaledRay2D lhs, ScaledRay2D rhs)
		{
			if (lhs.origin.y != rhs.origin.y) return lhs.origin.y > rhs.origin.y;
			if (lhs.origin.x != rhs.origin.x) return lhs.origin.x > rhs.origin.x;
			if (lhs.direction.y != rhs.direction.y) return lhs.direction.y > rhs.direction.y;
			return lhs.direction.x > rhs.direction.x;
		}

		/// <summary>
		/// Compares two scaled rays to find if the first is lexicographically greater than or equal to the second scaled ray.
		/// </summary>
		/// <param name="lhs">The first scaled ray to compare.</param>
		/// <param name="rhs">The second scaled ray to compare.</param>
		/// <returns>True if the first scaled ray is lexicographically greater than or equal to the second scaled ray, and false otherwise.</returns>
		/// <seealso cref="CompareTo(ScaledRay2D)"/>
		public static bool operator >=(ScaledRay2D lhs, ScaledRay2D rhs)
		{
			if (lhs.origin.y != rhs.origin.y) return lhs.origin.y > rhs.origin.y;
			if (lhs.origin.x != rhs.origin.x) return lhs.origin.x > rhs.origin.x;
			if (lhs.direction.y != rhs.direction.y) return lhs.direction.y > rhs.direction.y;
			return lhs.direction.x >= rhs.direction.x;
		}

		/// <summary>
		/// Converts the scaled ray to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <returns>A string representation of the scaled ray using default formatting.</returns>
		public override string ToString()
		{
			return string.Format("Origin = {0}; Direction = {1}", origin, direction);
		}

		/// <summary>
		/// Converts the scaled ray to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <param name="format">The numeric format string to be used for the origin and direction vectors.  Accepts the same values that can be passed to <see cref="UnityEngine.Vector3.ToString(string)"/>.</param>
		/// <returns>A string representation of the scaled ray using the specified formatting.</returns>
		public string ToString(string format)
		{
			return string.Format("Origin = {0}; Direction = {1}", origin.ToString(format), direction.ToString(format));
		}

		/// <summary>
		/// Converts the scaled ray to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <param name="originFormat">The numeric format string to be used for the origin vector.  Accepts the same values that can be passed to <see cref="UnityEngine.Vector3.ToString(string)"/>.</param>
		/// <param name="directionFormat">The numeric format string to be used for the direction vector.  Accepts the same values that can be passed to <see cref="UnityEngine.Vector3.ToString(string)"/>.</param>
		/// <returns>A string representation of the scaled ray using the specified formatting.</returns>
		public string ToString(string originFormat, string directionFormat)
		{
			return string.Format("Origin = {0}; Direction = {1}", origin.ToString(originFormat), direction.ToString(directionFormat));
		}
	}
}
