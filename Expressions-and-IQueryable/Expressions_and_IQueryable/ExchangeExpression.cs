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
		protected override Expression VisitBinary(BinaryExpression node)
		{
			//ParameterExpression parameterLeft = null;
			//ParameterExpression parameterRight = null;
			//ConstantExpression constant = null;

			//if (node.Right.NodeType == ExpressionType.Parameter && node.Left.NodeType == ExpressionType.Parameter)
			//{
			//	parameterLeft = (ParameterExpression)node.Left;
			//	parameterRight = (ParameterExpression)node.Right;

			//	if (_exchangeDict.ContainsKey(parameterRight.Name) && _exchangeDict.ContainsKey(parameterLeft.Name))
			//	{
			//		return Expression.Constant(_exchangeDict[parameterRight.Name] + _exchangeDict[parameterLeft.Name]);
			//	}
			//}




			//if (node.Right.NodeType == ExpressionType.Parameter)
			//{
			//	parameterRight = (ParameterExpression)node.Right;
			//	if (_exchangeDict.ContainsKey(parameterRight.Name))
			//	{
			//		return Expression.Constant(_exchangeDict[parameterRight.Name]);
			//	}
			//}

			//if (node.Left.NodeType == ExpressionType.Parameter)
			//{
			//	parameterLeft = (ParameterExpression)node.Left;
			//	if (_exchangeDict.ContainsKey(parameterLeft.Name))
			//	{
			//		return Expression.Constant(_exchangeDict[parameterLeft.Name]);
			//	}
			//}

			return base.VisitBinary(node);
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
