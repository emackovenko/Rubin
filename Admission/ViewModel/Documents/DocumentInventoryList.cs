using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;

namespace Admission.ViewModel.Documents
{
	internal class DocumentInventoryList: WordDocument
	{
		public DocumentInventoryList(Claim claim)
			: base("InventoryList")
		{
			BookmarkFields.Add(
				new DocumentField
				{
					Name = "ClaimNumber",
					Value = claim.Number
				});
			FillByBookmarks();
		}
	}
}
