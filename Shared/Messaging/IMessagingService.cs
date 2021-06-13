using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Messaging
{
	public interface IMessagingService
	{
		Task<bool> SendWelcomeMessageAsync(string mvdid);
	}
}