using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
	public class MappingGenerator
	{
		public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
		{
			var sourceType = typeof(TSource);
			var createdType = typeof(TDestination);
			var sourceParam = Expression.Parameter(sourceType, "param");
			var ctor = Expression.New(createdType);

			List<MemberAssignment> memberAssignments = new List<MemberAssignment>();

			foreach (var sourceProp in sourceType.GetProperties())
			{
				foreach (var createdProp in createdType.GetProperties())
				{
					if (sourceProp.Name == createdProp.Name)
					{
						var sourcePropValue = Expression.Property(sourceParam, sourceProp.Name);
						memberAssignments.Add(Expression.Bind(createdProp, sourcePropValue));
					}
				}
			}

			var memberInit = Expression.MemberInit(ctor, memberAssignments);

			var mapFunc = Expression.Lambda<Func<TSource, TDestination>>(memberInit, sourceParam);

			return new Mapper<TSource, TDestination>(mapFunc.Compile());
		}
	}
}
