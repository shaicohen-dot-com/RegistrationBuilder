using System;

namespace Shared.Registration.Interfaces
{
	public interface IType2AuthenticationPayload
	{
		DateTime DateOfBirth { get; set; }
		string IdMember { get; set; }
		string LastFourSsn { get; set; }
		string MemberName { get; set; }
	}

}