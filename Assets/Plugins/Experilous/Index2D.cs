using System;

namespace Experilous
{
	/// <summary>
	/// An integer-based two-dimensional index.
	/// </summary>
	[Serializable]
	public struct Index2D
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

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", x, y);
		}
	}
}
