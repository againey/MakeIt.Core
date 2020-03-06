/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

using System;
using UnityEngine;

namespace MakeIt.Numerics
{
	/// <summary>
	/// A three-dimensional embedding of a two-dimensional parallelogram frame for defining the UV values of any three-dimensional position relative to that frame.
	/// </summary>
	[Serializable]
	public struct UVFrame3
	{
		/// <summary>
		/// The plane along which the main U axis lies.
		/// </summary>
		public SerializablePlane uPlane;

		/// <summary>
		/// The plane along which the main V axis lies.
		/// </summary>
		public SerializablePlane vPlane;

		/// <summary>
		/// The scaled direction, parallel to the U plane, in which U values decrease.
		/// </summary>
		public Vector3 uNegAxis;

		/// <summary>
		/// The scaled direction, parallel to the V plane, in which V values decrease.
		/// </summary>
		public Vector3 vNegAxis;

		/// <summary>
		/// Construct a UV frame with the given U and V planes and scaled negative U and V axes.
		/// </summary>
		/// <param name="uPlane">The plane along with the main U axis lies.</param>
		/// <param name="vPlane">The plane along with the main V axis lies.</param>
		/// <param name="uNegAxis">The scaled direction, parallel to the U plane, in which U values decrease.</param>
		/// <param name="vNegAxis">The scaled direction, parallel to the V plane, in which V values decrease.</param>
		public UVFrame3(SerializablePlane uPlane, SerializablePlane vPlane, Vector3 uNegAxis, Vector3 vNegAxis)
		{
			this.uPlane = uPlane;
			this.vPlane = vPlane;
			this.uNegAxis = uNegAxis;
			this.vNegAxis = vNegAxis;
		}

		/// <summary>
		/// Construct a UV frame with the given U and V planes and scaled negative U and V axes.
		/// </summary>
		/// <param name="uPlane">The plane along with the main U axis lies.</param>
		/// <param name="vPlane">The plane along with the main V axis lies.</param>
		/// <param name="uNegAxis">The scaled direction, parallel to the U plane, in which U values decrease.</param>
		/// <param name="vNegAxis">The scaled direction, parallel to the V plane, in which V values decrease.</param>
		public UVFrame3(Plane uPlane, Plane vPlane, Vector3 uNegAxis, Vector3 vNegAxis)
		{
			this.uPlane = new SerializablePlane(uPlane.normal, uPlane.distance);
			this.vPlane = new SerializablePlane(vPlane.normal, vPlane.distance);
			this.uNegAxis = uNegAxis;
			this.vNegAxis = vNegAxis;
		}

		/// <summary>
		/// Calculates the UV coordinate of a given position relative to the current UV frame.
		/// </summary>
		/// <param name="position">The position in three-dimensional space.</param>
		/// <returns>The UV coordinate of <paramref name="position"/> relative to the current UV frame.</returns>
		public Vector2 GetUV(Vector3 position)
		{
			return new Vector2(
				Geometry.GetIntersectionParameter(vPlane, new ScaledRay(position, uNegAxis)),
				Geometry.GetIntersectionParameter(uPlane, new ScaledRay(position, vNegAxis)));
		}
	}
}
