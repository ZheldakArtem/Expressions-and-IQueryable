using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressions_and_IQueryable
{

	public class CustomExpressionTransformator : ExpressionVisitor
	{
		public override Expression Visit(Expression node)
		{
			return base.Visit(node);
		}
		protected override Expression VisitBinary(BinaryExpression node)
		{
			ParameterExpression parameter = null;
			ConstantExpression constant = null;

			if (node.NodeType == ExpressionType.Add)
			{
				if (node.Left.NodeType == ExpressionType.Parameter)
				{
					parameter = (ParameterExpression)node.Left;
				}
				else if (node.Left.NodeType == ExpressionType.Constant)
				{
					constant = (ConstantExpression)node.Left;
				}

				if (node.Right.NodeType == ExpressionType.Parameter)
				{
					parameter = (ParameterExpression)node.Right;
				}
				else if (node.Right.NodeType == ExpressionType.Constant)
				{
					constant = (ConstantExpression)node.Right;
				}

				if (parameter != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
				{
					return Expression.Increment(parameter);
				}
			}

			if (node.NodeType == ExpressionType.Subtract)
			{
				if (node.Left.NodeType == ExpressionType.Parameter)
				{
					parameter = (ParameterExpression)node.Left;
				}
				else if (node.Left.NodeType == ExpressionType.Constant)
				{
					constant = (ConstantExpression)node.Left;
				}

				if (node.Right.NodeType == ExpressionType.Parameter)
				{
					parameter = (ParameterExpression)node.Right;
				}
				else if (node.Right.NodeType == ExpressionType.Constant)
				{
					constant = (ConstantExpression)node.Right;
				}

				if (parameter != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
				{
					return Expression.Decrement(parameter);
				}
			}

			return base.VisitBinary(node);
		}

	}
}
