using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using RegistrationBuilder;

using Shared.Enums;
using Shared.Interactions;
using Shared.Messaging;
using Shared.Registration;
using Shared.Registration.Entities;
using Shared.Registration.Requests;

using static Utilities.Validation;

namespace VDT.Api.Mobile.Services.ApplicationUser.Builder
{


	internal class Type1RegistrationBuilder : RegistrationBuilderBase, IRegistrationBuilder
	{
		protected Type1RegistrationRequest RegistrationRequest { get; set; }

		public override UserTypes UserType => 
			UserTypes.Type1;

		public Type1RegistrationBuilder(Type1RegistrationRequest registration, IUserRegistrationService registrationService, IMessagingService messagingService, IInteractionService interactionService)
			: base(registrationService, messagingService, interactionService)
		{
			RegistrationRequest = registration;
		}

		public async override Task<bool> Initialization() =>
			await base.Initialization()
				? Validate(
						RegistrationRequest,
						out List<ValidationResult> validationResults)
					? HandleResult(RegistrationResult.Success)
					: HandleResult(
							RegistrationResult.FailedWith(
								UserStatus.Error,
								validationResults
									?? new List<ValidationResult>()))
				: IsValid;

		public override async Task<bool> Creation() =>
			await base.Creation()
				? HandleResult(
						await RegistrationService
							.CreateAsync(IdUser, RegistrationRequest, RegistrationRequest.Email))
				: IsValid;

		public async override Task<bool> Passwordation() =>
			await base.Passwordation()
				? HandleResult(
					await RegistrationService
						.PasswordInitializeAsync(IdUser, RegistrationRequest.Password))
				: IsValid;

		public async override Task<bool> PhoneNumberAssociation() =>
			await base.PhoneNumberAssociation()
				? HandleResult(
					await RegistrationService
						.PasswordInitializeAsync(IdUser, RegistrationRequest.Password))
				: IsValid;

		public async override Task<bool> EmailCommunication() =>
			await base.EmailCommunication()
				? HandleResult(
					await RegistrationService
						.RegisterEmailCommunication(IdUser, RegistrationRequest.Email))
				: IsValid;

		public async override Task<bool> DeviceRegistration() =>
			await base.DeviceRegistration()
				? HandleResult(
				await RegistrationService
					.RegisterDevice(
						IdUser.ToString(), 
						RegistrationRequest.Device)
					.ContinueWith(r =>
						r.Result))
				: IsValid;

		public async override Task<bool> RoleAssignation() =>
			await base.RoleAssignation()
				? HandleResult(
					RegistrationResult.FailedWith(
						UserStatus.Error,
						"role assignment is not implemented"))
				: IsValid;

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async override Task<bool> ClaimsConfiguration() =>
			await base.ClaimsConfiguration();

		public async override Task<bool> Activation() =>
			await base.Activation()
				? HandleResult(
						await RegistrationService
							.SetActiveStateAsync(IdUser, true))
				: IsValid;

	}
}