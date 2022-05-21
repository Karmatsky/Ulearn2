using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
		/// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
		{
			var list = items.ToList();
			if (list.Count == 0)
            {
				throw new InvalidOperationException();
			}

			list.Sort();

			if (list.Count % 2 == 1)
            {
				return list[list.Count / 2];
			}
			return (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2;
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var itr = items.GetEnumerator();
			itr.MoveNext();
			var past = itr.Current;

			while (itr.MoveNext())
			{
				yield return Tuple.Create(past, itr.Current);
				past = itr.Current;
			}
		}
	}
}