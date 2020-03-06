/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

namespace MakeIt.Numerics
{
	/// <summary>
	/// A general mathematics utility class.
	/// </summary>
	public static class Math
	{
		#region Arithmetic

		/// <summary>
		/// An integer division remainder function which also handles negative numbers in a way that is ideal for working with wrapping ranges.
		/// </summary>
		/// <param name="index">The index within the range from 0 up to but not including <paramref name="range"/>, or within any positive or negative repetition of that range.</param>
		/// <param name="range">The size of the range.</param>
		/// <returns>The index that is strictly greater than or equal to 0 and less than <paramref name="range"/>, as if the input <paramref name="index"/> had been shifted by <paramref name="range"/> the necessary number of times to fall within that range.</returns>
		public static int Modulo(int index, int range)
		{
			var remainder = index % range;
			return (remainder >= 0) ? remainder : remainder + range;
		}

		/// <summary>
		/// Quick test to determine if the input integer is even.
		/// </summary>
		/// <param name="n">The integer to test.</param>
		/// <returns>True if <paramref name="n"/> is even, false if it is odd.</returns>
		/// <remarks>Assumes that signed integers are represented using two's complement.</remarks>
		public static bool IsEven(int n)
		{
			return (n & 1) == 0;
		}

		/// <summary>
		/// Quick test to determine if the input integer is odd.
		/// </summary>
		/// <param name="n">The integer to test.</param>
		/// <returns>True if <paramref name="n"/> is odd, false if it is even.</returns>
		/// <remarks>Assumes that signed integers are represented using two's complement.</remarks>
		public static bool IsOdd(int n)
		{
			return (n & 1) == 1;
		}

		/// <summary>
		/// Quick test to determine if the two input integers both have the same sign.  Zero is considered positive.
		/// </summary>
		/// <param name="a">The integer to compare.</param>
		/// <param name="b">The integer to compare.</param>
		/// <returns>True if both are non-negative or if both are negative, false if one is negative while the other is not.</returns>
		/// <remarks>Assumes that signed integers are represented using two's complement.</remarks>
		public static bool HaveSameSign(int a, int b)
		{
			return (a ^ b) >= 0;
		}

		/// <summary>
		/// Performs a linear interpolation from <paramref name="a"/> to <paramref name="b"/> using the interpolation parameter <paramref name="t"/>, which does not get clamped to the range [0, 1] first.
		/// </summary>
		/// <param name="a">The starting value of the interpolation, when <paramref name="t"/> is zero.</param>
		/// <param name="b">The ending value of the interpolation, when <paramref name="t"/> is one.</param>
		/// <param name="t">The position at which the interpolation is done, determining the weighting applied to <paramref name="a"/> and <paramref name="b"/>.  Typically in the range [0, 1], but all values are valid.</param>
		/// <returns>The result of linearly interpolating from <paramref name="a"/> to <paramref name="b"/> at position <paramref name="t"/>.</returns>
		/// <remarks><para>This functionality was added to the <see cref="UnityEngine.Mathf"/> class in Unity 5.2, and is only included
		/// in this class for use in earlier versions of Unity which do not have it built in to the Unity library.</para></remarks>
		public static float LerpUnclamped(float a, float b, float t)
		{
			return (b - a) * t + a;
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
