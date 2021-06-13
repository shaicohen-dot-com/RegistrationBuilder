using Shared.Enums;

namespace Shared.Device.Interfaces
{
	public interface IDeviceDetailsPayload
	{
		DevicePlatform Platform { get; }
		/// <summary>
		/// The registration id, token or URI obtained from platform-specific notification service
		/// </summary>
		string PushChannel { get; }

	}
}