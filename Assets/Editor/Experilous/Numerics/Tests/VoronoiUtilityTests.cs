/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;
using UnityEngine;
using Experilous.Topologies.Detail;

namespace Experilous.Topologies.Tests
{
	class VoronoiUtilityTests
	{
		#region Assert

		private static void AssertApproximatelyEqual(float expected, float actual, float margin, string message = null)
		{
			Assert.IsFalse(float.IsNaN(actual), message != null ? message : string.Format("Expected {0:G8} but actual was {1:G8}.", expected, actual));
			Assert.LessOrEqual(Mathf.Abs(expected - actual), margin, message != null ? message : string.Format("Expected {0:G8} but actual was {1:G8}.", expected, actual));
		}

		private static void AssertApproximatelyEqual(Vector2 expected, Vector2 actual, float margin, string message = null)
		{
			Assert.IsFalse(float.IsNaN(actual.x) || float.IsNaN(actual.y), message != null ? message : string.Format("Expected {0:F4} but actual was {1:F4}.", expected, actual));
			Assert.LessOrEqual(Vector2.Distance(expected, actual), margin, message != null ? message : string.Format("Expected {0:F4} but actual was {1:F4}.", expected, actual));
		}

		private static void AssertApproximatelyLessThanOrEqual(float lhs, float rhs, float margin, string message = null)
		{
			Assert.LessOrEqual(lhs - rhs, margin, message != null ? message : string.Format("Expected {0:G8} to be less than or equal to {1:G8} but was actually greater.", lhs, rhs));
		}

		private static void AssertIsNaN(float actual, string message = null)
		{
			Assert.IsTrue(float.IsNaN(actual), message != null ? message : string.Format("Expected NaN but actual was {0:G8}.", actual));
		}

		private void AssertFalse_CheckForMergeEvent(float errorMargin, Transform2 transform, string methodName, string message, int[] indices, params Vector2[] rawPositions)
		{
			var method = typeof(VoronoiUtility).GetMethod("CheckForMergeEvent_" + methodName);

			GraphNodeDataArray<Vector2> nodePositions = new GraphNodeDataArray<Vector2>((Vector2[])rawPositions.Clone());
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);

			var parameterInfo = method.GetParameters();
			var parameters = new object[parameterInfo.Length];
			for (int i = 0; i < rawPositions.Length; ++i)
			{
				nodePositions[i] = transform.TransformPosition(rawPositions[i]);
			}
			for (int i = 0; i < indices.Length; ++i)
			{
				parameters[i] = indices[i];
			}
			for (int i = indices.Length; i < parameterInfo.Length; ++i)
			{
				if (parameterInfo[i].ParameterType == typeof(IGraphNodeData<Vector2>) && parameterInfo[i].Name == "nodePositions") parameters[i] = nodePositions;
				else if (parameterInfo[i].ParameterType == typeof(float) && parameterInfo[i].Name == "errorMargin") parameters[i] = errorMargin;
				else if (parameterInfo[i].ParameterType == typeof(VoronoiUtility.BeachSegment)) parameters[i] = segment;
				else if (parameterInfo[i].ParameterType == typeof(VoronoiUtility.MergeEventQueue)) parameters[i] = queue;
			}
			method.Invoke(null, parameters);

			bool found = ExtractActualValuesFromQueue(queue);

			object[] formattedPositions = new object[nodePositions.Count];
			for (int i = 0; i < nodePositions.Count; ++i)
			{
				formattedPositions[i] = nodePositions[i].ToString("F3");
			}
			var mergeMessage = string.Format(message, formattedPositions);

			Assert.IsFalse(found, string.Format("Expected no merge of {0}, but got a merge at {1} with distance {2:G8}.", mergeMessage, actualMergePosition.ToString("F4"), actualDistance));
		}

		private void AssertTrue_CheckForMergeEvent(Vector2 expectedMergePosition, float expectedDistance, float errorMargin, Transform2 transform, string methodName, string message, int[] indices, params Vector2[] rawPositions)
		{
			var method = typeof(VoronoiUtility).GetMethod("CheckForMergeEvent_" + methodName);

			expectedMergePosition = transform.TransformPosition(expectedMergePosition);
			expectedDistance = transform.TransformDistance(expectedDistance);

			GraphNodeDataArray<Vector2> nodePositions = new GraphNodeDataArray<Vector2>((Vector2[])rawPositions.Clone());
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);

			var parameterInfo = method.GetParameters();
			var parameters = new object[parameterInfo.Length];
			for (int i = 0; i < rawPositions.Length; ++i)
			{
				nodePositions[i] = transform.TransformPosition(rawPositions[i]);
			}
			for (int i = 0; i < indices.Length; ++i)
			{
				parameters[i] = indices[i];
			}
			for (int i = indices.Length; i < parameterInfo.Length; ++i)
			{
				if (parameterInfo[i].ParameterType == typeof(IGraphNodeData<Vector2>) && parameterInfo[i].Name == "nodePositions") parameters[i] = nodePositions;
				else if (parameterInfo[i].ParameterType == typeof(float) && parameterInfo[i].Name == "errorMargin") parameters[i] = errorMargin;
				else if (parameterInfo[i].ParameterType == typeof(VoronoiUtility.BeachSegment)) parameters[i] = segment;
				else if (parameterInfo[i].ParameterType == typeof(VoronoiUtility.MergeEventQueue)) parameters[i] = queue;
			}
			method.Invoke(null, parameters);

			bool found = ExtractActualValuesFromQueue(queue);

			object[] formattedPositions = new object[nodePositions.Count];
			for (int i = 0; i < nodePositions.Count; ++i)
			{
				formattedPositions[i] = nodePositions[i].ToString("F3");
			}
			var mergeMessage = string.Format(message, formattedPositions);

			Assert.IsTrue(found, string.Format("Expected a merge of {0} at {1} with distance {2:G8}, but got no merge.", mergeMessage, expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, errorMargin, string.Format("Expected a merge of {0} at {1} with distance {2:G8}, but got a merge at {3} with distance {4:G8}.", mergeMessage, expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, errorMargin, string.Format("Expected a merge of {0} at {1} with distance {2:G8}, but got a merge at {3} with distance {4:G8}.", mergeMessage, expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		#endregion

		#region Transform2

		private class Transform2
		{
			private Vector2 _rotateX;
			private Vector2 _rotateY;
			private Vector2 _translate;
			private float _scale;

			public static Transform2 identity
			{
				get
				{
					return new Transform2(1f, 0f, 0f, 0f);
				}
			}

			public static Transform2 Translate(float dx, float dy)
			{
				return new Transform2(1f, 0f, dx, dy);
			}

			public static Transform2 Rotate(float r)
			{
				return new Transform2(1f, r, 0f, 0f);
			}

			public static Transform2 RotateTranslate(float r, float dx, float dy)
			{
				return new Transform2(1f, r, dx, dy);
			}

			public static Transform2 Scale(float s)
			{
				return new Transform2(s, 0f, 0f, 0f);
			}

			public static Transform2 ScaleTranslate(float s, float dx, float dy)
			{
				return new Transform2(s, 0f, dx, dy);
			}

			public static Transform2 ScaleRotate(float s, float r)
			{
				return new Transform2(s, r, 0f, 0f);
			}

			public static Transform2 ScaleRotateTranslate(float s, float r, float dx, float dy)
			{
				return new Transform2(s, r, dx, dy);
			}

			public Transform2(float s, float r, float dx, float dy)
			{
				var radians = r * Mathf.Deg2Rad;
				var sine = Mathf.Sin(radians);
				var cosine = Mathf.Cos(radians);
				_rotateX = new Vector2(cosine, -sine);
				_rotateY = new Vector2(sine, cosine);
				_translate = new Vector2(dx, dy);
				_scale = s;
			}

			public Vector2 TransformPosition(Vector2 p)
			{
				return new Vector2(Vector2.Dot(p, _rotateX), Vector2.Dot(p, _rotateY)) * _scale + _translate;
			}

			public Vector2 TransformDirection(Vector2 v)
			{
				return new Vector2(Vector2.Dot(v, _rotateX), Vector2.Dot(v, _rotateY)) * _scale;
			}

			public float TransformDistance(float d)
			{
				return d * _scale;
			}
		}

		[Test]
		public static void TransformPosition_Identity()
		{
			AssertApproximatelyEqual(new Vector2(0f, 0f), Transform2.identity.TransformPosition(new Vector2(0f, 0f)), 0.0001f);
			AssertApproximatelyEqual(new Vector2(5f, -3f), Transform2.identity.TransformPosition(new Vector2(5f, -3f)), 0.0001f);
		}

		[Test]
		public static void TransformDirection_Identity()
		{
			AssertApproximatelyEqual(new Vector2(0f, 0f), Transform2.identity.TransformDirection(new Vector2(0f, 0f)), 0.0001f);
			AssertApproximatelyEqual(new Vector2(5f, -3f), Transform2.identity.TransformDirection(new Vector2(5f, -3f)), 0.0001f);
		}

		[Test]
		public static void TransformDistance_Identity()
		{
			AssertApproximatelyEqual(0f, Transform2.identity.TransformDistance(0f), 0.0001f);
			AssertApproximatelyEqual(5f, Transform2.identity.TransformDistance(5f), 0.0001f);
		}

		[Test]
		public static void TransformPosition_SRT()
		{
			var transform = Transform2.ScaleRotateTranslate(3f, 36.8698976f, 2f, -4f);
			AssertApproximatelyEqual(new Vector2(2f, -4f), transform.TransformPosition(new Vector2(0f, 0f)), 0.0001f);
			AssertApproximatelyEqual(new Vector2(17f, -4f), transform.TransformPosition(new Vector2(4f, -3f)), 0.0001f);
		}

		[Test]
		public static void TransformDirection_SRT()
		{
			var transform = Transform2.ScaleRotateTranslate(3f, 36.8698976f, 2f, -4f);
			AssertApproximatelyEqual(new Vector2(0f, 0f), transform.TransformDirection(new Vector2(0f, 0f)), 0.0001f);
			AssertApproximatelyEqual(new Vector2(15f, 0f), transform.TransformDirection(new Vector2(4f, -3f)), 0.0001f);
		}

		[Test]
		public static void TransformDistance_SRT()
		{
			var transform = Transform2.ScaleRotateTranslate(3f, 36.8698976f, 2f, -4f);
			AssertApproximatelyEqual(0f, transform.TransformDistance(0f), 0.0001f);
			AssertApproximatelyEqual(15f, transform.TransformDistance(5f), 0.0001f);
		}

		#endregion

		#region ComputeDirectionalOrder

		[Test]
		public static void ComputeDirectionalOrder_CardinalDirections()
		{
			AssertApproximatelyEqual(-4f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-1f,  0f)), 0.0001f);
			AssertApproximatelyEqual(-2f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2( 0f, -1f)), 0.0001f);
			AssertApproximatelyEqual( 0f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+1f,  0f)), 0.0001f);
			AssertApproximatelyEqual(+2f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2( 0f, +1f)), 0.0001f);
		}

		[Test]
		public static void ComputeDirectionalOrder_IntercardinalDirections()
		{
			AssertApproximatelyEqual(-3f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-1f, -1f)), 0.0001f);
			AssertApproximatelyEqual(-1f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+1f, -1f)), 0.0001f);
			AssertApproximatelyEqual(+1f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+1f, +1f)), 0.0001f);
			AssertApproximatelyEqual(+3f, VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-1f, +1f)), 0.0001f);
		}

		[Test]
		public static void ComputeDirectionalOrder_SecondaryIntercardinalDirections()
		{
			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-2f, -1f).normalized), -4f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-2f, -1f).normalized), -3f);

			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-1f, -2f).normalized), -3f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-1f, -2f).normalized), -2f);

			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+1f, -2f).normalized), -2f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+1f, -2f).normalized), -1f);

			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+2f, -1f).normalized), -1f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+2f, -1f).normalized),  0f);

			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+2f, +1f).normalized),  0f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+2f, +1f).normalized), +1f);

			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+1f, +2f).normalized), +1f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(+1f, +2f).normalized), +2f);

			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-1f, +2f).normalized), +2f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-1f, +2f).normalized), +3f);

			Assert.Greater(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-2f, +1f).normalized), +3f);
			Assert.Less(VoronoiUtility.DirectedEdge.ComputeOrder(new Vector2(-2f, +1f).normalized), +4f);
		}

		[Test]
		public static void ComputeDirectionalOrder_SortedList()
		{
			var directions = new Vector2[24];
			for (int i = 0; i < 24; ++i)
			{
				float radians = i * 15 * Mathf.Deg2Rad;
				directions[i] = new Vector2(-Mathf.Cos(radians), -Mathf.Sin(radians));
			}

			for (int i = 0; i < 23; ++i)
			{
				var lhs = directions[i];
				float lhsOrder = VoronoiUtility.DirectedEdge.ComputeOrder(lhs.normalized);

				for (int j = i + 1; j < 24; ++j)
				{
					var rhs = directions[j];
					float rhsOrder = VoronoiUtility.DirectedEdge.ComputeOrder(rhs.normalized);
					Assert.LessOrEqual(lhsOrder - rhsOrder, 0.0001f, string.Format("Expected {0:G8} to be less than or equal to {1:G8} but was actually greater, for direction[{2}] {3} and direction[{4}] {5}.", lhsOrder, rhsOrder, i, lhs.ToString("F4"), j, rhs.ToString("F4")));
				}
			}
		}

		#endregion

		#region GetSegmentRightBound

		#region PointPoint

		[Test]
		public static void GetSegmentRightBound_PointPoint_SameY()
		{
			AssertApproximatelyEqual(3f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 1f), new Vector2(5f, 1f), 1f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(3f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 1f), new Vector2(5f, 1f), 2f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(3f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 1f), new Vector2(5f, 1f), 3f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(3f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 1f), new Vector2(5f, 1f), 4f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_PointPoint_FirstHigher()
		{
			AssertApproximatelyEqual(3f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 4f), new Vector2(5f, 1f), 5f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_PointPoint_SecondHigher()
		{
			AssertApproximatelyEqual(3f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 1f), new Vector2(5f, 4f), 5f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_PointPoint_FirstOnSweepLine()
		{
			AssertApproximatelyEqual(1f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 5f), new Vector2(5f, 4f), 5f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_PointPoint_SecondOnSweepLine()
		{
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 4f), new Vector2(5f, 5f), 5f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_PointPoint_OppositeSides()
		{
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 1f), new Vector2(5f, 5f), 3f, 0.0001f));
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointPoint(new Vector2(1f, 5f), new Vector2(5f, 1f), 3f, 0.0001f));
		}

		#endregion

		#region PointLine

		[Test]
		public static void GetSegmentRightBound_PointLine_SmallPositiveSlope()
		{
			AssertApproximatelyEqual(7f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(5f, 8f), new Vector2(1f, 1f), new Vector2(9f, 7f), 10f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(1f, 8f), new Vector2(8f, 1f), new Vector2(16f, 7f), 10f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(5f, -8f), new Vector2(1f, 1f), new Vector2(9f, 7f), 10f, 0.0001f));
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(1f, -8f), new Vector2(8f, 1f), new Vector2(16f, 7f), 10f, 0.0001f));
		}

		[Test]
		public static void GetSegmentRightBound_PointLine_SmallNegativeSlope()
		{
			AssertApproximatelyEqual(12f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(8f, 11f), new Vector2(1f, 10f), new Vector2(13f, 1f), 13f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(9f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(5f, 9f), new Vector2(1f, 10f), new Vector2(13f, 1f), 13f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(8f, -5f), new Vector2(1f, 10f), new Vector2(13f, 1f), 13f, 0.0001f));
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(5f, -7f), new Vector2(1f, 10f), new Vector2(13f, 1f), 13f, 0.0001f));
		}

		[Test]
		public static void GetSegmentRightBound_PointLine_LargeNegativeSlope()
		{
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(1f, 5f), new Vector2(13f, 1f), new Vector2(4f, 13f), 9f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(9f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(1f, 5f), new Vector2(20f, 1f), new Vector2(11f, 13f), 9f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(21f, 5f), new Vector2(13f, 1f), new Vector2(4f, 13f), 9f, 0.0001f));
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(21f, 5f), new Vector2(20f, 1f), new Vector2(11f, 13f), 9f, 0.0001f));
		}

		[Test]
		public static void GetSegmentRightBound_PointLine_LargePositiveSlope()
		{
			AssertApproximatelyEqual(27f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(11f, 9f), new Vector2(13f, 17f), new Vector2(1f, 1f), 13f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(1f, 9f), new Vector2(13f, 17f), new Vector2(1f, 1f), 13f, 0.0001f));
		}

		[Test]
		public static void GetSegmentRightBound_PointLine_StraightUp()
		{
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(1f, 5f), new Vector2(9f, 1f), new Vector2(9f, 13f), 9f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_PointLine_StraightDown()
		{
			AssertApproximatelyEqual(35f, VoronoiUtility.GetSegmentRightBound_PointLine(new Vector2(19f, 9f), new Vector2(1f, 17f), new Vector2(1f, 1f), 13f, 0.0001f), 0.0001f);
		}

		#endregion

		#region LinePoint

		[Test]
		public static void GetSegmentRightBound_LinePoint_SmallPositiveSlope()
		{
			AssertApproximatelyEqual(2f, VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 1f), new Vector2(13f, 10f), new Vector2(6f, 11f), 13f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 1f), new Vector2(13f, 10f), new Vector2(9f, 9f), 13f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 1f), new Vector2(13f, 10f), new Vector2(6f, -5f), 13f, 0.0001f));
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 1f), new Vector2(13f, 10f), new Vector2(9f, -7f), 13f, 0.0001f));
		}

		[Test]
		public static void GetSegmentRightBound_LinePoint_SmallNegativeSlope()
		{
			AssertApproximatelyEqual(3f, VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 7f), new Vector2(9f, 1f), new Vector2(5f, 8f), 10f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(12f, VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 7f), new Vector2(9f, 1f), new Vector2(16f, 8f), 10f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 7f), new Vector2(9f, 1f), new Vector2(5f, -8f), 10f, 0.0001f));
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(1f, 7f), new Vector2(9f, 1f), new Vector2(16f, -8f), 10f, 0.0001f));
		}

		[Test]
		public static void GetSegmentRightBound_LinePoint_LargePositiveSlope()
		{
			AssertApproximatelyEqual(9f, VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(10f, 13f), new Vector2(1f, 1f), new Vector2(13f, 5f), 9f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(12f, VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(10f, 13f), new Vector2(1f, 1f), new Vector2(20f, 5f), 9f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(10f, 13f), new Vector2(1f, 1f), new Vector2(-7f, 5f), 9f, 0.0001f));
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(10f, 13f), new Vector2(1f, 1f), new Vector2(0f, 5f), 9f, 0.0001f));
		}

		[Test]
		public static void GetSegmentRightBound_LinePoint_LargeNegativeSlope()
		{
			AssertApproximatelyEqual(1f, VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(27f, 1f), new Vector2(15f, 17f), new Vector2(17f, 9f), 13f, 0.0001f), 0.0001f);
			AssertIsNaN(VoronoiUtility.GetSegmentRightBound_LinePoint(new Vector2(27f, 1f), new Vector2(15f, 17f), new Vector2(27f, 9f), 13f, 0.0001f));
		}

		#endregion

		#region LineLine

		[Test]
		public static void GetSegmentRightBound_LineLine_SmallMirroredSlopes()
		{
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(6f, 1f), new Vector2(9f, 5f), 4f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(6f, 1f), new Vector2(9f, 5f), 5f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(6f, 1f), new Vector2(9f, 5f), 6f, 0.0001f), 0.0001f);

			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(4.5f, -1f), new Vector2(6f, 1f), 3f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(4.5f, -1f), new Vector2(6f, 1f), 7f, 0.0001f), 0.0001f);

			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(7.5f, 3f), new Vector2(12f, 9f), 4f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(7.5f, 3f), new Vector2(12f, 9f), 8f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(7.5f, 3f), new Vector2(12f, 9f), 12f, 0.0001f), 0.0001f);

			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 4f), new Vector2(5f, 1f), new Vector2(5f, 1f), new Vector2(9f, 4f), 3f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 4f), new Vector2(5f, 1f), new Vector2(5f, 1f), new Vector2(9f, 4f), 4f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 4f), new Vector2(5f, 1f), new Vector2(5f, 1f), new Vector2(9f, 4f), 5f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_LineLine_LargeMirroredSlopes()
		{
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(5f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(5f, 4f), 0f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(5f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(5f, 4f), 2f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(5f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(5f, 4f), 4f, 0.0001f), 0.0001f);

			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 0f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 3f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 5f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 6f, 0.0001f), 0.0001f);

			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(4f, 6f), 0f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(4f, 6f), 2f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(4f, 6f), 4f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(4f, 6f), 5f, 0.0001f), 0.0001f);

			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(7f, 3f), 0f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(7f, 3f), 2f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(7f, 3f), 4f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 4f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(7f, 3f), 5f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_LineLine_HorizontalColinearConnected()
		{
			try
			{
				AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 1f), new Vector2(5f, 1f), new Vector2(5f, 1f), new Vector2(9f, 1f), 1f, 0.0001f), 0.0001f);
				AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 1f), new Vector2(5f, 1f), new Vector2(5f, 1f), new Vector2(9f, 1f), 2f, 0.0001f), 0.0001f);
			}
			catch (System.NotImplementedException)
			{
				Assert.Inconclusive("Not Yet Implemented");
			}
		}

		[Test]
		public static void GetSegmentRightBound_LineLine_HorizontalColinearDisconnected()
		{
			try
			{
				AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 1f), new Vector2(4f, 1f), new Vector2(6f, 1f), new Vector2(9f, 1f), 1f, 0.0001f), 0.0001f);
				AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 1f), new Vector2(4f, 1f), new Vector2(6f, 1f), new Vector2(9f, 1f), 2f, 0.0001f), 0.0001f);
			}
			catch (System.NotImplementedException)
			{
				Assert.Inconclusive("Not Yet Implemented");
			}
		}

		[Test]
		public static void GetSegmentRightBound_LineLine_HorizontalParallelDisconnected()
		{
			try
			{
				AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 1f), new Vector2(4f, 1f), new Vector2(6f, 3f), new Vector2(9f, 3f), 3f, 0.0001f), 0.0001f);
				AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 1f), new Vector2(4f, 1f), new Vector2(6f, 3f), new Vector2(9f, 3f), 4f, 0.0001f), 0.0001f);
			}
			catch (System.NotImplementedException)
			{
				Assert.Inconclusive("Not Yet Implemented");
			}
		}

		[Test]
		public static void GetSegmentRightBound_LineLine_VerticalParallel()
		{
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(9f, 5f), 1f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(9f, 5f), 3f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(9f, 5f), 5f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(1f, 1f), new Vector2(9f, 1f), new Vector2(9f, 5f), 7f, 0.0001f), 0.0001f);
		}

		[Test]
		public static void GetSegmentRightBound_LineLine_DiagonalParallel()
		{
			AssertApproximatelyEqual(2f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(6f, 1f), new Vector2(9f, 5f), 1f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(3.5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(6f, 1f), new Vector2(9f, 5f), 3f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(6f, 1f), new Vector2(9f, 5f), 5f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(6.5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(4f, 5f), new Vector2(1f, 1f), new Vector2(6f, 1f), new Vector2(9f, 5f), 7f, 0.0001f), 0.0001f);

			AssertApproximatelyEqual(8f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 1f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(6.5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 3f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 5f, 0.0001f), 0.0001f);
			AssertApproximatelyEqual(3.5f, VoronoiUtility.GetSegmentRightBound_LineLine(new Vector2(1f, 5f), new Vector2(4f, 1f), new Vector2(9f, 1f), new Vector2(6f, 5f), 7f, 0.0001f), 0.0001f);
		}

		#endregion

		#endregion

		#region CheckForMergeEvent

		private VoronoiUtility.BeachSegment segment = new VoronoiUtility.BeachSegment();
		private Vector2 actualMergePosition;
		private float actualDistance;

		private bool ExtractActualValuesFromQueue(VoronoiUtility.MergeEventQueue queue)
		{
			if (queue.isEmpty)
			{
				actualMergePosition = Vector2.zero;
				actualDistance = 0f;
				return false;
			}
			else
			{
				var ev = queue.Pop();
				actualMergePosition = ev.mergePosition;
				actualDistance = ev.distance;
				return true;
			}
		}

		#region PointPointPoint

		private void AssertFalse_CheckForMergeEvent_PointPointPoint(Vector2 p0, Vector2 p1, Vector2 p2)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0, p1, p2 });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_PointPointPoint(0, 1, 2, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsFalse(found, string.Format("Expected no merge of points {0}, {1}, and {2}, but got a merge at {3} with distance {4:G8}.", p0.ToString("F4"), p1.ToString("F4"), p2.ToString("F4"), actualMergePosition.ToString("F4"), actualDistance));
		}

		private void AssertTrue_CheckForMergeEvent_PointPointPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0, p1, p2 });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_PointPointPoint(0, 1, 2, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of points {0}, {1}, and {2} at {3} with distance {4:G8}, but got no merge.", p0.ToString("F4"), p1.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of points {0}, {1}, and {2} at {3} with distance {4:G8}, but got a merge at {5} with distance {6:G8}.", p0.ToString("F4"), p1.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of points {0}, {1}, and {2} at {3} with distance {4:G8}, but got a merge at {5} with distance {6:G8}.", p0.ToString("F4"), p1.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_3P0L_PointPointPoint_ConcaveSymmetric()
		{
			var p0 = new Vector2(1f, 19f);
			var p1 = new Vector2(25f, 1f);
			var p2 = new Vector2(49f, 19f);
			AssertTrue_CheckForMergeEvent_PointPointPoint(p0, p1, p2, new Vector2(25f, 26f), 25f, 0.0001f);
			AssertFalse_CheckForMergeEvent_PointPointPoint(p1, p0, p2);
			AssertFalse_CheckForMergeEvent_PointPointPoint(p0, p2, p1);
		}

		[Test]
		public void CheckForMergeEvent_3P0L_PointPointPoint_ConcaveAsymmetric()
		{
			var p0 = new Vector2(1f, 13f);
			var p1 = new Vector2(17f, 1f);
			var p2 = new Vector2(41f, 33f);
			AssertTrue_CheckForMergeEvent_PointPointPoint(p0, p1, p2, new Vector2(21f, 23f), Mathf.Sqrt(500f), 0.0001f);
			AssertFalse_CheckForMergeEvent_PointPointPoint(p1, p0, p2);
			AssertFalse_CheckForMergeEvent_PointPointPoint(p0, p2, p1);
		}

		[Test]
		public void CheckForMergeEvent_3P0L_PointPointPoint_ConvexAsymmetric()
		{
			var p0 = new Vector2(-12f + 47f/6f, 16f + 47f/8f);
			var p1 = new Vector2(-12f - 47f/6f, 16f - 47f/8f);
			var p2 = new Vector2(9f + 79f/6f, 12f - 79f/8f);
			AssertFalse_CheckForMergeEvent_PointPointPoint(p1, p0, p2);
			AssertTrue_CheckForMergeEvent_PointPointPoint(p0, p1, p2, new Vector2(0f, 0f), p0.magnitude, 0.0001f);
			AssertFalse_CheckForMergeEvent_PointPointPoint(p0, p2, p1);
		}

		#endregion

		#region PointPointLine

		private void AssertTrue_CheckForMergeEvent_PointTargetPointLine(Vector2 p0, Vector2 p2a, Vector2 p2b, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0, p2a, p2b });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_PointPointLine(0, 2, 1, 2, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of point {0}, point {1}, and line {2} -> {3} at {4} with distance {5:G8}, but got no merge.", p0.ToString("F4"), p2b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of point {0}, point {1}, and line {2} -> {3} at {4} with distance {5:G8}, but got a merge at {6} with distance {7:G8}.", p0.ToString("F4"), p2b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of points point {0}, point {1}, and line {2} -> {3} at {4} with distance {5:G8}, but got a merge at {6} with distance {7:G8}.", p0.ToString("F4"), p2b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_2P1L_PointTargetPointLine_LargeNegativeSlope()
		{
			var p0 = new Vector2(-120f/17f, 225f/17f);
			var p2a = new Vector2(15f, 5f);
			var p2b = new Vector2(12f, 9f);
			AssertTrue_CheckForMergeEvent_PointTargetPointLine(p0, p2a, p2b, new Vector2(0f, 0f), 15f, 0.0001f);
		}

		private void AssertTrue_CheckForMergeEvent_PointPointLine(Vector2 p0, Vector2 p1, Vector2 p2a, Vector2 p2b, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0, p1, p2a, p2b });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_PointPointLine(0, 1, 2, 3, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of point {0}, point {1}, and line {2} -> {3} at {4} with distance {5:G8}, but got no merge.", p0.ToString("F4"), p1.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of point {0}, point {1}, and line {2} -> {3} at {4} with distance {5:G8}, but got a merge at {6} with distance {7:G8}.", p0.ToString("F4"), p1.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of points point {0}, point {1}, and line {2} -> {3} at {4} with distance {5:G8}, but got a merge at {6} with distance {7:G8}.", p0.ToString("F4"), p1.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_2P1L_PointPointLine_SmallPositiveSlope()
		{
			var p0 = new Vector2(-8f, 6f);
			var p1 = new Vector2(-10f, 0f);
			var p2a = new Vector2(5f, -14f);
			var p2b = new Vector2(10f, -2f);
			AssertTrue_CheckForMergeEvent_PointPointLine(p0, p1, p2a, p2b, new Vector2(0f, 0f), 10f, 0.0001f);
		}

		#endregion

		#region PointLinePoint

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_2P1L_PointLinePoint_SmallPositiveSlope_NoMerge(float s, float r, float dx, float dy)
		{
			var p0 = new Vector2(-5f, 0f);
			var p1a = new Vector2(1f, -5f);
			var p1b = new Vector2(13f, 0f);
			var p2 = new Vector2(4f, -6f);
			AssertFalse_CheckForMergeEvent(0.0001f, new Transform2(s, r, dx, dy), "PointLinePoint", "point {0}, line {1} -> {2}, and point {3}", new int[] { 0, 1, 2, 3 }, p0, p1a, p1b, p2);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_2P1L_PointLinePoint_SmallPositiveSlope(float s, float r, float dx, float dy)
		{
			var p0 = new Vector2(-5f, 0f);
			var p1a = new Vector2(1f, -5f);
			var p1b = new Vector2(13f, 0f);
			var p2 = new Vector2(4f, 3f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 5f, 0.0001f, new Transform2(s, r, dx, dy), "PointLinePoint", "point {0}, line {1} -> {2}, and point {3}", new int[] { 0, 1, 2, 3 }, p0, p1a, p1b, p2);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_2P1L_PointLinePoint_SmallPositiveSlope_Symmetric(float s, float r, float dx, float dy)
		{
			var p0 = new Vector2(-25f, 0f);
			var p1a = new Vector2(3f, -29f);
			var p1b = new Vector2(39f, -2f);
			var p2 = new Vector2(7f, 24f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 25f, 0.0001f, new Transform2(s, r, dx, dy), "PointLinePoint", "point {0}, line {1} -> {2}, and point {3}", new int[] { 0, 1, 2, 3 }, p0, p1a, p1b, p2);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_2P1L_PointLinePoint_SmallPositiveSlope_SymmetricHorizontal(float s, float r, float dx, float dy)
		{
			var p0 = new Vector2(-4f, -3f);
			var p1a = new Vector2(-5f, -5f);
			var p1b = new Vector2(5f, -5f);
			var p2 = new Vector2(4f, -3f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 5f, 0.0001f, new Transform2(s, r, dx, dy), "PointLinePoint", "point {0}, line {1} -> {2}, and point {3}", new int[] { 0, 1, 2, 3 }, p0, p1a, p1b, p2);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_2P1L_SourcePointLinePoint_Horizontal(float s, float r, float dx, float dy)
		{
			var p1a = new Vector2(0f, -5f);
			var p1b = new Vector2(8f, -5f);
			var p2 = new Vector2(4f, -3f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 5f, 0.0001f, new Transform2(s, r, dx, dy), "PointLinePoint", "point {0}, line {0} -> {1}, and point {2}", new int[] { 0, 0, 1, 2 }, p1a, p1b, p2);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_2P1L_PointLineTargetPoint_Horizontal(float s, float r, float dx, float dy)
		{
			var p0 = new Vector2(-4f, -3f);
			var p1a = new Vector2(-8f, -5f);
			var p1b = new Vector2(0f, -5f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 5f, 0.0001f, new Transform2(s, r, dx, dy), "PointLinePoint", "point {0}, line {1} -> {2}, and point {2}", new int[] { 0, 1, 2, 2 }, p0, p1a, p1b);
		}

		#endregion

		#region PointLineLine

		private void AssertTrue_CheckForMergeEvent_PointLineLine(Vector2 p0, Vector2 p1a, Vector2 p1b, Vector2 p2a, Vector2 p2b, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0, p1a, p1b, p2a, p2b });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_PointLineLine(0, 1, 2, 3, 4, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of point {0}, line {1} -> {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got no merge.", p0.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of point {0}, line {1} -> {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of point {0}, line {1} -> {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_1P2L_PointLineLine_()
		{
			var p0 = new Vector2(-4f, 3f);
			var p1a = new Vector2(-15f, 5f);
			var p1b = new Vector2(-3f, -4f);
			var p2a = new Vector2(2.6f, -4.3f);
			var p2b = new Vector2(7f, -1f);
			AssertTrue_CheckForMergeEvent_PointLineLine(p0, p1a, p1b, p2a, p2b, new Vector2(0f, 0f), 5f, 0.0001f);
		}

		private void AssertTrue_CheckForMergeEvent_SourcePointLineLine(Vector2 p1a, Vector2 p1b, Vector2 p2a, Vector2 p2b, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p1a, p1b, p2a, p2b });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_PointLineLine(0, 0, 1, 2, 3, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of point {0}, line {1} -> {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got no merge.", p1a.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of point {0}, line {1} -> {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p1a.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of point {0}, line {1} -> {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p1a.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_1P2L_SourcePointLineLine_()
		{
			var p1a = new Vector2(-12f, -5f);
			var p1b = new Vector2(-7f, -17f);
			var p2a = new Vector2(-6.2f, -11.5f);
			var p2b = new Vector2(1f, -14.5f);
			AssertTrue_CheckForMergeEvent_SourcePointLineLine(p1a, p1b, p2a, p2b, new Vector2(0f, 0f), 13f, 0.0001f);
		}

		[Test]
		public void CheckForMergeEvent_1P2L_SourcePointLineLine_2()
		{
			var p1a = new Vector2(0f, -5f);
			var p1b = new Vector2(10f, -5f);
			var p2a = new Vector2(10f, -5f);
			var p2b = new Vector2(1f, 7f);
			AssertTrue_CheckForMergeEvent_SourcePointLineLine(p1a, p1b, p2a, p2b, new Vector2(0f, 0f), 5f, 0.0001f);
		}

		#endregion

		#region LinePointPoint

		private void AssertTrue_CheckForMergeEvent_LineSourcePointPoint(Vector2 p0a, Vector2 p0b, Vector2 p2, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0a, p0b, p2 });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_LinePointPoint(0, 1, 0, 2, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of line {0} -> {1}, point {2}, and point {3} at {4} with distance {5:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p0a.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of line {0} -> {1}, point {2}, and point {3} at {4} with distance {5:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p0a.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of line {0} -> {1}, point {2}, and point {3} at {4} with distance {5:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p0a.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_2P1L_LineSourcePointPoint_LargeNegativeSlope()
		{
			var p0a = new Vector2(-12f, 9f);
			var p0b = new Vector2(-15f, 5f);
			var p2 = new Vector2(120f/17f, 225f/17f);
			AssertTrue_CheckForMergeEvent_LineSourcePointPoint(p0a, p0b, p2, new Vector2(0f, 0f), 15f, 0.0001f);
		}

		private void AssertTrue_CheckForMergeEvent_LinePointPoint(Vector2 p0a, Vector2 p0b, Vector2 p1, Vector2 p2, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0a, p0b, p1, p2 });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_LinePointPoint(0, 1, 2, 3, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of line {0} -> {1}, point {2}, and point {3} at {4} with distance {5:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p1.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of line {0} -> {1}, point {2}, and point {3} at {4} with distance {5:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p1.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of line {0} -> {1}, point {2}, and point {3} at {4} with distance {5:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p1.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_2P1L_LinePointPoint_SmallPositiveSlope()
		{
			var p0a = new Vector2(-10f, -2f);
			var p0b = new Vector2(-5f, -14f);
			var p1 = new Vector2(10f, 0f);
			var p2 = new Vector2(8f, 6f);
			AssertTrue_CheckForMergeEvent_LinePointPoint(p0a, p0b, p1, p2, new Vector2(0f, 0f), 10f, 0.0001f);
		}

		#endregion

		#region LinePointLine

		private void AssertTrue_CheckForMergeEvent_LinePointLine(Vector2 p0a, Vector2 p0b, Vector2 p1, Vector2 p2a, Vector2 p2b, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0a, p0b, p1, p2a, p2b });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_LinePointLine(0, 1, 2, 3, 4, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of line {0} -> {1}, point {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p1.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of line {0} -> {1}, point {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of line {0} -> {1}, point {2}, and line {3} -> {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_1P2L_LinePointLine_SymmetricFunnelUp()
		{
			var p0a = new Vector2(-7f, -11.5f);
			var p0b = new Vector2(-1f, -19.5f);
			var p1 = new Vector2(0f, -12.5f);
			var p2a = new Vector2(1f, -19.5f);
			var p2b = new Vector2(7f, -11.5f);
			AssertTrue_CheckForMergeEvent_LinePointLine(p0a, p0b, p1, p2a, p2b, new Vector2(0f, 0f), 12.5f, 0.0001f);
		}

		[Test]
		public void CheckForMergeEvent_1P2L_LinePointLine_SymmetricFunnelUp45()
		{
			var d = 10f * Mathf.Sqrt(2f) + 10f;
			var p0a = new Vector2(-10f, -d);
			var p0b = new Vector2(0f, -d - 10f);
			var p1 = new Vector2(0f, -d);
			var p2a = new Vector2(0f, -d - 10f);
			var p2b = new Vector2(10f, -d);
			AssertTrue_CheckForMergeEvent_LinePointLine(p0a, p0b, p1, p2a, p2b, new Vector2(0f, 0f), d, 0.0001f);
		}

		[Test]
		public void CheckForMergeEvent_1P2L_LinePointLine_()
		{
			var p0a = new Vector2(-13f + 50f/13f, 120f/13f);
			var p0b = new Vector2(-13f, 0f);
			var p1 = new Vector2(84f/25f, -288f/25f);
			var p2a = new Vector2(20f - 32f/5f, 24f/5f);
			var p2b = new Vector2(4f, 12f);
			AssertTrue_CheckForMergeEvent_LinePointLine(p0a, p0b, p1, p2a, p2b, new Vector2(0f, 0f), 12f, 0.0001f);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, -55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_1P2L_LinePointLine_OpenUp(float s, float r, float dx, float dy)
		{
			var p0a = new Vector2(-32f, 1f);
			var p0b = new Vector2(-2f, -39f);
			var p1 = new Vector2(0f, -25f);
			var p2a = new Vector2(2f, -39f);
			var p2b = new Vector2(32f, 1f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 25f, 0.0001f, new Transform2(s, r, dx, dy), "LinePointLine", "line {0} -> {1}, point {2}, and line {3} -> {4}", new int[] { 0, 1, 2, 3, 4 }, p0a, p0b, p1, p2a, p2b);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, -55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_1P2L_LinePointLine_OpenDown(float s, float r, float dx, float dy)
		{
			var p0a = new Vector2(-8f, 19f);
			var p0b = new Vector2(-36f, -2f);
			var p1 = new Vector2(0f, -20f);
			var p2a = new Vector2(36f, -2f);
			var p2b = new Vector2(8f, 19f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 20f, 0.0001f, new Transform2(s, r, dx, dy), "LinePointLine", "line {0} -> {1}, point {2}, and line {3} -> {4}", new int[] { 0, 1, 2, 3, 4 }, p0a, p0b, p1, p2a, p2b);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, -55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_1P2L_LinePointLine_ParallelSymmetric(float s, float r, float dx, float dy)
		{
			var p0a = new Vector2(-1f, 1f);
			var p0b = new Vector2(-1f, -2f);
			var p1 = new Vector2(0f, -1f);
			var p2a = new Vector2(1f, -2f);
			var p2b = new Vector2(1f, 1f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 1f, 0.0001f, new Transform2(s, r, dx, dy), "LinePointLine", "line {0} -> {1}, point {2}, and line {3} -> {4}", new int[] { 0, 1, 2, 3, 4 }, p0a, p0b, p1, p2a, p2b);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, -55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_1P2L_LinePointLine_ParallelAsymmetric(float s, float r, float dx, float dy)
		{
			var p0a = new Vector2(-5f, 1f);
			var p0b = new Vector2(-5f, -5f);
			var p1 = new Vector2(0f, -4f);
			var p2a = new Vector2(5f, -5f);
			var p2b = new Vector2(5f, 1f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 5f, 0.0001f, new Transform2(s, r, dx, dy), "LinePointLine", "line {0} -> {1}, point {2}, and line {3} -> {4}", new int[] { 0, 1, 2, 3, 4 }, p0a, p0b, p1, p2a, p2b);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, -55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_1P2L_LineSourcePointLine(float s, float r, float dx, float dy)
		{
			var p0a = new Vector2(-10f, -5f);
			var p0b = new Vector2(10f, -5f);
			var p2a = new Vector2(4f, 3f);
			var p2b = new Vector2(-2f, 11f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 5f, 0.0001f, new Transform2(s, r, dx, dy), "LinePointLine", "line {0} -> {1}, point {2}, and line {2} -> {3}", new int[] { 0, 1, 2, 2, 3 }, p0a, p0b, p2a, p2b);
		}

		[TestCase(1f, 0f, 0f, 0f)]
		[TestCase(2.5f, 0f, 0f, 0f)]
		[TestCase(1f, 55f, 0f, 0f)]
		[TestCase(1f, -55f, 0f, 0f)]
		[TestCase(1f, 0f, 12f, -19f)]
		[TestCase(2.5f, 55f, 12f, -19f)]
		public void CheckForMergeEvent_1P2L_LineTargetPointLine(float s, float r, float dx, float dy)
		{
			var p0a = new Vector2(-10f, -5f);
			var p0b = new Vector2(0f, -5f);
			var p2a = new Vector2(10f, -5f);
			var p2b = new Vector2(1f, 7f);
			AssertTrue_CheckForMergeEvent(Vector2.zero, 5f, 0.0001f, new Transform2(s, r, dx, dy), "LinePointLine", "line {0} -> {1}, point {1}, and line {2} -> {3}", new int[] { 0, 1, 1, 2, 3 }, p0a, p0b, p2a, p2b);
		}

		#endregion

		#region LineLinePoint

		private void AssertTrue_CheckForMergeEvent_LineLinePoint(Vector2 p0a, Vector2 p0b, Vector2 p1a, Vector2 p1b, Vector2 p2, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0a, p0b, p1a, p1b, p2 });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_LineLinePoint(0, 1, 2, 3, 4, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and point {4} at {5} with distance {6:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and point {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and point {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_1P2L_LineLinePoint_()
		{
			var p0a = new Vector2(-7f, -1f);
			var p0b = new Vector2(-2.6f, -4.3f);
			var p1a = new Vector2(3f, -4f);
			var p1b = new Vector2(15f, 5f);
			var p2 = new Vector2(4f, 3f);
			AssertTrue_CheckForMergeEvent_LineLinePoint(p0a, p0b, p1a, p1b, p2, new Vector2(0f, 0f), 5f, 0.0001f);
		}

		private void AssertTrue_CheckForMergeEvent_LineLineTargetPoint(Vector2 p0a, Vector2 p0b, Vector2 p1a, Vector2 p1b, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0a, p0b, p1a, p1b });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_LineLinePoint(0, 1, 2, 3, 3, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and point {4} at {5} with distance {6:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p1b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and point {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p1b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and point {4} at {5} with distance {6:G8}, but got a merge at {7} with distance {8:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p1b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_1P2L_LineLineTargetPoint_()
		{
			var p0a = new Vector2(-1f, -14.5f);
			var p0b = new Vector2(6.2f, -11.5f);
			var p1a = new Vector2(7f, -17f);
			var p1b = new Vector2(12f, -5f);
			AssertTrue_CheckForMergeEvent_LineLineTargetPoint(p0a, p0b, p1a, p1b, new Vector2(0f, 0f), 13f, 0.0001f);
		}

		[Test]
		public void CheckForMergeEvent_1P2L_LineLineTargetPoint_2()
		{
			var p0a = new Vector2(-10f, -5f);
			var p0b = new Vector2(10f, -5f);
			var p1a = new Vector2(10f, -5f);
			var p1b = new Vector2(4f, 3f);
			AssertTrue_CheckForMergeEvent_LineLineTargetPoint(p0a, p0b, p1a, p1b, new Vector2(0f, 0f), 5f, 0.0001f);
		}

		#endregion

		#region LineLineLine

		private void AssertTrue_CheckForMergeEvent_LineLineLine(Vector2 p0a, Vector2 p0b, Vector2 p1a, Vector2 p1b, Vector2 p2a, Vector2 p2b, Vector2 expectedMergePosition, float expectedDistance, float margin)
		{
			GraphNodeDataArray<Vector2> positions = new GraphNodeDataArray<Vector2>(new Vector2[] { p0a, p0b, p1a, p1b, p2a, p2b });
			VoronoiUtility.MergeEventQueue queue = new VoronoiUtility.MergeEventQueue(0.0001f);
			VoronoiUtility.CheckForMergeEvent_LineLineLine(0, 1, 2, 3, 4, 5, positions, 0.0001f, segment, queue);
			bool found = ExtractActualValuesFromQueue(queue);
			Assert.IsTrue(found, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and line {4} -> {5} at {6} with distance {7:G8}, but got no merge.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance));
			AssertApproximatelyEqual(expectedMergePosition, actualMergePosition, margin, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and line {4} -> {5} at {6} with distance {7:G8}, but got a merge at {8} with distance {9:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
			AssertApproximatelyEqual(expectedDistance, actualDistance, margin, string.Format("Expected a merge of line {0} -> {1}, line {2} -> {3}, and line {4} -> {5} at {6} with distance {7:G8}, but got a merge at {8} with distance {9:G8}.", p0a.ToString("F4"), p0b.ToString("F4"), p1a.ToString("F4"), p1b.ToString("F4"), p2a.ToString("F4"), p2b.ToString("F4"), expectedMergePosition.ToString("F4"), expectedDistance, actualMergePosition.ToString("F4"), actualDistance));
		}

		[Test]
		public void CheckForMergeEvent_0P3L_LineLineLine_()
		{
			var p0a = new Vector2(-15f, 0f);
			var p0b = new Vector2(-6f, -12f);
			var p1a = new Vector2(-6f, -12f);
			var p1b = new Vector2(4f, -12f);
			var p2a = new Vector2(4f, -12f);
			var p2b = new Vector2(20f, 0f);
			AssertTrue_CheckForMergeEvent_LineLineLine(p0a, p0b, p1a, p1b, p2a, p2b, new Vector2(0f, 0f), 12f, 0.0001f);
		}

		#endregion

		#endregion
	}
}
#endif
