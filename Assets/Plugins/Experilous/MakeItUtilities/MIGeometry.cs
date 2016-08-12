/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.MakeIt.Utilities
{
	public static class MIGeometry
	{
		#region Vector Operations

		public static Vector2 LerpUnclamped(Vector2 lhs, Vector2 rhs, float t)
		{
			return lhs * (1f - t) + rhs * t;
		}

		public static Vector3 LerpUnclamped(Vector3 lhs, Vector3 rhs, float t)
		{
			return lhs * (1f - t) + rhs * t;
		}

		public static Vector4 LerpUnclamped(Vector4 lhs, Vector4 rhs, float t)
		{
			return lhs * (1f - t) + rhs * t;
		}

		public static Vector3 SlerpUnitVectors(Vector3 p0, Vector3 p1, float t)
		{
			var omega = Mathf.Acos(Vector3.Dot(p0, p1));
			var d = Mathf.Sin(omega);
			var s0 = Mathf.Sin((1f - t) * omega);
			var s1 = Mathf.Sin(t * omega);
			return (p0 * s0 + p1 * s1) / d;
		}

		public static float AngleBetweenUnitVectors(Vector3 lhs, Vector3 rhs)
		{
			return Mathf.Atan2(Vector3.Cross(lhs, rhs).magnitude, Vector3.Dot(lhs, rhs));
		}

		public static float AngleBetweenSphericalVectors(Vector3 lhs, Vector3 rhs, float sphereRadius)
		{
			lhs /= sphereRadius;
			rhs /= sphereRadius;
			return Mathf.Atan2(Vector3.Cross(lhs, rhs).magnitude, Vector3.Dot(lhs, rhs));
		}

		public static float AngleBetweenVectors(Vector3 lhs, Vector3 rhs)
		{
			lhs.Normalize();
			rhs.Normalize();
			return Mathf.Atan2(Vector3.Cross(lhs, rhs).magnitude, Vector3.Dot(lhs, rhs));
		}

		public static float SphericalArcLength(Vector3 lhs, Vector3 rhs, float sphereRadius)
		{
			return AngleBetweenSphericalVectors(lhs, rhs, sphereRadius) * sphereRadius;
		}

		public static Vector2 WithMagnitude(this Vector2 v, float newMagnitude)
		{
			var originalMagnitude = v.magnitude;
			return v * (newMagnitude / originalMagnitude);
		}

		public static Vector3 WithMagnitude(this Vector3 v, float newMagnitude)
		{
			var originalMagnitude = v.magnitude;
			return v * (newMagnitude / originalMagnitude);
		}

		public static Vector4 WithMagnitude(this Vector4 v, float newMagnitude)
		{
			var originalMagnitude = v.magnitude;
			return v * (newMagnitude / originalMagnitude);
		}

		public static Vector3 MultiplyComponents(this Vector2 v, Vector2 s)
		{
			v.Scale(s);
			return v;
		}

		public static Vector3 MultiplyComponents(this Vector3 v, Vector3 s)
		{
			v.Scale(s);
			return v;
		}

		public static Vector3 MultiplyComponents(this Vector4 v, Vector4 s)
		{
			v.Scale(s);
			return v;
		}

		public static Vector3 DivideComponents(this Vector2 v, Vector2 d)
		{
			v.x /= d.x;
			v.y /= d.y;
			return v;
		}

		public static Vector3 DivideComponents(this Vector3 v, Vector3 d)
		{
			v.x /= d.x;
			v.y /= d.y;
			v.z /= d.z;
			return v;
		}

		public static Vector3 DivideComponents(this Vector4 v, Vector4 d)
		{
			v.x /= d.x;
			v.y /= d.y;
			v.z /= d.z;
			v.w /= d.w;
			return v;
		}

		public static float MinComponent(this Vector2 v)
		{
			return Mathf.Min(v.x, v.y);
		}

		public static float MinComponent(this Vector3 v)
		{
			return Mathf.Min(Mathf.Min(v.x, v.y), v.z);
		}

		public static float MinComponent(this Vector4 v)
		{
			return Mathf.Min(Mathf.Min(Mathf.Min(v.x, v.y), v.z), v.w);
		}

		public static float MaxComponent(this Vector2 v)
		{
			return Mathf.Max(v.x, v.y);
		}

		public static float MaxComponent(this Vector3 v)
		{
			return Mathf.Max(Mathf.Max(v.x, v.y), v.z);
		}

		public static float MaxComponent(this Vector4 v)
		{
			return Mathf.Max(Mathf.Max(Mathf.Max(v.x, v.y), v.z), v.w);
		}

		public static float MinAbsComponent(this Vector2 v)
		{
			return Mathf.Min(Mathf.Abs(v.x), Mathf.Abs(v.y));
		}

		public static float MinAbsComponent(this Vector3 v)
		{
			return Mathf.Min(Mathf.Min(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z));
		}

		public static float MinAbsComponent(this Vector4 v)
		{
			return Mathf.Min(Mathf.Min(Mathf.Min(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z)), Mathf.Abs(v.w));
		}

		public static float MaxAbsComponent(this Vector2 v)
		{
			return Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
		}

		public static float MaxAbsComponent(this Vector3 v)
		{
			return Mathf.Max(Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z));
		}

		public static float MaxAbsComponent(this Vector4 v)
		{
			return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z)), Mathf.Abs(v.w));
		}

		public static float SinMagnitude(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.y - lhs.y * rhs.x;
		}

		public static Vector2 ProjectOnto(this Vector2 v, Vector2 target)
		{
			var targetUnit = target.normalized;
			return Vector2.Dot(v, targetUnit) * targetUnit;
		}

		public static Vector2 ProjectOntoUnit(this Vector2 v, Vector2 target)
		{
			return Vector2.Dot(v, target) * target;
		}

		public static Vector3 ProjectOnto(this Vector3 v, Vector3 target)
		{
			var targetUnit = target.normalized;
			return Vector3.Dot(v, targetUnit) * targetUnit;
		}

		public static Vector3 ProjectOntoUnit(this Vector3 v, Vector3 target)
		{
			return Vector3.Dot(v, target) * target;
		}

		public static Vector4 ProjectOnto(this Vector4 v, Vector4 target)
		{
			var targetUnit = target.normalized;
			return Vector4.Dot(v, targetUnit) * targetUnit;
		}

		public static Vector4 ProjectOntoUnit(this Vector4 v, Vector4 target)
		{
			return Vector4.Dot(v, target) * target;
		}

		#endregion

		#region Plane/Line Intersection

		public static float GetIntersectionParameter(Plane plane, Ray line)
		{
			return -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / Vector3.Dot(line.direction, plane.normal);
		}

		public static bool GetIntersectionParameter(Plane plane, Ray line, out float intersectionParameter)
		{
			var denominator = Vector3.Dot(line.direction, plane.normal);
			if (denominator != 0f)
			{
				intersectionParameter = -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / denominator;
				return true;
			}
			else
			{
				intersectionParameter = float.NaN;
				return false;
			}
		}

		public static float GetIntersectionParameter(Plane plane, ScaledRay line)
		{
			return -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / Vector3.Dot(line.direction, plane.normal);
		}

		public static bool GetIntersectionParameter(Plane plane, ScaledRay line, out float intersectionParameter)
		{
			var denominator = Vector3.Dot(line.direction, plane.normal);
			if (denominator != 0f)
			{
				intersectionParameter = -(Vector3.Dot(line.origin, plane.normal) + plane.distance) / denominator;
				return true;
			}
			else
			{
				intersectionParameter = float.NaN;
				return false;
			}
		}

		public static Vector3 Intersect(Plane plane, Ray line)
		{
			return line.direction * GetIntersectionParameter(plane, line) + line.origin;
		}

		public static bool Intersect(Plane plane, Ray line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return true;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		public static Vector3 Intersect(Plane plane, ScaledRay line)
		{
			return line.direction * GetIntersectionParameter(plane, line) + line.origin;
		}

		public static bool Intersect(Plane plane, ScaledRay line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return true;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		public static bool IntersectForward(Plane plane, Ray line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return intersectionParameter >= 0f;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		public static bool IntersectForward(Plane plane, ScaledRay line, out Vector3 intersectionPoint)
		{
			float intersectionParameter;
			if (GetIntersectionParameter(plane, line, out intersectionParameter))
			{
				intersectionPoint = line.direction * intersectionParameter + line.origin;
				return intersectionParameter >= 0f;
			}
			else
			{
				intersectionPoint = Vector3.zero;
				return false;
			}
		}

		public static Vector3 Intersect(Plane plane, Vector3 endPoint0, Vector3 endPoint1)
		{
			return Intersect(plane, new ScaledRay(endPoint0, endPoint1 - endPoint0));
		}

		public static bool Intersect(Plane plane, Vector3 endPoint0, Vector3 endPoint1, out Vector3 intersectionPoint)
		{
			return Intersect(plane, new ScaledRay(endPoint0, endPoint1 - endPoint0), out intersectionPoint);
		}

		#endregion

		#region Sphere/Line Intersection

		public static bool GetIntersectionParameters(Sphere sphere, Ray ray, out float t0, out float t1)
		{
			// Derived from the basic equation Length(d*t + p - q) = r, where d is the direction of the ray,
			// p is the origin of the ray, r is the radius of the sphere, and q is the origin of the sphere.
			// This turns into the quadratic formula, with:
			//     a = Length(d)^2 = Dot(d, d) = 1   (since a Ray direction is a unit vector)
			//     b = 2*Dot(d, p - q)
			//     c = Dot(p - q, p - q) - r^2
			// The twos and fours of the above and the quadratic formula all end up cancelling out.

			var delta = ray.origin - sphere.center;
			var directionDeltaDot = Vector3.Dot(ray.direction, delta);
			var directionDeltaSquared = directionDeltaDot * directionDeltaDot;
			var deltaLengthSquared = Vector3.Dot(delta, delta);
			var radiusSquared = sphere.radius * sphere.radius;

			var square = directionDeltaSquared - deltaLengthSquared + radiusSquared;

			if (square > 0f)
			{
				var squareRoot = Mathf.Sqrt(square);
				t0 = -directionDeltaDot + squareRoot;
				t1 = -directionDeltaDot - squareRoot;
				return true;
			}
			else if (square == 0f)
			{
				t0 = t1 = -directionDeltaDot;
				return true;
			}
			else
			{
				t0 = t1 = float.NaN;
				return false;
			}
		}

		public static bool GetIntersectionParameters(Sphere sphere, ScaledRay ray, out float t0, out float t1)
		{
			// Derived from the basic equation Length(d*t + p - q) = r, where d is the direction of the ray,
			// p is the origin of the ray, r is the radius of the sphere, and q is the origin of the sphere.
			// This turns into the quadratic formula, with:
			//     a = Length(d)^2 = Dot(d, d)
			//     b = 2*Dot(d, p - q)
			//     c = Dot(p - q, p - q) - r^2
			// The twos and fours of the above and the quadratic formula all end up cancelling out.

			var delta = ray.origin - sphere.center;
			var directionDeltaDot = Vector3.Dot(ray.direction, delta);
			var directionDeltaSquared = directionDeltaDot * directionDeltaDot;
			var directionLengthSquared = Vector3.Dot(ray.direction, ray.direction);
			var deltaLengthSquared = Vector3.Dot(delta, delta);
			var radiusSquared = sphere.radius * sphere.radius;

			var square = directionDeltaSquared - directionLengthSquared * (deltaLengthSquared - radiusSquared);

			if (square > 0f)
			{
				var squareRoot = Mathf.Sqrt(square);
				t0 = (-directionDeltaDot + squareRoot) / directionLengthSquared;
				t1 = (-directionDeltaDot - squareRoot) / directionLengthSquared;
				return true;
			}
			else if (square == 0f)
			{
				t0 = t1 = -directionDeltaDot / directionLengthSquared;
				return true;
			}
			else
			{
				t0 = t1 = float.NaN;
				return false;
			}
		}

		public static bool Intersect(Sphere sphere, Ray ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t0 >= 0f && t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * Mathf.Min(t0, t1);
					return true;
				}
				else if (t0 >= 0f)
				{
					intersection = ray.origin + ray.direction * t0;
					return true;
				}
				else if (t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * t1;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		public static bool IntersectForwardExternal(Sphere sphere, ScaledRay ray, out Vector3 intersection)
		{
			float t0, t1;
			if (GetIntersectionParameters(sphere, ray, out t0, out t1))
			{
				if (t0 >= 0f && t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * Mathf.Min(t0, t1);
					return true;
				}
				else if (t0 >= 0f)
				{
					intersection = ray.origin + ray.direction * t0;
					return true;
				}
				else if (t1 >= 0f)
				{
					intersection = ray.origin + ray.direction * t1;
					return true;
				}
				else
				{
					intersection = Vector3.zero;
					return false;
				}
			}
			else
			{
				intersection = Vector3.zero;
				return false;
			}
		}

		#endregion

		#region Plane Operations

		public static Ray Intersect(Plane plane0, Plane plane1)
		{
			var lineVector = Vector3.Cross(plane0.normal, plane1.normal);
			var angleCosine = Vector3.Dot(plane0.normal, plane1.normal);
			var angleSineSquared = Vector3.Dot(lineVector, lineVector);
			var component0 = (angleCosine * plane1.distance - plane0.distance) * plane0.normal;
			var component1 = (angleCosine * plane0.distance - plane1.distance) * plane1.normal;
			var lineOrigin = (component0 + component1) / angleSineSquared;
			return new Ray(lineOrigin, lineVector);
		}

		public static Vector3 Intersect(Plane plane0, Plane plane1, Plane plane2)
		{
			var line = Intersect(plane0, plane1);
			return Intersect(plane2, line);
		}

		public static Plane Flip(this Plane plane)
		{
			return new Plane(-plane.normal, -plane.distance);
		}

		public static void Flip(ref Plane plane)
		{
			plane.normal = -plane.normal;
			plane.distance = -plane.distance;
		}

		public static SerializablePlane Flip(this SerializablePlane plane)
		{
			return new SerializablePlane(-plane.normal, -plane.distance);
		}

		public static void Flip(ref SerializablePlane plane)
		{
			plane.normal = -plane.normal;
			plane.distance = -plane.distance;
		}

		public static Plane Shift(this Plane plane, float distance)
		{
			return new Plane(plane.normal, plane.distance - distance);
		}

		public static SerializablePlane Shift(this SerializablePlane plane, float distance)
		{
			return new SerializablePlane(plane.normal, plane.distance - distance);
		}

		#endregion

		#region Ray Operations

		public static Ray TransformRay(this Transform transform, Ray ray)
		{
			return new Ray(
				transform.TransformPoint(ray.origin),
				transform.TransformVector(ray.direction));
		}

		public static ScaledRay TransformRay(this Transform transform, ScaledRay ray)
		{
			return new ScaledRay(
				transform.TransformPoint(ray.origin),
				transform.TransformVector(ray.direction));
		}

		public static Ray InverseTransformRay(this Transform transform, Ray ray)
		{
			return new Ray(
				transform.InverseTransformPoint(ray.origin),
				transform.InverseTransformVector(ray.direction));
		}

		public static ScaledRay InverseTransformRay(this Transform transform, ScaledRay ray)
		{
			return new ScaledRay(
				transform.InverseTransformPoint(ray.origin),
				transform.InverseTransformVector(ray.direction));
		}

		#endregion

		#region Cuboid Operations

		public static int FindMatchingPlane(Vector3 normal, Plane[] planes)
		{
			int bestIndex = -1;
			float bestCosine = float.NegativeInfinity;

			for (int i = 0; i < planes.Length; ++i)
			{
				var cosine = Vector3.Dot(normal, planes[i].normal);
				if (cosine > bestCosine)
				{
					bestIndex = i;
					bestCosine = cosine;
				}
			}

			return bestIndex;
		}

		public static Vector3[] FindCuboidCorners(Vector3 right, Vector3 up, Vector3 forward, Plane[] planes)
		{
			return FindCuboidCorners(right, up, forward, planes, new Vector3[8]);
		}

		public static Vector3[] FindCuboidCorners(Vector3 right, Vector3 up, Vector3 forward, Plane[] planes, Vector3[] corners)
		{
			var rightPlane = planes[FindMatchingPlane(-right, planes)];
			var leftPlane = planes[FindMatchingPlane(right, planes)];
			var upperPlane = planes[FindMatchingPlane(-up, planes)];
			var lowerPlane = planes[FindMatchingPlane(up, planes)];
			var farPlane = planes[FindMatchingPlane(-forward, planes)];
			var nearPlane = planes[FindMatchingPlane(forward, planes)];

			var lineLeftLower = Intersect(lowerPlane, leftPlane);
			var lineRightLower = Intersect(lowerPlane, rightPlane);
			var lineLeftUpper = Intersect(upperPlane, leftPlane);
			var lineRightUpper = Intersect(upperPlane, rightPlane);

			corners[0] = Intersect(nearPlane, lineLeftLower);
			corners[1] = Intersect(nearPlane, lineRightLower);
			corners[2] = Intersect(nearPlane, lineLeftUpper);
			corners[3] = Intersect(nearPlane, lineRightUpper);
			corners[4] = Intersect(farPlane, lineLeftLower);
			corners[5] = Intersect(farPlane, lineRightLower);
			corners[6] = Intersect(farPlane, lineLeftUpper);
			corners[7] = Intersect(farPlane, lineRightUpper);

			return corners;
		}

		public static Vector3[] FindFrustumCorners(Camera camera, Plane[] planes)
		{
			return FindFrustumCorners(camera, planes, new Vector3[8]);
		}

		public static Vector3[] FindFrustumCorners(Camera camera, Plane[] planes, Vector3[] corners)
		{
			return FindCuboidCorners(camera.transform.right, camera.transform.up, camera.transform.forward, planes, corners);
		}

		#endregion

		#region Bounding Operations

		public static void FindBoundingSphere(Vector3[] points, out Vector3 center, out float radius)
		{
			var p0 = points[0];
			var p1 = points[1];
			center = (p0 + p1) * 0.5f;
			var delta = p0 - p1;
			var radiusSquared = delta.sqrMagnitude * 0.25f;
			radius = Mathf.Sqrt(radiusSquared);

			for (int i = 2; i < points.Length; ++i)
			{
				delta = points[i] - center;
				var distanceSquared = delta.sqrMagnitude;
				if (distanceSquared > radiusSquared)
				{
					var distance = Mathf.Sqrt(distanceSquared);
					var excess = distance - radius;
					center += delta * (excess * 0.5f / distance);
				}
			}
		}

		public static Vector2 AxisAlignedMin(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(
				Mathf.Min(lhs.x, rhs.x),
				Mathf.Min(lhs.y, rhs.y));
		}

		public static Vector3 AxisAlignedMin(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				Mathf.Min(lhs.x, rhs.x),
				Mathf.Min(lhs.y, rhs.y),
				Mathf.Min(lhs.z, rhs.z));
		}

		public static Vector4 AxisAlignedMin(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(
				Mathf.Min(lhs.x, rhs.x),
				Mathf.Min(lhs.y, rhs.y),
				Mathf.Min(lhs.z, rhs.z),
				Mathf.Min(lhs.w, rhs.w));
		}

		public static Vector2 AxisAlignedMin(params Vector2[] vectors)
		{
			var min = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				min = AxisAlignedMin(min, vectors[i]);
			}
			return min;
		}

		public static Vector3 AxisAlignedMin(params Vector3[] vectors)
		{
			var min = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				min = AxisAlignedMin(min, vectors[i]);
			}
			return min;
		}

		public static Vector4 AxisAlignedMin(params Vector4[] vectors)
		{
			var min = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				min = AxisAlignedMin(min, vectors[i]);
			}
			return min;
		}

		public static Vector2 AxisAlignedMax(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(
				Mathf.Max(lhs.x, rhs.x),
				Mathf.Max(lhs.y, rhs.y));
		}

		public static Vector3 AxisAlignedMax(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(
				Mathf.Max(lhs.x, rhs.x),
				Mathf.Max(lhs.y, rhs.y),
				Mathf.Max(lhs.z, rhs.z));
		}

		public static Vector4 AxisAlignedMax(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(
				Mathf.Max(lhs.x, rhs.x),
				Mathf.Max(lhs.y, rhs.y),
				Mathf.Max(lhs.z, rhs.z),
				Mathf.Max(lhs.w, rhs.w));
		}

		public static Vector2 AxisAlignedMax(params Vector2[] vectors)
		{
			var max = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				max = AxisAlignedMax(max, vectors[i]);
			}
			return max;
		}

		public static Vector3 AxisAlignedMax(params Vector3[] vectors)
		{
			var max = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				max = AxisAlignedMax(max, vectors[i]);
			}
			return max;
		}

		public static Vector4 AxisAlignedMax(params Vector4[] vectors)
		{
			var max = vectors[0];
			for (int i = 1; i < vectors.Length; ++i)
			{
				max = AxisAlignedMax(max, vectors[i]);
			}
			return max;
		}

		public static float TruncateLineSegment(ScaledRay ray, Plane plane, ref float minT, ref float maxT)
		{
			var denominator = Vector3.Dot(plane.normal, ray.direction);
			if (denominator > 0f)
			{
				var intersectionT = (Vector3.Dot(plane.normal, ray.origin) - plane.distance) / denominator;
				minT = Mathf.Min(minT, intersectionT);
			}
			else if (denominator < 0f)
			{
				var intersectionT = (Vector3.Dot(plane.normal, ray.origin) - plane.distance) / denominator;
				maxT = Mathf.Min(maxT, intersectionT);
			}
			else if (!plane.GetSide(ray.origin))
			{
				maxT = minT;
			}

			return maxT - minT;
		}

		public static bool AllAreAbove(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) <= 0f) return false;
			}
			return true;
		}

		public static bool AnyAreAbove(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) > 0f) return true;
			}
			return false;
		}

		public static bool AllAreBelow(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) >= 0f) return false;
			}
			return true;
		}

		public static bool AnyAreBelow(Vector3[] points, Plane plane)
		{
			foreach (var point in points)
			{
				if (plane.GetDistanceToPoint(point) < 0f) return true;
			}
			return false;
		}

		public static Vector3 GetMinRelativeToNormal(this Bounds box, Vector3 normal)
		{
			return new Vector3(
				(normal.x >= 0) ? box.min.x : box.max.x,
				(normal.y >= 0) ? box.min.y : box.max.y,
				(normal.z >= 0) ? box.min.z : box.max.z);
		}

		public static Vector3 GetMaxRelativeToNormal(this Bounds box, Vector3 normal)
		{
			return new Vector3(
				(normal.x >= 0) ? box.max.x : box.min.x,
				(normal.y >= 0) ? box.max.y : box.min.y,
				(normal.z >= 0) ? box.max.z : box.min.z);
		}

		public static void GetMinMaxRelativeToNormal(this Bounds box, Vector3 normal, out Vector3 min, out Vector3 max)
		{
			min = box.min;
			max = box.max;
			if (normal.x < 0) MIUtilities.Swap(ref min.x, ref max.x);
			if (normal.y < 0) MIUtilities.Swap(ref min.y, ref max.y);
			if (normal.z < 0) MIUtilities.Swap(ref min.z, ref max.z);
		}

		public static bool IntersectsOrTouches(this Bounds box, Plane plane)
		{
			Vector3 min, max;
			box.GetMinMaxRelativeToNormal(plane.normal, out min, out max);
			return plane.GetDistanceToPoint(min) <= 0f && plane.GetDistanceToPoint(max) >= 0f;
		}

		public static bool Intersects(this Bounds box, Plane plane)
		{
			Vector3 min, max;
			box.GetMinMaxRelativeToNormal(plane.normal, out min, out max);
			return plane.GetDistanceToPoint(min) < 0f && plane.GetDistanceToPoint(max) > 0f;
		}

		public static bool IsAbove(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMinRelativeToNormal(plane.normal)) > 0f;
		}

		public static bool IsAboveOrTouches(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMinRelativeToNormal(plane.normal)) >= 0f;
		}

		public static bool IsAboveOrIntersects(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMaxRelativeToNormal(plane.normal)) > 0f;
		}

		public static bool IsBelow(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMaxRelativeToNormal(plane.normal)) < 0f;
		}

		public static bool IsBelowOrTouches(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMaxRelativeToNormal(plane.normal)) <= 0f;
		}

		public static bool IsBelowOrIntersects(this Bounds box, Plane plane)
		{
			return plane.GetDistanceToPoint(box.GetMinRelativeToNormal(plane.normal)) < 0f;
		}

		#endregion
	}
}
