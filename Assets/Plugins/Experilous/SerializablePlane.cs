﻿using UnityEngine;
using System;

namespace Experilous
{
	[Serializable]
	public struct SerializablePlane
	{
		public Vector3 normal;
		public float distance;

		public SerializablePlane(Vector3 normal, float distance)
		{
			this.normal = normal.normalized;
			this.distance = distance;
		}

		public SerializablePlane(Vector3 normal, Vector3 point)
		{
			this.normal = normal.normalized;
			distance = -Vector3.Dot(normal, point);
		}

		public SerializablePlane(Vector3 point0, Vector3 point1, Vector3 point2)
		{
			normal = Vector3.Cross(point1 - point0, point2 - point0).normalized;
			distance = -Vector3.Dot(normal, point0);
		}

		public static implicit operator Plane(SerializablePlane plane)
		{
			return new Plane(plane.normal, plane.distance);
		}

		public void SetNormalAndPosition(Vector3 normal, Vector3 point)
		{
			this.normal = normal.normalized;
			distance = -Vector3.Dot(this.normal, point);
		}

		public void Set3Points(Vector3 point0, Vector3 point1, Vector3 point2)
		{
			normal = Vector3.Cross(point1 - point0, point2 - point0).normalized;
			distance = -Vector3.Dot(normal, point0);
		}

		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(normal, point) + distance;
		}

		public bool GetSide(Vector3 point)
		{
			return Vector3.Dot(normal, point) > -distance;
		}

		public bool SameSide(Vector3 point0, Vector3 point1)
		{
			return GetSide(point0) == GetSide(point1);
		}

		public bool Raycast(Ray ray, out float enter)
		{
			if (!MathUtility.GetIntersectionParameter(this, ray, out enter))
			{
				return false;
			}
			return enter > 0f;
		}

		public override string ToString()
		{
			return string.Format("Normal = {0}; Distance = {1}", normal, distance);
		}
	}
}
