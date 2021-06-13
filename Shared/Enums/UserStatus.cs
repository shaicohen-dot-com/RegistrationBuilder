using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
	public enum UserStatus
	{
		None = 0,
		OK = 1,
		NotFound = 2,
		NotActive = 3,
		RegistrationExists = 8,
		ExternalNotFound = 4,
		ExternalNotAuthorized = 5,
		PasswordExpired = 6,
		Error = 7
	}
}
