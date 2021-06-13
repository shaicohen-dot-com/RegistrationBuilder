using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utilities
{
	public static class ArgumentExceptionMethods
	{

		public static bool IsArgumentException(string value) =>
			 string.IsNullOrWhiteSpace(value);

		public static bool IsArgumentException(Guid value) =>
			 value == default;

		public static bool IsArgumentException<T>(T value) where T : class =>
			value is default(T);

		public static bool IsArgumentException<T>(T value, int invalidSelection = 0) where T : Enum =>
			value.GetHashCode() == invalidSelection;

		/// <summary>
		/// throws an exceptions if 
		/// <para><paramref name="value"/> -> null</para>
		/// <para><paramref name="value"/> -> any items null</para>
		/// <para>if <paramref name="isEmptyValid"/> is true => count < 1</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsArgumentException<T>(IEnumerable<T> value, bool isEmptyValid = true) where T : class =>
			value?.Count() switch
			{
				null => true,
				0 => isEmptyValid,
				_ => value //validate collection contents
							.Any(v =>
								IsArgumentException(v))
			}; 

		public static bool IsArgumentException(IEnumerable<Guid> value, bool isEmptyValid = true)=>
			value?.Count() switch
			{
				null => true,
				0 => isEmptyValid,
				_ => value //validate collection contents
							.Any(v =>
								IsArgumentException(v))
			}; 

		public static bool IsArgumentException(IEnumerable<string> value, bool isEmptyValid = true)=>
			value?.Count() switch
			{
				null => true,
				0 => isEmptyValid,
				_ => value //validate collection contents
							.Any(v =>
								IsArgumentException(v))
			}; 

		//value is null
		//	? true //validate collection is not null
		//	: value.Count() > 0 
		//		? !isEmptyValid //conditionally validate count
		//		: value
		//				.Any(v => 
		//					IsArgumentException(v)); //validate collection contents

		//public static void ThrowOnArgumentException(Guid value, string argumentName = null)
		//{
		//	if (IsArgumentException(value))
		//		throw new ArgumentException(ArgumentName(argumentName));
		//}

		/// <summary>
		/// The following will result in an argument exception being thrown
		/// <para>- default(T)</para>
		/// </summary>
		public static void ThrowOnArgumentException<T>(Expression<Func<T>> expression) where T : class
		{
			T value = expression.Compile()();
			if (IsArgumentException(value))
				throw new ArgumentException(ArgumentName(expression));
		}

		//public static void ThrowOnArgumentException(Expression<Func<object>>[] expression) 
		//{
		//	expression.ToList().ForEach(e => ThrowOnArgumentException(e));
		//}

		/// <summary>
		/// The following will result in an argument exception being thrown
		/// <para>- default(T)</para>
		/// </summary>
		public static void ThrowOnArgumentException<T>(Expression<Func<T>> expression, int invalidSelection = 0) where T : Enum
		{
			T value = expression.Compile()();
			if (IsArgumentException(value, invalidSelection))
				throw new ArgumentException(ArgumentName(expression));
		}

		/// <summary>
		/// The following will result in an argument exception being thrown
		/// <para>- default(T)</para>
		/// </summary>
		public static async Task ThrowOnArgumentExceptionAsync<T>(Expression<Func<T>> expression) where T : class
		{
			T value = expression.Compile()();
			if (IsArgumentException(value))
				throw new ArgumentException(ArgumentName(expression));
			await Task.FromResult(value);
		}

		/// <summary>
		/// The following will result in an argument exception being thrown
		/// <para>- a value of Guid.Empty</para>
		/// </summary>
		/// <param name="expression"></param>
		public static void ThrowOnArgumentException(Expression<Func<Guid>> expression)
		{
			Guid value = expression.Compile()();
			if (IsArgumentException(value))
				throw new ArgumentException(ArgumentName(expression));
		}

		/// <summary>
		/// The following will result in an argument exception being thrown
		/// <para>- String.IsNullOrWhiteSpace(value)</para>
		/// </summary>
		public static void ThrowOnArgumentException(Expression<Func<string>> expression)
		{
			string value = expression.Compile()();
			if (IsArgumentException(value))
				throw new ArgumentException(ArgumentName(expression));
		}


		//public static void ThrowOnArgumentException(string value, string argumentName = null)
		//{
		//	if (IsArgumentException(value))
		//		throw new ArgumentException(ArgumentName(argumentName));
		//}

		//public static void ThrowIfArgumentException(this Guid guid, string argumentName = null)
		//{
		//	if (guid.IsSet().Not())
		//		throw new ArgumentException(ArgumentName());

		//	string ArgumentName() =>
		//		argumentName ?? nameof(guid);
		//}

		//public static void ThrowIfArgumentException(string value, string argumentName = null)
		//{
		//	if (String.IsNullOrWhiteSpace(value))
		//		throw new ArgumentException(ArgumentName());

		//	string ArgumentName() =>
		//		argumentName ?? nameof(value);
		//}

		//public static void ThrowOnArgumentException(IEnumerable<string> value, string argumentName = null)
		//{
		//	//collection is null
		//	if (value == null) throw new ArgumentNullException(ArgumentName(argumentName));
		//	//collection is empty
		//	if (value.Count() == 0) throw new ArgumentException($"{ArgumentName(argumentName)} does not contain any items");
		//	//collection items are valid
		//	foreach (var item in value)
		//		IsArgumentException<string>(item);

		//}


		//public static void ThrowOnArgumentException<T>(IEnumerable<T> value, string argumentName = null) where T : class
		//{
		//	if (IsArgumentException(value))
		//		throw new ArgumentException(ArgumentName(argumentName));
		//}

		//public static void ThrowOnArgumentException<T>(T value, string argumentName = null) where T : class
		//{
		//	if (IsArgumentException(value))
		//		throw new ArgumentException(ArgumentName(argumentName));
		//}
		//public static bool ArgumentException<T>(IEnumerable<T> value, string argumentName = null) where T : class
		//{
		//	//collection is null
		//	if (value == null) throw new ArgumentNullException($"{ArgumentName(argumentName)}");
		//	//collection has no items
		//	if (value.Count() == 0) throw new ArgumentException($"{ArgumentName(argumentName)} does not contain any items");
		//	//no unset items in collection 
		//	if (value.Any(v => v == default(T))) throw new ArgumentException($"{ArgumentName(argumentName)} contains invalid items");
		//}


		//private static string ArgumentName(string value) =>
		//	value ?? nameof(value);

		private static string ArgumentName<T>(Expression<Func<T>> expression) =>
			((MemberExpression)expression.Body).Member.Name;

		//public static void ThrowIfArgumentException<T>(this IEnumerable<T> enumerableT, string argumentName = null) where T : struct
		//{
		//	string ArgumentName() =>
		//		argumentName ?? nameof(enumerableT);

		//	if (enumerableT.Count() == 0)
		//		throw new ArgumentException($"{ArgumentName()} contains no elements.", ArgumentName());
		//	if(enumerableT.Any(c => c.IsSet().Not()))
		//		throw new ArgumentException($"{ArgumentName()} contains an invalid element.", ArgumentName());
		//}

		//public static void ThrowIfArgumentException<T>(this IEnumerable<T> enumerableT, string argumentName = null) where T : struct
		//{
		//	string ArgumentName() =>
		//		argumentName ?? nameof(enumerableT);

		//	if (enumerableT.Count() == 0)
		//		throw new ArgumentException($"{ArgumentName()} contains no elements.", ArgumentName());
		//	if (enumerableT.Any(c => c.IsSet().Not()))
		//		throw new ArgumentException($"{ArgumentName()} contains an invalid element.", ArgumentName());
		//}

		public static bool IsSet(this Guid value) =>
			!value.Equals(default);

		//public static class MVDIDValidation
		//{
		//	public static bool IsValid(string MVDID) =>
		//		(!string.IsNullOrEmpty && MVDID.i)

		//}

	}
}
