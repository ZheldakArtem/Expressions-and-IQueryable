using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressions_and_IQueryable
{
	public class ExchangeExpressionV2 : ExpressionVisitor
	{
		private ParameterExpression _dictionary;

		protected override Expression VisitParameter(ParameterExpression node)
		{
			if (_dictionary != null)
			{
				var method = typeof(Dictionary<string, int>).GetMethod("get_Item");

				return Expression.Call(_dictionary, method, Expression.Constant(node.Name));
			}

			return base.VisitParameter(node);
		}

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			// find dictionary parameter
			foreach (var item in node.Parameters)
			{
				if (_dictionary == null)
				{
					if (item.Type == typeof(Dictionary<string, int>))
					{
						_dictionary = item;
					}
				}
			}

			return Expression.Lambda<T>(Visit(node.Body), node.Parameters);
		}

	}
}
