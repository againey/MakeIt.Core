/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.Numerics
{
	/// <summary>
	/// A simple plane class that is serializable.
	/// </summary>
	/// <remarks>
	/// <para><see cref="UnityEngine.Plane"/> is not serializable, which complicates its usage in classes
	/// which contain planes as member fields which need to be serialized.  Instead of storing the individual
	/// plane fields directly in the class and constructing a Plane instance from them when needed, this
	/// serialized plane class can be used as the member field type.</para>
	/// </remarks>
	[Serializable]
	public struct SerializablePlane : IEquatable<SerializablePlane>, IComparable<SerializablePlane>
	{
		/// <summary>
		/// The surface normal vector of the plane, pointing in the direction of the plane's exterior side.
		/// </summary>
		public Vector3 normal;

		/// <summary>
		/// The signed nearest distance of the plane from the world origin at (0, 0, 0).
		/// </summary>
		/// <remarks><para>This field is positive if the world origin is on the positive or exterior
		/// side of the plane, and negative if the world origin is on the negative or exterior side
		/// of the plane.  If the plane passes directly through the world origin, then this field
		/// will be zero.</para></remarks>
		public float distance;

		/// <summary>
		/// Constructs a plane with the given surface normal, with a default distance of zero.
		/// </summary>
		/// <param name="normal">The surface normal of the plane.</param>
		public SerializablePlane(Vector3 normal)
		{
			this.normal = normal.normalized;
			distance = 0f;
		}

		/// <summary>
		/// Constructs a plane with the given surface normal and nearest distance from world origin.
		/// </summary>
		/// <param name="normal">The surface normal of the plane.</param>
		/// <param name="distance">The nearest distance from origin of the plane.</param>
		public SerializablePlane(Vector3 normal, float distance)
		{
			this.normal = normal.normalized;
			this.distance = distance;
		}

		/// <summary>
		/// Constructs a plane with the given surface normal, which passes through the given point.
		/// </summary>
		/// <param name="normal">The surface normal of the plane.</param>
		/// <param name="point">A point through which the plane passes.</param>
		public SerializablePlane(Vector3 normal, Vector3 point)
		{
			this.normal = normal.normalized;
			distance = -Vector3.Dot(normal, point);
		}

		/// <summary>
		/// Constructs a plane that passes through three given non-colinear points.
		/// </summary>
		/// <param name="point0">The first point through which the plane passes.</param>
		/// <param name="point1">The second point through which the plane passes.</param>
		/// <param name="point2">The third point through which the plane passes.</param>
		public SerializablePlane(Vector3 point0, Vector3 point1, Vector3 point2)
		{
			var axis = Vector3.Cross(point1 - point0, point2 - point0);
			var axisSqrMagnitude = axis.sqrMagnitude;
			if (Mathf.Approximately(axisSqrMagnitude, 0f)) throw new ArgumentException("The provided points are colinear and therefore cannot uniquely specify a plane.");
			normal = axis / axisSqrMagnitude;
			distance = -Vector3.Dot(normal, point0);
		}

		/// <summary>
		/// Converts a serializable plane into a regular plane.
		/// </summary>
		/// <param name="plane">The serializable plane to be converted.</param>
		/// <returns>The converted plane.</returns>
		public static implicit operator Plane(SerializablePlane plane)
		{
			return new Plane(plane.normal, plane.distance);
		}

		/// <summary>
		/// Converts a regular plane into a serializable plane.
		/// </summary>
		/// <param name="plane">The regular plane to be converted.</param>
		/// <returns>The converted serializable plane.</returns>
		public static implicit operator SerializablePlane(Plane plane)
		{
			return new SerializablePlane(plane.normal, plane.distance);
		}

		/// <summary>
		/// Sets the definition of the plane using the given surface normal and a point through which the plane passes.
		/// </summary>
		/// <param name="normal">The surface normal of the plane.</param>
		/// <param name="point">A point through which the plane passes.</param>
		public void SetNormalAndPosition(Vector3 normal, Vector3 point)
		{
			this.normal = normal.normalized;
			distance = -Vector3.Dot(this.normal, point);
		}

		/// <summary>
		/// Sets the definition of the plane using three non-colinear points through which the plane passes.
		/// </summary>
		/// <param name="point0">The first point through which the plane passes.</param>
		/// <param name="point1">The second point through which the plane passes.</param>
		/// <param name="point2">The third point through which the plane passes.</param>
		public void Set3Points(Vector3 point0, Vector3 point1, Vector3 point2)
		{
			var axis = Vector3.Cross(point1 - point0, point2 - point0);
			var axisSqrMagnitude = axis.sqrMagnitude;
			if (Mathf.Approximately(axisSqrMagnitude, 0f)) throw new ArgumentException("The provided points are colinear and therefore cannot uniquely specify a plane.");
			normal = axis / axisSqrMagnitude;
			distance = -Vector3.Dot(normal, point0);
		}

		/// <summary>
		/// Calculates the signed shortest distance from the surface of the plane to the specified point.
		/// </summary>
		/// <param name="point">The point from which distance to the plane is to be calculated.</param>
		/// <returns>The signed shortest distance from the plane to the point.</returns>
		/// <remarks><para>The calculated distance is positive if the point is on the exterior side of the
		/// plane, and negative if the point is on the interior side of the plane.</para></remarks>
		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(normal, point) + distance;
		}

		/// <summary>
		/// Determines if the given point is on the interior or exterior side of the plane.
		/// </summary>
		/// <param name="point">The point to be compared against the plane.</param>
		/// <returns>True if the point is on the exterior side of the plane, and false if the point is
		/// on the interior side or lies exactly on the surface of the plane.</returns>
		public bool GetSide(Vector3 point)
		{
			return Vector3.Dot(normal, point) > -distance;
		}

		/// <summary>
		/// Determines if two points are on the same or opposite sides of the plane.
		/// </summary>
		/// <param name="point0">The first point to be compared against the plane.</param>
		/// <param name="point1">The second point to be compared against the plane.</param>
		/// <returns>True if either both points are on the exterior side or both points are on the
		/// interior side or the surface, and false if one is on the exterior side and one is on
		/// the interior side or on the surface.</returns>
		public bool SameSide(Vector3 point0, Vector3 point1)
		{
			return GetSide(point0) == GetSide(point1);
		}

		/// <summary>
		/// Checks if the given ray intersects the surface of the plane, and calculates the distance along the ray at which the intersection occurs.
		/// </summary>
		/// <param name="ray">The ray to be intersected with the plane.</param>
		/// <param name="enter">The distance along the ray at which the intersection occurs.  Will be negative if the ray points away from the surface of the plane.</param>
		/// <returns>True if the ray is not parallel to the plane and is pointing toward the surface of the plane,
		/// and false if either the ray is parallel to the plane or is point away from the surface.</returns>
		public bool Raycast(Ray ray, out float enter)
		{
			if (!Geometry.GetIntersectionParameter(this, ray, out enter))
			{
				enter = 0f;
				return false;
			}
			return enter >= 0f;
		}

		/// <summary>
		/// Compares the current plane to the supplied plane, using a lexicographical ordering.
		/// </summary>
		/// <param name="other">The plane to be compared to the current plane.</param>
		/// <returns>Returns -1 if the current plane comes before the supplied plane in the lexicographical ordering,
		/// +1 if it comes after the supplied plane, and 0 if they are equal.</returns>
		/// <remarks><para>The lexicographical ordering proceeds starting with the later components and moving toward
		/// the earlier components last, and checks normal before distance.</para></remarks>
		public int CompareTo(SerializablePlane other)
		{
			if (normal.z != other.normal.z) return (normal.z < other.normal.z) ? -1 : +1;
			if (normal.y != other.normal.y) return (normal.y < other.normal.y) ? -1 : +1;
			if (normal.x != other.normal.x) return (normal.x < other.normal.x) ? -1 : +1;
			if (distance != other.distance) return (distance < other.distance) ? -1 : +1;
			return 0;
		}

		/// <summary>
		/// Compares the current plane to the supplied plane to find if they are equal.
		/// </summary>
		/// <param name="other">The plane to be compared to the current plane.</param>
		/// <returns>Returns true if the two planes are equal, and false otherwise.</returns>
		public bool Equals(SerializablePlane other)
		{
			return this == other;
		}

		/// <summary>
		/// Compares the current plane to the supplied object to find if they are equal.
		/// </summary>
		/// <param name="obj">The object to be compared to the current plane.</param>
		/// <returns>Returns true if the supplied object is an instance of SerializablePlane and equal to the current plane, and false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return obj is SerializablePlane && this == (SerializablePlane)obj;
		}

		/// <summary>
		/// Calculates a 32-bit integer hash code for the current plane.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code based on the component values of the current plane.</returns>
		public override int GetHashCode()
		{
			return normal.GetHashCode() ^ distance.GetHashCode();
		}

		/// <summary>
		/// Compares two planes for equality.
		/// </summary>
		/// <param name="lhs">The first plane to compare.</param>
		/// <param name="rhs">The second plane to compare.</param>
		/// <returns>Returns true if the two planes are equal, and false otherwise.</returns>
		public static bool operator ==(SerializablePlane lhs, SerializablePlane rhs) { return lhs.normal == rhs.normal && lhs.distance == rhs.distance; }

		/// <summary>
		/// Compares two planes for inequality.
		/// </summary>
		/// <param name="lhs">The first plane to compare.</param>
		/// <param name="rhs">The second plane to compare.</param>
		/// <returns>Returns true if the two planes are not equal, and false otherwise.</returns>
		public static bool operator !=(SerializablePlane lhs, SerializablePlane rhs) { return lhs.normal != rhs.normal || lhs.distance != rhs.distance; }

		/// <summary>
		/// Compares two planes to find if the first is lexicographically less than the second plane.
		/// </summary>
		/// <param name="lhs">The first plane to compare.</param>
		/// <param name="rhs">The second plane to compare.</param>
		/// <returns>True if the first plane is lexicographically less than the second plane, and false otherwise.</returns>
		/// <seealso cref="CompareTo(SerializablePlane)"/>
		public static bool operator < (SerializablePlane lhs, SerializablePlane rhs)
		{
			if (lhs.normal.z != rhs.normal.z) return lhs.normal.z < rhs.normal.z;
			if (lhs.normal.y != rhs.normal.y) return lhs.normal.y < rhs.normal.y;
			if (lhs.normal.x != rhs.normal.x) return lhs.normal.x < rhs.normal.x;
			return lhs.distance < rhs.distance;
		}

		/// <summary>
		/// Compares two planes to find if the first is lexicographically less than or equal to the second plane.
		/// </summary>
		/// <param name="lhs">The first plane to compare.</param>
		/// <param name="rhs">The second plane to compare.</param>
		/// <returns>True if the first plane is lexicographically less than or equal to the second plane, and false otherwise.</returns>
		/// <seealso cref="CompareTo(SerializablePlane)"/>
		public static bool operator <=(SerializablePlane lhs, SerializablePlane rhs)
		{
			if (lhs.normal.z != rhs.normal.z) return lhs.normal.z < rhs.normal.z;
			if (lhs.normal.y != rhs.normal.y) return lhs.normal.y < rhs.normal.y;
			if (lhs.normal.x != rhs.normal.x) return lhs.normal.x < rhs.normal.x;
			return lhs.distance <= rhs.distance;
		}

		/// <summary>
		/// Compares two planes to find if the first is lexicographically greater than the second plane.
		/// </summary>
		/// <param name="lhs">The first plane to compare.</param>
		/// <param name="rhs">The second plane to compare.</param>
		/// <returns>True if the first plane is lexicographically greater than the second plane, and false otherwise.</returns>
		/// <seealso cref="CompareTo(SerializablePlane)"/>
		public static bool operator > (SerializablePlane lhs, SerializablePlane rhs)
		{
			if (lhs.normal.z != rhs.normal.z) return lhs.normal.z > rhs.normal.z;
			if (lhs.normal.y != rhs.normal.y) return lhs.normal.y > rhs.normal.y;
			if (lhs.normal.x != rhs.normal.x) return lhs.normal.x > rhs.normal.x;
			return lhs.distance > rhs.distance;
		}

		/// <summary>
		/// Compares two planes to find if the first is lexicographically greater than or equal to the second plane.
		/// </summary>
		/// <param name="lhs">The first plane to compare.</param>
		/// <param name="rhs">The second plane to compare.</param>
		/// <returns>True if the first plane is lexicographically greater than or equal to the second plane, and false otherwise.</returns>
		/// <seealso cref="CompareTo(SerializablePlane)"/>
		public static bool operator >=(SerializablePlane lhs, SerializablePlane rhs)
		{
			if (lhs.normal.z != rhs.normal.z) return lhs.normal.z > rhs.normal.z;
			if (lhs.normal.y != rhs.normal.y) return lhs.normal.y > rhs.normal.y;
			if (lhs.normal.x != rhs.normal.x) return lhs.normal.x > rhs.normal.x;
			return lhs.distance > rhs.distance;
		}

		/// <summary>
		/// Converts the plane to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <returns>A string representation of the plane using default formatting.</returns>
		public override string ToString()
		{
			return string.Format("Normal = {0}; Distance = {1}", normal, distance);
		}

		/// <summary>
		/// Converts the plane to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <param name="format">The numeric format string to be used for the normal vector and distance value.  Accepts the same values that can be passed to <see cref="UnityEngine.Vector3.ToString(string)"/> and <see cref="System.Single.ToString(string)"/>.</param>
		/// <returns>A string representation of the plane using the specified formatting.</returns>
		public string ToString(string format)
		{
			return string.Format("Normal = {0}; Distance = {1}", normal.ToString(format), distance.ToString(format));
		}

		/// <summary>
		/// Converts the plane to string representation, appropriate for diagnositic display.
		/// </summary>
		/// <param name="normalFormat">The numeric format string to be used for the normal vector.  Accepts the same values that can be passed to <see cref="UnityEngine.Vector3.ToString(string)"/>.</param>
		/// <param name="distanceFormat">The numeric format string to be used for the distance value.  Accepts the same values that can be passed to <see cref="System.Single.ToString(string)"/>.</param>
		/// <returns>A string representation of the plane using the specified formatting.</returns>
		public string ToString(string normalFormat, string distanceFormat)
		{
			return string.Format("Normal = {0}; Distance = {1}", normal.ToString(normalFormat), distance.ToString(distanceFormat));
		}
	}
}
