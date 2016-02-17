using UnityEngine;
using System;

namespace Experilous
{
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
