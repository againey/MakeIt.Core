/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.MakeIt.Utilities.Tests
{
	public class AspectRatioToolsTests
	{
		#region Private Helpers

		private delegate void AdjustMidAnchorDelegate(ref Vector2 min, ref Vector2 size, float targetAspectRatio);
		private delegate void AdjustSharedAnchorDelegate(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 anchor);
		private delegate void AdjustDistinctAnchorsDelegate(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor);

		private void AssertNoChange(Vector2 min, Vector2 size, float targetAspectRatio, AdjustMidAnchorDelegate adjust)
		{
			Vector2 expectedMin = min;
			Vector2 expectedSize = size;
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjust(ref actualMin, ref actualSize, targetAspectRatio);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertNoChange(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 anchor, AdjustSharedAnchorDelegate adjust)
		{
			Vector2 expectedMin = min;
			Vector2 expectedSize = size;
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjust(ref actualMin, ref actualSize, targetAspectRatio, anchor);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertNoChange(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor, AdjustDistinctAnchorsDelegate adjust)
		{
			Vector2 expectedMin = min;
			Vector2 expectedSize = size;
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjust(ref actualMin, ref actualSize, targetAspectRatio, sourceAnchor, targetAnchor);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertAreSame(Vector2 expectedMin, Vector2 expectedSize, Vector2 min, Vector2 size, float targetAspectRatio, AdjustMidAnchorDelegate adjust)
		{
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjust(ref actualMin, ref actualSize, targetAspectRatio);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertAreSame(Vector2 expectedMin, Vector2 expectedSize, Vector2 min, Vector2 size, float targetAspectRatio, Vector2 anchor, AdjustSharedAnchorDelegate adjust)
		{
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjust(ref actualMin, ref actualSize, targetAspectRatio, anchor);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertAreSame(Vector2 expectedMin, Vector2 expectedSize, Vector2 min, Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor, AdjustDistinctAnchorsDelegate adjust)
		{
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjust(ref actualMin, ref actualSize, targetAspectRatio, sourceAnchor, targetAnchor);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertAreSame(Vector2 min, Vector2 size, float targetAspectRatio, AdjustMidAnchorDelegate adjustExpected, AdjustMidAnchorDelegate adjustActual)
		{
			Vector2 expectedMin = min;
			Vector2 expectedSize = size;
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjustExpected(ref expectedMin, ref expectedSize, targetAspectRatio);
			adjustActual(ref actualMin, ref actualSize, targetAspectRatio);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertAreSame(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 anchor, AdjustSharedAnchorDelegate adjustExpected, AdjustSharedAnchorDelegate adjustActual)
		{
			Vector2 expectedMin = min;
			Vector2 expectedSize = size;
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjustExpected(ref expectedMin, ref expectedSize, targetAspectRatio, anchor);
			adjustActual(ref actualMin, ref actualSize, targetAspectRatio, anchor);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		private void AssertAreSame(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor, AdjustDistinctAnchorsDelegate adjustExpected, AdjustDistinctAnchorsDelegate adjustActual)
		{
			Vector2 expectedMin = min;
			Vector2 expectedSize = size;
			Vector2 actualMin = min;
			Vector2 actualSize = size;
			adjustExpected(ref expectedMin, ref expectedSize, targetAspectRatio, sourceAnchor, targetAnchor);
			adjustActual(ref actualMin, ref actualSize, targetAspectRatio, sourceAnchor, targetAnchor);
			Assert.AreEqual(expectedMin, actualMin);
			Assert.AreEqual(expectedSize, actualSize);
		}

		#endregion

		#region AdjustWidth()

		#region 1:1

		[Test]
		public void AdjustWidthTest_OneToOne()
		{
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustWidth(64f, 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustWidth(new Vector2(64f, 64f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustWidth(new Vector2(32f, 64f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustWidth(new Vector2(96f, 64f), 1f));
		}

		[Test]
		public void AdjustWidthTest_OneToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(0f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(28f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(36f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(4f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(60f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(96f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(128f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(-16f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-20f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-12f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		#endregion

		#region 2:1

		[Test]
		public void AdjustWidthTest_TwoToOne()
		{
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioTools.AdjustWidth(32f, 2f));
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioTools.AdjustWidth(new Vector2(64f, 32f), 2f));
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioTools.AdjustWidth(new Vector2(32f, 32f), 2f));
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioTools.AdjustWidth(new Vector2(96f, 32f), 2f));
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(0f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(28f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(36f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(4f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(60f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(96f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(128f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_TwoToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(-16f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-20f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-12f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		#endregion

		#region 1:2

		[Test]
		public void AdjustWidthTest_OneToTwo()
		{
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioTools.AdjustWidth(128f, 0.5f));
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioTools.AdjustWidth(new Vector2(64f, 128f), 0.5f));
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioTools.AdjustWidth(new Vector2(32f, 128f), 0.5f));
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioTools.AdjustWidth(new Vector2(96f, 128f), 0.5f));
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(0f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(28f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(36f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.125f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(4f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(60f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(96f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(128f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		[Test]
		public void AdjustWidthTest_OneToTwo_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(-16f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-20f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
			AssertAreSame(new Vector2(-12f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioTools.AdjustWidth);
		}

		#endregion

		#endregion

		#region AdjustHeight()

		#region 1:1

		[Test]
		public void AdjustHeightTest_OneToOne()
		{
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustHeight(64f, 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustHeight(new Vector2(64f, 64f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustHeight(new Vector2(64f, 32f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioTools.AdjustHeight(new Vector2(64f, 96f), 1f));
		}

		[Test]
		public void AdjustHeightTest_OneToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 0f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 28f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 36f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 4f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 60f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(32f, -32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(32f, 96f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 128f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(32f, -16f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -20f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -12f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
		}

		#endregion

		#region 2:1

		[Test]
		public void AdjustHeightTest_TwoToOne()
		{
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioTools.AdjustHeight(128f, 2f));
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioTools.AdjustHeight(new Vector2(128f, 64f), 2f));
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioTools.AdjustHeight(new Vector2(128f, 32f), 2f));
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioTools.AdjustHeight(new Vector2(128f, 96f), 2f));
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 0f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 28f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 36f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 4f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 60f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(32f, -32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(32f, 96f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 128f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_TwoToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(32f, -16f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -20f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -12f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
		}

		#endregion

		#region 1:2

		[Test]
		public void AdjustHeightTest_OneToTwo()
		{
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioTools.AdjustHeight(32f, 0.5f));
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioTools.AdjustHeight(new Vector2(32f, 64f), 0.5f));
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioTools.AdjustHeight(new Vector2(32f, 32f), 0.5f));
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioTools.AdjustHeight(new Vector2(32f, 96f), 0.5f));
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 0f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 28f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 36f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.125f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 4f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 60f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(32f, -32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(32f, 96f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, 128f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioTools.AdjustHeight);
		}

		[Test]
		public void AdjustHeightTest_OneToTwo_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(32f, -16f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -20f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
			AssertAreSame(new Vector2(32f, -12f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioTools.AdjustHeight);
		}

		#endregion

		#endregion

		#region Expand()

		#region 1:1

		[Test]
		public void ExpandTest_OneToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 1f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioTools.Expand);
		}

		[Test]
		public void ExpandTest_OneToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
		}

		[Test]
		public void ExpandTest_OneToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
		}

		#endregion

		#region 2:1

		[Test]
		public void ExpandTest_TwoToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 1f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.125f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.875f, 0.875f), AspectRatioTools.Expand);
		}

		[Test]
		public void ExpandTest_TwoToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
		}

		[Test]
		public void ExpandTest_TwoToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
		}

		#endregion

		#region 1:2

		[Test]
		public void ExpandTest_OneToTwo_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioTools.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioTools.Expand);
		}

		[Test]
		public void ExpandTest_OneToTwo_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Expand);
		}

		[Test]
		public void ExpandTest_OneToTwo_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Expand);
		}

		#endregion

		#endregion

		#region Shrink()

		#region 1:1

		[Test]
		public void ShrinkTest_OneToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 1f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioTools.Shrink);
		}

		[Test]
		public void ShrinkTest_OneToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
		}

		[Test]
		public void ShrinkTest_OneToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
		}

		#endregion

		#region 2:1

		[Test]
		public void ShrinkTest_TwoToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 1f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.125f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.875f, 0.875f), AspectRatioTools.Shrink);
		}

		[Test]
		public void ShrinkTest_TwoToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
		}

		[Test]
		public void ShrinkTest_TwoToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
		}

		#endregion

		#region 1:2

		[Test]
		public void ShrinkTest_OneToTwo_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioTools.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioTools.Shrink);
		}

		[Test]
		public void ShrinkTest_OneToTwo_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustWidth, AspectRatioTools.Shrink);
		}

		[Test]
		public void ShrinkTest_OneToTwo_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioTools.AdjustHeight, AspectRatioTools.Shrink);
		}

		#endregion

		#endregion

		#region AdjustAverage()

		[Test]
		public void AdjustAverageTest()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(24f, 40f), new Vector2(48f, 48f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(52f, 12f), new Vector2(56f, 56f), new Vector2(32f, 32f), new Vector2(96f, 16f), 1f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(8f, 44f), new Vector2(80f, 40f), new Vector2(32f, 32f), new Vector2(32f, 64f), 2f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(48f, 24f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 16f), 2f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(40f, 16f), new Vector2(48f, 96f), new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, AspectRatioTools.AdjustAverage);
			AssertAreSame(new Vector2(54f, -12f), new Vector2(52f, 104f), new Vector2(32f, 32f), new Vector2(96f, 16f), 0.5f, AspectRatioTools.AdjustAverage);
		}

		#endregion
	}
}
#endif
