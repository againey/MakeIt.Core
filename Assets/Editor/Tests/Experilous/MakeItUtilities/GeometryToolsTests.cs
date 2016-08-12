/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Tests
{
	public class GeometryToolsTests
	{
		#region GetIntersectionParameter

		#region GetIntersectionParameter_NegativeOffset

		[Test] public static void GetIntersectionParameter_NegativeOffset_PerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InversePerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_PerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InversePerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_PerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InversePerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_NonPerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InverseNonPerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_NonPerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InverseNonPerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_NonPerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_InverseNonPerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelNegative()
		{
			Assert.That(float.IsInfinity(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelZeroOffset()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(2f, 3f, 4f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_NegativeOffset_ParallelPositive()
		{
			Assert.That(float.IsInfinity(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), -2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		#endregion

		#region GetIntersectionParameter_ZeroOffset

		[Test] public static void GetIntersectionParameter_ZeroOffset_PerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InversePerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_PerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InversePerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_PerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InversePerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_NonPerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InverseNonPerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_NonPerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InverseNonPerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_NonPerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_InverseNonPerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelNegative()
		{
			Assert.That(float.IsInfinity(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 0f, 0f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelZeroOffset()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(0f, 3f, 4f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_ZeroOffset_ParallelPositive()
		{
			Assert.That(float.IsInfinity(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 0f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		#endregion

		#region GetIntersectionParameter_PositiveOffset

		[Test] public static void GetIntersectionParameter_PositiveOffset_PerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InversePerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(-0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_PerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InversePerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_PerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InversePerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(2f, 0f, 0f))),
				Is.EqualTo(0.5f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_NonPerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InverseNonPerpendicularNegative()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(-0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_NonPerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InverseNonPerpendicularZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(2f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_NonPerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_InverseNonPerpendicularPositive()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(-1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(1f, 0f, 0f), new Vector3(4f, 2f, 1f))),
				Is.EqualTo(0.25f).Within(0.0001f));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelNegative()
		{
			Assert.That(float.IsInfinity(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-3f, 0f, 0f), new Vector3(0f, 4f, 3f)))));
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelZero()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 0f, 0f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelZeroOffset()
		{
			Assert.That(
				GeometryTools.GetIntersectionParameter(
					new Plane(new Vector3(1f, 0f, 0f), 2f),
					new ScaledRay(new Vector3(-2f, 3f, 4f), new Vector3(0f, 4f, 3f))),
				Is.NaN);
		}

		[Test] public static void GetIntersectionParameter_PositiveOffset_ParallelPositive()
		{
			Assert.That(float.IsInfinity(
				GeometryTools.GetIntersectionParameter(
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
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -2f),
						new Ray(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), -2f),
						new Ray(new Vector3(3f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_PerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -2f),
						new Ray(new Vector3(-2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_InversePerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), -2f),
						new Ray(new Vector3(2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_PerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), -2f),
						new Ray(new Vector3(-3f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_NegativeOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 0f),
						new Ray(new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 0f),
						new Ray(new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_PerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 0f),
						new Ray(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_InversePerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 0f),
						new Ray(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_PerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 0f),
						new Ray(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(0f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_ZeroOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 2f),
						new Ray(new Vector3(3f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_InversePerpendicularNegative()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 2f),
						new Ray(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_PerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 2f),
						new Ray(new Vector3(2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_InversePerpendicularZero()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), 2f),
						new Ray(new Vector3(-2f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_PerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
						new Plane(new Vector3(1f, 0f, 0f), 2f),
						new Ray(new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f))),
					new Vector3(-2f, 0f, 0f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		[Test] public static void Intersect_PlaneLine_PositiveOffset_InversePerpendicularPositive()
		{
			Assert.That(
				Vector3.Distance(
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
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
					GeometryTools.Intersect(
						new Plane(new Vector3(-1f, 0f, 0f), -3f),
						new Plane(new Vector3(0f, -1f, 0f), -4f),
						new Plane(new Vector3(0f, 0f, -1f), -5f)),
					new Vector3(-3f, -4f, -5f)),
				Is.EqualTo(0f).Within(0.0001f));
		}

		#endregion
	}
}
#endif
