using System;
using System.ComponentModel.DataAnnotations;

using Shared.Device.Interfaces;
using Shared.Enums;
using Shared.Registration.Interfaces;

namespace Shared.Registration.Requests
{
	/// <summary>
	/// The information required for new type 1 registration
	/// </summary>
	public class Type1RegistrationRequest : IUserRegistrationPayload, IUserPasswordPayload, IUserEmailPayload, IDeviceDetailsPayload
	{
		public UserTypes UserType =>
			UserTypes.Type1;

		[Required]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
		public string UserName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		public string Password { get; set; }

		[Required(AllowEmptyStrings = false)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public IDeviceDetailsPayload Device { get; set; }

		public DevicePlatform Platform =>
			Device.Platform;

		public string PushChannel =>
			Device.PushChannel;
	}
}