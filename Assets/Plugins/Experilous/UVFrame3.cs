/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous
{
	/// <summary>
	/// A three-dimensional embedding of a two-dimensional parallelogram frame for defining the UV values of any three-dimensional position relative to that frame.
	/// </summary>
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

		/// <summary>
		/// Given a three-dimensional position, calculates the UV coordinate of that position relative to this parallelogram frame.
		/// </summary>
		/// <param name="position">The position in three-dimensional space.</param>
		/// <returns>The UV coordinate of <paramref name="position"/> relative to this frame.</returns>
		public Vector2 GetUV(Vector3 position)
		{
			return new Vector2(
				GeometryTools.GetIntersectionParameter(vPlane, new ScaledRay(position, uNegAxis)),
				GeometryTools.GetIntersectionParameter(uPlane, new ScaledRay(position, vNegAxis)));
		}
	}
}
