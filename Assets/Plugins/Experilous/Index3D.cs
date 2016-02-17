using System;

namespace Experilous
{
	[Serializable]
	public struct Index3D
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

		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}]", x, y, z);
		}
	}
}
