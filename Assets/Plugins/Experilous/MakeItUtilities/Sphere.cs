/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous
{
	/// <summary>
	/// A simple representation of a sphere with a given origin and radius.
	/// </summary>
	[Serializable]
	public struct Sphere : IEquatable<Sphere>
	{
		public Vector3 center;
		public float radius;

		public Sphere(Vector3 center, float radius)
		{
			this.center = center;
			this.radius = radius;
		}

		public Vector3 ClosestPoint(Vector3 point)
		{
			var delta = point - center;
			var distanceSquared = delta.sqrMagnitude;
			if (distanceSquared <= radius * radius) return point;
			return delta / Mathf.Sqrt(distanceSquared) * radius + center;
		}

		public bool Contains(Vector3 point)
		{
			var delta = point - center;
			var distanceSquared = delta.sqrMagnitude;
			return (distanceSquared <= radius * radius);
		}

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

		public void Expand(float amount)
		{
			radius = Mathf.Max(0f, radius + amount);
		}

		public override bool Equals(object other)
		{
			return (other is Sphere && this == (Sphere)other);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool Equals(Sphere other)
		{
			return this == other;
		}

		public static bool operator ==(Sphere lhs, Sphere rhs) { return lhs.radius == rhs.radius && lhs.center == rhs.center; }
		public static bool operator !=(Sphere lhs, Sphere rhs) { return lhs.radius != rhs.radius || lhs.center != rhs.center; }

		public override string ToString()
		{
			return string.Format("Center = {0}; Radius = {1}", center, radius);
		}
	}
}
