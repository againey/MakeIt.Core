/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.MakeIt.Utilities
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
	public static class MIAspectRatio
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

		#region Adjust(..., AspectRatioPreservation)

		public static Vector2 Adjust(Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation)
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

		public static void Adjust(ref Vector2 min, ref Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: break;
				case AspectRatioPreservation.Expand: Expand(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.Shrink: Shrink(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustWidth: AdjustWidth(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustHeight: AdjustHeight(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustAverage: AdjustAverage(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		public static void Adjust(ref Vector2 min, ref Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: break;
				case AspectRatioPreservation.Expand: Expand(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.Shrink: Shrink(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustWidth: AdjustWidth(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustHeight: AdjustHeight(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustAverage: AdjustAverage(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		public static void Adjust(ref Vector2 min, ref Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: break;
				case AspectRatioPreservation.Expand: Expand(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.Shrink: Shrink(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustWidth: AdjustWidth(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustHeight: AdjustHeight(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustAverage: AdjustAverage(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		public static Vector2 Adjust(Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation, float adjustWidthWeight)
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

		public static void Adjust(ref Vector2 min, ref Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation, float adjustWidthWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: break;
				case AspectRatioPreservation.Expand: Expand(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.Shrink: Shrink(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustWidth: AdjustWidth(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustHeight: AdjustHeight(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustAverage: AdjustAverage(ref min, ref size, targetAspectRatio); break;
				case AspectRatioPreservation.AdjustProportionally: AdjustProportionally(ref min, ref size, targetAspectRatio, adjustWidthWeight); break;
				default: throw new NotImplementedException();
			}
		}

		public static void Adjust(ref Vector2 min, ref Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation, float adjustWidthWeight, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: break;
				case AspectRatioPreservation.Expand: Expand(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.Shrink: Shrink(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustWidth: AdjustWidth(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustHeight: AdjustHeight(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustAverage: AdjustAverage(ref min, ref size, targetAspectRatio, anchor); break;
				case AspectRatioPreservation.AdjustProportionally: AdjustProportionally(ref min, ref size, targetAspectRatio, adjustWidthWeight, anchor); break;
				default: throw new NotImplementedException();
			}
		}

		public static void Adjust(ref Vector2 min, ref Vector2 size, float targetAspectRatio, AspectRatioPreservation preservation, float adjustWidthWeight, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: break;
				case AspectRatioPreservation.Expand: Expand(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.Shrink: Shrink(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustWidth: AdjustWidth(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustHeight: AdjustHeight(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustAverage: AdjustAverage(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); break;
				case AspectRatioPreservation.AdjustProportionally: AdjustProportionally(ref min, ref size, targetAspectRatio, adjustWidthWeight, sourceAnchor, targetAnchor); break;
				default: throw new NotImplementedException();
			}
		}

		#endregion

		#region Get..AdjustmentDelegate(...)

		public delegate Vector2 SizeAdjustmentDelegate(Vector2 size);
		public delegate void MinAndSizeAdjustmentDelegate(ref Vector2 min, ref Vector2 size);

		public static SizeAdjustmentDelegate GetSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Vector2 size) => { return size; };
				case AspectRatioPreservation.Expand: return (Vector2 size) => { return Expand(size, targetAspectRatio); };
				case AspectRatioPreservation.Shrink: return (Vector2 size) => { return Shrink(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustWidth: return (Vector2 size) => { return AdjustWidth(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustHeight: return (Vector2 size) => { return AdjustHeight(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustAverage: return (Vector2 size) => { return AdjustAverage(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		public static MinAndSizeAdjustmentDelegate GetMinAndSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (ref Vector2 min, ref Vector2 size) => { };
				case AspectRatioPreservation.Expand: return (ref Vector2 min, ref Vector2 size) => { Expand(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.Shrink: return (ref Vector2 min, ref Vector2 size) => { Shrink(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustWidth: return (ref Vector2 min, ref Vector2 size) => { AdjustWidth(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustHeight: return (ref Vector2 min, ref Vector2 size) => { AdjustHeight(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustAverage: return (ref Vector2 min, ref Vector2 size) => { AdjustAverage(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		public static MinAndSizeAdjustmentDelegate GetMinAndSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (ref Vector2 min, ref Vector2 size) => { };
				case AspectRatioPreservation.Expand: return (ref Vector2 min, ref Vector2 size) => { Expand(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.Shrink: return (ref Vector2 min, ref Vector2 size) => { Shrink(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustWidth: return (ref Vector2 min, ref Vector2 size) => { AdjustWidth(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustHeight: return (ref Vector2 min, ref Vector2 size) => { AdjustHeight(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustAverage: return (ref Vector2 min, ref Vector2 size) => { AdjustAverage(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		public static MinAndSizeAdjustmentDelegate GetMinAndSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (ref Vector2 min, ref Vector2 size) => { };
				case AspectRatioPreservation.Expand: return (ref Vector2 min, ref Vector2 size) => { Expand(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.Shrink: return (ref Vector2 min, ref Vector2 size) => { Shrink(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustWidth: return (ref Vector2 min, ref Vector2 size) => { AdjustWidth(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustHeight: return (ref Vector2 min, ref Vector2 size) => { AdjustHeight(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustAverage: return (ref Vector2 min, ref Vector2 size) => { AdjustAverage(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustProportionally: throw new ArgumentException(string.Format("{0}.{1} cannot be used with the overload that does not include a width adjustment weight argument.", typeof(AspectRatioPreservation).Name, preservation.ToString()), "preservation");
				default: throw new NotImplementedException();
			}
		}

		public static SizeAdjustmentDelegate GetSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float widthAdjustmentWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (Vector2 size) => { return size; };
				case AspectRatioPreservation.Expand: return (Vector2 size) => { return Expand(size, targetAspectRatio); };
				case AspectRatioPreservation.Shrink: return (Vector2 size) => { return Shrink(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustWidth: return (Vector2 size) => { return AdjustWidth(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustHeight: return (Vector2 size) => { return AdjustHeight(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustAverage: return (Vector2 size) => { return AdjustAverage(size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustProportionally: return (Vector2 size) => { return AdjustProportionally(size, targetAspectRatio, widthAdjustmentWeight); };
				default: throw new NotImplementedException();
			}
		}

		public static MinAndSizeAdjustmentDelegate GetMinAndSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float widthAdjustmentWeight)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (ref Vector2 min, ref Vector2 size) => { };
				case AspectRatioPreservation.Expand: return (ref Vector2 min, ref Vector2 size) => { Expand(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.Shrink: return (ref Vector2 min, ref Vector2 size) => { Shrink(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustWidth: return (ref Vector2 min, ref Vector2 size) => { AdjustWidth(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustHeight: return (ref Vector2 min, ref Vector2 size) => { AdjustHeight(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustAverage: return (ref Vector2 min, ref Vector2 size) => { AdjustAverage(ref min, ref size, targetAspectRatio); };
				case AspectRatioPreservation.AdjustProportionally: return (ref Vector2 min, ref Vector2 size) => { AdjustProportionally(ref min, ref size, targetAspectRatio, widthAdjustmentWeight); };
				default: throw new NotImplementedException();
			}
		}

		public static MinAndSizeAdjustmentDelegate GetMinAndSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float widthAdjustmentWeight, Vector2 anchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (ref Vector2 min, ref Vector2 size) => { };
				case AspectRatioPreservation.Expand: return (ref Vector2 min, ref Vector2 size) => { Expand(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.Shrink: return (ref Vector2 min, ref Vector2 size) => { Shrink(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustWidth: return (ref Vector2 min, ref Vector2 size) => { AdjustWidth(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustHeight: return (ref Vector2 min, ref Vector2 size) => { AdjustHeight(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustAverage: return (ref Vector2 min, ref Vector2 size) => { AdjustAverage(ref min, ref size, targetAspectRatio, anchor); };
				case AspectRatioPreservation.AdjustProportionally: return (ref Vector2 min, ref Vector2 size) => { AdjustProportionally(ref min, ref size, targetAspectRatio, widthAdjustmentWeight, anchor); };
				default: throw new NotImplementedException();
			}
		}

		public static MinAndSizeAdjustmentDelegate GetMinAndSizeAdjustmentDelegate(AspectRatioPreservation preservation, float targetAspectRatio, float widthAdjustmentWeight, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			switch (preservation)
			{
				case AspectRatioPreservation.None: return (ref Vector2 min, ref Vector2 size) => { };
				case AspectRatioPreservation.Expand: return (ref Vector2 min, ref Vector2 size) => { Expand(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.Shrink: return (ref Vector2 min, ref Vector2 size) => { Shrink(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustWidth: return (ref Vector2 min, ref Vector2 size) => { AdjustWidth(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustHeight: return (ref Vector2 min, ref Vector2 size) => { AdjustHeight(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustAverage: return (ref Vector2 min, ref Vector2 size) => { AdjustAverage(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor); };
				case AspectRatioPreservation.AdjustProportionally: return (ref Vector2 min, ref Vector2 size) => { AdjustProportionally(ref min, ref size, targetAspectRatio, widthAdjustmentWeight, sourceAnchor, targetAnchor); };
				default: throw new NotImplementedException();
			}
		}

		#endregion

		#region Expand(...)

		public static Vector2 Expand(Vector2 size, float targetAspectRatio)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				return AdjustHeight(size.x, targetAspectRatio);
			}
			else
			{
				return AdjustWidth(size.y, targetAspectRatio);
			}
		}

		public static void Expand(ref Vector2 min, ref Vector2 size, float targetAspectRatio)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				AdjustHeight(ref min, ref size, targetAspectRatio);
			}
			else
			{
				AdjustWidth(ref min, ref size, targetAspectRatio);
			}
		}

		public static void Expand(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 anchor)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				AdjustHeight(ref min, ref size, targetAspectRatio, anchor);
			}
			else
			{
				AdjustWidth(ref min, ref size, targetAspectRatio, anchor);
			}
		}

		public static void Expand(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				AdjustHeight(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor);
			}
			else
			{
				AdjustWidth(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor);
			}
		}

		#endregion

		#region Shrink(...)

		public static Vector2 Shrink(Vector2 size, float targetAspectRatio)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				return AdjustWidth(size.y, targetAspectRatio);
			}
			else
			{
				return AdjustHeight(size.x, targetAspectRatio);
			}
		}

		public static void Shrink(ref Vector2 min, ref Vector2 size, float targetAspectRatio)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				AdjustWidth(ref min, ref size, targetAspectRatio);
			}
			else
			{
				AdjustHeight(ref min, ref size, targetAspectRatio);
			}
		}

		public static void Shrink(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 anchor)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				AdjustWidth(ref min, ref size, targetAspectRatio, anchor);
			}
			else
			{
				AdjustHeight(ref min, ref size, targetAspectRatio, anchor);
			}
		}

		public static void Shrink(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var aspectRatio = size.x / size.y;
			if (aspectRatio >= targetAspectRatio)
			{
				AdjustWidth(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor);
			}
			else
			{
				AdjustHeight(ref min, ref size, targetAspectRatio, sourceAnchor, targetAnchor);
			}
		}

		#endregion

		#region AdjustWidth(...)

		public static Vector2 AdjustWidth(float height, float targetAspectRatio)
		{
			return new Vector2(height * targetAspectRatio, height);
		}

		public static Vector2 AdjustWidth(Vector2 size, float targetAspectRatio)
		{
			return new Vector2(size.y * targetAspectRatio, size.y);
		}

		public static void AdjustWidth(ref Vector2 min, ref Vector2 size, float targetAspectRatio)
		{
			var newWidth = size.y * targetAspectRatio;
			min.x += (size.x - newWidth) * 0.5f;
			size.x = newWidth;
		}

		public static void AdjustWidth(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 anchor)
		{
			var newWidth = size.y * targetAspectRatio;
			min.x += (size.x - newWidth) * anchor.x;
			size.x = newWidth;
		}

		public static void AdjustWidth(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var newWidth = size.y * targetAspectRatio;
			min.x += size.x * sourceAnchor.x - newWidth * targetAnchor.x;
			size.x = newWidth;
		}

		#endregion

		#region AdjustHeight(...)

		public static Vector2 AdjustHeight(float width, float targetAspectRatio)
		{
			return new Vector2(width, width / targetAspectRatio);
		}

		public static Vector2 AdjustHeight(Vector2 size, float targetAspectRatio)
		{
			return new Vector2(size.x, size.x / targetAspectRatio);
		}

		public static void AdjustHeight(ref Vector2 min, ref Vector2 size, float targetAspectRatio)
		{
			var newHeight = size.x / targetAspectRatio;
			min.y += (size.y - newHeight) * 0.5f;
			size.y = newHeight;
		}

		public static void AdjustHeight(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 anchor)
		{
			var newHeight = size.x / targetAspectRatio;
			min.y += (size.y - newHeight) * anchor.y;
			size.y = newHeight;
		}

		public static void AdjustHeight(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var newHeight = size.x / targetAspectRatio;
			min.y += size.y * sourceAnchor.y - newHeight * targetAnchor.y;
			size.y = newHeight;
		}

		#endregion

		#region AdjustAverage(...)

		public static Vector2 AdjustAverage(Vector2 size, float targetAspectRatio)
		{
			return new Vector2((size.x + size.y * targetAspectRatio) * 0.5f, (size.x / targetAspectRatio + size.y) * 0.5f);
		}

		public static void AdjustAverage(ref Vector2 min, ref Vector2 size, float targetAspectRatio)
		{
			var newWidth = size.y * targetAspectRatio;
			var newHeight = size.x / targetAspectRatio;

			min.x += (size.x - newWidth) * 0.25f;
			min.y += (size.y - newHeight) * 0.25f;

			size.x = (size.x + newWidth) * 0.5f;
			size.y = (size.y + newHeight) * 0.5f;
		}

		public static void AdjustAverage(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 anchor)
		{
			var newWidth = size.y * targetAspectRatio;
			var newHeight = size.x / targetAspectRatio;

			min.x += (size.x - newWidth) * anchor.x * 0.5f;
			min.y += (size.y - newHeight) * anchor.y * 0.5f;

			size.x = (size.x + newWidth) * 0.5f;
			size.y = (size.y + newHeight) * 0.5f;
		}

		public static void AdjustAverage(ref Vector2 min, ref Vector2 size, float targetAspectRatio, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			var newWidth = size.y * targetAspectRatio;
			var newHeight = size.x / targetAspectRatio;

			min.x += (size.x * sourceAnchor.x - newWidth * targetAnchor.x) * 0.5f;
			min.y += (size.y * sourceAnchor.y - newHeight * targetAnchor.y) * 0.5f;

			size.x = (size.x + newWidth) * 0.5f;
			size.y = (size.y + newHeight) * 0.5f;
		}

		#endregion

		#region AdjustProportionally(...)

		public static Vector2 AdjustProportionally(Vector2 size, float targetAspectRatio, float adjustWidthWeight)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;
			return new Vector2(
				size.x * adjustHeightWeight + size.y * targetAspectRatio * adjustWidthWeight,
				size.y * adjustWidthWeight + size.x / targetAspectRatio * adjustHeightWeight);
		}

		public static void AdjustProportionally(ref Vector2 min, ref Vector2 size, float targetAspectRatio, float adjustWidthWeight)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;

			var newWidth = size.y * targetAspectRatio;
			var newHeight = size.x / targetAspectRatio;

			min.x += (size.x - newWidth) * 0.5f * adjustWidthWeight;
			min.y += (size.y - newHeight) * 0.5f * adjustHeightWeight;

			size.x = size.x * adjustHeightWeight + newWidth * adjustWidthWeight;
			size.y = size.y * adjustWidthWeight + newHeight * adjustHeightWeight;
		}

		public static void AdjustProportionally(ref Vector2 min, ref Vector2 size, float targetAspectRatio, float adjustWidthWeight, Vector2 anchor)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;

			var newWidth = size.y * targetAspectRatio;
			var newHeight = size.x / targetAspectRatio;

			min.x += (size.x - newWidth) * anchor.x * adjustWidthWeight;
			min.y += (size.y - newHeight) * anchor.y * adjustHeightWeight;

			size.x = size.x * adjustHeightWeight + newWidth * adjustWidthWeight;
			size.y = size.y * adjustWidthWeight + newHeight * adjustHeightWeight;
		}

		public static void AdjustProportionally(ref Vector2 min, ref Vector2 size, float targetAspectRatio, float adjustWidthWeight, Vector2 sourceAnchor, Vector2 targetAnchor)
		{
			float adjustHeightWeight = 1f - adjustWidthWeight;

			var newWidth = size.y * targetAspectRatio;
			var newHeight = size.x / targetAspectRatio;

			min.x += (size.x * sourceAnchor.x - newWidth * targetAnchor.x) * adjustWidthWeight;
			min.y += (size.y * sourceAnchor.y - newHeight * targetAnchor.y) * adjustHeightWeight;

			size.x = size.x * adjustHeightWeight + newWidth * adjustWidthWeight;
			size.y = size.y * adjustWidthWeight + newHeight * adjustHeightWeight;
		}

		#endregion
	}
}
