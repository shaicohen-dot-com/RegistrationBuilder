using System.Collections.Generic;

using static Utilities.ArgumentExceptionMethods;

namespace System.Collections.Generic
{
	public static class ListExtensions
	{
		/// <summary>
		/// ass 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="this"></param>
		/// <param name="items"></param>
		/// <returns></returns>
		public static List<T> AddRangeSafely<T>(this List<T> @this , IEnumerable<T> items) where T : class
		{
			if (IsArgumentException(items, true))
				return @this;

			@this.AddRange(items);
			return @this;

		}

		public static List<Guid> AddRangeSafely(this List<Guid> list, IEnumerable<Guid> enumerableToAdd) 
		{
			if (IsArgumentException(enumerableToAdd, true))
				return list;

			list.AddRange(enumerableToAdd);
			return list;

		}

		public static List<string> AddRangeSafely(this List<string> list, IEnumerable<string> enumerableToAdd) 
		{
			if (IsArgumentException(enumerableToAdd, true))
				return list;

			list.AddRange(enumerableToAdd);
			return list;

		}


		public static List<T> AddSafely<T>(this List<T> list, T toAdd) where T : class
		{
			if (toAdd != default(T))
				list.Add(toAdd);

			return list;

		}
	}
}
