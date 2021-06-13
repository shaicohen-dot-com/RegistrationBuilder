using System;
using System.ComponentModel.DataAnnotations;

using Shared.Device.Interfaces;
using Shared.Enums;
using Shared.Registration.Interfaces;

namespace Shared.Registration.Requests
{
	public class Type2RegistrationRequest : IType2AuthenticationPayload, IUserRegistrationPayload, IUserPasswordPayload, IDeviceDetailsPayload
	{
		public UserTypes UserType =>
			UserTypes.Type2;

		[Required]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
		public string UserName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		public string Password { get; set; }

		[Required]
		public string IdMember { get; set; }

		[Required]
		public string MemberName { get; set; }

		[Required]
		[StringLength(4, ErrorMessage = "The {0} must be {2} characters long.", MinimumLength = 4)]
		public string LastFourSsn { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		[Required]
		public IDeviceDetailsPayload Device { get; set; }

		public DevicePlatform Platform =>
			Device.Platform;

		public string PushChannel =>
			Device.PushChannel;
	}
}