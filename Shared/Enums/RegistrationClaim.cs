using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using VDT.Utilities.enums;

namespace Shared.Enums
{
	public abstract class RegistrationClaim : EnumerationAsClass
	{
		#region properties

		protected string Type { get; private set; }
		protected new string Value { get; private set; }
		protected RegistrationClaim(string type, string value)
		{
			Type = type
				?? throw new ArgumentNullException(nameof(type));
			Value = value
				?? throw new ArgumentNullException(nameof(value));
		}

		//protected ClaimType(string value) { }
		//protected ClaimType(int value, string displayName) : base(value, displayName) { }

		//public ApplicationUserClaim ToAppUserClaim() =>
		//	new ApplicationUserClaim
		//	{
		//		ClaimType = Type,
		//		ClaimValue = Value
		//	};

		public Claim ToClaim() =>
			new Claim(Type, Value);

		#endregion //properties

		#region claim types

		public static RegistrationClaim MemberAccess(string value) =>
			new MemberAccessClaim(value);

		public static RegistrationClaim RefreshToken(string value) =>
			new RefreshTokenClaim(value);

		public static RegistrationClaim DeviceRegistration(string value) =>
			new DeviceRegistrationClaim(value);

		#endregion //claim types

		#region claim classes

		private class MemberAccessClaim : RegistrationClaim
		{
			private static string claimType = "mvdid";
			public MemberAccessClaim(string value) : base(claimType, value) { }
		}

		private class RefreshTokenClaim : RegistrationClaim
		{
			private static string claimType = "refreshtoken";
			public RefreshTokenClaim(string value) : base(claimType, value) { }
		}

		private class DeviceRegistrationClaim : RegistrationClaim
		{
			private static string claimType = "deviceregistration";
			public DeviceRegistrationClaim(string value) : base(claimType, value) { }
		}

		#endregion //claim classes
	}
}
