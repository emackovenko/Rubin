using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Admission
{
	public partial class EducationOrganization
	{
		public EducationOrganization(bool bug)
			: this()
		// сий костыль сделан ради исправления нихерового бага, 
		// описанного в SchoolCertificateDocumentEditorViewModel.cs
		{
			using (var context = new AdmissionDatabase())
			{
				var address = new Address();
				address.Country = context.Countries.First();
				context.Addresses.Add(address);
				context.SaveChanges();
				Address = address;
			}
		}

		public override string ToString()
		{
			return Name;
		}

		[NotMapped]	 
		public string IdentityName
		{
			get
			{
				string name = string.Empty;

				if (Address.LocalityId != null)
				{
					name = string.Format("{0} ({1}. {2})", Name, Address.Locality.Prefix, Address.Locality.Name);
				}
				else
				{
					if (Address.TownId != null)
					{
						name = string.Format("{0} ({1}. {2})", Name, Address.Town.Prefix, Address.Town.Name);
					}
				}

				return name;
			}
		}

	}
}
