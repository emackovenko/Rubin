using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Admission.ViewModel.ValidationRules.Validators
{
	public interface IEntityValidator
	{
		bool IsValid { get; }
		
		List<string> ErrorList { get; set; }
	}
}
