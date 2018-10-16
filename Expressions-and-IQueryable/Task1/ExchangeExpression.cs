using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressions_and_IQueryable
{
	public class ExchangeExpression : ExpressionVisitor
	{
		private Dictionary<string, int> _exchangeDict;

		public ExchangeExpression()
		{

		}

		public ExchangeExpression(Dictionary<string, int> exchangeDict)
		{
			_exchangeDict = exchangeDict;
		}
		private int depth = 0;

		public override Expression Visit(Expression node)
		{
			depth++;
			var result = base.Visit(node);
			depth--;

			return result;
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			if (depth > 2)
			{
				if (_exchangeDict.ContainsKey(node.Name))
				{
					return Expression.Constant(_exchangeDict[node.Name]);
				}
			}

			return base.VisitParameter(node);
		}
	}
}
