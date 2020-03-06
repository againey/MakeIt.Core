/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;

namespace MakeIt.Numerics.Tests
{
	class MathTests
	{
		#region Arithmetic Tests

		[TestCase(Category = "Normal")]
		public void ModuloTest()
		{
			Assert.AreEqual(0, Math.Modulo(0, 1));
			Assert.AreEqual(0, Math.Modulo(1, 1));
			Assert.AreEqual(0, Math.Modulo(-1, 1));
			Assert.AreEqual(0, Math.Modulo(2, 1));
			Assert.AreEqual(0, Math.Modulo(-2, 1));
			Assert.AreEqual(0, Math.Modulo(int.MaxValue, 1));
			Assert.AreEqual(0, Math.Modulo(int.MinValue, 1));
			Assert.AreEqual(0, Math.Modulo(0, 16));
			Assert.AreEqual(0, Math.Modulo(16, 16));
			Assert.AreEqual(0, Math.Modulo(-16, 16));
			Assert.AreEqual(0, Math.Modulo(32, 16));
			Assert.AreEqual(0, Math.Modulo(-32, 16));
			Assert.AreEqual(0, Math.Modulo(int.MaxValue / 16 * 16, 16));
			Assert.AreEqual(0, Math.Modulo(int.MinValue / 16 * 16, 16));
			Assert.AreEqual(0, Math.Modulo(0, 23));
			Assert.AreEqual(0, Math.Modulo(23, 23));
			Assert.AreEqual(0, Math.Modulo(-23, 23));
			Assert.AreEqual(0, Math.Modulo(46, 23));
			Assert.AreEqual(0, Math.Modulo(-46, 23));
			Assert.AreEqual(0, Math.Modulo(int.MaxValue / 23 * 23, 23));
			Assert.AreEqual(0, Math.Modulo(int.MinValue / 23 * 23, 23));

			Assert.AreEqual(1, Math.Modulo(1, 16));
			Assert.AreEqual(1, Math.Modulo(17, 16));
			Assert.AreEqual(1, Math.Modulo(-15, 16));
			Assert.AreEqual(1, Math.Modulo(33, 16));
			Assert.AreEqual(1, Math.Modulo(-31, 16));
			Assert.AreEqual(1, Math.Modulo(int.MaxValue / 16 * 16 - 15, 16));
			Assert.AreEqual(1, Math.Modulo(int.MinValue / 16 * 16 + 1, 16));
			Assert.AreEqual(1, Math.Modulo(1, 23));
			Assert.AreEqual(1, Math.Modulo(24, 23));
			Assert.AreEqual(1, Math.Modulo(-22, 23));
			Assert.AreEqual(1, Math.Modulo(47, 23));
			Assert.AreEqual(1, Math.Modulo(-45, 23));
			Assert.AreEqual(1, Math.Modulo(int.MaxValue / 23 * 23 - 22, 23));
			Assert.AreEqual(1, Math.Modulo(int.MinValue / 23 * 23 + 1, 23));

			Assert.AreEqual(15, Math.Modulo(15, 16));
			Assert.AreEqual(15, Math.Modulo(31, 16));
			Assert.AreEqual(15, Math.Modulo(-1, 16));
			Assert.AreEqual(15, Math.Modulo(47, 16));
			Assert.AreEqual(15, Math.Modulo(-17, 16));
			Assert.AreEqual(15, Math.Modulo(int.MaxValue / 16 * 16 - 1, 16));
			Assert.AreEqual(15, Math.Modulo(int.MinValue / 16 * 16 + 15, 16));
			Assert.AreEqual(22, Math.Modulo(22, 23));
			Assert.AreEqual(22, Math.Modulo(45, 23));
			Assert.AreEqual(22, Math.Modulo(-1, 23));
			Assert.AreEqual(22, Math.Modulo(68, 23));
			Assert.AreEqual(22, Math.Modulo(-24, 23));
			Assert.AreEqual(22, Math.Modulo(int.MaxValue / 23 * 23 - 1, 23));
			Assert.AreEqual(22, Math.Modulo(int.MinValue / 23 * 23 + 22, 23));
		}

		[TestCase(Category = "Normal")]
		public void IsEvenTest()
		{
			Assert.IsTrue(Math.IsEven(0), "MathTools.IsEven(0)");
			Assert.IsTrue(Math.IsEven(2), "MathTools.IsEven(2)");
			Assert.IsTrue(Math.IsEven(323543450), "MathTools.IsEven(323543450)");
			Assert.IsTrue(Math.IsEven(323543452), "MathTools.IsEven(323543452)");
			Assert.IsTrue(Math.IsEven(323543454), "MathTools.IsEven(323543454)");
			Assert.IsTrue(Math.IsEven(323543456), "MathTools.IsEven(323543456)");
			Assert.IsTrue(Math.IsEven(323543458), "MathTools.IsEven(323543458)");
			Assert.IsTrue(Math.IsEven(int.MaxValue - 3), string.Format("MathTools.IsEven({0})", int.MaxValue - 3));
			Assert.IsTrue(Math.IsEven(int.MaxValue - 1), string.Format("MathTools.IsEven({0})", int.MaxValue - 1));

			Assert.IsTrue(Math.IsEven(-0), "MathTools.IsEven(-0)");
			Assert.IsTrue(Math.IsEven(-2), "MathTools.IsEven(-2)");
			Assert.IsTrue(Math.IsEven(-83293480), "MathTools.IsEven(-83293480)");
			Assert.IsTrue(Math.IsEven(-83293482), "MathTools.IsEven(-83293482)");
			Assert.IsTrue(Math.IsEven(-83293484), "MathTools.IsEven(-83293484)");
			Assert.IsTrue(Math.IsEven(-83293486), "MathTools.IsEven(-83293486)");
			Assert.IsTrue(Math.IsEven(-83293488), "MathTools.IsEven(-83293488)");
			Assert.IsTrue(Math.IsEven(int.MinValue + 2), string.Format("MathTools.IsEven({0})", int.MinValue + 2));
			Assert.IsTrue(Math.IsEven(int.MinValue), string.Format("MathTools.IsEven({0})", int.MinValue));

			Assert.IsFalse(Math.IsEven(1), "MathTools.IsEven(1)");
			Assert.IsFalse(Math.IsEven(3), "MathTools.IsEven(3)");
			Assert.IsFalse(Math.IsEven(323543451), "MathTools.IsEven(323543451)");
			Assert.IsFalse(Math.IsEven(323543453), "MathTools.IsEven(323543453)");
			Assert.IsFalse(Math.IsEven(323543455), "MathTools.IsEven(323543455)");
			Assert.IsFalse(Math.IsEven(323543457), "MathTools.IsEven(323543457)");
			Assert.IsFalse(Math.IsEven(323543459), "MathTools.IsEven(323543459)");
			Assert.IsFalse(Math.IsEven(int.MaxValue - 2), string.Format("MathTools.IsEven({0})", int.MaxValue - 2));
			Assert.IsFalse(Math.IsEven(int.MaxValue), string.Format("MathTools.IsEven({0})", int.MaxValue));

			Assert.IsFalse(Math.IsEven(-1), "MathTools.IsEven(-1)");
			Assert.IsFalse(Math.IsEven(-3), "MathTools.IsEven(-3)");
			Assert.IsFalse(Math.IsEven(-83293481), "MathTools.IsEven(-83293481)");
			Assert.IsFalse(Math.IsEven(-83293483), "MathTools.IsEven(-83293483)");
			Assert.IsFalse(Math.IsEven(-83293485), "MathTools.IsEven(-83293485)");
			Assert.IsFalse(Math.IsEven(-83293487), "MathTools.IsEven(-83293487)");
			Assert.IsFalse(Math.IsEven(-83293489), "MathTools.IsEven(-83293489)");
			Assert.IsFalse(Math.IsEven(int.MinValue + 3), string.Format("MathTools.IsEven({0})", int.MinValue + 3));
			Assert.IsFalse(Math.IsEven(int.MinValue + 1), string.Format("MathTools.IsEven({0})", int.MinValue + 1));
		}

		[TestCase(Category = "Normal")]
		public void IsOddTest()
		{
			Assert.IsTrue(Math.IsOdd(1), "MathTools.IsOdd(1)");
			Assert.IsTrue(Math.IsOdd(3), "MathTools.IsOdd(3)");
			Assert.IsTrue(Math.IsOdd(323543451), "MathTools.IsOdd(323543451)");
			Assert.IsTrue(Math.IsOdd(323543453), "MathTools.IsOdd(323543453)");
			Assert.IsTrue(Math.IsOdd(323543455), "MathTools.IsOdd(323543455)");
			Assert.IsTrue(Math.IsOdd(323543457), "MathTools.IsOdd(323543457)");
			Assert.IsTrue(Math.IsOdd(323543459), "MathTools.IsOdd(323543459)");
			Assert.IsTrue(Math.IsOdd(int.MaxValue - 2), string.Format("MathTools.IsOdd({0})", int.MaxValue - 2));
			Assert.IsTrue(Math.IsOdd(int.MaxValue), string.Format("MathTools.IsOdd({0})", int.MaxValue));

			Assert.IsTrue(Math.IsOdd(-1), "MathTools.IsOdd(-1)");
			Assert.IsTrue(Math.IsOdd(-3), "MathTools.IsOdd(-3)");
			Assert.IsTrue(Math.IsOdd(-83293481), "MathTools.IsOdd(-83293481)");
			Assert.IsTrue(Math.IsOdd(-83293483), "MathTools.IsOdd(-83293483)");
			Assert.IsTrue(Math.IsOdd(-83293485), "MathTools.IsOdd(-83293485)");
			Assert.IsTrue(Math.IsOdd(-83293487), "MathTools.IsOdd(-83293487)");
			Assert.IsTrue(Math.IsOdd(-83293489), "MathTools.IsOdd(-83293489)");
			Assert.IsTrue(Math.IsOdd(int.MinValue + 3), string.Format("MathTools.IsOdd({0})", int.MinValue + 3));
			Assert.IsTrue(Math.IsOdd(int.MinValue + 1), string.Format("MathTools.IsOdd({0})", int.MinValue + 1));

			Assert.IsFalse(Math.IsOdd(0), "MathTools.IsOdd(0)");
			Assert.IsFalse(Math.IsOdd(2), "MathTools.IsOdd(2)");
			Assert.IsFalse(Math.IsOdd(323543450), "MathTools.IsOdd(323543450)");
			Assert.IsFalse(Math.IsOdd(323543452), "MathTools.IsOdd(323543452)");
			Assert.IsFalse(Math.IsOdd(323543454), "MathTools.IsOdd(323543454)");
			Assert.IsFalse(Math.IsOdd(323543456), "MathTools.IsOdd(323543456)");
			Assert.IsFalse(Math.IsOdd(323543458), "MathTools.IsOdd(323543458)");
			Assert.IsFalse(Math.IsOdd(int.MaxValue - 3), string.Format("MathTools.IsOdd({0})", int.MaxValue - 3));
			Assert.IsFalse(Math.IsOdd(int.MaxValue - 1), string.Format("MathTools.IsOdd({0})", int.MaxValue - 1));

			Assert.IsFalse(Math.IsOdd(-0), "MathTools.IsOdd(-0)");
			Assert.IsFalse(Math.IsOdd(-2), "MathTools.IsOdd(-2)");
			Assert.IsFalse(Math.IsOdd(-83293480), "MathTools.IsOdd(-83293480)");
			Assert.IsFalse(Math.IsOdd(-83293482), "MathTools.IsOdd(-83293482)");
			Assert.IsFalse(Math.IsOdd(-83293484), "MathTools.IsOdd(-83293484)");
			Assert.IsFalse(Math.IsOdd(-83293486), "MathTools.IsOdd(-83293486)");
			Assert.IsFalse(Math.IsOdd(-83293488), "MathTools.IsOdd(-83293488)");
			Assert.IsFalse(Math.IsOdd(int.MinValue + 2), string.Format("MathTools.IsOdd({0})", int.MinValue + 2));
			Assert.IsFalse(Math.IsOdd(int.MinValue), string.Format("MathTools.IsOdd({0})", int.MinValue));
		}

		[TestCase(Category = "Normal")]
		public void HaveSameSignTest()
		{
			var nonNegative = new int[] { 0, 1, 2, 34578345, int.MaxValue - 1, int.MaxValue };
			var negative = new int[] { -1, -2, -3, -8548234, int.MinValue + 1, int.MinValue };

			foreach (var nonNegative0 in nonNegative)
			{
				foreach (var nonNegative1 in nonNegative)
				{
					Assert.IsTrue(Math.HaveSameSign(nonNegative0, nonNegative1), string.Format("HaveSameSign({0}, {1}) should be true.", nonNegative0, nonNegative1));
				}

				foreach (var negative1 in negative)
				{
					Assert.IsFalse(Math.HaveSameSign(nonNegative0, negative1), string.Format("HaveSameSign({0}, {1}) should be false.", nonNegative0, negative1));
				}
			}

			foreach (var negative0 in negative)
			{
				foreach (var negative1 in negative)
				{
					Assert.IsTrue(Math.HaveSameSign(negative0, negative1), string.Format("HaveSameSign({0}, {1}) should be true.", negative0, negative1));
				}

				foreach (var nonNegative1 in nonNegative)
				{
					Assert.IsFalse(Math.HaveSameSign(negative0, nonNegative1), string.Format("HaveSameSign({0}, {1}) should be false.", negative0, nonNegative1));
				}
			}
		}

		#endregion

		#region Base 2 Integer Logarithm Tests

		[TestCase(Category = "Normal")]
		public void Log2Ceil32BitPowerOfTwo()
		{
			for (int i = 0; i < 32; ++i)
			{
				Assert.AreEqual(i, Math.Log2Ceil(1U << i), string.Format("MathTools.Log2Ceil(1U << {0}), MathTools.Log2Ceil({1})", i, 1U << i));
			}
		}

		[TestCase(Category = "Normal")]
		public void Log2Ceil64BitPowerOfTwo()
		{
			for (int i = 0; i < 64; ++i)
			{
				Assert.AreEqual(i, Math.Log2Ceil(1UL << i), string.Format("MathTools.Log2Ceil(1UL << {0}), MathTools.Log2Ceil({1})", i, 1UL << i));
			}
		}

		[TestCase(Category = "Normal")]
		public void Plus1Log2Ceil32BitPowerOfTwo()
		{
			for (int i = 0; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, Math.Plus1Log2Ceil(1U << i), string.Format("MathTools.Plus1Log2Ceil(1U << {0}), MathTools.Plus1Log2Ceil({1})", i, 1U << i));
			}
		}

		[TestCase(Category = "Normal")]
		public void Plus1Log2Ceil64BitPowerOfTwo()
		{
			for (int i = 0; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, Math.Plus1Log2Ceil(1UL << i), string.Format("MathTools.Plus1Log2Ceil(1UL << {0}), MathTools.Plus1Log2Ceil({1})", i, 1UL << i));
			}
		}

		[TestCase(Category = "Normal")]
		public void Log2Ceil32BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 32; ++i)
			{
				Assert.AreEqual(i, Math.Log2Ceil((1U << i) - 1), string.Format("MathTools.Log2Ceil((1U << {0}) - 1), MathTools.Log2Ceil({1})", i, (1U << i) - 1));
			}
		}

		[TestCase(Category = "Normal")]
		public void Log2Ceil64BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 64; ++i)
			{
				Assert.AreEqual(i, Math.Log2Ceil((1UL << i) - 1), string.Format("MathTools.Log2Ceil((1UL << {0}) - 1), MathTools.Log2Ceil({1})", i, (1UL << i) - 1));
			}
		}

		[TestCase(Category = "Normal")]
		public void Plus1Log2Ceil32BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 32; ++i)
			{
				Assert.AreEqual(i, Math.Plus1Log2Ceil((1U << i) - 1), string.Format("MathTools.Plus1Log2Ceil((1U << {0}) - 1), MathTools.Plus1Log2Ceil({1})", i, (1U << i) - 1));
			}
		}

		[TestCase(Category = "Normal")]
		public void Plus1Log2Ceil64BitPowerOfTwoMinusOne()
		{
			for (int i = 2; i < 64; ++i)
			{
				Assert.AreEqual(i, Math.Plus1Log2Ceil((1UL << i) - 1), string.Format("MathTools.Plus1Log2Ceil((1UL << {0}) - 1), MathTools.Plus1Log2Ceil({1})", i, (1UL << i) - 1));
			}
		}

		[TestCase(Category = "Normal")]
		public void Log2Ceil32BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, Math.Log2Ceil((1U << i) + 1), string.Format("MathTools.Log2Ceil((1U << {0}) + 1), MathTools.Log2Ceil({1})", i, (1U << i) + 1));
			}
		}

		[TestCase(Category = "Normal")]
		public void Log2Ceil64BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, Math.Log2Ceil((1UL << i) + 1), string.Format("MathTools.Log2Ceil((1UL << {0}) + 1), MathTools.Log2Ceil({1})", i, (1UL << i) + 1));
			}
		}

		[TestCase(Category = "Normal")]
		public void Plus1Log2Ceil32BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 32; ++i)
			{
				Assert.AreEqual(i + 1, Math.Plus1Log2Ceil((1U << i) + 1), string.Format("MathTools.Plus1Log2Ceil((1U << {0}) + 1), MathTools.Plus1Log2Ceil({1})", i, (1U << i) + 1));
			}
		}

		[TestCase(Category = "Normal")]
		public void Plus1Log2Ceil64BitPowerOfTwoPlusOne()
		{
			for (int i = 1; i < 64; ++i)
			{
				Assert.AreEqual(i + 1, Math.Plus1Log2Ceil((1UL << i) + 1), string.Format("MathTools.Plus1Log2Ceil((1UL << {0}) + 1), MathTools.Plus1Log2Ceil({1})", i, (1UL << i) + 1));
			}
		}

		#endregion
	}
}
#endif
