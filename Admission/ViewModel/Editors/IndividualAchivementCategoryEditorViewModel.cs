using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
    public class IndividualAchivementCategoryEditorViewModel : ViewModelBase
    {
        public IndividualAchivementCategoryEditorViewModel(IndividualAchievementCategory individualAchievementCategory)
        {
            _individualAchievementCategory = individualAchievementCategory;
        }
        IndividualAchievementCategory _individualAchievementCategory;
        public IndividualAchievementCategory IndividualAchievementCategory
        {
            get
            {
                return _individualAchievementCategory;
            }
            set
            {
                _individualAchievementCategory = value;
                RaisePropertyChanged("IndividualAchievementCategory");
            }
        }
    }
}
