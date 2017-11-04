using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;

namespace Admission.ViewModel.Documents
{
	internal class DocumentHostelClaim: WordDocument
	{
		public DocumentHostelClaim(Claim claim)
			: base("HostelClaim")
		{
			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Entrant",
					Value = string.Format("{0} {1} {2}",
						claim.Entrants.First().LastName,
						claim.Entrants.First().FirstName,
						claim.Entrants.First().Patronymic)
				});										
			BookmarkFields.Add(
				new DocumentField
				{
					Name = "Address",
					Value = claim.Entrants.First().Address.ViewAddress
				});

			string docData = string.Empty;
			var doc = claim.IdentityDocuments.First();
			docData = string.Format("{0} {1} {2}, выдан {3}, {4}",
				doc.IdentityDocumentType.Name,
				doc.Series,
				doc.Number,
				doc.Organization,
				((DateTime)doc.Date).ToString("dd.MM.yyyy"));

			BookmarkFields.Add(
				new DocumentField
				{
					Name = "DocumentData",
					Value = docData
				});

			FillByBookmarks();
		}
	}
}
