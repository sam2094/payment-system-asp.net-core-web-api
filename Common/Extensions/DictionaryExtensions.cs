using System;
using System.Collections.Generic;

namespace Common.Extensions
{
	public static class DictionaryExtensions
	{
		public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<KeyValuePair<TKey, TValue>> action)
		{
			if (action == null) throw new ArgumentNullException("action");
			if (dictionary == null) return;

			foreach (KeyValuePair<TKey, TValue> kvp in dictionary) action(kvp);
		}
	}
}
