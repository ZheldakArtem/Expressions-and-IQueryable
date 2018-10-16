using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressions_and_IQueryable
{
	[TestClass]
	public class ExpressionVisitorTest
	{
		// Task1.1 - Замена выражений вида<переменная> + 1 / <переменная> - 1 на операции инкремента и декремента
	   [TestMethod]
		public void IncremenAndtDecrement()
		{
			Expression<Func<int, int, int>> source = (a, b) => (a + 1) / (b - 1);

			var result = new CustomExpressionTransformator().VisitAndConvert(source, "");

			Console.WriteLine(source); // (a, b) => ((a + 1) / (b - 1))
			Console.WriteLine(result); // (a, b) => (Increment(a) / Decrement(b))

			Assert.AreEqual(source.Compile().Invoke(9, 6), result.Compile().Invoke(9, 6));
		}

		// Task 1.2 - Замену параметров, входящих в lambda-выражение, на константы
		// в этом варианте передаем словарь в качестве параметра конструктора
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

		// Task 1.2 Суть такая же как и в предыдущем тесте, но теперь можно передавать словарь как параметр в выражении
		[TestMethod]
		public void ExchangeParamsV2()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>()
			{
				{ "a", 2 },
				{ "b", 4 },
				{ "c", 6 }
			};

			Expression<Func<Dictionary<string, int>, int, int, int, int>> source = (dictionary, a, b, c) => a + b + c;

			var result = new ExchangeExpressionV2().VisitAndConvert(source, "");

			var compiledResilt = result.Compile().Invoke(dict, 1, 1, 1);

			Console.WriteLine(result); // return 12
		}
	}
}
