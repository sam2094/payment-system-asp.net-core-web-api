using System;
using System.Collections.Generic;

namespace Common.Extensions
{
	public static class IEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> ienumerable, Action<T> action)
		{
			if (action == null) throw new ArgumentNullException("action");
			if (ienumerable == null) return;

			foreach (T ienumerableItem in ienumerable) action(ienumerableItem);
		}
	}
}
