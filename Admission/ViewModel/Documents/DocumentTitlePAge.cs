using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;

namespace Admission.ViewModel.Documents
{
	internal class DocumentTitlePage : WordDocument
	{
		public DocumentTitlePage(Claim claim) 
			: base("TitlePage")
		{
			BookmarkFields.Add(
				new DocumentField
				{
					Name = "ClaimNumber",
					Value = claim.Number
				});
			BookmarkFields.Add(
				new DocumentField
				{
					Name = "LastName",
					Value = claim.Entrants.First().LastName
				});
			BookmarkFields.Add(
				new DocumentField
				{
					Name = "FirstName",
					Value = string.Format("{0} {1}", claim.Entrants.First().FirstName, claim.Entrants.First().Patronymic)
				});
			FillByBookmarks();
		}
	}
}
