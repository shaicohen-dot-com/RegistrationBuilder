using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Device
{
	public interface IDeviceManagementService
	{
		Task NotifyAsync(string idExternal, string message);
	}
}