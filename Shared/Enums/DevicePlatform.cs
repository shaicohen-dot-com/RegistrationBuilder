namespace Shared.Enums
{
	/// <summary>
	/// Supported Intallation Platforms
	/// <para>
	/// important: the backing integer maps directly to the NotificationHub.NotificationPlatform enum 
	/// </para>
	/// </summary>
	public enum DevicePlatform
	{
		None = 0,
		/// <summary>
		/// Apns
		/// </summary>
		IOS = 2,
		/// <summary>
		/// FCM
		/// </summary>
		Android = 4
	}

}
