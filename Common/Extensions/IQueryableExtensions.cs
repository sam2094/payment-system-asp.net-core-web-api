using System;
using System.Linq;

namespace Common.Extensions
{
	public static class IQueryableExtensions
	{
		public static void ForEach<T>(this IQueryable<T> iqueryable, Action<T> action)
		{
			if (action == null) throw new ArgumentNullException("action");
			if (iqueryable == null) return;

			foreach (T iqueryableItem in iqueryable) action(iqueryableItem);
		}
	}
}
