using System.Linq;

namespace System.Collections.Generic
{
	public static class IEnumerableExtensions
	{

		/// <summary>
		/// Returns the entity as an IEnumerable<typeparamref name="T"/>, along with any additional objects passed as params
		/// </summary>
		/// <typeparam name="T">le type</typeparam>
		/// <param name="item">The first item in the returned IEnumerable</param>
		/// <param name="objects">Any additional item(s) to be included</param>
		/// <returns></returns>
		public static IEnumerable<T> StartEnumerable<T>(this T item, params T[] objects) =>
			objects.Prepend(item);

		private static IEnumerable<object> StartEnumerable_Usage() =>
			new object()
			.StartEnumerable(
				new object(),
				new object());

		public static IEnumerable<T> ToEnumerable<T>(this T item)
		{
			yield return item;
		}

		public static List<T> ToList<T>(this T item) =>
			new List<T> { item };

		/// <summary>
		/// Concatenates the list into a single string
		/// </summary>
		/// <typeparam name="T">.ToString() is called</typeparam>
		/// <param name="list">the values to concat</param>
		/// <param name="separator">the text inserted between each T</param>
		/// <returns></returns>
		public static string Concat<T>(this IEnumerable<T> list, string separator) =>
			string.Join(separator, list);

		/*******************************
		* shamelessly lifted from https://github.com/morelinq/MoreLINQ/blob/master/MoreLinq/DistinctBy.cs
		* *****************************/
		/// <summary>
		/// Returns all distinct elements of the given source, where "distinctness"
		/// is determined via a projection and the default equality comparer for the projected type.
		/// </summary>
		/// <remarks>
		/// This operator uses deferred execution and streams the results, although
		/// a set of already-seen keys is retained. If a key is seen multiple times,
		/// only the first element with that key is returned.
		/// </remarks>
		/// <typeparam name="TSource">Type of the source sequence</typeparam>
		/// <typeparam name="TKey">Type of the projected element</typeparam>
		/// <param name="source">Source sequence</param>
		/// <param name="keySelector">Projection for determining "distinctness"</param>
		/// <returns>A sequence consisting of distinct elements from the source sequence,
		/// comparing them by the specified key projection.</returns>

		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return source.DistinctBy(keySelector, null);
		}

		/// <summary>
		/// Returns all distinct elements of the given source, where "distinctness"
		/// is determined via a projection and the specified comparer for the projected type.
		/// </summary>
		/// <remarks>
		/// This operator uses deferred execution and streams the results, although
		/// a set of already-seen keys is retained. If a key is seen multiple times,
		/// only the first element with that key is returned.
		/// </remarks>
		/// <typeparam name="TSource">Type of the source sequence</typeparam>
		/// <typeparam name="TKey">Type of the projected element</typeparam>
		/// <param name="source">Source sequence</param>
		/// <param name="keySelector">Projection for determining "distinctness"</param>
		/// <param name="comparer">The equality comparer to use to determine whether or not keys are equal.
		/// If null, the default equality comparer for <c>TSource</c> is used.</param>
		/// <returns>A sequence consisting of distinct elements from the source sequence,
		/// comparing them by the specified key projection.</returns>

		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

			return _(); IEnumerable<TSource> _()
			{
				var knownKeys = new HashSet<TKey>(comparer);
				foreach (var element in source)
				{
					if (knownKeys.Add(keySelector(element)))
						yield return element;
				}
			}
		}
	}

}