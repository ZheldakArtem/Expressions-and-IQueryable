using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
	public class Mapper<TSource, TDestination>
	{
		private Func<TSource, TDestination> mFunc;
		public Mapper(Func<TSource, TDestination> func)
		{
			mFunc = func;
		}

		public TDestination Map(TSource source)
		{
			return mFunc(source);
		}
	}
}
