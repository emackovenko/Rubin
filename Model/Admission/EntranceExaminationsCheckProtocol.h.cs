using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class EntranceExaminationsCheckProtocol
	{
		public string Examination
		{
			get
			{
				if (EntranceTests.Count > 0)
				{
					var exam = EntranceTests.First();
					return string.Format("{0} от {1}", 
						exam.ExamSubject.Name, ((DateTime)exam.ExaminationDate).ToString("dd.MM.yyyy г."));
				}
				else
				{
					return "ЕГЭ";
				}
			}
		}
	}
}
