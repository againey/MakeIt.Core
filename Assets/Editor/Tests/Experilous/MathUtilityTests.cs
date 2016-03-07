/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Tests
{
	public class MathUtilityTests
	{
		[Test]
		public void Log2Ceil32BitPowerOfTwo()
		{
			for (int i = 0; i < 32; ++i)
			{
				Assert.AreEqual(i, MathUtility.Log2Ceil(1U << i), string.Format("MathUtility.Log2Ceil(1U << {0}), MathUtility.Log2Ceil({1})", i, 1U << i));
			}
		}

		[Test]
		public void Log2Ceil64BitPowerOfTwo()
		{
			for (int i = 0; i < 64; ++i)
			{
				Assert.AreEqual(i, MathUtility.Log2Ceil(1UL << i), string.Format("MathUtility.Log2Ceil(1UL << {0}), MathUtility.Log2Ceil({1})", i, 1UL << i));
			}
		}

		[Test]
		public void Plus1Log2Ceil32BitPowerOfTwo()
		{
			for (int i = 0; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, MathUtility.Plus1Log2Ceil(1U << i), string.Format("MathUtility.Plus1Log2Ceil(1U << {0}), MathUtility.Plus1Log2Ceil({1})", i, 1U << i));
			}
		}

		[Test]
		public void Plus1Log2Ceil64BitPowerOfTwo()
		{
			for (int i = 0; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, MathUtility.Plus1Log2Ceil(1UL << i), string.Format("MathUtility.Plus1Log2Ceil(1UL << {0}), MathUtility.Plus1Log2Ceil({1})", i, 1UL << i));
			}
		}

		[Test]
		public void Log2Ceil32BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 32; ++i)
			{
				Assert.AreEqual(i, MathUtility.Log2Ceil((1U << i) - 1), string.Format("MathUtility.Log2Ceil((1U << {0}) - 1), MathUtility.Log2Ceil({1})", i, (1U << i) - 1));
			}
		}

		[Test]
		public void Log2Ceil64BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 64; ++i)
			{
				Assert.AreEqual(i, MathUtility.Log2Ceil((1UL << i) - 1), string.Format("MathUtility.Log2Ceil((1UL << {0}) - 1), MathUtility.Log2Ceil({1})", i, (1UL << i) - 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil32BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 32; ++i)
			{
				Assert.AreEqual(i, MathUtility.Plus1Log2Ceil((1U << i) - 1), string.Format("MathUtility.Plus1Log2Ceil((1U << {0}) - 1), MathUtility.Plus1Log2Ceil({1})", i, (1U << i) - 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil64BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 64; ++i)
			{
				Assert.AreEqual(i, MathUtility.Plus1Log2Ceil((1UL << i) - 1), string.Format("MathUtility.Plus1Log2Ceil((1UL << {0}) - 1), MathUtility.Plus1Log2Ceil({1})", i, (1UL << i) - 1));
			}
		}

		[Test]
		public void Log2Ceil32BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, MathUtility.Log2Ceil((1U << i) + 1), string.Format("MathUtility.Log2Ceil((1U << {0}) + 1), MathUtility.Log2Ceil({1})", i, (1U << i) + 1));
			}
		}

		[Test]
		public void Log2Ceil64BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, MathUtility.Log2Ceil((1UL << i) + 1), string.Format("MathUtility.Log2Ceil((1UL << {0}) + 1), MathUtility.Log2Ceil({1})", i, (1UL << i) + 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil32BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, MathUtility.Plus1Log2Ceil((1U << i) + 1), string.Format("MathUtility.Plus1Log2Ceil((1U << {0}) + 1), MathUtility.Plus1Log2Ceil({1})", i, (1U << i) + 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil64BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, MathUtility.Plus1Log2Ceil((1UL << i) + 1), string.Format("MathUtility.Plus1Log2Ceil((1UL << {0}) + 1), MathUtility.Plus1Log2Ceil({1})", i, (1UL << i) + 1));
			}
		}
	}
}
#endif
