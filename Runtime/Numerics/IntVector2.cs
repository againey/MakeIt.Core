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

using System;
using UnityEngine;

namespace MakeIt.Numerics
{
	/// <summary>
	/// An integer-based two-dimensional vector.
	/// </summary>
	[Serializable]
	public struct IntVector2 : IEquatable<IntVector2>, IComparable<IntVector2>
	{
		/// <summary>
		/// The first component of the vector.
		/// </summary>
		public int x;

		/// <summary>
		/// The second component of the vector.
		/// </summary>
		public int y;

		/// <summary>
		/// Access the components using an integer index.
		/// </summary>
		/// <param name="index">A value in the range [0, 1], mapping 0 to x, and 1 to y.</param>
		/// <returns>The value of the component corresponding to the provided index.</returns>
		public int this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return x;
					case 1: return y;
					default: throw new ArgumentOutOfRangeException("index", index, "The index must be in the range [0, 1].");
				}
			}
			set
			{
				switch (index)
				{
					case 0: x = value; break;
					case 1: y = value; break;
					default: throw new ArgumentOutOfRangeException("index", index, "The index must be in the range [0, 1].");
				}
			}
		}

		/// <summary>
		/// An instance of IntVector2 with both components set to zero.
		/// </summary>
		public static IntVector2 zero { get { return new IntVector2(); } }

		/// <summary>
		/// An instance of IntVector2 with both components set to one.
		/// </summary>
		public static IntVector2 one { get { return new IntVector2(1); } }

		/// <summary>
		/// An instance of IntVector2 with both components set to (+1, 0).
		/// </summary>
		public static IntVector2 right { get { return new IntVector2(+1, 0); } }

		/// <summary>
		/// An instance of IntVector2 with both components set to (-1, 0).
		/// </summary>
		public static IntVector2 left { get { return new IntVector2(-1, 0); } }

		/// <summary>
		/// An instance of IntVector2 with both components set to (0, +1).
		/// </summary>
		public static IntVector2 up { get { return new IntVector2(0, +1); } }

		/// <summary>
		/// An instance of IntVector2 with both components set to (0, -1).
		/// </summary>
		public static IntVector2 down { get { return new IntVector2(0, -1); } }

		/// <summary>
		/// Constructs an IntVector2 instance using the supplied value for both components.
		/// </summary>
		/// <param name="n">The value to be assigned to both components.</param>
		public IntVector2(int n)
		{
			x = n;
			y = n;
		}

		/// <summary>
		/// Constructs an IntVector2 instance using the supplied values for each component.
		/// </summary>
		/// <param name="x">The value to be assigned to the first component.</param>
		/// <param name="y">The value to be assigned to the second component.</param>
		public IntVector2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Convert an IntVector4 instance to an IntVector2 instance by copying the first two components and ignoring the third.
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted IntVector2.</returns>
		public static implicit operator IntVector2(IntVector3 v)
		{
			return new IntVector2(v.x, v.y);
		}

		/// <summary>
		/// Convert an IntVector4 instance to an IntVector2 instance by copying the first two components and ignoring the rest.
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted IntVector2.</returns>
		public static implicit operator IntVector2(IntVector4 v)
		{
			return new IntVector2(v.x, v.y);
		}

		/// <summary>
		/// Convert a Vector2 instance to an IntVector2 instance by copying the first two components (converting them from float to int in the process).
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted IntVector2.</returns>
		public static explicit operator IntVector2(Vector2 v)
		{
			return new IntVector2((int)v.x, (int)v.y);
		}

		/// <summary>
		/// Convert a Vector2 instance to an IntVector2 instance by copying the first two components (converting them from float to int in the process) and ignoring the third.
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted IntVector2.</returns>
		public static explicit operator IntVector2(Vector3 v)
		{
			return new IntVector2((int)v.x, (int)v.y);
		}

		/// <summary>
		/// Convert a Vector2 instance to an IntVector2 instance by copying the first two components (converting them from float to int in the process) and ignoring the rest.
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted IntVector2.</returns>
		public static explicit operator IntVector2(Vector4 v)
		{
			return new IntVector2((int)v.x, (int)v.y);
		}

		/// <summary>
		/// Convert an IntVector2 instance to a Vector2 instance by copying the first two components (converting them from int to float in the process).
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted Vector2.</returns>
		public static implicit operator Vector2(IntVector2 v)
		{
			return new Vector2(v.x, v.y);
		}

		/// <summary>
		/// Convert an IntVector2 instance to a Vector3 instance by copying the components (converting them from int to float in the process) and setting the third to zero.
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted Vector3.</returns>
		public static implicit operator Vector3(IntVector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		/// <summary>
		/// Convert an IntVector2 instance to a Vector2 instance by copying the first two components (converting them from int to float in the process) and setting the rest to zero.
		/// </summary>
		/// <param name="v">The vector to convert.</param>
		/// <returns>The converted Vector4.</returns>
		public static implicit operator Vector4(IntVector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		/// <summary>
		/// Returns the result of offsetting the current vector by a given value for both components.
		/// </summary>
		/// <param name="dn">The amount by which to offset both components.</param>
		/// <returns>The offset vector.</returns>
		public IntVector2 Offset(int dn)
		{
			return new IntVector2(x + dn, y + dn);
		}

		/// <summary>
		/// Returns the result of offsetting the current vector by given values for each component.
		/// </summary>
		/// <param name="dx">The amount by which to offset the first component.</param>
		/// <param name="dy">The amount by which to offset the second component.</param>
		/// <returns>The offset vector.</returns>
		public IntVector2 Offset(int dx, int dy)
		{
			return new IntVector2(x + dx, y + dy);
		}

		/// <summary>
		/// A no-op unary plus operation provided for symmetry with unary negation.
		/// </summary>
		/// <param name="v">The input vector.</param>
		/// <returns>A copy of the input vector.</returns>
		public static IntVector2 operator +(IntVector2 v) { return v; }

		/// <summary>
		/// Negates the vector by negating each of its components.
		/// </summary>
		/// <param name="v">The vector to be negated.</param>
		/// <returns>The negated vector.</returns>
		public static IntVector2 operator -(IntVector2 v) { return new IntVector2(-v.x, -v.y); }

		/// <summary>
		/// Adds the components of two vectors.
		/// </summary>
		/// <param name="lhs">The first vector to add.</param>
		/// <param name="rhs">The second vector to add.</param>
		/// <returns>The vector produced by a component-wise addition of the two input vectors.</returns>
		public static IntVector2 operator +(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(lhs.x + rhs.x, lhs.y + rhs.y); }

		/// <summary>
		/// Subtracts the components of two vectors.
		/// </summary>
		/// <param name="lhs">The vector from which the second vector will be subtracted.</param>
		/// <param name="rhs">The vector to be subtracted from the first vector.</param>
		/// <returns>The vector produced by a component-wise subtraction of the two input vectors.</returns>
		public static IntVector2 operator -(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(lhs.x - rhs.x, lhs.y - rhs.y); }

		/// <summary>
		/// Multiplies the components of a vector by a scalar value.
		/// </summary>
		/// <param name="lhs">The scalar value by which to multiply the vector components.</param>
		/// <param name="rhs">The vector whose elements are to be multiplied by the scalar value.</param>
		/// <returns>The vector produced multiplying the input vector's components by the provided scalar value.</returns>
		public static IntVector2 operator *(int lhs, IntVector2 rhs) { return new IntVector2(lhs * rhs.x, lhs * rhs.y); }

		/// <summary>
		/// Multiplies the components of a vector by a scalar value.
		/// </summary>
		/// <param name="lhs">The vector whose elements are to be multiplied by the scalar value.</param>
		/// <param name="rhs">The scalar value by which to multiply the vector components.</param>
		/// <returns>The vector produced multiplying the input vector's components by the provided scalar value.</returns>
		public static IntVector2 operator *(IntVector2 lhs, int rhs) { return new IntVector2(lhs.x * rhs, lhs.y * rhs); }

		/// <summary>
		/// Divides the components of a vector by a scalar value.
		/// </summary>
		/// <param name="lhs">The vector whose elements are to be divided by the scalar value.</param>
		/// <param name="rhs">The scalar value by which to divide the vector components.</param>
		/// <returns>The vector produced dividing the input vector's components by the provided scalar value.</returns>
		public static IntVector2 operator /(IntVector2 lhs, int rhs) { return new IntVector2(lhs.x / rhs, lhs.y / rhs); }

		/// <summary>
		/// Scales the current vector by multiplying its components by the components of the supplied vector.
		/// </summary>
		/// <param name="v">The vector whose components are used to scale the current vector.</param>
		public void Scale(IntVector2 v) { x *= v.x; y *= v.y; }

		/// <summary>
		/// Sets the component values of the current vector using the provided parameter values.
		/// </summary>
		/// <param name="x">The value to assign to the first component.</param>
		/// <param name="y">The value to assign to the second component.</param>
		public void Set(int x, int y) { this.x = x; this.y = y; }

		/// <summary>
		/// The length of the vector squared, calculated by adding the square of each of the components.
		/// </summary>
		public int sqrMagnitude { get { return x * x + y * y; } }

		/// <summary>
		/// Calculates the dot product of two vectors.
		/// </summary>
		/// <param name="lhs">The first vector of the dot product.</param>
		/// <param name="rhs">The second vector of the dot product.</param>
		/// <returns>The dot product of the supplied vectors.</returns>
		public static int Dot(IntVector2 lhs, IntVector2 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }

		/// <summary>
		/// Returns the component-wise maximum of two vectors.
		/// </summary>
		/// <param name="lhs">The first vector.</param>
		/// <param name="rhs">The second vector.</param>
		/// <returns>The vector whose components are each set to the maximum value of that component in each of the input vectors.</returns>
		public static IntVector2 Max(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y)); }

		/// <summary>
		/// Returns the component-wise minimum of two vectors.
		/// </summary>
		/// <param name="lhs">The first vector.</param>
		/// <param name="rhs">The second vector.</param>
		/// <returns>The vector whose components are each set to the minimum value of that component in each of the input vectors.</returns>
		public static IntVector2 Min(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y)); }

		/// <summary>
		/// Scales one vector by multiplying its components by the components of a second vector.
		/// </summary>
		/// <param name="lhs">The first vector to be scaled by the second.</param>
		/// <param name="rhs">The second vector whose components are used to scale the current vector.</param>
		/// <returns>The vector produced by scaling the components of the first vector by those of the second vector.</returns>
		public static IntVector2 Scale(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(lhs.x * rhs.x, lhs.y * rhs.y); }

		/// <summary>
		/// Calculates the squared length of the supplied vector, determined by adding the square of each of the components.
		/// </summary>
		/// <param name="v">The vector whose squared length is to be calculated.</param>
		/// <returns>The length of the vector squared.</returns>
		public static int SqrMagnitude(IntVector2 v) { return v.x * v.x + v.y * v.y; }

		/// <summary>
		/// Compares the current vector to the supplied vector, using a lexicographical ordering.
		/// </summary>
		/// <param name="other">The vector to be compared to the current vector.</param>
		/// <returns>Returns -1 if the current vector comes before the supplied vector in the lexicographical ordering,
		/// +1 if it comes after the supplied vector, and 0 if they are equal.</returns>
		/// <remarks><para>The lexicographical ordering proceeds starting with the later components and moving toward
		/// the earlier components last, such that (2, 1) compares less than (1, 2).  Component-wise comparisons use
		/// ordinary integer comparison.  This results in a row-major type of ordering.</para></remarks>
		public int CompareTo(IntVector2 other)
		{
			if (y != other.y) return (y < other.y) ? -1 : +1;
			if (x != other.x) return (x < other.x) ? -1 : +1;
			return 0;
		}

		/// <summary>
		/// Compares the current vector to the supplied vector to find if they are equal.
		/// </summary>
		/// <param name="other">The vector to be compared to the current vector.</param>
		/// <returns>Returns true if the two vectors are equal, and false otherwise.</returns>
		public bool Equals(IntVector2 other)
		{
			return this == other;
		}

		/// <summary>
		/// Compares the current vector to the supplied object to find if they are equal.
		/// </summary>
		/// <param name="obj">The object to be compared to the current vector.</param>
		/// <returns>Returns true if the supplied object is an instance of IntVector2 and equal to the current vector, and false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return obj is IntVector2 && this == (IntVector2)obj;
		}

		/// <summary>
		/// Calculates a 32-bit integer hash code for the current vector.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code based on the component values of the current vector.</returns>
		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		/// <summary>
		/// Compares two vectors for equality.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>Returns true if the two vectors are equal, and false otherwise.</returns>
		public static bool operator ==(IntVector2 lhs, IntVector2 rhs) { return lhs.x == rhs.x && lhs.y == rhs.y; }

		/// <summary>
		/// Compares two vectors for inequality.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>Returns true if the two vectors are not equal, and false otherwise.</returns>
		public static bool operator !=(IntVector2 lhs, IntVector2 rhs) { return lhs.x != rhs.x || lhs.y != rhs.y; }

		/// <summary>
		/// Compares two vectors to find if the first is lexicographically less than the second vector.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>True if the first vector is lexicographically less than the second vector, and false otherwise.</returns>
		/// <seealso cref="CompareTo(IntVector2)"/>
		public static bool operator < (IntVector2 lhs, IntVector2 rhs) { return lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <  rhs.x; }

		/// <summary>
		/// Compares two vectors to find if the first is lexicographically less than or equal to the second vector.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>True if the first vector is lexicographically less than or equal to the second vector, and false otherwise.</returns>
		/// <seealso cref="CompareTo(IntVector2)"/>
		public static bool operator <=(IntVector2 lhs, IntVector2 rhs) { return lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <= rhs.x; }

		/// <summary>
		/// Compares two vectors to find if the first is lexicographically greater than the second vector.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>True if the first vector is lexicographically greater than the second vector, and false otherwise.</returns>
		/// <seealso cref="CompareTo(IntVector2)"/>
		public static bool operator > (IntVector2 lhs, IntVector2 rhs) { return lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >  rhs.x; }

		/// <summary>
		/// Compares two vectors to find if the first is lexicographically greater than or equal to the second vector.
		/// </summary>
		/// <param name="lhs">The first vector to compare.</param>
		/// <param name="rhs">The second vector to compare.</param>
		/// <returns>True if the first vector is lexicographically greater than or equal to the second vector, and false otherwise.</returns>
		/// <seealso cref="CompareTo(IntVector2)"/>
		public static bool operator >=(IntVector2 lhs, IntVector2 rhs) { return lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >= rhs.x; }

		/// <summary>
		/// Converts the current vector to a string representation, appropriate for diagnositic display.
		/// </summary>
		/// <returns>A string representation of the vector using default formatting.</returns>
		public override string ToString()
		{
			return string.Format("[{0}, {1}]", x, y);
		}

		/// <summary>
		/// Converts the current vector to a string representation, appropriate for diagnositic display.
		/// </summary>
		/// <param name="format">The numeric format string to be used for each component.  Accepts the same values that can be passed to <see cref="System.Int32.ToString(string)"/>.</param>
		/// <returns>A string representation of the vector using the specified formatting.</returns>
		public string ToString(string format)
		{
			return string.Format("[{0}, {1}]", x.ToString(format), y.ToString(format));
		}
	}
}
