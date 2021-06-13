using System;

namespace Utilities.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class PersistedPropertyAttribute : Attribute
	{
		public enum PersistenceType
		{
			Direct, Nested, NestedCollection
		}

		public PersistedPropertyAttribute(PersistenceType persistedType)
		{
			PersistedVia = persistedType;
		}
		public PersistedPropertyAttribute()
		{ }


		public PersistenceType PersistedVia { get; protected set; } = PersistenceType.Direct;
		public string Prefix { get; set; } = null;
		public Type TypeOverride { get; set; } = null;
		public bool SetOnInsert { get; set; } = false;
		public bool SetOnUpdate { get; set; } = false;
		public bool SetOnDelete { get; set; } = false;
		public bool SetForAllActions
		{
			get =>
				SetOnInsert && SetOnUpdate && SetOnDelete;
			set { setAll(value); }
		}

		private void setAll(bool value)
		{
			SetOnInsert = value;
			SetOnUpdate = value;
			SetOnDelete = value;
		}
	}

}
