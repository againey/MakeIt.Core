/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Tests
{
	public class MathToolsTests
	{
		#region Arithmetic Tests

		[Test]
		public void ModuloTest()
		{
			Assert.AreEqual(0, MathTools.Modulo(0, 1));
			Assert.AreEqual(0, MathTools.Modulo(1, 1));
			Assert.AreEqual(0, MathTools.Modulo(-1, 1));
			Assert.AreEqual(0, MathTools.Modulo(2, 1));
			Assert.AreEqual(0, MathTools.Modulo(-2, 1));
			Assert.AreEqual(0, MathTools.Modulo(int.MaxValue, 1));
			Assert.AreEqual(0, MathTools.Modulo(int.MinValue, 1));
			Assert.AreEqual(0, MathTools.Modulo(0, 16));
			Assert.AreEqual(0, MathTools.Modulo(16, 16));
			Assert.AreEqual(0, MathTools.Modulo(-16, 16));
			Assert.AreEqual(0, MathTools.Modulo(32, 16));
			Assert.AreEqual(0, MathTools.Modulo(-32, 16));
			Assert.AreEqual(0, MathTools.Modulo(int.MaxValue / 16 * 16, 16));
			Assert.AreEqual(0, MathTools.Modulo(int.MinValue / 16 * 16, 16));
			Assert.AreEqual(0, MathTools.Modulo(0, 23));
			Assert.AreEqual(0, MathTools.Modulo(23, 23));
			Assert.AreEqual(0, MathTools.Modulo(-23, 23));
			Assert.AreEqual(0, MathTools.Modulo(46, 23));
			Assert.AreEqual(0, MathTools.Modulo(-46, 23));
			Assert.AreEqual(0, MathTools.Modulo(int.MaxValue / 23 * 23, 23));
			Assert.AreEqual(0, MathTools.Modulo(int.MinValue / 23 * 23, 23));

			Assert.AreEqual(1, MathTools.Modulo(1, 16));
			Assert.AreEqual(1, MathTools.Modulo(17, 16));
			Assert.AreEqual(1, MathTools.Modulo(-15, 16));
			Assert.AreEqual(1, MathTools.Modulo(33, 16));
			Assert.AreEqual(1, MathTools.Modulo(-31, 16));
			Assert.AreEqual(1, MathTools.Modulo(int.MaxValue / 16 * 16 - 15, 16));
			Assert.AreEqual(1, MathTools.Modulo(int.MinValue / 16 * 16 + 1, 16));
			Assert.AreEqual(1, MathTools.Modulo(1, 23));
			Assert.AreEqual(1, MathTools.Modulo(24, 23));
			Assert.AreEqual(1, MathTools.Modulo(-22, 23));
			Assert.AreEqual(1, MathTools.Modulo(47, 23));
			Assert.AreEqual(1, MathTools.Modulo(-45, 23));
			Assert.AreEqual(1, MathTools.Modulo(int.MaxValue / 23 * 23 - 22, 23));
			Assert.AreEqual(1, MathTools.Modulo(int.MinValue / 23 * 23 + 1, 23));

			Assert.AreEqual(15, MathTools.Modulo(15, 16));
			Assert.AreEqual(15, MathTools.Modulo(31, 16));
			Assert.AreEqual(15, MathTools.Modulo(-1, 16));
			Assert.AreEqual(15, MathTools.Modulo(47, 16));
			Assert.AreEqual(15, MathTools.Modulo(-17, 16));
			Assert.AreEqual(15, MathTools.Modulo(int.MaxValue / 16 * 16 - 1, 16));
			Assert.AreEqual(15, MathTools.Modulo(int.MinValue / 16 * 16 + 15, 16));
			Assert.AreEqual(22, MathTools.Modulo(22, 23));
			Assert.AreEqual(22, MathTools.Modulo(45, 23));
			Assert.AreEqual(22, MathTools.Modulo(-1, 23));
			Assert.AreEqual(22, MathTools.Modulo(68, 23));
			Assert.AreEqual(22, MathTools.Modulo(-24, 23));
			Assert.AreEqual(22, MathTools.Modulo(int.MaxValue / 23 * 23 - 1, 23));
			Assert.AreEqual(22, MathTools.Modulo(int.MinValue / 23 * 23 + 22, 23));
		}

		[Test]
		public void IsEvenTest()
		{
			Assert.IsTrue(MathTools.IsEven(0), "MathTools.IsEven(0)");
			Assert.IsTrue(MathTools.IsEven(2), "MathTools.IsEven(2)");
			Assert.IsTrue(MathTools.IsEven(323543450), "MathTools.IsEven(323543450)");
			Assert.IsTrue(MathTools.IsEven(323543452), "MathTools.IsEven(323543452)");
			Assert.IsTrue(MathTools.IsEven(323543454), "MathTools.IsEven(323543454)");
			Assert.IsTrue(MathTools.IsEven(323543456), "MathTools.IsEven(323543456)");
			Assert.IsTrue(MathTools.IsEven(323543458), "MathTools.IsEven(323543458)");
			Assert.IsTrue(MathTools.IsEven(int.MaxValue - 3), string.Format("MathTools.IsEven({0})", int.MaxValue - 3));
			Assert.IsTrue(MathTools.IsEven(int.MaxValue - 1), string.Format("MathTools.IsEven({0})", int.MaxValue - 1));

			Assert.IsTrue(MathTools.IsEven(-0), "MathTools.IsEven(-0)");
			Assert.IsTrue(MathTools.IsEven(-2), "MathTools.IsEven(-2)");
			Assert.IsTrue(MathTools.IsEven(-83293480), "MathTools.IsEven(-83293480)");
			Assert.IsTrue(MathTools.IsEven(-83293482), "MathTools.IsEven(-83293482)");
			Assert.IsTrue(MathTools.IsEven(-83293484), "MathTools.IsEven(-83293484)");
			Assert.IsTrue(MathTools.IsEven(-83293486), "MathTools.IsEven(-83293486)");
			Assert.IsTrue(MathTools.IsEven(-83293488), "MathTools.IsEven(-83293488)");
			Assert.IsTrue(MathTools.IsEven(int.MinValue + 2), string.Format("MathTools.IsEven({0})", int.MinValue + 2));
			Assert.IsTrue(MathTools.IsEven(int.MinValue), string.Format("MathTools.IsEven({0})", int.MinValue));

			Assert.IsFalse(MathTools.IsEven(1), "MathTools.IsEven(1)");
			Assert.IsFalse(MathTools.IsEven(3), "MathTools.IsEven(3)");
			Assert.IsFalse(MathTools.IsEven(323543451), "MathTools.IsEven(323543451)");
			Assert.IsFalse(MathTools.IsEven(323543453), "MathTools.IsEven(323543453)");
			Assert.IsFalse(MathTools.IsEven(323543455), "MathTools.IsEven(323543455)");
			Assert.IsFalse(MathTools.IsEven(323543457), "MathTools.IsEven(323543457)");
			Assert.IsFalse(MathTools.IsEven(323543459), "MathTools.IsEven(323543459)");
			Assert.IsFalse(MathTools.IsEven(int.MaxValue - 2), string.Format("MathTools.IsEven({0})", int.MaxValue - 2));
			Assert.IsFalse(MathTools.IsEven(int.MaxValue), string.Format("MathTools.IsEven({0})", int.MaxValue));

			Assert.IsFalse(MathTools.IsEven(-1), "MathTools.IsEven(-1)");
			Assert.IsFalse(MathTools.IsEven(-3), "MathTools.IsEven(-3)");
			Assert.IsFalse(MathTools.IsEven(-83293481), "MathTools.IsEven(-83293481)");
			Assert.IsFalse(MathTools.IsEven(-83293483), "MathTools.IsEven(-83293483)");
			Assert.IsFalse(MathTools.IsEven(-83293485), "MathTools.IsEven(-83293485)");
			Assert.IsFalse(MathTools.IsEven(-83293487), "MathTools.IsEven(-83293487)");
			Assert.IsFalse(MathTools.IsEven(-83293489), "MathTools.IsEven(-83293489)");
			Assert.IsFalse(MathTools.IsEven(int.MinValue + 3), string.Format("MathTools.IsEven({0})", int.MinValue + 3));
			Assert.IsFalse(MathTools.IsEven(int.MinValue + 1), string.Format("MathTools.IsEven({0})", int.MinValue + 1));
		}

		[Test]
		public void IsOddTest()
		{
			Assert.IsTrue(MathTools.IsOdd(1), "MathTools.IsOdd(1)");
			Assert.IsTrue(MathTools.IsOdd(3), "MathTools.IsOdd(3)");
			Assert.IsTrue(MathTools.IsOdd(323543451), "MathTools.IsOdd(323543451)");
			Assert.IsTrue(MathTools.IsOdd(323543453), "MathTools.IsOdd(323543453)");
			Assert.IsTrue(MathTools.IsOdd(323543455), "MathTools.IsOdd(323543455)");
			Assert.IsTrue(MathTools.IsOdd(323543457), "MathTools.IsOdd(323543457)");
			Assert.IsTrue(MathTools.IsOdd(323543459), "MathTools.IsOdd(323543459)");
			Assert.IsTrue(MathTools.IsOdd(int.MaxValue - 2), string.Format("MathTools.IsOdd({0})", int.MaxValue - 2));
			Assert.IsTrue(MathTools.IsOdd(int.MaxValue), string.Format("MathTools.IsOdd({0})", int.MaxValue));

			Assert.IsTrue(MathTools.IsOdd(-1), "MathTools.IsOdd(-1)");
			Assert.IsTrue(MathTools.IsOdd(-3), "MathTools.IsOdd(-3)");
			Assert.IsTrue(MathTools.IsOdd(-83293481), "MathTools.IsOdd(-83293481)");
			Assert.IsTrue(MathTools.IsOdd(-83293483), "MathTools.IsOdd(-83293483)");
			Assert.IsTrue(MathTools.IsOdd(-83293485), "MathTools.IsOdd(-83293485)");
			Assert.IsTrue(MathTools.IsOdd(-83293487), "MathTools.IsOdd(-83293487)");
			Assert.IsTrue(MathTools.IsOdd(-83293489), "MathTools.IsOdd(-83293489)");
			Assert.IsTrue(MathTools.IsOdd(int.MinValue + 3), string.Format("MathTools.IsOdd({0})", int.MinValue + 3));
			Assert.IsTrue(MathTools.IsOdd(int.MinValue + 1), string.Format("MathTools.IsOdd({0})", int.MinValue + 1));

			Assert.IsFalse(MathTools.IsOdd(0), "MathTools.IsOdd(0)");
			Assert.IsFalse(MathTools.IsOdd(2), "MathTools.IsOdd(2)");
			Assert.IsFalse(MathTools.IsOdd(323543450), "MathTools.IsOdd(323543450)");
			Assert.IsFalse(MathTools.IsOdd(323543452), "MathTools.IsOdd(323543452)");
			Assert.IsFalse(MathTools.IsOdd(323543454), "MathTools.IsOdd(323543454)");
			Assert.IsFalse(MathTools.IsOdd(323543456), "MathTools.IsOdd(323543456)");
			Assert.IsFalse(MathTools.IsOdd(323543458), "MathTools.IsOdd(323543458)");
			Assert.IsFalse(MathTools.IsOdd(int.MaxValue - 3), string.Format("MathTools.IsOdd({0})", int.MaxValue - 3));
			Assert.IsFalse(MathTools.IsOdd(int.MaxValue - 1), string.Format("MathTools.IsOdd({0})", int.MaxValue - 1));

			Assert.IsFalse(MathTools.IsOdd(-0), "MathTools.IsOdd(-0)");
			Assert.IsFalse(MathTools.IsOdd(-2), "MathTools.IsOdd(-2)");
			Assert.IsFalse(MathTools.IsOdd(-83293480), "MathTools.IsOdd(-83293480)");
			Assert.IsFalse(MathTools.IsOdd(-83293482), "MathTools.IsOdd(-83293482)");
			Assert.IsFalse(MathTools.IsOdd(-83293484), "MathTools.IsOdd(-83293484)");
			Assert.IsFalse(MathTools.IsOdd(-83293486), "MathTools.IsOdd(-83293486)");
			Assert.IsFalse(MathTools.IsOdd(-83293488), "MathTools.IsOdd(-83293488)");
			Assert.IsFalse(MathTools.IsOdd(int.MinValue + 2), string.Format("MathTools.IsOdd({0})", int.MinValue + 2));
			Assert.IsFalse(MathTools.IsOdd(int.MinValue), string.Format("MathTools.IsOdd({0})", int.MinValue));
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
					Assert.IsTrue(MathTools.HaveSameSign(nonNegative0, nonNegative1), string.Format("HaveSameSign({0}, {1}) should be true.", nonNegative0, nonNegative1));
				}

				foreach (var negative1 in negative)
				{
					Assert.IsFalse(MathTools.HaveSameSign(nonNegative0, negative1), string.Format("HaveSameSign({0}, {1}) should be false.", nonNegative0, negative1));
				}
			}

			foreach (var negative0 in negative)
			{
				foreach (var negative1 in negative)
				{
					Assert.IsTrue(MathTools.HaveSameSign(negative0, negative1), string.Format("HaveSameSign({0}, {1}) should be true.", negative0, negative1));
				}

				foreach (var nonNegative1 in nonNegative)
				{
					Assert.IsFalse(MathTools.HaveSameSign(negative0, nonNegative1), string.Format("HaveSameSign({0}, {1}) should be false.", negative0, nonNegative1));
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
				Assert.AreEqual(i, MathTools.Log2Ceil(1U << i), string.Format("MathTools.Log2Ceil(1U << {0}), MathTools.Log2Ceil({1})", i, 1U << i));
			}
		}

		[Test]
		public void Log2Ceil64BitPowerOfTwo()
		{
			for (int i = 0; i < 64; ++i)
			{
				Assert.AreEqual(i, MathTools.Log2Ceil(1UL << i), string.Format("MathTools.Log2Ceil(1UL << {0}), MathTools.Log2Ceil({1})", i, 1UL << i));
			}
		}

		[Test]
		public void Plus1Log2Ceil32BitPowerOfTwo()
		{
			for (int i = 0; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, MathTools.Plus1Log2Ceil(1U << i), string.Format("MathTools.Plus1Log2Ceil(1U << {0}), MathTools.Plus1Log2Ceil({1})", i, 1U << i));
			}
		}

		[Test]
		public void Plus1Log2Ceil64BitPowerOfTwo()
		{
			for (int i = 0; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, MathTools.Plus1Log2Ceil(1UL << i), string.Format("MathTools.Plus1Log2Ceil(1UL << {0}), MathTools.Plus1Log2Ceil({1})", i, 1UL << i));
			}
		}

		[Test]
		public void Log2Ceil32BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 32; ++i)
			{
				Assert.AreEqual(i, MathTools.Log2Ceil((1U << i) - 1), string.Format("MathTools.Log2Ceil((1U << {0}) - 1), MathTools.Log2Ceil({1})", i, (1U << i) - 1));
			}
		}

		[Test]
		public void Log2Ceil64BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 64; ++i)
			{
				Assert.AreEqual(i, MathTools.Log2Ceil((1UL << i) - 1), string.Format("MathTools.Log2Ceil((1UL << {0}) - 1), MathTools.Log2Ceil({1})", i, (1UL << i) - 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil32BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 32; ++i)
			{
				Assert.AreEqual(i, MathTools.Plus1Log2Ceil((1U << i) - 1), string.Format("MathTools.Plus1Log2Ceil((1U << {0}) - 1), MathTools.Plus1Log2Ceil({1})", i, (1U << i) - 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil64BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 64; ++i)
			{
				Assert.AreEqual(i, MathTools.Plus1Log2Ceil((1UL << i) - 1), string.Format("MathTools.Plus1Log2Ceil((1UL << {0}) - 1), MathTools.Plus1Log2Ceil({1})", i, (1UL << i) - 1));
			}
		}

		[Test]
		public void Log2Ceil32BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, MathTools.Log2Ceil((1U << i) + 1), string.Format("MathTools.Log2Ceil((1U << {0}) + 1), MathTools.Log2Ceil({1})", i, (1U << i) + 1));
			}
		}

		[Test]
		public void Log2Ceil64BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, MathTools.Log2Ceil((1UL << i) + 1), string.Format("MathTools.Log2Ceil((1UL << {0}) + 1), MathTools.Log2Ceil({1})", i, (1UL << i) + 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil32BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, MathTools.Plus1Log2Ceil((1U << i) + 1), string.Format("MathTools.Plus1Log2Ceil((1U << {0}) + 1), MathTools.Plus1Log2Ceil({1})", i, (1U << i) + 1));
			}
		}

		[Test]
		public void Plus1Log2Ceil64BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, MathTools.Plus1Log2Ceil((1UL << i) + 1), string.Format("MathTools.Plus1Log2Ceil((1UL << {0}) + 1), MathTools.Plus1Log2Ceil({1})", i, (1UL << i) + 1));
			}
		}

		#endregion
	}
}
#endif
