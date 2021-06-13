namespace System
{
	public static class ArrayExtensions
	{
		/// <summary>
		/// returns the instance as a typed array, along with any additional instances passed as params
		/// </summary>
		/// <typeparam name="T">le type</typeparam>
		/// <param name="this">The first item in the returned IEnumerable</param>
		/// <param name="additionalInstances">Any additional item(s) to be included</param>
		/// <returns></returns>
		//public static T[] StartArray<T>(this T @this, params T[] additionalInstances) =>
		//	 additionalInstances.Prepend(@this);

		//additionalInstances.ToList().Insert(0, @this);
			//new T[1] { @this }.Append(additionalInstances);
			//new T[] { @this }. . @this.ToArray().  additionalInstances.Prepend(@this).ToArray();
			//additionalInstances.ToEnumerable().Prepend(@this).ToArray();

		public static T[] ToArray<T>(this T @this) =>
			new T[] { @this };

	}
}
