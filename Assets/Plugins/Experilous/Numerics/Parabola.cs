/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.Numerics
{
	[Serializable]
	public struct Parabola : IEquatable<Parabola>
	{
		public Vector3 vertex;
		public Vector3 axis;
		public Vector3 tangent;

		public Parabola(Vector3 axis, Vector3 tangent)
		{
			vertex = Vector3.zero;
			this.axis = axis;
			this.tangent = tangent;
		}

		public Parabola(Vector3 vertex, Vector3 axis, Vector3 tangent)
		{
			this.vertex = vertex;
			this.axis = axis;
			this.tangent = tangent;
		}

		public static Parabola FromFocusDirectrix(Vector3 focus, ScaledRay directrix)
		{
			var focusOnDirectrix = focus.ProjectOnto(directrix);
			var vertex = (focus + focusOnDirectrix) / 2f;
			var axis = (focus - focusOnDirectrix) / 2f;
			var tangent = directrix.direction.WithMagnitude(axis.magnitude);
			return new Parabola(vertex, axis, tangent);
		}

		public Vector3 Evaluate(float t)
		{
			return (axis * t + tangent) * t + vertex;
		}

		public void MirrorAboutAxis()
		{
			tangent = -tangent;
		}

		public Parabola mirroredAboutAxis
		{
			get
			{
				return new Parabola(vertex, axis, -tangent);
			}
		}

		public bool Equals(Parabola other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return obj is Parabola && this == (Parabola)obj;
		}

		public override int GetHashCode()
		{
			return vertex.GetHashCode() ^ axis.GetHashCode() ^ tangent.GetHashCode();
		}

		public static bool operator ==(Parabola lhs, Parabola rhs) { return lhs.vertex == rhs.vertex && lhs.axis == rhs.axis && lhs.tangent == rhs.tangent; }

		public static bool operator !=(Parabola lhs, Parabola rhs) { return lhs.vertex != rhs.vertex || lhs.axis != rhs.axis || lhs.tangent != rhs.tangent; }

		public override string ToString()
		{
			return string.Format("Parabola with Vertex = {0}, Axis = {1}, Tangent = {2}", vertex, axis, tangent);
		}

		public string ToString(string format)
		{
			return string.Format("Parabola with Vertex = {0}, Axis = {1}, Tangent = {2}", vertex.ToString(format), axis.ToString(format), tangent.ToString(format));
		}

		public string ToString(string vertexFormat, string axisTangentFormat)
		{
			return string.Format("Parabola with Vertex = {0}, Axis = {1}, Tangent = {2}", vertex.ToString(vertexFormat), axis.ToString(axisTangentFormat), tangent.ToString(axisTangentFormat));
		}

		public string ToString(string vertexFormat, string axisFormat, string tangentFormat)
		{
			return string.Format("Parabola with Vertex = {0}, Axis = {1}, Tangent = {2}", vertex.ToString(vertexFormat), axis.ToString(axisFormat), tangent.ToString(tangentFormat));
		}
	}
}
