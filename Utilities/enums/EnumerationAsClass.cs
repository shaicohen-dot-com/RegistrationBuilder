using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VDT.Utilities.enums
{
	public abstract class EnumerationAsClass : IComparable
	{
		private readonly int _value;
		private readonly string _displayName;

		protected EnumerationAsClass()
		{
		}

		protected EnumerationAsClass(int value, string displayName)
		{
			_value = value;
			_displayName = displayName;
		}

		public int Value
		{
			get { return _value; }
		}

		public string DisplayName
		{
			get { return _displayName; }
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public static IEnumerable<T> GetAll<T>() where T : EnumerationAsClass, new()
		{
			Type type = typeof(T);
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

			foreach (FieldInfo info in fields)
			{
				T instance = new T();
				T locatedValue = info.GetValue(instance) as T;

				if (locatedValue != null)
				{
					yield return locatedValue;
				}
			}
		}

		public override bool Equals(object obj)
		{
			var otherValue = obj as EnumerationAsClass;

			if (otherValue == null)
			{
				return false;
			}

			var typeMatches = GetType().Equals(obj.GetType());
			var valueMatches = _value.Equals(otherValue.Value);

			return typeMatches && valueMatches;
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		public static int AbsoluteDifference(EnumerationAsClass firstValue, EnumerationAsClass secondValue)
		{
			var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
			return absoluteDifference;
		}

		public static T FromValue<T>(int value) where T : EnumerationAsClass, new()
		{
			var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
			return matchingItem;
		}

		public static T FromDisplayName<T>(string displayName) where T : EnumerationAsClass, new()
		{
			var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
			return matchingItem;
		}

		private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : EnumerationAsClass, new()
		{
			var matchingItem = GetAll<T>().FirstOrDefault(predicate);

			if (matchingItem == null)
			{
				var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
				throw new ApplicationException(message);
			}

			return matchingItem;
		}

		public int CompareTo(object other)
		{
			return Value.CompareTo(((EnumerationAsClass)other).Value);
		}
	}
}
