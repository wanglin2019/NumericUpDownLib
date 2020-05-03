using NumericUpDownLib;
using NUnit.Framework;
using System;
using System.Threading;

namespace NUnitTestProject
{
	[Apartment(ApartmentState.STA)]
	public class ShortTests
	{
		[SetUp]
		public void Setup()
		{
		}

		/// <summary>
		/// Tests whether values are set correctly as required while maintaining
		/// valid values at all times.
		/// </summary>
		[Test]
		public void TestValues()
		{
			TestAllPermutations(25, 50, 75);
			TestAllPermutations(-25, 50, 75);
			TestAllPermutations(-50, -50, 75);
			TestAllPermutations(50, 50, 50);
			TestAllPermutations(-50, -50, -50);
		}

		/// <summary>
		/// Tests all permutions for all sequences of three elements.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="val"></param>
		/// <param name="max"></param>
		public void TestAllPermutations(short min, short val, short max)
		{
			string[,] ctrl = new string[6, 3]
			{
				{ "min", "val", "max" },
				{ "val", "min", "max" },
				{ "val", "max", "min" },
				{ "val", "min", "max" },
				{ "min", "max", "val" },
				{ "max", "min", "val" },
			};

			for (int i = 0; i < 6; i++)
			{
				var range = new ShortUpDown();
				string testPermutation = "";

				for (int j = 0; j < 3; j++)
				{
					var itemToSet = ctrl[i, j];

					if (string.IsNullOrEmpty(testPermutation))
						testPermutation = itemToSet;
					else
						testPermutation += ", " + itemToSet;

					switch (itemToSet)
					{
						case "min":
							range.MinValue = min;
							Assert.IsTrue(IsValidRange(range));
							break;

						case "val":
							range.Value = val;
							Assert.IsTrue(IsValidRange(range));
							break;

						case "max":
							range.MaxValue = max;
							Assert.IsTrue(IsValidRange(range));
							break;

						default:
							break;
					}
				}

				Console.WriteLine("Testing Permutation {0}: {1} - min={2}, val={3}, max={4}", i, testPermutation, min, val, max);
				Assert.IsTrue(IsValidRange(range));

				Assert.IsTrue(range.MinValue == min);
				Assert.IsTrue(range.Value == val);
				Assert.IsTrue(range.MaxValue == max);
			}
		}

		/// <summary>
		/// Determines whether the given set of values defines a valid range or not.
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		bool IsValidRange(ShortUpDown range)
		{
			if (range.MinValue <= range.Value && range.Value <= range.MaxValue)
				return true;

			return false;

		}
	}
}
