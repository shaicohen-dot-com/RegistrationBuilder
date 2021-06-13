using Shared.Device.Interfaces;

namespace Shared.Registration.Entities
{
	public interface IDevicePayload
	{
		IDeviceDetailsPayload Device { get; }
	}
}