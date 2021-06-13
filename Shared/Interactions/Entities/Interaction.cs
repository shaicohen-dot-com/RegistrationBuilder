using System;

using Shared.Enums;

using Utilities.Attributes;

namespace Shared.Interactions.Entities
{

	public class Interaction
	{
		public Guid Id { get; set; }
		[PersistedProperty(SetOnInsert = true)]
		public Guid IdUser { get; set; }
		[PersistedProperty(SetOnInsert = true)]
		public string IdUserExternal { get; set; }
		[PersistedProperty(SetOnInsert = true)]
		public UserTypes UserType { get; set; }
		[PersistedProperty(SetOnInsert = true)]
		public DateTime Timestamp { get; set; }
		[PersistedProperty(SetOnInsert = true)]
		public string Payload { get; set; }
	}
}