using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Utilities
{
	public static class Validation
	{

		public static bool Validate(object instance, out List<ValidationResult> resultsReturned)
		{
			ICollection<ValidationResult> results = new List<ValidationResult>(); //the results of the validation
			ValidationContext vc = new ValidationContext(instance);
			if (Validator.TryValidateObject(instance, vc, results, true))
			{
				resultsReturned = null;
				return true;
			}
			else
			{
				resultsReturned = (List<ValidationResult>)results;
				return false;
			}
		}


	}
}
