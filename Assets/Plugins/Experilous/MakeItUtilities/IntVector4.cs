/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous
{
	/// <summary>
	/// An integer-based four-dimensional index.
	/// </summary>
	[Serializable]
	public struct IntVector4 : IEquatable<IntVector4>, IComparable<IntVector4>
	{
		public int x;
		public int y;
		public int z;
		public int w;

		public IntVector4(int n)
		{
			x = n;
			y = n;
			z = n;
			w = n;
		}

		public IntVector4(int x, int y)
		{
			this.x = x;
			this.y = y;
			z = 0;
			w = 0;
		}

		public IntVector4(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			w = 0;
		}

		public IntVector4(int x, int y, int z, int w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public IntVector4 Offset(int dn)
		{
			return new IntVector4(x + dn, y + dn, z + dn, w + dn);
		}

		public IntVector4 Offset(int dx, int dy)
		{
			return new IntVector4(x + dx, y + dy, z, w);
		}

		public IntVector4 Offset(int dx, int dy, int dz)
		{
			return new IntVector4(x + dx, y + dy, z + dz, w);
		}

		public IntVector4 Offset(int dx, int dy, int dz, int dw)
		{
			return new IntVector4(x + dx, y + dy, z + dz, w + dw);
		}

		public static IntVector4 operator +(IntVector4 lhs, IntVector4 rhs) { return new IntVector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w); }
		public static IntVector4 operator -(IntVector4 v) { return new IntVector4(-v.x, -v.y, -v.z, -v.w); }
		public static IntVector4 operator -(IntVector4 lhs, IntVector4 rhs) { return new IntVector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w); }
		public static IntVector4 operator *(int lhs, IntVector4 rhs) { return new IntVector4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w); }
		public static IntVector4 operator *(IntVector4 lhs, int rhs) { return new IntVector4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs); }
		public static IntVector4 operator /(IntVector4 lhs, int rhs) { return new IntVector4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs); }

		public void Scale(IntVector4 v) { x *= v.x; y *= v.y; z *= v.z; w *= v.w; }
		public void Set(int x, int y, int z, int w) { this.x = x; this.y = y; this.z = z; this.w = w; }

		public int sqrMagnitude { get { return x * x + y * y + z * z + w * w; } }

		public static int Dot(IntVector4 lhs, IntVector4 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w; }
		public static IntVector4 Max(IntVector4 lhs, IntVector4 rhs) { return new IntVector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w)); }
		public static IntVector4 Min(IntVector4 lhs, IntVector4 rhs) { return new IntVector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w)); }
		public static IntVector4 Scale(IntVector4 lhs, IntVector4 rhs) { return new IntVector4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w); }
		public static int SqrMagnitude(IntVector4 v) { return v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w; }

		public int CompareTo(IntVector4 other)
		{
			if (w != other.w) return (w < other.w) ? -1 : +1;
			if (z != other.z) return (z < other.z) ? -1 : +1;
			if (y != other.y) return (y < other.y) ? -1 : +1;
			if (x != other.x) return (x < other.x) ? -1 : +1;
			return 0;
		}

		public bool Equals(IntVector4 other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return obj is IntVector4 && this == (IntVector4)obj;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}

		public static bool operator ==(IntVector4 lhs, IntVector4 rhs) { return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w; }
		public static bool operator !=(IntVector4 lhs, IntVector4 rhs) { return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w; }
		public static bool operator < (IntVector4 lhs, IntVector4 rhs) { return lhs.w < rhs.w || lhs.w == rhs.w && (lhs.z < rhs.z || lhs.z == rhs.z && (lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <  rhs.x)); }
		public static bool operator <=(IntVector4 lhs, IntVector4 rhs) { return lhs.w < rhs.w || lhs.w == rhs.w && (lhs.z < rhs.z || lhs.z == rhs.z && (lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <= rhs.x)); }
		public static bool operator > (IntVector4 lhs, IntVector4 rhs) { return lhs.w < rhs.w || lhs.w == rhs.w && (lhs.z > rhs.z || lhs.z == rhs.z && (lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >  rhs.x)); }
		public static bool operator >=(IntVector4 lhs, IntVector4 rhs) { return lhs.w < rhs.w || lhs.w == rhs.w && (lhs.z > rhs.z || lhs.z == rhs.z && (lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >= rhs.x)); }

		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
		}
	}
}
