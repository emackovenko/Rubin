using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exNumeric;

namespace Admission.ViewModel.Documents
{
	internal class DocumentIndividualAchievementsProtocol: WordDocument
	{
		public DocumentIndividualAchievementsProtocol(Claim claim)
			: base("IndividualAchievementsProtocol")
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
					Name = "BallsCountNumber",
					Value = claim.IndividualAchievementsScore.ToString()
				});
	 
			BookmarkFields.Add(
				new DocumentField
				{
					Name = "BallsCountString",
					Value = ((int)claim.IndividualAchievementsScore).ScoreString()
				});

			FillByBookmarks();
		}
	}
}
