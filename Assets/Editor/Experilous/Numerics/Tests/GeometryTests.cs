/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Numerics.Tests
{
	class GeometryTests
	{
		#region GetIntersectionParameter

		#region GetIntersectionParameter_NegativeOffset

		[Test] public static void GetIntersectionParameter_NegativeOffset_PerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_PerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InversePerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_PerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_NonPerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InverseNonPerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_NonPerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InverseNonPerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_NonPerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InverseNonPerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelNegative()
		{
			Assert.That(float.IsInfinity(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelZeroOffset()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 3f, 4f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelPositive()
		{
			Assert.That(float.IsInfinity(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		#endregion

		#region GetIntersectionParameter_ZeroOffset

		[Test] public static void GetIntersectionParameter_ZeroOffset_PerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_PerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InversePerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_PerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_NonPerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InverseNonPerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_NonPerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InverseNonPerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_NonPerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InverseNonPerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelNegative()
		{
			Assert.That(float.IsInfinity(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelZeroOffset()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 3f, 4f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelPositive()
		{
			Assert.That(float.IsInfinity(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		#endregion

		#region GetIntersectionParameter_PositiveOffset

		[Test] public static void GetIntersectionParameter_PositiveOffset_PerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_PerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InversePerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_PerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_NonPerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InverseNonPerpendicularNegative()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_NonPerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InverseNonPerpendicularZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_NonPerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InverseNonPerpendicularPositive()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelNegative()
		{
			Assert.That(float.IsInfinity(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelZero()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelZeroOffset()
		{
			Assert.That(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 3f, 4f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelPositive()
		{
			Assert.That(float.IsInfinity(
				Geometry.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		#endregion

		#endregion

		#region Intersect_PlaneLine

		#region Intersect_PlaneLine_NegativeOffset

		[Test] public static void Intersect_PlaneLine_NegativeOffset_PerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -2f),
						new Ray(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), -2f),
						new Ray(new Vector3(3f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_PerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -2f),
						new Ray(new Vector3(-2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_InversePerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), -2f),
						new Ray(new Vector3(2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_PerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -2f),
						new Ray(new Vector3(-3f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), -2f),
						new Ray(new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		#endregion

		#region Intersect_PlaneLine_ZeroOffset

		[Test] public static void Intersect_PlaneLine_ZeroOffset_PerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 0f),
						new Ray(new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 0f),
						new Ray(new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_PerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 0f),
						new Ray(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_InversePerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 0f),
						new Ray(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_PerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 0f),
						new Ray(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 0f),
						new Ray(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		#endregion

		#region Intersect_PlaneLine_PositiveOffset

		[Test] public static void Intersect_PlaneLine_PositiveOffset_PerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 2f),
						new Ray(new Vector3(3f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 2f),
						new Ray(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_PerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 2f),
						new Ray(new Vector3(2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_InversePerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 2f),
						new Ray(new Vector3(-2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_PerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 2f),
						new Ray(new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 2f),
						new Ray(new Vector3(-3f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		#endregion

		#endregion

		#region Intersect_PlanePlanePlane

		[Test] public static void Intersect_PlanePlanePlane_PosPosPos_PosPosPos()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, 1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, 1f), 5f)),
					new Vector3(-3f, -4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosPosPos_PosPosNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, 1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, 1f), -5f)),
					new Vector3(-3f, -4f, 5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosPosPos_PosNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, 1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, 1f), -5f)),
					new Vector3(-3f, 4f, 5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosPosPos_NegNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -3f),
						new Plane(new Vector3(0f, 1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, 1f), -5f)),
					new Vector3(3f, 4f, 5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosPosNeg_PosPosPos()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, 1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, -1f), 5f)),
					new Vector3(-3f, -4f, 5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosPosNeg_PosPosNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, 1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(-3f, -4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosPosNeg_PosNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, 1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(-3f, 4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosPosNeg_NegNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -3f),
						new Plane(new Vector3(0f, 1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(3f, 4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosNegNeg_PosPosPos()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, -1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, -1f), 5f)),
					new Vector3(-3f, 4f, 5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosNegNeg_PosPosNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, -1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(-3f, 4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosNegNeg_PosNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, -1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(-3f, -4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_PosNegNeg_NegNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -3f),
						new Plane(new Vector3(0f, -1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(3f, -4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_NegNegNeg_PosPosPos()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, -1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, -1f), 5f)),
					new Vector3(3f, 4f, 5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_NegNegNeg_PosPosNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, -1f, 0f), 4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(3f, 4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_NegNegNeg_PosNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 3f),
						new Plane(new Vector3(0f, -1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(3f, -4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlanePlanePlane_NegNegNeg_NegNegNeg()
		{
			Assert.That(
				Vector3.Distance(
					Geometry.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), -3f),
						new Plane(new Vector3(0f, -1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(-3f, -4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		#endregion

		[Test] public static void Intersect_SphereRay_NoIntersection()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(Vector3.zero, Vector3.forward), out intersection));
		}

		[Test] public static void Intersect_SphereRay_PosTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(-1f, 1f, 0f), Vector3.right), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereRay_ZeroTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0f), Vector3.right), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereRay_NegTangent()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(2f, 1f, 0f), Vector3.right), out intersection));
		}

		[Test] public static void Intersect_SphereRay_External()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, -2f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereRay_SurfacePointingIn()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereRay_Internal()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0.5f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereRay_SurfacePointingOut()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 2f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereRay_NegExternal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.Intersect(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 3f), Vector3.forward), out intersection));
		}

		[Test] public static void Intersect_SphereScaledRay_NoIntersection()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(Vector3.zero, Vector3.forward * 1.5f), out intersection));
		}

		[Test] public static void Intersect_SphereScaledRay_PosTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(-1f, 1f, 0f), Vector3.right * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereScaledRay_ZeroTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0f), Vector3.right * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereScaledRay_NegTangent()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(2f, 1f, 0f), Vector3.right * 1.5f), out intersection));
		}

		[Test] public static void Intersect_SphereScaledRay_External()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, -2f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereScaledRay_SurfacePointingIn()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereScaledRay_Internal()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0.5f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereScaledRay_SurfacePointingOut()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 2f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void Intersect_SphereScaledRay_NegExternal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.Intersect(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 3f), Vector3.forward * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereRay_NoIntersection()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(Vector3.zero, Vector3.forward), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereRay_PosTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(-1f, 1f, 0f), Vector3.right), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereRay_ZeroTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0f), Vector3.right), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereRay_NegTangent()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(2f, 1f, 0f), Vector3.right), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereRay_External()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, -2f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereRay_SurfacePointingIn()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereRay_Internal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0.5f), Vector3.forward), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereRay_SurfacePointingOut()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 2f), Vector3.forward), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereRay_NegExternal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 3f), Vector3.forward), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_NoIntersection()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(Vector3.zero, Vector3.forward * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_PosTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(-1f, 1f, 0f), Vector3.right * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_ZeroTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0f), Vector3.right * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_NegTangent()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(2f, 1f, 0f), Vector3.right * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_External()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, -2f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_SurfacePointingIn()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_Internal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0.5f), Vector3.forward * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_SurfacePointingOut()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 2f), Vector3.forward * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardExternal_SphereScaledRay_NegExternal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardExternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 3f), Vector3.forward * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardInternal_SphereRay_NoIntersection()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(Vector3.zero, Vector3.forward), out intersection));
		}

		[Test] public static void IntersectForwardInternal_SphereRay_PosTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(-1f, 1f, 0f), Vector3.right), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereRay_ZeroTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0f), Vector3.right), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereRay_NegTangent()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(2f, 1f, 0f), Vector3.right), out intersection));
		}

		[Test] public static void IntersectForwardInternal_SphereRay_External()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, -2f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereRay_SurfacePointingIn()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereRay_Internal()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 0.5f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereRay_SurfacePointingOut()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 2f), Vector3.forward), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereRay_NegExternal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new Ray(new Vector3(1f, 1f, 3f), Vector3.forward), out intersection));
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_NoIntersection()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(Vector3.zero, Vector3.forward * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_PosTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(-1f, 1f, 0f), Vector3.right * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_ZeroTangent()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0f), Vector3.right * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 0f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_NegTangent()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(2f, 1f, 0f), Vector3.right * 1.5f), out intersection));
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_External()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, -2f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_SurfacePointingIn()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_Internal()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 0.5f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_SurfacePointingOut()
		{
			Vector3 intersection;
			Assert.IsTrue(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 2f), Vector3.forward * 1.5f), out intersection));
			Assert.Less((intersection - new Vector3(1f, 1f, 2f)).magnitude, 0.0001f);
		}

		[Test] public static void IntersectForwardInternal_SphereScaledRay_NegExternal()
		{
			Vector3 intersection;
			Assert.IsFalse(Geometry.IntersectForwardInternal(new Sphere(Vector3.one, 1f), new ScaledRay(new Vector3(1f, 1f, 3f), Vector3.forward * 1.5f), out intersection));
		}
	}
}
#endif
