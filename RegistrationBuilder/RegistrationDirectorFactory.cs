using System;

using Shared.Enums;
using Shared.Interactions;
using Shared.Messaging;
using Shared.Registration;
using Shared.Registration.Interfaces;
using Shared.Registration.Requests;

using VDT.Api.Mobile.Services.ApplicationUser.Builder;

namespace RegistrationBuilder
{
	internal static class RegistrationDirectorFactory
	{
		internal static RegistrationDirector InitializeDirector(
			IUserRegistrationPayload userRegistration,
			IUserRegistrationService registrationService,
			IMessagingService messagingService,
			IInteractionService interactionService
			) =>
				new RegistrationDirector(
					userRegistration.UserType switch
					{
						UserTypes.Type2 =>
							new Type2RegistrationBuilder(
								userRegistration as Type2RegistrationRequest,
								registrationService,
								messagingService,
								interactionService),
						UserTypes.Type1 =>
							new Type1RegistrationBuilder(
								userRegistration as Type1RegistrationRequest,
								registrationService,
								messagingService,
								interactionService),
						_ =>
							throw new NotImplementedException(
								userRegistration.UserType.ToString())
					});
	}
}