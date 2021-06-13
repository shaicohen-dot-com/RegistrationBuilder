using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

using Shared.Device.Interfaces;
using Shared.Enums;
using Shared.Registration.Entities;
using Shared.Registration.Interfaces;
namespace Shared.Registration
{
	public interface IUserRegistrationService
	{

		//Task<Identity.Models.ApplicationUser> RetrieveAsync(Guid idUser);
		//Task<RegistrationResult> CreateAsync(Guid id, IUserRegistrationPayload registration, string email = null);
		//Task<(RegistrationResult result, string mvdid)> AuthenticateAsync(IPlanMemberAuthenticationPayload memberAuthentication);
		//Task<RegistrationResult> SetIdMvdAsync(Guid idUser, string idMvd);
		//Task<RegistrationResult> AddClaimAsync(Guid idUser, ClaimType claim);
		//Task<RegistrationResult> PasswordInitializeAsync(Guid idUser, string password);
		//Task<RegistrationResult> RegisterEmailCommunication(Guid idUser, string email);
		//Task<RegistrationResult> SetActiveStateAsync(Guid idUser, bool isActive);
		//Task<RegistrationResult> RegisterDevice(string idUser, IDeviceDetailsPayload request);
		//Task<RegistrationResult> RegisterDevice(IRegisterDeviceRequest request);


		//Task<Identity.Models.ApplicationUser> RetrieveAsync(Guid idUser);
		Task<RegistrationResult> CreateAsync(Guid id, IUserRegistrationPayload registration, string email = null);
		Task<(RegistrationResult result, string mvdid)> AuthenticateAsync(IType2AuthenticationPayload memberAuthentication);
		Task<RegistrationResult> SetIdMvdAsync(Guid idUser, string idMvd);
		Task<RegistrationResult> AddClaimAsync(Guid idUser, RegistrationClaim claim);
		Task<RegistrationResult> PasswordInitializeAsync(Guid idUser, string password);
		Task<RegistrationResult> RegisterEmailCommunication(Guid idUser, string email);
		Task<RegistrationResult> SetActiveStateAsync(Guid idUser, bool isActive);
		Task<RegistrationResult> RegisterDevice(string idUser, IDeviceDetailsPayload request);
	}
}
