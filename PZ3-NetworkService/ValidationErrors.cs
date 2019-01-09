using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3_NetworkService
{
	public class ValidationErrors : BindableBase
	{
		private readonly Dictionary<string, string> validationsErrors = new Dictionary<string, string>();

		public bool IsValid
		{
			get
			{
				return validationsErrors.Count < 1;
			}
		}

		public string this[string fieldName]
		{
			get
			{
				return validationsErrors.ContainsKey(fieldName) ? validationsErrors[fieldName] : "";
			}
			set
			{
				if (validationsErrors.ContainsKey(fieldName))
				{
					if (string.IsNullOrWhiteSpace(value))
					{
						validationsErrors.Remove(fieldName);
					}
					else
					{
						validationsErrors[fieldName] = value;
					}
				}
				else
				{
					if (!string.IsNullOrWhiteSpace(value))
					{
						validationsErrors.Add(fieldName, value);
					}
				}
				OnPropertyChanged("IsValid");
			}
		}

		public void Clear()
		{
			validationsErrors.Clear();
		}
	}
}
