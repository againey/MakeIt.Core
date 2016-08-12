/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous
{
	/// <summary>
	/// An integer-based three-dimensional index.
	/// </summary>
	[Serializable]
	public struct IntVector3 : IEquatable<IntVector3>, IComparable<IntVector3>
	{
		public int x;
		public int y;
		public int z;

		public IntVector3(int n)
		{
			x = n;
			y = n;
			z = n;
		}

		public IntVector3(int x, int y)
		{
			this.x = x;
			this.y = y;
			z = 0;
		}

		public IntVector3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public IntVector3 Offset(int dn)
		{
			return new IntVector3(x + dn, y + dn, z + dn);
		}

		public IntVector3 Offset(int dx, int dy)
		{
			return new IntVector3(x + dx, y + dy, z);
		}

		public IntVector3 Offset(int dx, int dy, int dz)
		{
			return new IntVector3(x + dx, y + dy, z + dz);
		}

		public static IntVector3 operator +(IntVector3 lhs, IntVector3 rhs) { return new IntVector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z); }
		public static IntVector3 operator -(IntVector3 v) { return new IntVector3(-v.x, -v.y, -v.z); }
		public static IntVector3 operator -(IntVector3 lhs, IntVector3 rhs) { return new IntVector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z); }
		public static IntVector3 operator *(int lhs, IntVector3 rhs) { return new IntVector3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z); }
		public static IntVector3 operator *(IntVector3 lhs, int rhs) { return new IntVector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs); }
		public static IntVector3 operator /(IntVector3 lhs, int rhs) { return new IntVector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs); }

		public void Scale(IntVector3 v) { x *= v.x; y *= v.y; z *= v.z; }
		public void Set(int x, int y, int z, int w) { this.x = x; this.y = y; this.z = z; }

		public int sqrMagnitude { get { return x * x + y * y + z * z; } }

		public static int Dot(IntVector3 lhs, IntVector3 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z; }
		public static IntVector3 Cross(IntVector3 lhs, IntVector3 rhs) { return new IntVector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x); }
		public static IntVector3 Max(IntVector3 lhs, IntVector3 rhs) { return new IntVector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z)); }
		public static IntVector3 Min(IntVector3 lhs, IntVector3 rhs) { return new IntVector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z)); }
		public static IntVector3 Scale(IntVector3 lhs, IntVector3 rhs) { return new IntVector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z); }
		public static int SqrMagnitude(IntVector3 v) { return v.x * v.x + v.y * v.y + v.z * v.z; }

		public int CompareTo(IntVector3 other)
		{
			if (z != other.z) return (z < other.z) ? -1 : +1;
			if (y != other.y) return (y < other.y) ? -1 : +1;
			if (x != other.x) return (x < other.x) ? -1 : +1;
			return 0;
		}

		public bool Equals(IntVector3 other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return obj is IntVector3 && this == (IntVector3)obj;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}

		public static bool operator ==(IntVector3 lhs, IntVector3 rhs) { return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z; }
		public static bool operator !=(IntVector3 lhs, IntVector3 rhs) { return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z; }
		public static bool operator < (IntVector3 lhs, IntVector3 rhs) { return lhs.z < rhs.z || lhs.z == rhs.z && (lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <  rhs.x); }
		public static bool operator <=(IntVector3 lhs, IntVector3 rhs) { return lhs.z < rhs.z || lhs.z == rhs.z && (lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <= rhs.x); }
		public static bool operator > (IntVector3 lhs, IntVector3 rhs) { return lhs.z > rhs.z || lhs.z == rhs.z && (lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >  rhs.x); }
		public static bool operator >=(IntVector3 lhs, IntVector3 rhs) { return lhs.z > rhs.z || lhs.z == rhs.z && (lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >= rhs.x); }

		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}]", x, y, z);
		}
	}
}
