using System;

namespace Experilous
{
	/// <summary>
	/// An integer-based three-dimensional index.
	/// </summary>
	[Serializable]
	public struct Index3D : IEquatable<Index3D>, IComparable<Index3D>
	{
		public int x;
		public int y;
		public int z;

		public Index3D(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Index3D Offset(int dx, int dy, int dz)
		{
			return new Index3D(x + dx, y + dy, z + dz);
		}

		public int CompareTo(Index3D other)
		{
			if (z != other.z) return (z < other.z) ? -1 : +1;
			if (y != other.y) return (y < other.y) ? -1 : +1;
			if (x != other.x) return (x < other.x) ? -1 : +1;
			return 0;
		}

		public bool Equals(Index3D other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return obj is Index3D && this == (Index3D)obj;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}

		public static bool operator ==(Index3D lhs, Index3D rhs) { return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z; }
		public static bool operator !=(Index3D lhs, Index3D rhs) { return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z; }
		public static bool operator < (Index3D lhs, Index3D rhs) { return lhs.z < rhs.z || lhs.z == rhs.z && (lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <  rhs.x); }
		public static bool operator <=(Index3D lhs, Index3D rhs) { return lhs.z < rhs.z || lhs.z == rhs.z && (lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <= rhs.x); }
		public static bool operator > (Index3D lhs, Index3D rhs) { return lhs.z > rhs.z || lhs.z == rhs.z && (lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >  rhs.x); }
		public static bool operator >=(Index3D lhs, Index3D rhs) { return lhs.z > rhs.z || lhs.z == rhs.z && (lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >= rhs.x); }

		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}]", x, y, z);
		}
	}
}
