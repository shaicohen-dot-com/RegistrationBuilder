using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.Enums;
using Shared.Interactions;
using Shared.Interactions.ServiceModels;
using Shared.Messaging;
using Shared.Registration;
using Shared.Registration.Entities;

namespace VDT.Api.Mobile.Services.ApplicationUser.Builder
{
	internal abstract class RegistrationBuilderBase
	{
		public abstract UserTypes UserType { get; }

		protected string TaskName { get; set; }
		protected bool IsTaskExceptionFatal { get; private set; }
		public bool IsValid { get; protected set; } = false;
		public IEnumerable<string> Errors { get; protected set; }
		protected Guid IdUser { get; set; }
		protected IUserRegistrationService RegistrationService { get; set; }
		protected IMessagingService MessagingService { get; set; }
		protected IInteractionService InteractionService { get; set; }


		public RegistrationBuilderBase(IUserRegistrationService registrationService, IMessagingService messagingService, IInteractionService interactionService)
		{
			RegistrationService = registrationService
				?? throw new ArgumentNullException(nameof(registrationService));
			MessagingService = messagingService
				?? throw new ArgumentNullException(nameof(messagingService));
			InteractionService = interactionService
				?? throw new ArgumentNullException(nameof(interactionService));
			IsValid = true;
		}

		//follows the -ation pattern
		public virtual Task<bool> Initialization()
		{
			TaskName = "initialization";
			IsTaskExceptionFatal = true;
			IdUser = Guid.NewGuid();
			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> Creation()
		{
			TaskName = "creation";
			IsTaskExceptionFatal = true;

			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> Passwordation()
		{
			TaskName = "passwordation";
			IsTaskExceptionFatal = true;

			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> Authentication()
		{
			TaskName = "authentication";
			IsTaskExceptionFatal = true;
		
			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> PhoneNumberAssociation()
		{
			TaskName = "phone number association";
			IsTaskExceptionFatal = false;
		
			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> EmailCommunication()
		{
			TaskName = "emailcommunication";
			IsTaskExceptionFatal = true;
		
			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> ClaimsConfiguration()
		{
			TaskName = "claimsconfiguration";
			IsTaskExceptionFatal = true;

			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> DeviceRegistration()
		{
			TaskName = "deviceregistration";
			IsTaskExceptionFatal = false;

			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> RoleAssignation()
		{
			TaskName = "roleassignation";
			IsTaskExceptionFatal = true;

			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> Activation()
		{
			TaskName = "activation";
			IsTaskExceptionFatal = true;

			return Task.FromResult(IsValid);
		}

		public virtual Task<bool> WelcomeCommunication()
		{
			TaskName = "welcomecommunication";
			IsTaskExceptionFatal = false;

			return Task.FromResult(IsValid);
		}

		public async Task<RegistrationResult> Finalization() =>
			await Task.FromResult(
				IsValid
					? RegistrationResult
						.SuccessWith(
							UserStatus.OK, 
							IdUser.ToString())
					: RegistrationResult.Failed);

		/// <summary>
		/// sets properties 'IsValid' and 'Errors' based on 'identityResult'
		/// records the task in interactions
		/// </summary>
		/// <param name="identityResult"></param>
		/// <returns></returns>
		protected bool HandleResult(RegistrationResult identityResult, string taskName = "")
		{
			IsValid =
				(identityResult.Succeeded, IsTaskExceptionFatal) switch
				{
					(true, _) => true,
					(false, false) => true,
					(false, true) => false
				};

			if (!IsValid && !(identityResult?.Errors is null))
				Errors = identityResult.Errors;

			_ = RecordTaskStage(taskName);

			return IsValid;
		}

		protected async Task RecordTaskStage(string taskName = "") =>
			await InteractionService.CreateAsync(
				new InteractionModel
				{
					IdUser = IdUser,
					UserType = UserType,
					Timestamp = DateTime.UtcNow,
					Payload =
						$"registration|" +
						$"{IsValid}|" +
						$"{(string.IsNullOrWhiteSpace(taskName) ? TaskName : taskName)}|" +
						$"{IsTaskExceptionFatal}|" +
						$"{string.Join(",", Errors ?? new string[0])}"
				});


	}
}