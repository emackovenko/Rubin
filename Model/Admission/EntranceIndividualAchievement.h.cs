using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Model.Admission
{
	public partial class EntranceIndividualAchievement
	{

		public ObservableCollection<CampaignIndividualAchievement> AvailableAchievements
		{
			get
			{
				var context = new AdmissionDatabase();
				return new ObservableCollection<CampaignIndividualAchievement>(context.CampaignIndividualAchievements);
			}
		}

	}
}
