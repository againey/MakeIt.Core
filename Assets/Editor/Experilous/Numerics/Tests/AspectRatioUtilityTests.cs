/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Numerics.Tests
{
	class AspectRatioUtilityTests
	{
		#region Private Helpers

		private delegate Rect AdjustMidAnchorDelegate(Rect rect, float targetAspectRatio);
		private delegate Rect AdjustSharedAnchorDelegate(Rect rect, float targetAspectRatio, Vector2 anchor);
		private delegate Rect AdjustDistinctAnchorsDelegate(Rect rect, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor);

		private void AssertNoChange(Vector2 min, Vector2 size, float targetAspectRatio, AdjustMidAnchorDelegate adjust)
		{
			var adjusted = adjust(new Rect(min, size), targetAspectRatio);
			Assert.AreEqual(new Rect(min, size), adjusted);
		}

		private void AssertNoChange(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 anchor, AdjustSharedAnchorDelegate adjust)
		{
			var adjusted = adjust(new Rect(min, size), targetAspectRatio, anchor);
			Assert.AreEqual(new Rect(min, size), adjusted);
		}

		private void AssertNoChange(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor, AdjustDistinctAnchorsDelegate adjust)
		{
			var adjusted = adjust(new Rect(min, size), targetAspectRatio, sourceAnchor, targetAnchor);
			Assert.AreEqual(new Rect(min, size), adjusted);
		}

		private void AssertAreSame(Vector2 expectedMin, Vector2 expectedSize, Vector2 min, Vector2 size, float targetAspectRatio, AdjustMidAnchorDelegate adjust)
		{
			var adjusted = adjust(new Rect(min, size), targetAspectRatio);
			Assert.AreEqual(new Rect(expectedMin, expectedSize), adjusted);
		}

		private void AssertAreSame(Vector2 expectedMin, Vector2 expectedSize, Vector2 min, Vector2 size, float targetAspectRatio, Vector2 anchor, AdjustSharedAnchorDelegate adjust)
		{
			var adjusted = adjust(new Rect(min, size), targetAspectRatio, anchor);
			Assert.AreEqual(new Rect(expectedMin, expectedSize), adjusted);
		}

		private void AssertAreSame(Vector2 expectedMin, Vector2 expectedSize, Vector2 min, Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor, AdjustDistinctAnchorsDelegate adjust)
		{
			var adjusted = adjust(new Rect(min, size), targetAspectRatio, sourceAnchor, targetAnchor);
			Assert.AreEqual(new Rect(expectedMin, expectedSize), adjusted);
		}

		private void AssertAreSame(Vector2 min, Vector2 size, float targetAspectRatio, AdjustMidAnchorDelegate adjustExpected, AdjustMidAnchorDelegate adjustActual)
		{
			var expected = adjustExpected(new Rect(min, size), targetAspectRatio);
			var adjusted = adjustActual(new Rect(min, size), targetAspectRatio);
			Assert.AreEqual(expected, adjusted);
		}

		private void AssertAreSame(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 anchor, AdjustSharedAnchorDelegate adjustExpected, AdjustSharedAnchorDelegate adjustActual)
		{
			var expected = adjustExpected(new Rect(min, size), targetAspectRatio, anchor);
			var adjusted = adjustActual(new Rect(min, size), targetAspectRatio, anchor);
			Assert.AreEqual(expected, adjusted);
		}

		private void AssertAreSame(Vector2 min, Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor, AdjustDistinctAnchorsDelegate adjustExpected, AdjustDistinctAnchorsDelegate adjustActual)
		{
			var expected = adjustExpected(new Rect(min, size), targetAspectRatio, sourceAnchor, targetAnchor);
			var adjusted = adjustActual(new Rect(min, size), targetAspectRatio, sourceAnchor, targetAnchor);
			Assert.AreEqual(expected, adjusted);
		}

		#endregion

		#region AdjustWidth()

		#region 1:1

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne()
		{
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustWidth(64f, 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustWidth(new Vector2(64f, 64f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustWidth(new Vector2(32f, 64f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustWidth(new Vector2(96f, 64f), 1f));
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(0f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(28f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(36f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(4f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(60f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(96f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(128f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(-16f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-20f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-12f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(96f, 64f), 1f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		#endregion

		#region 2:1

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne()
		{
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioUtility.AdjustWidth(32f, 2f));
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioUtility.AdjustWidth(new Vector2(64f, 32f), 2f));
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioUtility.AdjustWidth(new Vector2(32f, 32f), 2f));
			Assert.AreEqual(new Vector2(64f, 32f), AspectRatioUtility.AdjustWidth(new Vector2(96f, 32f), 2f));
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(0f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(28f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(36f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(4f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(60f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(96f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(128f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_TwoToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(-16f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-20f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-12f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 32f), 2f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		#endregion

		#region 1:2

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo()
		{
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioUtility.AdjustWidth(128f, 0.5f));
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioUtility.AdjustWidth(new Vector2(64f, 128f), 0.5f));
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioUtility.AdjustWidth(new Vector2(32f, 128f), 0.5f));
			Assert.AreEqual(new Vector2(64f, 128f), AspectRatioUtility.AdjustWidth(new Vector2(96f, 128f), 0.5f));
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(16f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(48f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(0f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(28f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(36f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.125f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(4f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(60f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-32f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(96f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(64f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(128f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(1f, 0.5f), new Vector2(0f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		[TestCase(Category = "Normal")]
		public void AdjustWidthTest_OneToTwo_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(-16f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(64f, 128f), 0.5f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-20f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(32f, 128f), 0.5f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
			AssertAreSame(new Vector2(-12f, 32f), new Vector2(64f, 128f), new Vector2(32f, 32f), new Vector2(96f, 128f), 0.5f, new Vector2(0.125f, 0.5f), new Vector2(0.875f, 0.5f), AspectRatioUtility.AdjustWidth);
		}

		#endregion

		#endregion

		#region AdjustHeight()

		#region 1:1

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne()
		{
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustHeight(64f, 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(64f, 64f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(64f, 32f), 1f));
			Assert.AreEqual(new Vector2(64f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(64f, 96f), 1f));
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 0f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 28f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 36f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 4f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 60f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(32f, -32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(32f, 96f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 128f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(32f, -16f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -20f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -12f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 96f), 1f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
		}

		#endregion

		#region 2:1

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne()
		{
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioUtility.AdjustHeight(128f, 2f));
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(128f, 64f), 2f));
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(128f, 32f), 2f));
			Assert.AreEqual(new Vector2(128f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(128f, 96f), 2f));
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 0f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 28f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 36f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 4f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 60f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(32f, -32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(32f, 96f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 128f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_TwoToOne_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(32f, -16f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 64f), 2f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -20f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 32f), 2f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -12f), new Vector2(128f, 64f), new Vector2(32f, 32f), new Vector2(128f, 96f), 2f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
		}

		#endregion

		#region 1:2

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo()
		{
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioUtility.AdjustHeight(32f, 0.5f));
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(32f, 64f), 0.5f));
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(32f, 32f), 0.5f));
			Assert.AreEqual(new Vector2(32f, 64f), AspectRatioUtility.AdjustHeight(new Vector2(32f, 96f), 0.5f));
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_MidAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 16f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 48f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_ZeroAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_OneAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 0f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_OneEighthAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 28f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 36f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.125f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_SevenEighthsAnchor()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 4f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 60f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_ZeroToOneAnchors()
		{
			AssertAreSame(new Vector2(32f, -32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_OneToZeroAnchors()
		{
			AssertAreSame(new Vector2(32f, 96f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 64f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, 128f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), AspectRatioUtility.AdjustHeight);
		}

		[TestCase(Category = "Normal")]
		public void AdjustHeightTest_OneToTwo_OneEighthToZeroEighthsAnchors()
		{
			AssertAreSame(new Vector2(32f, -16f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -20f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
			AssertAreSame(new Vector2(32f, -12f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 96f), 0.5f, new Vector2(0.5f, 0.125f), new Vector2(0.5f, 0.875f), AspectRatioUtility.AdjustHeight);
		}

		#endregion

		#endregion

		#region Expand()

		#region 1:1

		[TestCase(Category = "Normal")]
		public void ExpandTest_OneToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 1f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioUtility.Expand);
		}

		[TestCase(Category = "Normal")]
		public void ExpandTest_OneToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
		}

		[TestCase(Category = "Normal")]
		public void ExpandTest_OneToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
		}

		#endregion

		#region 2:1

		[TestCase(Category = "Normal")]
		public void ExpandTest_TwoToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 1f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.125f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.875f, 0.875f), AspectRatioUtility.Expand);
		}

		[TestCase(Category = "Normal")]
		public void ExpandTest_TwoToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
		}

		[TestCase(Category = "Normal")]
		public void ExpandTest_TwoToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
		}

		#endregion

		#region 1:2

		[TestCase(Category = "Normal")]
		public void ExpandTest_OneToTwo_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioUtility.Expand);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioUtility.Expand);
		}

		[TestCase(Category = "Normal")]
		public void ExpandTest_OneToTwo_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Expand);
		}

		[TestCase(Category = "Normal")]
		public void ExpandTest_OneToTwo_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 32f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Expand);
		}

		#endregion

		#endregion

		#region Shrink()

		#region 1:1

		[TestCase(Category = "Normal")]
		public void ShrinkTest_OneToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0f, 0f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(1f, 1f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioUtility.Shrink);
		}

		[TestCase(Category = "Normal")]
		public void ShrinkTest_OneToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
		}

		[TestCase(Category = "Normal")]
		public void ShrinkTest_OneToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
		}

		#endregion

		#region 2:1

		[TestCase(Category = "Normal")]
		public void ShrinkTest_TwoToOne_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0f, 0f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(1f, 1f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.125f, 0.125f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, new Vector2(0.875f, 0.875f), AspectRatioUtility.Shrink);
		}

		[TestCase(Category = "Normal")]
		public void ShrinkTest_TwoToOne_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 16f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
		}

		[TestCase(Category = "Normal")]
		public void ShrinkTest_TwoToOne_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 2f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
		}

		#endregion

		#region 1:2

		[TestCase(Category = "Normal")]
		public void ShrinkTest_OneToTwo_NoChange()
		{
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioUtility.Shrink);
			AssertNoChange(new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioUtility.Shrink);
		}

		[TestCase(Category = "Normal")]
		public void ShrinkTest_OneToTwo_AdjustWidth()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustWidth, AspectRatioUtility.Shrink);
		}

		[TestCase(Category = "Normal")]
		public void ShrinkTest_OneToTwo_AdjustHeight()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0f, 0f), new Vector2(1f, 1f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(1f, 1f), new Vector2(0f, 0f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.125f), new Vector2(0.875f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.875f), new Vector2(0.125f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.125f, 0.875f), new Vector2(0.875f, 0.125f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(16f, 64f), 0.5f, new Vector2(0.875f, 0.125f), new Vector2(0.125f, 0.875f), AspectRatioUtility.AdjustHeight, AspectRatioUtility.Shrink);
		}

		#endregion

		#endregion

		#region AdjustAverage()

		[TestCase(Category = "Normal")]
		public void AdjustAverageTest()
		{
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 64f), new Vector2(32f, 32f), new Vector2(64f, 64f), 1f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(24f, 40f), new Vector2(48f, 48f), new Vector2(32f, 32f), new Vector2(32f, 64f), 1f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(52f, 12f), new Vector2(56f, 56f), new Vector2(32f, 32f), new Vector2(96f, 16f), 1f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(64f, 32f), 2f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(8f, 44f), new Vector2(80f, 40f), new Vector2(32f, 32f), new Vector2(32f, 64f), 2f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(48f, 24f), new Vector2(64f, 32f), new Vector2(32f, 32f), new Vector2(96f, 16f), 2f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(32f, 32f), new Vector2(32f, 64f), new Vector2(32f, 32f), new Vector2(32f, 64f), 0.5f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(40f, 16f), new Vector2(48f, 96f), new Vector2(32f, 32f), new Vector2(64f, 64f), 0.5f, AspectRatioUtility.AdjustAverage);
			AssertAreSame(new Vector2(54f, -12f), new Vector2(52f, 104f), new Vector2(32f, 32f), new Vector2(96f, 16f), 0.5f, AspectRatioUtility.AdjustAverage);
		}

		#endregion
	}
}
#endif
