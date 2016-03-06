/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous
{
	[Serializable]
	public struct UVFrame3
	{
		public SerializablePlane uPlane;
		public SerializablePlane vPlane;
		public Vector3 uNegAxis;
		public Vector3 vNegAxis;

		public UVFrame3(SerializablePlane uPlane, SerializablePlane vPlane, Vector3 uNegAxis, Vector3 vNegAxis)
		{
			this.uPlane = uPlane;
			this.vPlane = vPlane;
			this.uNegAxis = uNegAxis;
			this.vNegAxis = vNegAxis;
		}

		public UVFrame3(Plane uPlane, Plane vPlane, Vector3 uNegAxis, Vector3 vNegAxis)
		{
			this.uPlane = new SerializablePlane(uPlane.normal, uPlane.distance);
			this.vPlane = new SerializablePlane(vPlane.normal, vPlane.distance);
			this.uNegAxis = uNegAxis;
			this.vNegAxis = vNegAxis;
		}
	}
}
