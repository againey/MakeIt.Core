/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous.Numerics
{
	/// <summary>
	/// An integer-based two-dimensional index.
	/// </summary>
	[Serializable]
	public struct IntVector2 : IEquatable<IntVector2>, IComparable<IntVector2>
	{
		public int x;
		public int y;

		public IntVector2(int n)
		{
			x = n;
			y = n;
		}

		public IntVector2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public IntVector2 Offset(int dn)
		{
			return new IntVector2(x + dn, y + dn);
		}

		public IntVector2 Offset(int dx, int dy)
		{
			return new IntVector2(x + dx, y + dy);
		}

		public static IntVector2 operator +(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(lhs.x + rhs.x, lhs.y + rhs.y); }
		public static IntVector2 operator -(IntVector2 v) { return new IntVector2(-v.x, -v.y); }
		public static IntVector2 operator -(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(lhs.x - rhs.x, lhs.y - rhs.y); }
		public static IntVector2 operator *(int lhs, IntVector2 rhs) { return new IntVector2(lhs * rhs.x, lhs * rhs.y); }
		public static IntVector2 operator *(IntVector2 lhs, int rhs) { return new IntVector2(lhs.x * rhs, lhs.y * rhs); }
		public static IntVector2 operator /(IntVector2 lhs, int rhs) { return new IntVector2(lhs.x / rhs, lhs.y / rhs); }

		public void Scale(IntVector2 v) { x *= v.x; y *= v.y; }
		public void Set(int x, int y, int z, int w) { this.x = x; this.y = y; }

		public int sqrMagnitude { get { return x * x + y * y; } }

		public static int Dot(IntVector2 lhs, IntVector2 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }
		public static IntVector2 Max(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y)); }
		public static IntVector2 Min(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y)); }
		public static IntVector2 Scale(IntVector2 lhs, IntVector2 rhs) { return new IntVector2(lhs.x * rhs.x, lhs.y * rhs.y); }
		public static int SqrMagnitude(IntVector2 v) { return v.x * v.x + v.y * v.y; }

		public int CompareTo(IntVector2 other)
		{
			if (y != other.y) return (y < other.y) ? -1 : +1;
			if (x != other.x) return (x < other.x) ? -1 : +1;
			return 0;
		}

		public bool Equals(IntVector2 other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return obj is IntVector2 && this == (IntVector2)obj;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public static bool operator ==(IntVector2 lhs, IntVector2 rhs) { return lhs.x == rhs.x && lhs.y == rhs.y; }
		public static bool operator !=(IntVector2 lhs, IntVector2 rhs) { return lhs.x != rhs.x || lhs.y != rhs.y; }
		public static bool operator < (IntVector2 lhs, IntVector2 rhs) { return lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <  rhs.x; }
		public static bool operator <=(IntVector2 lhs, IntVector2 rhs) { return lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <= rhs.x; }
		public static bool operator > (IntVector2 lhs, IntVector2 rhs) { return lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >  rhs.x; }
		public static bool operator >=(IntVector2 lhs, IntVector2 rhs) { return lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >= rhs.x; }

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", x, y);
		}
	}
}
