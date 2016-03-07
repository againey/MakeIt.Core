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
		#region Arithmetic Tests

		[Test]
		public void ModuloTest()
		{
			Assert.AreEqual(0, MathUtility.Modulo(0, 1));
			Assert.AreEqual(0, MathUtility.Modulo(1, 1));
			Assert.AreEqual(0, MathUtility.Modulo(-1, 1));
			Assert.AreEqual(0, MathUtility.Modulo(2, 1));
			Assert.AreEqual(0, MathUtility.Modulo(-2, 1));
			Assert.AreEqual(0, MathUtility.Modulo(int.MaxValue, 1));
			Assert.AreEqual(0, MathUtility.Modulo(int.MinValue, 1));
			Assert.AreEqual(0, MathUtility.Modulo(0, 16));
			Assert.AreEqual(0, MathUtility.Modulo(16, 16));
			Assert.AreEqual(0, MathUtility.Modulo(-16, 16));
			Assert.AreEqual(0, MathUtility.Modulo(32, 16));
			Assert.AreEqual(0, MathUtility.Modulo(-32, 16));
			Assert.AreEqual(0, MathUtility.Modulo(int.MaxValue / 16 * 16, 16));
			Assert.AreEqual(0, MathUtility.Modulo(int.MinValue / 16 * 16, 16));
			Assert.AreEqual(0, MathUtility.Modulo(0, 23));
			Assert.AreEqual(0, MathUtility.Modulo(23, 23));
			Assert.AreEqual(0, MathUtility.Modulo(-23, 23));
			Assert.AreEqual(0, MathUtility.Modulo(46, 23));
			Assert.AreEqual(0, MathUtility.Modulo(-46, 23));
			Assert.AreEqual(0, MathUtility.Modulo(int.MaxValue / 23 * 23, 23));
			Assert.AreEqual(0, MathUtility.Modulo(int.MinValue / 23 * 23, 23));

			Assert.AreEqual(1, MathUtility.Modulo(1, 16));
			Assert.AreEqual(1, MathUtility.Modulo(17, 16));
			Assert.AreEqual(1, MathUtility.Modulo(-15, 16));
			Assert.AreEqual(1, MathUtility.Modulo(33, 16));
			Assert.AreEqual(1, MathUtility.Modulo(-31, 16));
			Assert.AreEqual(1, MathUtility.Modulo(int.MaxValue / 16 * 16 - 15, 16));
			Assert.AreEqual(1, MathUtility.Modulo(int.MinValue / 16 * 16 + 1, 16));
			Assert.AreEqual(1, MathUtility.Modulo(1, 23));
			Assert.AreEqual(1, MathUtility.Modulo(24, 23));
			Assert.AreEqual(1, MathUtility.Modulo(-22, 23));
			Assert.AreEqual(1, MathUtility.Modulo(47, 23));
			Assert.AreEqual(1, MathUtility.Modulo(-45, 23));
			Assert.AreEqual(1, MathUtility.Modulo(int.MaxValue / 23 * 23 - 22, 23));
			Assert.AreEqual(1, MathUtility.Modulo(int.MinValue / 23 * 23 + 1, 23));

			Assert.AreEqual(15, MathUtility.Modulo(15, 16));
			Assert.AreEqual(15, MathUtility.Modulo(31, 16));
			Assert.AreEqual(15, MathUtility.Modulo(-1, 16));
			Assert.AreEqual(15, MathUtility.Modulo(47, 16));
			Assert.AreEqual(15, MathUtility.Modulo(-17, 16));
			Assert.AreEqual(15, MathUtility.Modulo(int.MaxValue / 16 * 16 - 1, 16));
			Assert.AreEqual(15, MathUtility.Modulo(int.MinValue / 16 * 16 + 15, 16));
			Assert.AreEqual(22, MathUtility.Modulo(22, 23));
			Assert.AreEqual(22, MathUtility.Modulo(45, 23));
			Assert.AreEqual(22, MathUtility.Modulo(-1, 23));
			Assert.AreEqual(22, MathUtility.Modulo(68, 23));
			Assert.AreEqual(22, MathUtility.Modulo(-24, 23));
			Assert.AreEqual(22, MathUtility.Modulo(int.MaxValue / 23 * 23 - 1, 23));
			Assert.AreEqual(22, MathUtility.Modulo(int.MinValue / 23 * 23 + 22, 23));
		}

		[Test]
		public void IsEvenTest()
		{
			Assert.IsTrue(MathUtility.IsEven(0), "MathUtility.IsEven(0)");
			Assert.IsTrue(MathUtility.IsEven(2), "MathUtility.IsEven(2)");
			Assert.IsTrue(MathUtility.IsEven(323543450), "MathUtility.IsEven(323543450)");
			Assert.IsTrue(MathUtility.IsEven(323543452), "MathUtility.IsEven(323543452)");
			Assert.IsTrue(MathUtility.IsEven(323543454), "MathUtility.IsEven(323543454)");
			Assert.IsTrue(MathUtility.IsEven(323543456), "MathUtility.IsEven(323543456)");
			Assert.IsTrue(MathUtility.IsEven(323543458), "MathUtility.IsEven(323543458)");
			Assert.IsTrue(MathUtility.IsEven(int.MaxValue - 3), string.Format("MathUtility.IsEven({0})", int.MaxValue - 3));
			Assert.IsTrue(MathUtility.IsEven(int.MaxValue - 1), string.Format("MathUtility.IsEven({0})", int.MaxValue - 1));

			Assert.IsTrue(MathUtility.IsEven(-0), "MathUtility.IsEven(-0)");
			Assert.IsTrue(MathUtility.IsEven(-2), "MathUtility.IsEven(-2)");
			Assert.IsTrue(MathUtility.IsEven(-83293480), "MathUtility.IsEven(-83293480)");
			Assert.IsTrue(MathUtility.IsEven(-83293482), "MathUtility.IsEven(-83293482)");
			Assert.IsTrue(MathUtility.IsEven(-83293484), "MathUtility.IsEven(-83293484)");
			Assert.IsTrue(MathUtility.IsEven(-83293486), "MathUtility.IsEven(-83293486)");
			Assert.IsTrue(MathUtility.IsEven(-83293488), "MathUtility.IsEven(-83293488)");
			Assert.IsTrue(MathUtility.IsEven(int.MinValue + 2), string.Format("MathUtility.IsEven({0})", int.MinValue + 2));
			Assert.IsTrue(MathUtility.IsEven(int.MinValue), string.Format("MathUtility.IsEven({0})", int.MinValue));

			Assert.IsFalse(MathUtility.IsEven(1), "MathUtility.IsEven(1)");
			Assert.IsFalse(MathUtility.IsEven(3), "MathUtility.IsEven(3)");
			Assert.IsFalse(MathUtility.IsEven(323543451), "MathUtility.IsEven(323543451)");
			Assert.IsFalse(MathUtility.IsEven(323543453), "MathUtility.IsEven(323543453)");
			Assert.IsFalse(MathUtility.IsEven(323543455), "MathUtility.IsEven(323543455)");
			Assert.IsFalse(MathUtility.IsEven(323543457), "MathUtility.IsEven(323543457)");
			Assert.IsFalse(MathUtility.IsEven(323543459), "MathUtility.IsEven(323543459)");
			Assert.IsFalse(MathUtility.IsEven(int.MaxValue - 2), string.Format("MathUtility.IsEven({0})", int.MaxValue - 2));
			Assert.IsFalse(MathUtility.IsEven(int.MaxValue), string.Format("MathUtility.IsEven({0})", int.MaxValue));

			Assert.IsFalse(MathUtility.IsEven(-1), "MathUtility.IsEven(-1)");
			Assert.IsFalse(MathUtility.IsEven(-3), "MathUtility.IsEven(-3)");
			Assert.IsFalse(MathUtility.IsEven(-83293481), "MathUtility.IsEven(-83293481)");
			Assert.IsFalse(MathUtility.IsEven(-83293483), "MathUtility.IsEven(-83293483)");
			Assert.IsFalse(MathUtility.IsEven(-83293485), "MathUtility.IsEven(-83293485)");
			Assert.IsFalse(MathUtility.IsEven(-83293487), "MathUtility.IsEven(-83293487)");
			Assert.IsFalse(MathUtility.IsEven(-83293489), "MathUtility.IsEven(-83293489)");
			Assert.IsFalse(MathUtility.IsEven(int.MinValue + 3), string.Format("MathUtility.IsEven({0})", int.MinValue + 3));
			Assert.IsFalse(MathUtility.IsEven(int.MinValue + 1), string.Format("MathUtility.IsEven({0})", int.MinValue + 1));
		}

		[Test]
		public void IsOddTest()
		{
			Assert.IsTrue(MathUtility.IsOdd(1), "MathUtility.IsOdd(1)");
			Assert.IsTrue(MathUtility.IsOdd(3), "MathUtility.IsOdd(3)");
			Assert.IsTrue(MathUtility.IsOdd(323543451), "MathUtility.IsOdd(323543451)");
			Assert.IsTrue(MathUtility.IsOdd(323543453), "MathUtility.IsOdd(323543453)");
			Assert.IsTrue(MathUtility.IsOdd(323543455), "MathUtility.IsOdd(323543455)");
			Assert.IsTrue(MathUtility.IsOdd(323543457), "MathUtility.IsOdd(323543457)");
			Assert.IsTrue(MathUtility.IsOdd(323543459), "MathUtility.IsOdd(323543459)");
			Assert.IsTrue(MathUtility.IsOdd(int.MaxValue - 2), string.Format("MathUtility.IsOdd({0})", int.MaxValue - 2));
			Assert.IsTrue(MathUtility.IsOdd(int.MaxValue), string.Format("MathUtility.IsOdd({0})", int.MaxValue));

			Assert.IsTrue(MathUtility.IsOdd(-1), "MathUtility.IsOdd(-1)");
			Assert.IsTrue(MathUtility.IsOdd(-3), "MathUtility.IsOdd(-3)");
			Assert.IsTrue(MathUtility.IsOdd(-83293481), "MathUtility.IsOdd(-83293481)");
			Assert.IsTrue(MathUtility.IsOdd(-83293483), "MathUtility.IsOdd(-83293483)");
			Assert.IsTrue(MathUtility.IsOdd(-83293485), "MathUtility.IsOdd(-83293485)");
			Assert.IsTrue(MathUtility.IsOdd(-83293487), "MathUtility.IsOdd(-83293487)");
			Assert.IsTrue(MathUtility.IsOdd(-83293489), "MathUtility.IsOdd(-83293489)");
			Assert.IsTrue(MathUtility.IsOdd(int.MinValue + 3), string.Format("MathUtility.IsOdd({0})", int.MinValue + 3));
			Assert.IsTrue(MathUtility.IsOdd(int.MinValue + 1), string.Format("MathUtility.IsOdd({0})", int.MinValue + 1));

			Assert.IsFalse(MathUtility.IsOdd(0), "MathUtility.IsOdd(0)");
			Assert.IsFalse(MathUtility.IsOdd(2), "MathUtility.IsOdd(2)");
			Assert.IsFalse(MathUtility.IsOdd(323543450), "MathUtility.IsOdd(323543450)");
			Assert.IsFalse(MathUtility.IsOdd(323543452), "MathUtility.IsOdd(323543452)");
			Assert.IsFalse(MathUtility.IsOdd(323543454), "MathUtility.IsOdd(323543454)");
			Assert.IsFalse(MathUtility.IsOdd(323543456), "MathUtility.IsOdd(323543456)");
			Assert.IsFalse(MathUtility.IsOdd(323543458), "MathUtility.IsOdd(323543458)");
			Assert.IsFalse(MathUtility.IsOdd(int.MaxValue - 3), string.Format("MathUtility.IsOdd({0})", int.MaxValue - 3));
			Assert.IsFalse(MathUtility.IsOdd(int.MaxValue - 1), string.Format("MathUtility.IsOdd({0})", int.MaxValue - 1));

			Assert.IsFalse(MathUtility.IsOdd(-0), "MathUtility.IsOdd(-0)");
			Assert.IsFalse(MathUtility.IsOdd(-2), "MathUtility.IsOdd(-2)");
			Assert.IsFalse(MathUtility.IsOdd(-83293480), "MathUtility.IsOdd(-83293480)");
			Assert.IsFalse(MathUtility.IsOdd(-83293482), "MathUtility.IsOdd(-83293482)");
			Assert.IsFalse(MathUtility.IsOdd(-83293484), "MathUtility.IsOdd(-83293484)");
			Assert.IsFalse(MathUtility.IsOdd(-83293486), "MathUtility.IsOdd(-83293486)");
			Assert.IsFalse(MathUtility.IsOdd(-83293488), "MathUtility.IsOdd(-83293488)");
			Assert.IsFalse(MathUtility.IsOdd(int.MinValue + 2), string.Format("MathUtility.IsOdd({0})", int.MinValue + 2));
			Assert.IsFalse(MathUtility.IsOdd(int.MinValue), string.Format("MathUtility.IsOdd({0})", int.MinValue));
		}

		[Test]
		public void HaveSameSignTest()
		{
			var nonNegative = new int[] { 0, 1, 2, 34578345, int.MaxValue - 1, int.MaxValue };
			var negative = new int[] { -1, -2, -3, -8548234, int.MinValue + 1, int.MinValue };

			foreach (var nonNegative0 in nonNegative)
			{
				foreach (var nonNegative1 in nonNegative)
				{
					Assert.IsTrue(MathUtility.HaveSameSign(nonNegative0, nonNegative1), string.Format("HaveSameSign({0}, {1}) should be true.", nonNegative0, nonNegative1));
				}

				foreach (var negative1 in negative)
				{
					Assert.IsFalse(MathUtility.HaveSameSign(nonNegative0, negative1), string.Format("HaveSameSign({0}, {1}) should be false.", nonNegative0, negative1));
				}
			}

			foreach (var negative0 in negative)
			{
				foreach (var negative1 in negative)
				{
					Assert.IsTrue(MathUtility.HaveSameSign(negative0, negative1), string.Format("HaveSameSign({0}, {1}) should be true.", negative0, negative1));
				}

				foreach (var nonNegative1 in nonNegative)
				{
					Assert.IsFalse(MathUtility.HaveSameSign(negative0, nonNegative1), string.Format("HaveSameSign({0}, {1}) should be false.", negative0, nonNegative1));
				}
			}
		}

		#endregion

		#region Base 2 Integer Logarithm Tests

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

		#endregion
	}
}
#endif
