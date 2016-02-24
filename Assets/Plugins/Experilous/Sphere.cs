/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous
{
	/// <summary>
	/// A simple representation of a sphere with a given origin and radius.
	/// </summary>
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
