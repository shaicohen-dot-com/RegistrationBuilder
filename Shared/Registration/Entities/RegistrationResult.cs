using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Shared.Enums;

namespace Shared.Registration.Entities
{

	/// <summary>
	/// {className}.Split(CapitalizedWords.Last()).Output($"the {[1] ?? "doohicky"} of a {[0]}")
	/// </summary>
	public class RegistrationResult
	{
		private static readonly RegistrationResult _success = new RegistrationResult(true, userStatus: UserStatus.OK);
		private static readonly RegistrationResult _failed = new RegistrationResult(false);

		public bool Succeeded { get; private set; }
		public IEnumerable<string> Errors { get; private set; }
		public IEnumerable<string> Notes => Errors;
		public UserStatus StatusCode { get; set; } = UserStatus.None;

		/// <summary>
		/// Failure constructor that takes error messages
		/// </summary>
		//public RegistrationResult(UserStatus status, params string[] errors)
		//		: this(status, (IEnumerable<string>)errors)
		//{ }

		/// <summary>
		/// constructor that takes an IdentityResult
		/// </summary>
		/// <param name="errors"></param>
		protected RegistrationResult(bool succeeded, UserStatus userStatus = UserStatus.None, IEnumerable<string> errors = null)
		{
			Succeeded = succeeded;
			StatusCode = userStatus;
			Errors =
				errors
					?? defaultErrors();
		}

		private static string[] defaultErrors() =>
			new string[1] { "An error occurred" };

		/// <summary>
		/// failure constructor that takes ValidationResults
		/// </summary>
		/// <param name="errors"></param>
		//public RegistrationResult(UserStatus status, IEnumerable<RegistrationResult> validationResults)
		//{
		//	Succeeded = false;
		//	StatusCode = status;
		//	Errors = validationResults?.Select(v => v.ErrorMessage) ?? new string[1] { "An error occurred" };
		//}

		/// <summary>
		/// Failure constructor that takes error messages
		/// </summary>
		/// <param name="errors"></param>
		//public RegistrationResult(UserStatus status, IEnumerable<string> errors)
		//{
		//	if (errors == null)
		//		errors = new string[1] { "An error occurred" };

		//	Succeeded = false;
		//	StatusCode = status;
		//	Errors = errors;
		//}

		/// <summary>
		/// Static success result
		/// </summary>
		public static RegistrationResult Success => _success;

		/// <summary>
		/// Static failed result
		/// </summary>
		public static RegistrationResult Failed => _failed;

		/// <summary>
		/// Constructor that takes whether the result is successful
		/// </summary>
		/// <param name="success"></param>
		//protected RegistrationResult(UserStatus status, bool success)
		//{
		//	Succeeded = success;
		//	StatusCode = status;
		//	Errors = new string[0];
		//}

		public static RegistrationResult ResultWith(bool isSuccess, UserStatus status, string info = "") =>
			new RegistrationResult(isSuccess, userStatus: status, errors: new string[] { info });

		public static RegistrationResult SuccessWith(UserStatus status, string info) =>
			new RegistrationResult(false, userStatus: status, errors: new string[] { info });

		/// <summary>
		/// Failed helper method
		/// </summary>
		/// <param name="errors"></param>
		/// <returns></returns>
		public static RegistrationResult FailedWith(UserStatus status, params string[] errors) =>
			new RegistrationResult(false, userStatus: status, errors: errors);

		public static RegistrationResult FailedWith(UserStatus status, IEnumerable<string> errors) =>
			new RegistrationResult(false, userStatus: status, errors: errors);

		public static RegistrationResult FailedWith(UserStatus status, IEnumerable<ValidationResult> validationResults) =>
			new RegistrationResult(
				false,
				userStatus: status,
				errors:
					validationResults
						.SelectMany(r =>
							r.MemberNames
								.Select(n =>
									$"{n}: {r.ErrorMessage}")));

	}
}
