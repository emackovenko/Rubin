using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;							
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Admission
{
	public partial class SchoolCertificateDocument
	{
		public SchoolCertificateDocument()
		{
			Date = DateTime.Now;
			FiveCount = 0;
			FourCount = 0;
			ThreeCount = 0;
		}

		[NotMapped]
		public bool IsOriginal
		{
			get
			{
				return OriginalReceivedDate != null;
			}
			set
			{
				if (value)
				{
					OriginalReceivedDate = DateTime.Now;
				}
				else
				{
					OriginalReceivedDate = null;
				}
			}
		}

		[NotMapped]
		public double MiddleMark
		{
			get
			{
				double result = 0.0;

				double c5 = FiveCount ?? 0;
				double c4 = FourCount ?? 0;
				double c3 = ThreeCount ?? 0;

				try
				{
					result = Math.Round(((c5 * 5 + c4 * 4 + c3 * 3) / (c5 + c4 + c3)), 2);
				}
				catch (Exception)
				{
					result = 0;
				}

				return result;
			}
		}

		[NotMapped]
		public int GraduationYear
		{
			get
			{
				return ((DateTime)Date).Year;
			}
		}

	}
}
