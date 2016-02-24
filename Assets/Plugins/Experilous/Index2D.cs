/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

using System;

namespace Experilous
{
	/// <summary>
	/// An integer-based two-dimensional index.
	/// </summary>
	[Serializable]
	public struct Index2D : IEquatable<Index2D>, IComparable<Index2D>
	{
		public int x;
		public int y;

		public Index2D(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Index2D Offset(int dx, int dy)
		{
			return new Index2D(x + dx, y + dy);
		}

		public int CompareTo(Index2D other)
		{
			if (y != other.y) return (y < other.y) ? -1 : +1;
			if (x != other.x) return (x < other.x) ? -1 : +1;
			return 0;
		}

		public bool Equals(Index2D other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return obj is Index2D && this == (Index2D)obj;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public static bool operator ==(Index2D lhs, Index2D rhs) { return lhs.x == rhs.x && lhs.y == rhs.y; }
		public static bool operator !=(Index2D lhs, Index2D rhs) { return lhs.x != rhs.x || lhs.y != rhs.y; }
		public static bool operator < (Index2D lhs, Index2D rhs) { return lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <  rhs.x; }
		public static bool operator <=(Index2D lhs, Index2D rhs) { return lhs.y < rhs.y || lhs.y == rhs.y && lhs.x <= rhs.x; }
		public static bool operator > (Index2D lhs, Index2D rhs) { return lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >  rhs.x; }
		public static bool operator >=(Index2D lhs, Index2D rhs) { return lhs.y > rhs.y || lhs.y == rhs.y && lhs.x >= rhs.x; }

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", x, y);
		}
	}
}
