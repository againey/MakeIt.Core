/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.Numerics
{
	/// <summary>
	/// A collection of strategies for how to adjust a size or a size and position pair in order
	/// to obtain a two-dimensional region with a target aspect ratio.
	/// </summary>
	public enum AspectRatioPreservation
	{
		/// <summary>
		/// Don't modify anything in an attempt to acquire the desired aspect ratio.
		/// </summary>
		None,

		/// <summary>
		/// Expand whichever dimension is too small in order to acquire the desired aspect ratio.
		/// </summary>
		Expand,

		/// <summary>
		/// Shrink whichever dimension is too large in order to acquire the desired aspect ratio.
		/// </summary>
		Shrink,

		/// <summary>
		/// Expand or shrink the horizontal dimension in order to acquire the desired aspect ratio.
		/// </summary>
		AdjustWidth,

		/// <summary>
		/// Expand or shrink the vertical dimension in order to acquire the desired aspect ratio.
		/// </summary>
		AdjustHeight,

		/// <summary>
		/// Expand or shrink the both the horizontal dimension and vertical dimension in equal proportion in order to acquire the desired aspect ratio.
		/// </summary>
		AdjustAverage,

		/// <summary>
		/// Expand or shrink the both the horizontal dimension and vertical dimension according to a specified proportion in order to acquire the desired aspect ratio.
		/// </summary>
		AdjustProportionally,
	}

	/// <summary>
	/// Utility class for working with aspect ratios and two-dimensional sizes and positions relative to desired aspect ratios.
	/// </summary>
	public static class AspectRatioUtility
	{
		/// <summary>
		/// Returns the aspect ratio (width-to-height) of the given two-dimensional size.
		/// </summary>
		/// <param name="size">The size whose aspect ratio is to be computed.</param>
		/// <returns>The width-over-height aspect ratio of the given size.</returns>
		public static float GetAspectRatio(Vector2 size)
		{
			return size.x / size.y;
		}

		/// <summary>
		/// Returns the aspect ratio (width-to-height) of the given two-dimensional rectangle.
		/// </summary>
		/// <param name="rect">The rectangle whose aspect ratio is to be computed.</param>
		/// <returns>The width-over-height aspect ratio of the given rectangle.</returns>
		public static float GetAspectRatio(Rect rect)
		{
			return rect.width / rect.height;
		}

		#region Adjust(..., AspectRatioPreservation)

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>An adjusted size vector that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		public static Vector2 Adjust(AspectRatioPreservation preservation, Vector2 size, float targetAspectRatio)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return size;
				case AspectRatioPreservation.Expand: return Expand(size, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return Shrink(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>An adjusted rectangle that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		public static Rect Adjust(AspectRatioPreservation preservation, Rect rect, float targetAspectRatio)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return rect;
				case AspectRatioPreservation.Expand: return Expand(rect, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return Shrink(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="anchor">The normalized point from which the rectangle should adjust.</param>
		/// <returns>An adjusted rectangle that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect Adjust(AspectRatioPreservation preservation, Rect rect, float targetAspectRatio, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return rect;
				case AspectRatioPreservation.Expand: return Expand(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.Shrink: return Shrink(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should adjust.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should adjust.</param>
		/// <returns>An adjusted rectangle that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect Adjust(AspectRatioPreservation preservation, Rect rect, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return rect;
				case AspectRatioPreservation.Expand: return Expand(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.Shrink: return Shrink(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>An adjusted rectangle that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		public static Vector2 Adjust(AspectRatioPreservation preservation, Vector2 size, float targetAspectRatio, float adjustWidthWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return size;
				case AspectRatioPreservation.Expand: return Expand(size, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return Shrink(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: return AdjustProportionally(size, targetAspectRatio, adjustWidthWeight);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>An adjusted rectangle that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		public static Rect Adjust(AspectRatioPreservation preservation, Rect rect, float targetAspectRatio, float adjustWidthWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return rect;
				case AspectRatioPreservation.Expand: return Expand(rect, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return Shrink(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: return AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="anchor">The normalized point from which the rectangle should adjust.</param>
		/// <returns>An adjusted rectangle that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect Adjust(AspectRatioPreservation preservation, Rect rect, float targetAspectRatio, float adjustWidthWeight, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return rect;
				case AspectRatioPreservation.Expand: return Expand(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.Shrink: return Shrink(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustProportionally: return AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight, anchor);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, using the specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust the rectangle.</param>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should adjust.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should adjust.</param>
		/// <returns>An adjusted rectangle that has the desired aspect ratio and was derived from the input rectangle using modifications according to the specified preservation method.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect Adjust(AspectRatioPreservation preservation, Rect rect, float targetAspectRatio, float adjustWidthWeight, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return rect;
				case AspectRatioPreservation.Expand: return Expand(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.Shrink: return Shrink(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustWidth: return AdjustWidth(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustHeight: return AdjustHeight(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustAverage: return AdjustAverage(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustProportionally: return AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight, sourceAnchor, targetAnchor);
				default: throw new NotImplementedException();
			}
		}

		#endregion

		#region GetFixedRatioAdjustmentDelegate(...)

		/// <summary>
		/// A delegate for adjusting a rectangle size to fit a specific aspect ratio according to a method and parameters specified elsewhere.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <returns>The size of the adjusted rectangle.</returns>
		public delegate Vector2 FixedRatioSizeAdjustmentDelegate(Vector2 size);

		/// <summary>
		/// A delegate for adjusting a rectangle to fit a specific aspect ratio according to a method and parameters specified elsewhere.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <returns>The adjusted rectangle.</returns>
		public delegate Rect FixedRatioRectAdjustmentDelegate(Rect rect);

		/// <summary>
		/// Creates a delegate to adjust rectangle sizes to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <returns>The fixed ratio size adjustment delegate.</returns>
		public static FixedRatioSizeAdjustmentDelegate GetFixedRatioSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Vector2 size) => size;
				case AspectRatioPreservation.Expand: return (Vector2 size) => Expand(size, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Vector2 size) => Shrink(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Vector2 size) => AdjustWidth(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Vector2 size) => AdjustHeight(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Vector2 size) => AdjustAverage(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		public static FixedRatioRectAdjustmentDelegate GetFixedRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect) => Expand(rect, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Rect rect) => Shrink(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect) => AdjustWidth(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect) => AdjustHeight(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect) => AdjustAverage(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <param name="anchor">The normalized point from which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static FixedRatioRectAdjustmentDelegate GetFixedRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect) => Expand(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.Shrink: return (Rect rect) => Shrink(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect) => AdjustWidth(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect) => AdjustHeight(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect) => AdjustAverage(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <param name="sourceAnchor">The normalized point from which rectangles should adjust.</param>
		/// <param name="targetAnchor">The normalized point to which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static FixedRatioRectAdjustmentDelegate GetFixedRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect) => Expand(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.Shrink: return (Rect rect) => Shrink(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect) => AdjustWidth(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect) => AdjustHeight(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect) => AdjustAverage(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>The fixed ratio size adjustment delegate.</returns>
		public static FixedRatioSizeAdjustmentDelegate GetFixedRatioSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float adjustWidthWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Vector2 size) => size;
				case AspectRatioPreservation.Expand: return (Vector2 size) => Expand(size, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Vector2 size) => Shrink(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Vector2 size) => AdjustWidth(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Vector2 size) => AdjustHeight(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Vector2 size) => AdjustAverage(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: return (Vector2 size) => AdjustProportionally(size, targetAspectRatio, adjustWidthWeight);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		public static FixedRatioRectAdjustmentDelegate GetFixedRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float adjustWidthWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect) => Expand(rect, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Rect rect) => Shrink(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect) => AdjustWidth(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect) => AdjustHeight(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect) => AdjustAverage(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: return (Rect rect) => AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="anchor">The normalized point from which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static FixedRatioRectAdjustmentDelegate GetFixedRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float adjustWidthWeight, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect) => Expand(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.Shrink: return (Rect rect) => Shrink(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect) => AdjustWidth(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect) => AdjustHeight(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect) => AdjustAverage(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustProportionally: return (Rect rect) => AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight, anchor);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which rectangles should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="sourceAnchor">The normalized point from which rectangles should adjust.</param>
		/// <param name="targetAnchor">The normalized point to which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static FixedRatioRectAdjustmentDelegate GetFixedRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float adjustWidthWeight, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect) => Expand(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.Shrink: return (Rect rect) => Shrink(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect) => AdjustWidth(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect) => AdjustHeight(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect) => AdjustAverage(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustProportionally: return (Rect rect) => AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight, sourceAnchor, targetAnchor);
				default: throw new NotImplementedException();
			}
		}

		#endregion

		#region GetVariableRatioAdjustmentDelegate(...)

		/// <summary>
		/// A delegate for adjusting a rectangle size to fit a specifiable aspect ratio according to a method and parameters specified elsewhere.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>The size of the adjusted rectangle.</returns>
		public delegate Vector2 VariableRatioSizeAdjustmentDelegate(Vector2 size, float targetAspectRatio);

		/// <summary>
		/// A delegate for adjusting a rectangle to fit a specifiable aspect ratio according to a method and parameters specified elsewhere.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>The adjusted rectangle.</returns>
		public delegate Rect VariableRatioRectAdjustmentDelegate(Rect rect, float targetAspectRatio);

		/// <summary>
		/// Creates a delegate to adjust rectangle sizes to fit a specifiable aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <returns>The fixed ratio size adjustment delegate.</returns>
		public static VariableRatioSizeAdjustmentDelegate GetVariableRatioSizeAdjustmentDelegate(AspectRatioPreservation preservation)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Vector2 size, float targetAspectRatio) => size;
				case AspectRatioPreservation.Expand: return (Vector2 size, float targetAspectRatio) => Expand(size, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Vector2 size, float targetAspectRatio) => Shrink(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Vector2 size, float targetAspectRatio) => AdjustWidth(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Vector2 size, float targetAspectRatio) => AdjustHeight(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Vector2 size, float targetAspectRatio) => AdjustAverage(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit the given aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		public static VariableRatioRectAdjustmentDelegate GetVariableRatioRectAdjustmentDelegate(AspectRatioPreservation preservation)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect, float targetAspectRatio) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect, float targetAspectRatio) => Expand(rect, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Rect rect, float targetAspectRatio) => Shrink(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect, float targetAspectRatio) => AdjustWidth(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect, float targetAspectRatio) => AdjustHeight(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect, float targetAspectRatio) => AdjustAverage(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit a specifiable aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="anchor">The normalized point from which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static VariableRatioRectAdjustmentDelegate GetVariableRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect, float targetAspectRatio) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect, float targetAspectRatio) => Expand(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.Shrink: return (Rect rect, float targetAspectRatio) => Shrink(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect, float targetAspectRatio) => AdjustWidth(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect, float targetAspectRatio) => AdjustHeight(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect, float targetAspectRatio) => AdjustAverage(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit a specifiable aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="sourceAnchor">The normalized point from which rectangles should adjust.</param>
		/// <param name="targetAnchor">The normalized point to which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static VariableRatioRectAdjustmentDelegate GetVariableRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect, float targetAspectRatio) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect, float targetAspectRatio) => Expand(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.Shrink: return (Rect rect, float targetAspectRatio) => Shrink(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect, float targetAspectRatio) => AdjustWidth(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect, float targetAspectRatio) => AdjustHeight(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect, float targetAspectRatio) => AdjustAverage(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit a specifiable aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>The fixed ratio size adjustment delegate.</returns>
		public static VariableRatioSizeAdjustmentDelegate GetVariableRatioSizeAdjustmentDelegate(AspectRatioPreservation preservation, float adjustWidthWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Vector2 size, float targetAspectRatio) => size;
				case AspectRatioPreservation.Expand: return (Vector2 size, float targetAspectRatio) => Expand(size, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Vector2 size, float targetAspectRatio) => Shrink(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Vector2 size, float targetAspectRatio) => AdjustWidth(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Vector2 size, float targetAspectRatio) => AdjustHeight(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Vector2 size, float targetAspectRatio) => AdjustAverage(size, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: return (Vector2 size, float targetAspectRatio) => AdjustProportionally(size, targetAspectRatio, adjustWidthWeight);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit a specifiable aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		public static VariableRatioRectAdjustmentDelegate GetVariableRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float adjustWidthWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect, float targetAspectRatio) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect, float targetAspectRatio) => Expand(rect, targetAspectRatio);
				case AspectRatioPreservation.Shrink: return (Rect rect, float targetAspectRatio) => Shrink(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect, float targetAspectRatio) => AdjustWidth(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect, float targetAspectRatio) => AdjustHeight(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect, float targetAspectRatio) => AdjustAverage(rect, targetAspectRatio);
				case AspectRatioPreservation.AdjustProportionally: return (Rect rect, float targetAspectRatio) => AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit a specifiable aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="anchor">The normalized point from which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static VariableRatioRectAdjustmentDelegate GetVariableRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float adjustWidthWeight, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect, float targetAspectRatio) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect, float targetAspectRatio) => Expand(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.Shrink: return (Rect rect, float targetAspectRatio) => Shrink(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect, float targetAspectRatio) => AdjustWidth(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect, float targetAspectRatio) => AdjustHeight(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect, float targetAspectRatio) => AdjustAverage(rect, targetAspectRatio, anchor);
				case AspectRatioPreservation.AdjustProportionally: return (Rect rect, float targetAspectRatio) => AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight, anchor);
				default: throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Creates a delegate to adjust rectangles to fit a specifiable aspect ratio according to specified preservation method.
		/// </summary>
		/// <param name="preservation">The method to use to adjust rectangles.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="sourceAnchor">The normalized point from which rectangles should adjust.</param>
		/// <param name="targetAnchor">The normalized point to which rectangles should adjust.</param>
		/// <returns>The fixed ratio rectangle adjustment delegate.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static VariableRatioRectAdjustmentDelegate GetVariableRatioRectAdjustmentDelegate(AspectRatioPreservation preservation, float adjustWidthWeight, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Rect rect, float targetAspectRatio) => rect;
				case AspectRatioPreservation.Expand: return (Rect rect, float targetAspectRatio) => Expand(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.Shrink: return (Rect rect, float targetAspectRatio) => Shrink(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustWidth: return (Rect rect, float targetAspectRatio) => AdjustWidth(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustHeight: return (Rect rect, float targetAspectRatio) => AdjustHeight(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustAverage: return (Rect rect, float targetAspectRatio) => AdjustAverage(rect, targetAspectRatio, sourceAnchor, targetAnchor);
				case AspectRatioPreservation.AdjustProportionally: return (Rect rect, float targetAspectRatio) => AdjustProportionally(rect, targetAspectRatio, adjustWidthWeight, sourceAnchor, targetAnchor);
				default: throw new NotImplementedException();
			}
		}

		#endregion

		#region Expand(...)

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by increasing either the width or height and keeping the other at its present value.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, expanded from the input rectangle.</returns>
		/// <seealso cref="AdjustWidth(float, float)"/>
		/// <seealso cref="AdjustHeight(float, float)"/>
		public static Vector2 Expand(Vector2 size, float targetAspectRatio)
		{
			if (size.x >= targetAspectRatio * size.y)
			{
				return AdjustHeight(size.x, targetAspectRatio);
			}
			else
			{
				return AdjustWidth(size.y, targetAspectRatio);
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by increasing either the width or height and keeping the other at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the size should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, expanded from the input size.</returns>
		/// <seealso cref="AdjustWidth(Rect, float)"/>
		/// <seealso cref="AdjustHeight(Rect, float)"/>
		/// <remarks><para>There is an implied anchor at the center of the rectangle.  The world coordinate
		/// corresponding to the center of the rectangle before the adjustment will still be the same for
		/// the center point even after the adjustment.</para></remarks>
		public static Rect Expand(Rect rect, float targetAspectRatio)
		{
			if (rect.width >= targetAspectRatio * rect.height)
			{
				return AdjustHeight(rect, targetAspectRatio);
			}
			else
			{
				return AdjustWidth(rect, targetAspectRatio);
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by increasing either the width or height and keeping the other at its present value, anchored around the specified normalized point.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the size should conform.</param>
		/// <param name="anchor">The normalized point from which the rectangle should expand.</param>
		/// <returns>A rectangle that has the desired aspect ratio, expanded from the input rectangle.</returns>
		/// <seealso cref="AdjustWidth(Rect, float, float)"/>
		/// <seealso cref="AdjustHeight(Rect, float, float)"/>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect Expand(Rect rect, float targetAspectRatio, Vector2 anchor)
		{
			if (rect.width >= targetAspectRatio * rect.height)
			{
				return AdjustHeight(rect, targetAspectRatio, anchor.y);
			}
			else
			{
				return AdjustWidth(rect, targetAspectRatio, anchor.x);
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by increasing either the width or height and keeping the other at its present value, and keeping the anchor point in the same position.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the size should conform.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should expand.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should expand.</param>
		/// <returns>A rectangle that has the desired aspect ratio, expanded from the input rectangle.</returns>
		/// <seealso cref="AdjustWidth(Rect, float, float, float)"/>
		/// <seealso cref="AdjustHeight(Rect, float, float, float)"/>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect Expand(Rect rect, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			if (rect.width >= targetAspectRatio * rect.height)
			{
				return AdjustHeight(rect, targetAspectRatio, sourceAnchor.y, targetAnchor.y);
			}
			else
			{
				return AdjustWidth(rect, targetAspectRatio, sourceAnchor.x, targetAnchor.x);
			}
		}

		#endregion

		#region Shrink(...)

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by decreasing either the width or height and keeping the other at its present value.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, shrunk from the input rectangle.</returns>
		/// <seealso cref="AdjustWidth(float, float)"/>
		/// <seealso cref="AdjustHeight(float, float)"/>
		public static Vector2 Shrink(Vector2 size, float targetAspectRatio)
		{
			if (size.x >= targetAspectRatio * size.y)
			{
				return AdjustWidth(size.y, targetAspectRatio);
			}
			else
			{
				return AdjustHeight(size.x, targetAspectRatio);
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by decreasing either the width or height and keeping the other at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the size should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, shrunk from the input size.</returns>
		/// <seealso cref="AdjustWidth(Rect, float)"/>
		/// <seealso cref="AdjustHeight(Rect, float)"/>
		/// <remarks><para>There is an implied anchor at the center of the rectangle.  The world coordinate
		/// corresponding to the center of the rectangle before the adjustment will still be the same for
		/// the center point even after the adjustment.</para></remarks>
		public static Rect Shrink(Rect rect, float targetAspectRatio)
		{
			if (rect.width >= targetAspectRatio * rect.height)
			{
				return AdjustWidth(rect, targetAspectRatio);
			}
			else
			{
				return AdjustHeight(rect, targetAspectRatio);
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by decreasing either the width or height and keeping the other at its present value, anchored around the specified normalized point.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the size should conform.</param>
		/// <param name="anchor">The normalized point from which the rectangle should shrink.</param>
		/// <returns>A rectangle that has the desired aspect ratio, shrunk from the input rectangle.</returns>
		/// <seealso cref="AdjustWidth(Rect, float, float)"/>
		/// <seealso cref="AdjustHeight(Rect, float, float)"/>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect Shrink(Rect rect, float targetAspectRatio, Vector2 anchor)
		{
			if (rect.width >= targetAspectRatio * rect.height)
			{
				return AdjustWidth(rect, targetAspectRatio, anchor);
			}
			else
			{
				return AdjustHeight(rect, targetAspectRatio, anchor);
			}
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by decreasing either the width or height and keeping the other at its present value, and keeping the anchor point in the same position.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the size should conform.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should shrink.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should shrink.</param>
		/// <returns>A rectangle that has the desired aspect ratio, shrunk from the input rectangle.</returns>
		/// <seealso cref="AdjustWidth(Rect, float, float, float)"/>
		/// <seealso cref="AdjustHeight(Rect, float, float, float)"/>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect Shrink(Rect rect, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			if (rect.width >= targetAspectRatio * rect.height)
			{
				return AdjustWidth(rect, targetAspectRatio, sourceAnchor, targetAnchor);
			}
			else
			{
				return AdjustHeight(rect, targetAspectRatio, sourceAnchor, targetAnchor);
			}
		}

		#endregion

		#region AdjustWidth(...)

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and keeping the height at its present value.
		/// </summary>
		/// <param name="height">The height of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the height of the input rectangle and an appropriate width.</returns>
		public static Vector2 AdjustWidth(float height, float targetAspectRatio)
		{
			return new Vector2(height * targetAspectRatio, height);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and keeping the height at its present value.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the height of the input rectangle and an appropriate width.</returns>
		public static Vector2 AdjustWidth(Vector2 size, float targetAspectRatio)
		{
			return new Vector2(size.y * targetAspectRatio, size.y);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and keeping the height at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the height of the input rectangle and an appropriate width.</returns>
		public static Rect AdjustWidth(Rect rect, float targetAspectRatio)
		{
			var newWidth = rect.height * targetAspectRatio;
			return new Rect(rect.xMin + (rect.width - newWidth) * 0.5f, rect.yMin, newWidth, rect.height);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and keeping the height at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="anchorX">The x coordinate of the normalized point from which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the height of the input rectangle and an appropriate width.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect AdjustWidth(Rect rect, float targetAspectRatio, float anchorX)
		{
			var newWidth = rect.height * targetAspectRatio;
			return new Rect(rect.xMin + (rect.width - newWidth) * anchorX, rect.yMin, newWidth, rect.height);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and keeping the height at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="anchor">The normalized point from which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the height of the input rectangle and an appropriate width.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect AdjustWidth(Rect rect, float targetAspectRatio, Vector2 anchor)
		{
			var newWidth = rect.height * targetAspectRatio;
			return new Rect(rect.xMin + (rect.width - newWidth) * anchor.x, rect.yMin, newWidth, rect.height);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and keeping the height at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="sourceAnchorX">The x coordinate of the normalized point from which the rectangle should conform.</param>
		/// <param name="targetAnchorX">The x coordinate of the normalized point to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the height of the input rectangle and an appropriate width.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect AdjustWidth(Rect rect, float targetAspectRatio, float sourceAnchorX, float targetAnchorX)
		{
			var newWidth = rect.height * targetAspectRatio;
			return new Rect(rect.xMin + rect.width * sourceAnchorX - newWidth * targetAnchorX, rect.yMin, newWidth, rect.height);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and keeping the height at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should conform.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the height of the input rectangle and an appropriate width.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect AdjustWidth(Rect rect, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var newWidth = rect.height * targetAspectRatio;
			return new Rect(rect.xMin + rect.width * sourceAnchor.x - newWidth * targetAnchor.x, rect.yMin, newWidth, rect.height);
		}

		#endregion

		#region AdjustHeight(...)

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the height and keeping the width at its present value.
		/// </summary>
		/// <param name="width">The width of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the width of the input rectangle and an appropriate height.</returns>
		public static Vector2 AdjustHeight(float width, float targetAspectRatio)
		{
			return new Vector2(width, width / targetAspectRatio);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the height and keeping the width at its present value.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the width of the input rectangle and an appropriate height.</returns>
		public static Vector2 AdjustHeight(Vector2 size, float targetAspectRatio)
		{
			return new Vector2(size.x, size.x / targetAspectRatio);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the height and keeping the width at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the width of the input rectangle and an appropriate height.</returns>
		public static Rect AdjustHeight(Rect rect, float targetAspectRatio)
		{
			var newHeight = rect.width / targetAspectRatio;
			return new Rect(rect.xMin, rect.yMin + (rect.height - newHeight) * 0.5f, rect.width, newHeight);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the height and keeping the width at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="anchorY">The y coordinate of the normalized point from which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the width of the input rectangle and an appropriate height.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect AdjustHeight(Rect rect, float targetAspectRatio, float anchorY)
		{
			var newHeight = rect.width / targetAspectRatio;
			return new Rect(rect.xMin, rect.yMin + (rect.height - newHeight) * anchorY, rect.width, newHeight);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the height and keeping the width at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="anchor">The normalized point from which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the width of the input rectangle and an appropriate height.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect AdjustHeight(Rect rect, float targetAspectRatio, Vector2 anchor)
		{
			var newHeight = rect.width / targetAspectRatio;
			return new Rect(rect.xMin, rect.yMin + (rect.height - newHeight) * anchor.y, rect.width, newHeight);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the height and keeping the width at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="sourceAnchorY">The y coordinate of the normalized point from which the rectangle should conform.</param>
		/// <param name="targetAnchorY">The y coordinate of the normalized point to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the width of the input rectangle and an appropriate height.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect AdjustHeight(Rect rect, float targetAspectRatio, float sourceAnchorY, float targetAnchorY)
		{
			var newHeight = rect.width / targetAspectRatio;
			return new Rect(rect.xMin, rect.yMin + rect.height * sourceAnchorY - newHeight * targetAnchorY, rect.width, newHeight);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the height and keeping the width at its present value.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should conform.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio, using the width of the input rectangle and an appropriate height.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect AdjustHeight(Rect rect, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var newHeight = rect.width / targetAspectRatio;
			return new Rect(rect.xMin, rect.yMin + rect.height * sourceAnchor.y - newHeight * targetAnchor.y, rect.width, newHeight);
		}

		#endregion

		#region AdjustAverage(...)

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height in equal proportions.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		public static Vector2 AdjustAverage(Vector2 size, float targetAspectRatio)
		{
			return new Vector2((size.x + size.y * targetAspectRatio) * 0.5f, (size.x / targetAspectRatio + size.y) * 0.5f);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height in equal proportions.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		public static Rect AdjustAverage(Rect rect, float targetAspectRatio)
		{
			var newWidth = rect.height * targetAspectRatio;
			var newHeight = rect.width / targetAspectRatio;

			return new Rect(
				rect.xMin + (rect.width - newWidth) * 0.25f,
				rect.yMin + (rect.height - newHeight) * 0.25f,
				(rect.width + newWidth) * 0.5f,
				(rect.height + newHeight) * 0.5f);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height in equal proportions.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="anchor">The normalized point from which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect AdjustAverage(Rect rect, float targetAspectRatio, Vector2 anchor)
		{
			var newWidth = rect.height * targetAspectRatio;
			var newHeight = rect.width / targetAspectRatio;

			return new Rect(
				rect.xMin + (rect.width - newWidth) * anchor.x * 0.5f,
				rect.yMin + (rect.height - newHeight) * anchor.y * 0.5f,
				(rect.width + newWidth) * 0.5f,
				(rect.height + newHeight) * 0.5f);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height in equal proportions.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should conform.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect AdjustAverage(Rect rect, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var newWidth = rect.height * targetAspectRatio;
			var newHeight = rect.width / targetAspectRatio;

			return new Rect(
				rect.xMin + (rect.width * sourceAnchor.x - newWidth * targetAnchor.x) * 0.5f,
				rect.yMin + (rect.height * sourceAnchor.y - newHeight * targetAnchor.y) * 0.5f,
				(rect.width + newWidth) * 0.5f,
				(rect.height + newHeight) * 0.5f);
		}

		#endregion

		#region AdjustProportionally(...)

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height proportionally, weighted according <paramref name="adjustWidthWeight"/>.
		/// </summary>
		/// <param name="size">The size of the rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		public static Vector2 AdjustProportionally(Vector2 size, float targetAspectRatio, float adjustWidthWeight)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;

			return new Vector2(
				size.x * adjustHeightWeight + size.y * targetAspectRatio * adjustWidthWeight,
				size.y * adjustWidthWeight + size.x / targetAspectRatio * adjustHeightWeight);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height proportionally, weighted according <paramref name="adjustWidthWeight"/>.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		public static Rect AdjustProportionally(Rect rect, float targetAspectRatio, float adjustWidthWeight)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;

			var newWidth = rect.height * targetAspectRatio;
			var newHeight = rect.width / targetAspectRatio;

			return new Rect(
				rect.xMin + (rect.width - newWidth) * 0.5f * adjustWidthWeight,
				rect.yMin + (rect.height - newHeight) * 0.5f * adjustHeightWeight,
				rect.width * adjustHeightWeight + newWidth * adjustWidthWeight,
				rect.height * adjustWidthWeight + newHeight * adjustHeightWeight);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height proportionally, weighted according <paramref name="adjustWidthWeight"/>.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="anchor">The normalized point from which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		/// <remarks><para>The anchor is specified as a normalized point in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the anchor point before the adjustment will still be the same for
		/// that anchor point even after the adjustment.</para></remarks>
		public static Rect AdjustProportionally(Rect rect, float targetAspectRatio, float adjustWidthWeight, Vector2 anchor)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;

			var newWidth = rect.height * targetAspectRatio;
			var newHeight = rect.width / targetAspectRatio;

			return new Rect(
				rect.xMin + (rect.width - newWidth) * anchor.x * adjustWidthWeight,
				rect.yMin + (rect.height - newHeight) * anchor.y * adjustHeightWeight,
				rect.width * adjustHeightWeight + newWidth * adjustWidthWeight,
				rect.height * adjustWidthWeight + newHeight * adjustHeightWeight);
		}

		/// <summary>
		/// Adjust the provided rectangle to conform to the indicated target aspect ratio, by changing the width and height proportionally, weighted according <paramref name="adjustWidthWeight"/>.
		/// </summary>
		/// <param name="rect">The rectangle to adjust.</param>
		/// <param name="targetAspectRatio">The desired aspect ratio to which the rectangle should conform.</param>
		/// <param name="adjustWidthWeight">The proportional weight indicating the degree to which the weight should change in contrast with how much the height should change.</param>
		/// <param name="sourceAnchor">The normalized point from which the rectangle should conform.</param>
		/// <param name="targetAnchor">The normalized point to which the rectangle should conform.</param>
		/// <returns>A size vector that has the desired aspect ratio.</returns>
		/// <remarks><para>The anchors are specified as normalized points in the coordinate space defined by the
		/// rectangle.  (0, 0) is at the min corner of the rectangle, while (1, 1) is at the max corner.  The
		/// world coordinate corresponding to the source anchor point before the adjustment will be the same for
		/// the target anchor point after the adjustment.</para></remarks>
		public static Rect AdjustProportionally(Rect rect, float targetAspectRatio, float adjustWidthWeight, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;

			var newWidth = rect.height * targetAspectRatio;
			var newHeight = rect.width / targetAspectRatio;

			return new Rect(
				rect.xMin + (rect.width * sourceAnchor.x - newWidth * targetAnchor.x) * adjustWidthWeight,
				rect.yMin + (rect.height * sourceAnchor.y - newHeight * targetAnchor.y) * adjustHeightWeight,
				rect.width * adjustHeightWeight + newWidth * adjustWidthWeight,
				rect.height * adjustWidthWeight + newHeight * adjustHeightWeight);
		}

		#endregion
	}
}
