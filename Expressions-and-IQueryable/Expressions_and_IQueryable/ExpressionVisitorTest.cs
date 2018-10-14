using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressions_and_IQueryable
{
	[TestClass]
	public class ExpressionVisitorTest
	{
		// Task1 - Замена выражений вида<переменная> + 1 / <переменная> - 1 на операции инкремента и декремента
	   [TestMethod]
		public void IncremenAndtDecrement()
		{
			Expression<Func<int, int, int>> source = (a, b) => (a + 1) / (b - 1);

			var result = new CustomExpressionTransformator().VisitAndConvert(source, "");

			Console.WriteLine(source); // (a, b) => ((a + 1) / (b - 1))
			Console.WriteLine(result); // (a, b) => (Increment(a) / Decrement(b))

			Assert.AreEqual(source.Compile().Invoke(9, 6), result.Compile().Invoke(9, 6));
		}

		// Task2 - Замену параметров, входящих в lambda-выражение, на константы
		[TestMethod]
		public void ExchangeParams()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>()
			{
				{ "a", 2 },
				{ "b", 4 },
				{ "c", 6 }
			};

			Expression<Func<int, int, int, int>> source = (a, b, c) => a + b + c;

			var result = new ExchangeExpression(dict).VisitAndConvert(source, "");

			Console.WriteLine(result); // (a, b, c) => ((2 + 4) + 6)
		}

	}
}
