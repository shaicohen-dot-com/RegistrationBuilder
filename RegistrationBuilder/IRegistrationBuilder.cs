using System.Threading.Tasks;

using Shared.Enums;
using Shared.Registration.Entities;

namespace RegistrationBuilder
{
	//manages user registration via the builder pattern

	/// <summary>
	/// the actions taken during registration
	/// </summary>
	internal interface IRegistrationBuilder
	{
		bool IsValid { get; }
		UserTypes UserType { get; }
		Task<bool> Initialization();
		Task<bool> Creation();
		Task<bool> Authentication();
		Task<bool> Passwordation();
		Task<bool> EmailCommunication();
		Task<bool> DeviceRegistration();
		Task<bool> RoleAssignation();
		Task<bool> ClaimsConfiguration();
		Task<bool> WelcomeCommunication();
		Task<bool> Activation();
		Task<RegistrationResult> Finalization();
	}
}