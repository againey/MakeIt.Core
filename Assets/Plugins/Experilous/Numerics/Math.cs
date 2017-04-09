/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Numerics
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

		public static float ZeroExclusiveSign(float n)
		{
			if (float.IsNaN(n)) return float.NaN;
			return System.BitConverter.DoubleToInt64Bits(n) < 0f ? -1f : +1f;
		}

		public static float ZeroInclusiveSign(float n)
		{
			if (float.IsNaN(n)) return float.NaN;
			if (n == 0f) return 0f;
			return n < 0f ? -1f : +1f;
		}

		public static float ApproximateSign(float n, float epsilon = 0.0001f)
		{
			if (float.IsNaN(n)) return float.NaN;
			if (n > -epsilon && n < epsilon) return 0f;
			return n < 0f ? -1f : +1f;
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

		#region Quadratic Equation

		public static int SolveQuadratic(float a, float b, float c, out float t0, out float t1, float epsilon = 0.0001f)
		{
			// Conditionally uses the quadratic formula or the citardauq formula to minimize precision loss.
			if (a != 0f)
			{
				var sqr = b * b - 4f * a * c;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Make sure the smaller t value is stored in t0, the larger in t1.
					if (b >= 0f)
					{
						t0 = -0.5f * (b + sqrt) / a; // Quadratic:  (-b - sqrt(b^2 + 4ac)) / 2a
						t1 = -2f * c / (b + sqrt); // Citardauq:  2c / (-b - sqrt(b^2 + 4ac))
						return 2;
					}
					else
					{
						t0 = -2f * c / (b - sqrt); // Citardauq:  2c / (-b + sqrt(b^2 + 4ac))
						t1 = -0.5f * (b - sqrt) / a; // Quadratic:  (-b + sqrt(b^2 + 4ac)) / 2a
						return 2;
					}
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Make sure the larger value is in the denominator.
					if (Mathf.Abs(a) > Mathf.Abs(b))
					{
						t0 = t1 = -0.5f * b / a; // Quadratic:  (-b - 0) / 2a
						return 1;
					}
					else
					{
						t0 = t1 = -2f * c / b; // Citardauq:  2c / (-b - 0)
						return 1;
					}
				}
				// If the square is definitely negative, there is no solution.
				else
				{
					t0 = t1 = float.NaN;
					return 0;
				}
			}
			// This is actually at most a linear equation, no need to mess with square roots.
			else
			{
				// If the linear component is not zero, then it must have exactly one root.
				if (b != 0f)
				{
					t0 = t1 = -c / b;
					return 1;
				}
				// Else, it is a constant function; if it is a non-zero constant, it has no root.
				else if (c != 0f)
				{
					t0 = t1 = float.NaN;
					return 0;
				}
				// Else, it has infinite roots.
				else
				{
					t0 = float.NegativeInfinity;
					t1 = float.PositiveInfinity;
					return 0; // Return 0 as the number of roots anyway.  If the caller cares about this case, t0 and t1 can be checked.
				}
			}
		}

		public static float SolveQuadraticNegRoot(float a, float b, float c, float epsilon = 0.0001f)
		{
			// Conditionally uses the quadratic formula or the citardauq formula to minimize precision loss.
			if (a != 0f)
			{
				var sqr = b * b - 4f * a * c;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Make sure the smaller t value is stored in t0, the larger in t1.
					if (b >= 0f)
					{
						return -0.5f * (b + sqrt) / a; // Quadratic:  (-b - sqrt(b^2 + 4ac)) / 2a
					}
					else
					{
						return -2f * c / (b - sqrt); // Citardauq:  2c / (-b + sqrt(b^2 + 4ac))
					}
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Make sure the larger value is in the denominator.
					if (Mathf.Abs(a) > Mathf.Abs(b))
					{
						return -0.5f * b / a; // Quadratic:  (-b - 0) / 2a
					}
					else
					{
						return -2f * c / b; // Citardauq:  2c / (-b - 0)
					}
				}
				// If the square is definitely negative, there is no solution.
				else
				{
					return float.NaN;
				}
			}
			// This is actually at most a linear equation, no need to mess with square roots.
			else
			{
				// If the linear component is not zero, then it must have exactly one root.
				if (b != 0f)
				{
					return -c / b;
				}
				// Else, it is a constant function; if it is a non-zero constant, it has no root.
				else if (c != 0f)
				{
					return float.NaN;
				}
				// Else, it has infinite roots.
				else
				{
					return float.NaN;
				}
			}
		}

		public static float SolveQuadraticPosRoot(float a, float b, float c, float epsilon = 0.0001f)
		{
			// Conditionally uses the quadratic formula or the citardauq formula to minimize precision loss.
			if (a != 0f)
			{
				var sqr = b * b - 4f * a * c;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Make sure the smaller t value is stored in t0, the larger in t1.
					if (b >= 0f)
					{
						return -2f * c / (b + sqrt); // Citardauq:  2c / (-b - sqrt(b^2 + 4ac))
					}
					else
					{
						return -0.5f * (b - sqrt) / a; // Quadratic:  (-b + sqrt(b^2 + 4ac)) / 2a
					}
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Make sure the larger value is in the denominator.
					if (Mathf.Abs(a) > Mathf.Abs(b))
					{
						return -0.5f * b / a; // Quadratic:  (-b - 0) / 2a
					}
					else
					{
						return -2f * c / b; // Citardauq:  2c / (-b - 0)
					}
				}
				// If the square is definitely negative, there is no solution.
				else
				{
					return float.NaN;
				}
			}
			// This is actually at most a linear equation, no need to mess with square roots.
			else
			{
				// If the linear component is not zero, then it must have exactly one root.
				if (b != 0f)
				{
					return -c / b;
				}
				// Else, it is a constant function; if it is a non-zero constant, it has no root.
				else if (c != 0f)
				{
					return float.NaN;
				}
				// Else, it has infinite roots.
				else
				{
					return float.NaN;
				}
			}
		}

		#endregion

		#region Quartic Equation

		public static int SolveQuartic(float k4, float k3, float k2, float k1, float k0, out float t0, out float t1, out float t2, out float t3, float epsilon = 0.0001f, int maxIterations = 1000)
		{
			// Solve the quartic k4*t^4 + k3*t^3 + k2*t^2 + k1*t + k0 = 0
			float u = k3 / k4;
			float v = k2 / k4;

			float delta = float.PositiveInfinity;
			int iterations = 0;
			while (delta > epsilon && iterations < maxIterations)
			{
				float n0 = k3 - u * k4;
				float n1 = k2 - u * n0 - v * k4;
				float n2 = k1 - u * n1 - v * n0;
				float n3 = k0 - v * n1;
				float n4 = n0 - u * k4;
				float n5 = n1 - v * k4;

				float s = v * n4 * n4 + n5 * (n5 - u * n4);
				float du = (n4 * n3 - n5 * n2) / s;
				float dv = ((u * n4 - n5) * n3 - v * n4 * n2) / s;
				u -= du;
				v -= dv;
				delta = Mathf.Sqrt((du * du) / (u * u + 1f) + (dv * dv) / (v * v + 1f));
				
				++iterations;
			}

			if (delta <= epsilon)
			{
				float n0 = k3 - u * k4;
				float n1 = k2 - u * n0 - v * k4;

				int solutionCount = SolveQuadratic(1f, u, v, out t0, out t3, epsilon);

				if (k4 >= 0f)
				{
					solutionCount += SolveQuadratic(k4, n0, n1, out t1, out t2, epsilon);
				}
				else
				{
					solutionCount += SolveQuadratic(k4, n0, n1, out t2, out t1, epsilon);
				}

				if (Mathf.Abs(t0 - t1) <= epsilon * 2f)
				{
					t0 = t1 = (t0 + t1) / 2f;
					solutionCount -= 1;
				}

				if (Mathf.Abs(t2 - t3) <= epsilon * 2f)
				{
					t2 = t3 = (t2 + t3) / 2f;
					solutionCount -= 1;
				}

				return solutionCount;
			}
			else
			{
				t0 = t1 = t2 = t3 = float.NaN;
				return 0;
			}
		}

		#endregion
	}
}
