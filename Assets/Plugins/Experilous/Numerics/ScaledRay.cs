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
	/// <para><see cref="UnityEngine.Ray"/> requires that its direction vecotor be a unit vector.  Not only does
	/// this result in a performance cost when constructing a ray (the provided direction must be normalized, just
	/// in case), but it also precludes the use of a ray to represent a ray that propagates at a particular velocity,
	/// or a parametric line that has endpoints at t = 0 and t = 1.  Additionally, it is not marked as serializable,
	/// and thus cannot be used directly as a serialized field in a class.  The scaled ray solves all of these
	/// problems, at the expense of not guaranteeing a unit vector direction.</para>
	/// </remarks>
	[Serializable]
	public struct ScaledRay
	{
		public Vector3 origin;
		public Vector3 direction;

		public ScaledRay(Vector3 origin, Vector3 direction)
		{
			this.origin = origin;
			this.direction = direction;
		}

		public static implicit operator ScaledRay(Ray ray)
		{
			return new ScaledRay(ray.origin, ray.direction);
		}

		public override string ToString()
		{
			return string.Format("Origin = {0}; Direction = {1}", origin, direction);
		}
	}
}
