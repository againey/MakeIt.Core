using UnityEngine;
using System;

namespace Experilous
{
	[Serializable]
	public struct Sphere
	{
		public Vector3 origin;
		public float radius;

		public Sphere(Vector3 origin, float radius)
		{
			this.origin = origin;
			this.radius = radius;
		}

		public override string ToString()
		{
			return string.Format("Origin = {0}; Radius = {1}", origin, radius);
		}
	}
}
