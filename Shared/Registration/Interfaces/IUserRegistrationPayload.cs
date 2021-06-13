using Shared.Enums;

namespace Shared.Registration.Interfaces
{
	public interface IUserRegistrationPayload
	{
		string UserName { get; set; }
		UserTypes UserType { get; }
	}
}