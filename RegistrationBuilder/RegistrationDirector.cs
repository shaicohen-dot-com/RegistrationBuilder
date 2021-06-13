using System;
using System.Threading.Tasks;

using RegistrationBuilder;

using Shared.Enums;
using Shared.Registration.Entities;

namespace VDT.Api.Mobile.Services.ApplicationUser.Builder
{
	internal class RegistrationDirector
	{
		public IRegistrationBuilder Builder { get; set; }
		public RegistrationDirector(IRegistrationBuilder builder)
		{
			Builder = builder
				?? throw new ArgumentNullException(nameof(builder));
		}

		public async Task<RegistrationResult> RegisterAsync() =>
			Builder.UserType switch
			{
				UserTypes.Type2=>
					await RegisterType2Async(),
				UserTypes.Type1 =>
					await RegisterType1Async(),
				UserTypes.None =>
					throw new ArgumentOutOfRangeException(nameof(Builder.UserType), "Cannot be set to 'None'."),
				_ =>
					throw new NotImplementedException(Builder.UserType.ToString())
			};
	
		private async Task<RegistrationResult> RegisterType2Async()
		{ 
			await Builder.Initialization();
			await Builder.Creation();
			await Builder.Authentication();
			await Builder.Passwordation();
			await Builder.ClaimsConfiguration();
			await Builder.DeviceRegistration();
			await Builder.Activation();
			await Builder.WelcomeCommunication();
			return
				await Builder.Finalization();
		}

		private async Task<RegistrationResult> RegisterType1Async()
		{ 
			await Builder.Initialization();
			await Builder.Creation();
			await Builder.Passwordation();
			await Builder.EmailCommunication();
			await Builder.DeviceRegistration();
			await Builder.Activation();
			return
				await Builder.Finalization();
		}
	}
}