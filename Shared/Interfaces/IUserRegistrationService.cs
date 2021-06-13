using System;
using System.Threading.Tasks;

using Shared.Enums;
using Shared.Registration.Entities;
using Shared.Registration.Interfaces;

namespace Shared.Interfaces
{
	public interface IUserRegistrationService
	{
		//Task<Identity.Models.ApplicationUser> RetrieveAsync(Guid idUser);
		Task<RegistrationResult> CreateAsync(Guid id, IUserRegistrationPayload registration, string email = null);
		Task<(RegistrationResult result, string mvdid)> AuthenticateAsync(IExternalAuthenticationPayload memberAuthentication);
		Task<RegistrationResult> SetIdMvdAsync(Guid idUser, string idMvd);
		Task<RegistrationResult> AddClaimAsync(Guid idUser, RegistrationClaim claim);
		Task<RegistrationResult> PasswordInitializeAsync(Guid idUser, string password);
		Task<RegistrationResult> RegisterEmailCommunication(Guid idUser, string email);
		Task<RegistrationResult> SetActiveStateAsync(Guid idUser, bool isActive);
	}
}
