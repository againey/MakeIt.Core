/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.Numerics
{
	/// <summary>
	/// A simple representation of a sphere with a given origin and radius.
	/// </summary>
	[Serializable]
	public struct Sphere : IEquatable<Sphere>, IComparable<Sphere>
	{
		/// <summary>
		/// The center point of the sphere.
		/// </summary>
		public Vector3 center;

		/// <summary>
		/// The readius of the sphere.
		/// </summary>
		public float radius;

		/// <summary>
		/// The square of the radius.
		/// </summary>
		public float sqrRadius { get { return radius * radius; } }

		/// <summary>
		/// Constructs a sphere with the given radius centered on the world origin at (0, 0, 0).
		/// </summary>
		/// <param name="radius">The radius of the sphere.</param>
		public Sphere(float radius)
		{
			center = Vector3.zero;
			this.radius = radius;
		}

		/// <summary>
		/// Constructs a unit sphere centered on the given point.
		/// </summary>
		/// <param name="center">The center of the sphere.</param>
		public Sphere(Vector3 center)
		{
			this.center = center;
			radius = 1f;
		}

		/// <summary>
		/// Constructs a sphere with the given radius and centered on the given point.
		/// </summary>
		/// <param name="center">The center of the sphere.</param>
		/// <param name="radius">The radius of the sphere.</param>
		public Sphere(Vector3 center, float radius)
		{
			this.center = center;
			this.radius = radius;
		}

		/// <summary>
		/// Determines the closest point on the surface of the sphere to the given point.
		/// </summary>
		/// <param name="point">The point to which the closest point of the surface is to be found.</param>
		/// <returns>The point on the surface of the sphere closest to the given point.</returns>
		/// <remarks><para>The return value is undefined if the given point is exactly at the center of the sphere.</para></remarks>
		public Vector3 ClosestPoint(Vector3 point)
		{
			var delta = point - center;
			var distanceSquared = delta.sqrMagnitude;
			if (Mathf.Approximately(distanceSquared, 0f)) return point;
			return delta / Mathf.Sqrt(distanceSquared) * radius + center;
		}

		/// <summary>
		/// Determines the closest point in or on the sphere to the given point.
		/// </summary>
		/// <param name="point">The point to which the closest point of the sphere is to be found.</param>
		/// <returns>The point on the in or on the sphere closest to the given point.</returns>
		/// <remarks><para>If the given point is inside the sphere, then that point is returned unchanged.</para></remarks>
		public Vector3 ClosestVolumePoint(Vector3 point)
		{
			var delta = point - center;
			var distanceSquared = delta.sqrMagnitude;
			if (distanceSquared <= sqrRadius) return point;
			return delta / Mathf.Sqrt(distanceSquared) * radius + center;
		}

		/// <summary>
		/// Calculates the signed shortest distance from the surface of the sphere to the specified point.
		/// </summary>
		/// <param name="point">The point from which distance to the sphere is to be calculated.</param>
		/// <returns>The signed shortest distance from the sphere to the point.</returns>
		/// <remarks><para>The calculated distance is positive if the point is on the exterior side of the
		/// sphere, and negative if the point is on the interior side of the sphere.</para></remarks>
		public float GetDistanceToPoint(Vector3 point)
		{
			return (point - center).magnitude - radius;
		}

		/// <summary>
		/// Calculates the shortest distance from the sphere to the specified point.
		/// </summary>
		/// <param name="point">The point from which distance to the sphere is to be calculated.</param>
		/// <returns>The shorted distance from the sphere to the point.</returns>
		/// <remarks><para>If the given point is inside the sphere, then a distance of zero is returned.</para></remarks>
		public float GetVolumeDistanceToPoint(Vector3 point)
		{
			var delta = point - center;
			var distanceSquared = delta.sqrMagnitude;
			if (distanceSquared <= sqrRadius) return 0f;
			return Mathf.Sqrt(distanceSquared) - radius;
		}

		/// <summary>
		/// Determines if the sphere contains the given point.
		/// </summary>
		/// <param name="point">The point to be compared against the sphere.</param>
		/// <returns>True if the point is within or exactly on the surface of the sphere, and false if it is outside the sphere.</returns>
		public bool Contains(Vector3 point)
		{
			var delta = point - center;
			var distanceSquared = delta.sqrMagnitude;
			return (distanceSquared <= radius * radius);
		}

		/// <summary>
		/// Expands the radius of the current sphere just enough to encapsulate the given point, without altering the sphere's center.
		/// </summary>
		/// <param name="point">The point to be encapsulated by the sphere.</param>
		/// <remarks><para>If the given point is already within or on the surface of the sphere, then the radius does not change.</para></remarks>
		public void Encapsulate(Vector3 point)
		{
			var delta = point - center;
			var distanceSquared = delta.sqrMagnitude;
			if (distanceSquared > radius * radius)
			{
				var distance = Mathf.Sqrt(distanceSquared);
				var shiftDistance = (distance - radius) * 0.5f;
				center += delta / Mathf.Sqrt(distanceSquared) * shiftDistance;
				radius += shiftDistance;
			}
		}

		/// <summary>
		/// Expands the radius of the current sphere just enough to encapsulate the entirety of the given bounding box, without altering the sphere's center.
		/// </summary>
		/// <param name="bounds">The bounding box to be encapsulated by the sphere.</param>
		/// <remarks><para>If the given bounding box is already entirely within the sphere, then the radius does not change.</para></remarks>
		public void Encapsulate(Bounds bounds)
		{
			var min = bounds.min;
			var max = bounds.max;
			Encapsulate(min);
			Encapsulate(new Vector3(min.x, min.y, max.z));
			Encapsulate(new Vector3(min.x, max.y, min.z));
			Encapsulate(new Vector3(min.x, max.y, max.z));
			Encapsulate(new Vector3(max.x, min.y, min.z));
			Encapsulate(new Vector3(max.x, min.y, max.z));
			Encapsulate(new Vector3(max.x, max.y, min.z));
			Encapsulate(max);
		}

		/// <summary>
		/// Expands the radius of the current sphere just enough to encapsulate the entirety of the given bounding sphere, without altering the current sphere's center.
		/// </summary>
		/// <param name="bounds">The bounding sphere to be encapsulated by the current sphere.</param>
		/// <remarks><para>If the given bounding sphere is already entirely within the current sphere, then the radius does not change.</para></remarks>
		public void Encapsulate(Sphere bounds)
		{
			if (center != bounds.center)
			{
				var delta = bounds.center - center;
				var centerDistance = delta.magnitude;
				var farDistance = centerDistance + bounds.radius;

				if (farDistance > radius)
				{
					var nearDistance = bounds.radius - centerDistance;
					if (nearDistance <= radius)
					{
						var shiftDistance = (farDistance - radius) * 0.5f;
						center += delta / centerDistance * shiftDistance;
						radius += shiftDistance;
					}
					else
					{
						center = bounds.center;
						radius = bounds.radius;
					}
				}
			}
			else
			{
				radius = Mathf.Max(radius, bounds.radius);
			}
		}

		/// <summary>
		/// Expands the radius of the current sphere by the given amount.
		/// </summary>
		/// <param name="amount">The amount to increase the radius of the current sphere, or decrease it if the amount is negative.</param>
		/// <remarks><para>The radius is guaranteed to not be negative after this operation, even if <paramref name="amount"/>
		/// is a negative value with magnitude greater than the current radius.</para></remarks>
		public void Expand(float amount)
		{
			radius = Mathf.Max(0f, radius + amount);
		}

		/// <summary>
		/// Compares the current sphere to the supplied sphere, using a lexicographical ordering.
		/// </summary>
		/// <param name="other">The sphere to be compared to the current sphere.</param>
		/// <returns>Returns -1 if the current sphere comes before the supplied sphere in the lexicographical ordering,
		/// +1 if it comes after the supplied sphere, and 0 if they are equal.</returns>
		/// <remarks><para>The lexicographical ordering proceeds starting with the later components and moving toward
		/// the earlier components last, and checks center before radius.</para></remarks>
		public int CompareTo(Sphere other)
		{
			if (center.z != other.center.z) return (center.z < other.center.z) ? -1 : +1;
			if (center.y != other.center.y) return (center.y < other.center.y) ? -1 : +1;
			if (center.x != other.center.x) return (center.x < other.center.x) ? -1 : +1;
			if (radius != other.radius) return (radius < other.radius) ? -1 : +1;
			return 0;
		}

		/// <summary>
		/// Compares the current sphere to the supplied sphere to find if they are equal.
		/// </summary>
		/// <param name="other">The sphere to be compared to the current sphere.</param>
		/// <returns>Returns true if the two spheres are equal, and false otherwise.</returns>
		public bool Equals(Sphere other)
		{
			return this == other;
		}

		/// <summary>
		/// Compares the current sphere to the supplied object to find if they are equal.
		/// </summary>
		/// <param name="obj">The object to be compared to the current sphere.</param>
		/// <returns>Returns true if the supplied object is an instance of Sphere and equal to the current sphere, and false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return obj is Sphere && this == (Sphere)obj;
		}

		/// <summary>
		/// Calculates a 32-bit integer hash code for the current sphere.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code based on the component values of the current sphere.</returns>
		public override int GetHashCode()
		{
			return center.GetHashCode() ^ radius.GetHashCode();
		}

		/// <summary>
		/// Compares two spheres for equality.
		/// </summary>
		/// <param name="lhs">The first sphere to compare.</param>
		/// <param name="rhs">The second sphere to compare.</param>
		/// <returns>Returns true if the two spheres are equal, and false otherwise.</returns>
		public static bool operator ==(Sphere lhs, Sphere rhs) { return lhs.center == rhs.center && lhs.radius == rhs.radius; }

		/// <summary>
		/// Compares two spheres for inequality.
		/// </summary>
		/// <param name="lhs">The first sphere to compare.</param>
		/// <param name="rhs">The second sphere to compare.</param>
		/// <returns>Returns true if the two spheres are not equal, and false otherwise.</returns>
		public static bool operator !=(Sphere lhs, Sphere rhs) { return lhs.center != rhs.center || lhs.radius != rhs.radius; }

		/// <summary>
		/// Compares two spheres to find if the first is lexicographically less than the second sphere.
		/// </summary>
		/// <param name="lhs">The first sphere to compare.</param>
		/// <param name="rhs">The second sphere to compare.</param>
		/// <returns>True if the first sphere is lexicographically less than the second sphere, and false otherwise.</returns>
		/// <seealso cref="CompareTo(Sphere)"/>
		public static bool operator < (Sphere lhs, Sphere rhs)
		{
			if (lhs.center.z != rhs.center.z) return lhs.center.z < rhs.center.z;
			if (lhs.center.y != rhs.center.y) return lhs.center.y < rhs.center.y;
			if (lhs.center.x != rhs.center.x) return lhs.center.x < rhs.center.x;
			return lhs.radius < rhs.radius;
		}

		/// <summary>
		/// Compares two spheres to find if the first is lexicographically less than or equal to the second sphere.
		/// </summary>
		/// <param name="lhs">The first sphere to compare.</param>
		/// <param name="rhs">The second sphere to compare.</param>
		/// <returns>True if the first sphere is lexicographically less than or equal to the second sphere, and false otherwise.</returns>
		/// <seealso cref="CompareTo(Sphere)"/>
		public static bool operator <=(Sphere lhs, Sphere rhs)
		{
			if (lhs.center.z != rhs.center.z) return lhs.center.z < rhs.center.z;
			if (lhs.center.y != rhs.center.y) return lhs.center.y < rhs.center.y;
			if (lhs.center.x != rhs.center.x) return lhs.center.x < rhs.center.x;
			return lhs.radius <= rhs.radius;
		}

		/// <summary>
		/// Compares two spheres to find if the first is lexicographically greater than the second sphere.
		/// </summary>
		/// <param name="lhs">The first sphere to compare.</param>
		/// <param name="rhs">The second sphere to compare.</param>
		/// <returns>True if the first sphere is lexicographically greater than the second sphere, and false otherwise.</returns>
		/// <seealso cref="CompareTo(Sphere)"/>
		public static bool operator > (Sphere lhs, Sphere rhs)
		{
			if (lhs.center.z != rhs.center.z) return lhs.center.z > rhs.center.z;
			if (lhs.center.y != rhs.center.y) return lhs.center.y > rhs.center.y;
			if (lhs.center.x != rhs.center.x) return lhs.center.x > rhs.center.x;
			return lhs.radius > rhs.radius;
		}

		/// <summary>
		/// Compares two spheres to find if the first is lexicographically greater than or equal to the second sphere.
		/// </summary>
		/// <param name="lhs">The first sphere to compare.</param>
		/// <param name="rhs">The second sphere to compare.</param>
		/// <returns>True if the first sphere is lexicographically greater than or equal to the second sphere, and false otherwise.</returns>
		/// <seealso cref="CompareTo(Sphere)"/>
		public static bool operator >=(Sphere lhs, Sphere rhs)
		{
			if (lhs.center.z != rhs.center.z) return lhs.center.z > rhs.center.z;
			if (lhs.center.y != rhs.center.y) return lhs.center.y > rhs.center.y;
			if (lhs.center.x != rhs.center.x) return lhs.center.x > rhs.center.x;
			return lhs.radius > rhs.radius;
		}

		/// <summary>
		/// Converts the sphere to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <returns>A string representation of the sphere using default formatting.</returns>
		public override string ToString()
		{
			return string.Format("Center = {0}; Radius = {1}", center, radius);
		}

		/// <summary>
		/// Converts the sphere to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <param name="format">The numeric format string to be used for the center vector and radius value.  Accepts the same values that can be passed to <see cref="UnityEngine.Vector3.ToString(string)"/> and <see cref="System.Single.ToString(string)"/>.</param>
		/// <returns>A string representation of the sphere using the specified formatting.</returns>
		public string ToString(string format)
		{
			return string.Format("Center = {0}; Radius = {1}", center.ToString(format), radius.ToString(format));
		}

		/// <summary>
		/// Converts the sphere to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <param name="centerFormat">The numeric format string to be used for the center vector.  Accepts the same values that can be passed to <see cref="UnityEngine.Vector3.ToString(string)"/>.</param>
		/// <param name="radiusFormat">The numeric format string to be used for the radius value.  Accepts the same values that can be passed to <see cref="System.Single.ToString(string)"/>.</param>
		/// <returns>A string representation of the sphere using the specified formatting.</returns>
		public string ToString(string centerFormat, string radiusFormat)
		{
			return string.Format("Center = {0}; Radius = {1}", center.ToString(centerFormat), radius.ToString(radiusFormat));
		}
	}
}
