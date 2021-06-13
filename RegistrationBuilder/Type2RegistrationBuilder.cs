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
	internal class Type2RegistrationBuilder : RegistrationBuilderBase, IRegistrationBuilder
	{
		protected Type2RegistrationRequest RegistrationRequest { get; set; }
		protected string IdAuth { get; set; }
		public override UserTypes UserType =>
			UserTypes.Type2;

		public Type2RegistrationBuilder(Type2RegistrationRequest registration, IUserRegistrationService registrationService, IMessagingService messagingService, IInteractionService interactionService)
			: base(registrationService, messagingService, interactionService)
		{
			RegistrationRequest = registration;
		}

		public override async Task<bool> Initialization() =>
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
							.CreateAsync(IdUser, RegistrationRequest))
				: IsValid;

		public override async Task<bool> Passwordation() =>
			await base.Passwordation()
				? HandleResult(
						await RegistrationService
							.PasswordInitializeAsync(IdUser, RegistrationRequest.Password))
				: IsValid;

		public override async Task<bool> Authentication()
		{
			await base.Authentication();
			if (!IsValid) return IsValid;

			(RegistrationResult result,
				string mvdid) =
					await RegistrationService
						.AuthenticateAsync(RegistrationRequest);

			return
				HandleResult(result)
					? await SetIdMvdAsync(mvdid)
					: IsValid;
		}

		private async Task<bool> SetIdMvdAsync(string idMvd)
		{
			IdAuth = idMvd;
			return HandleResult(
				await RegistrationService
					.SetIdMvdAsync(IdUser, IdAuth)
					, "setIdAuth");
		}

		public override async Task<bool> DeviceRegistration() =>
			await base.DeviceRegistration()
				? HandleResult(
					await RegistrationService.
						RegisterDevice(
							IdUser.ToString(), 
							RegistrationRequest.Device)
						.ContinueWith(r =>
							r.Result))
				: IsValid;

		public override async Task<bool> RoleAssignation() =>
			await base.RoleAssignation()
				? HandleResult(
					RegistrationResult.FailedWith(
						UserStatus.Error,
						"role assignment is not implemented"))
				: IsValid;

		public override async Task<bool> WelcomeCommunication() =>
			await base.WelcomeCommunication()
				? HandleResult(
					RegistrationResult.ResultWith(
						isSuccess: 
							await MessagingService
								.SendWelcomeMessageAsync(IdAuth)
								.ContinueWith(r =>
									r.Result), 
						status: UserStatus.None))
				: IsValid;

		public override async Task<bool> ClaimsConfiguration() =>
			await base.ClaimsConfiguration()
				? HandleResult(
						await RegistrationService
							.AddClaimAsync(
								IdUser,
								RegistrationClaim.MemberAccess(IdAuth)))
				: IsValid;

		public override async Task<bool> Activation() =>
			await base.Activation()
				? HandleResult(
						await RegistrationService
							.SetActiveStateAsync(IdUser, true))
				: IsValid;

	}
}