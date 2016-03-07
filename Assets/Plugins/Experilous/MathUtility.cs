/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

namespace Experilous
{
	public static class MathUtility
	{
		#region Arithmetic

		public static int Modulo(int index, int range)
		{
			var remainder = index % range;
			return (remainder >= 0) ? remainder : remainder + range;
		}

		public static bool IsEven(int n)
		{
			return (n & 1) == 0;
		}

		public static bool IsOdd(int n)
		{
			return (n & 1) == 1;
		}

		public static bool HaveSameSign(int a, int b)
		{
			return (a ^ b) >= 0;
		}

		#endregion

		#region Integer Base-2 Logarithms

		private static sbyte[] _log2CeilLookupTable = // Table[i] = Ceil(Log2(i+1))
		{
			0, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
		};

		/// <summary>
		/// Calculates the integer base-2 logarithm of a supplied integer.
		/// </summary>
		/// <param name="n">The integer whose base-2 logarithm is to be calculated.</param>
		/// <returns>The base-2 logarithm, rounded up to the nearest integer, of <paramref name="n"/>.</returns>
		/// <remarks>
		/// <para>This is particularly useful for determining the number of bits necessary for storing any integer
		/// in the range greater than or equal to 0 and less than <paramref name="n"/>.</para>
		/// </remarks>
		public static int Log2Ceil(uint n)
		{
			return Plus1Log2Ceil(n - 1);
		}

		/// <summary>
		/// Calculates the integer base-2 logarithm of one greater than a supplied integer.
		/// </summary>
		/// <param name="n">The integer whose base-2 logarithm is to be calculated.</param>
		/// <returns>The base-2 logarithm, rounded up to the nearest integer, of <paramref name="n"/>.</returns>
		/// <remarks>
		/// <para>This is particularly useful for determining the number of bits necessary for storing any integer
		/// in the range greater than or equal to 0 and less than or equal to <paramref name="n"/>.</para>
		/// </remarks>
		public static int Plus1Log2Ceil(uint n)
		{
			var high16 = n >> 16;
			if (high16 != 0U)
			{
				var high8 = high16 >> 8;
				return (high8 != 0U) ? 24 + _log2CeilLookupTable[high8] : 16 + _log2CeilLookupTable[high16];
			}
			else
			{
				var high8 = n >> 8;
				return (high8 != 0U) ? 8 + _log2CeilLookupTable[high8] : _log2CeilLookupTable[n];
			}
		}

		/// <summary>
		/// Calculates the integer base-2 logarithm of a supplied integer.
		/// </summary>
		/// <param name="n">The integer whose base-2 logarithm is to be calculated.</param>
		/// <returns>The base-2 logarithm, rounded up to the nearest integer, of <paramref name="n"/>.</returns>
		/// <remarks>
		/// <para>This is particularly useful for determining the number of bits necessary for storing any integer
		/// in the range greater than or equal to 0 and less than <paramref name="n"/>.</para>
		/// </remarks>
		public static int Log2Ceil(ulong n)
		{
			return Plus1Log2Ceil(n - 1);
		}

		/// <summary>
		/// Calculates the integer base-2 logarithm of a supplied integer.
		/// </summary>
		/// <param name="n">The integer whose base-2 logarithm is to be calculated.</param>
		/// <returns>The base-2 logarithm, rounded up to the nearest integer, of <paramref name="n"/>.</returns>
		/// <remarks>
		/// <para>This is particularly useful for determining the number of bits necessary for storing any integer
		/// in the range greater than or equal to 0 and less than or equal to <paramref name="n"/>.</para>
		/// </remarks>
		public static int Plus1Log2Ceil(ulong n)
		{
			var high32 = n >> 32;
			if (high32 != 0UL)
			{
				return 32 + Plus1Log2Ceil((uint)high32);
			}
			else
			{
				return Plus1Log2Ceil((uint)n);
			}
		}

		#endregion
	}
}
