using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Shared.Enums;
using Shared.Interactions.Entities;
using Shared.Interactions.ServiceModels;

namespace Shared.Interactions.ServiceModels
{
	/// <summary>
	/// event tied to app user action
	/// ie: message read status
	/// </summary>
	public class InteractionModel
	{
		public Guid IdUser { get; set; }
		public string IdUserExternal { get; set; }
		public UserTypes UserType { get; set; }
		public DateTime Timestamp { get; set; }
		public string Payload { get; set; }

		public InteractionModel() { }

		public InteractionModel(Interaction interaction)
		{
			IdUser = interaction.IdUser;
			IdUserExternal = interaction.IdUserExternal;
			UserType = interaction.UserType ;
			Timestamp = interaction.Timestamp;
			Payload = interaction.Payload;
		}

		public Interaction ToEntity() =>
			new Interaction
			{
				IdUser = IdUser,
				IdUserExternal = IdUserExternal,
				UserType = UserType,
				Timestamp = Timestamp,
				Payload = Payload
			};
	}

}

namespace Shared.Interactions.Entities
{
	public static class InteractionExtensions
	{
		public static InteractionModel ToModel(this Interaction @this) =>
			new InteractionModel(@this);

		public static IEnumerable<InteractionModel> ToModel(this IEnumerable<Interaction> @this) =>
			@this.Select(i =>
				new InteractionModel(i));
	}
}