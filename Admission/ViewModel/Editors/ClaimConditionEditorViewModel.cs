using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
	public class ClaimConditionEditorViewModel: ViewModelBase
	{	 

		#region	Constructors

		public ClaimConditionEditorViewModel(ClaimCondition condition)
		{
			_condition = condition;
			EducationForm = _condition.CompetitiveGroup.EducationForm;
			FinanceSource = _condition.CompetitiveGroup.FinanceSource;
			Direction = _condition.CompetitiveGroup.Direction;
		}

		#endregion

		#region Parents

		ClaimCondition _condition;

		public ClaimCondition Condition
		{
			get
			{	 
				return _condition;
			}

			set
			{
				_condition = value;
				RaisePropertyChanged("Condition");
			}
		}
			   
		void SetClaimCondition()
		{
			if (EducationForm != null && 
					FinanceSource != null && 
					Direction != null)
			{
				Condition.CompetitiveGroup =
				(from cg in Session.DataModel.CompetitiveGroups.Where(cg => cg.Campaign.CampaignStatusId == 2)
				 where cg.EducationFormId == EducationForm.Id &&
					cg.FinanceSourceId == FinanceSource.Id &&
					cg.DirectionId == Direction.Id
				 select cg).FirstOrDefault();
			}  
		}

		#endregion

		#region FilterEntities

		EducationForm _educationForm;
		FinanceSource _financeSource;
		Direction _direction;
        DirectionProfile _directionProfile;

		public EducationForm EducationForm
		{
			get
			{
				if (_educationForm == null)
				{
					_educationForm = Session.DataModel.EducationForms.FirstOrDefault();
				}
				return _educationForm;
			}

			set
			{
				_educationForm = value;

				if (value != null)
				{
					RefreshFinSources();
					RefreshDirections();
					SetClaimCondition();
				}

				RaisePropertyChanged("EducationForm");
			}
		}

		public FinanceSource FinanceSource
		{
			get
			{	  
				return _financeSource;
			}

			set
			{
				_financeSource = value;
									  
				if (value != null)
				{
					RefreshDirections();
					SetClaimCondition();
				}

				RaisePropertyChanged("FinanceSource");
			}
		}

		public Direction Direction
		{
			get
			{		 
				return _direction;
			}

			set
			{
				_direction = value;

				if (value != null)
				{
                    RefreshDirectionProfiles();
					SetClaimCondition();
				}

				RaisePropertyChanged("Direction");
			}
		}

        public DirectionProfile DirectionProfile
        {
            get => _directionProfile;
            set
            {
                _directionProfile = value;
                RaisePropertyChanged("DirectionProfile");
            }
        }

		#endregion

		#region FilteredCollections

		ObservableCollection<EducationForm> _educationForms;
		ObservableCollection<FinanceSource> _financeSources;
		ObservableCollection<Direction> _directions;
        ObservableCollection<DirectionProfile> _directionProfiles;

		public ObservableCollection<EducationForm> EducationForms
		{
			get
			{
				if (_educationForms == null)
				{
					_educationForms = new ObservableCollection<EducationForm>(
											(from compGroup in Session.DataModel.CompetitiveGroups
											 orderby compGroup.EducationForm.Name
											 select compGroup.EducationForm).Distinct());
				}
				return _educationForms;
			}

			set
			{
				_educationForms = value;
				RaisePropertyChanged("EducationForms");
			}
		}

		public ObservableCollection<FinanceSource> FinanceSources
		{
			get
			{
				if (_financeSources == null)
				{
					_financeSources = new ObservableCollection<FinanceSource>();
				}
				return _financeSources;
			}

			set
			{
				_financeSources = value;
				RaisePropertyChanged("FinanceSources");
			}
		}

		public ObservableCollection<Direction> Directions
		{
			get
			{
				if (_directions == null)
				{
					_directions = new ObservableCollection<Direction>();
				}
				return _directions;
			}

			set
			{
				_directions = value;
				RaisePropertyChanged("Directions");
			}
		}

        public ObservableCollection<DirectionProfile> DirectionProfiles
        {
            get
            {
                if (_directionProfiles == null)
                {
                    _directionProfiles = new ObservableCollection<DirectionProfile>();
                }
                return _directionProfiles;
            }
            set
            {
                _directionProfiles = value;
                RaisePropertyChanged("DirectionProfiles");
            }
        }

		#endregion
						 
		#region FilterMethods

		void RefreshFinSources()
		{
			if (EducationForm != null)
			{
				FinanceSources = new ObservableCollection<FinanceSource>(
				(from compGroup in Session.DataModel.CompetitiveGroups
				 where compGroup.EducationFormId == EducationForm.Id
				 orderby compGroup.FinanceSource.Name
				 select compGroup.FinanceSource).Distinct());
			}
			else
			{
				FinanceSources = new ObservableCollection<FinanceSource>();
			}
			
			if (!FinanceSources.Contains(FinanceSource))
			{
				FinanceSource = null;
			}
		}

		void RefreshDirections()
		{
			if (FinanceSource != null)
			{
				Directions = new ObservableCollection<Direction>(
				(from compGroup in Session.DataModel.CompetitiveGroups
				 where compGroup.EducationFormId == EducationForm.Id &&
					compGroup.FinanceSourceId == FinanceSource.Id
				 orderby compGroup.Direction.Name
				 select compGroup.Direction).Distinct());
			}
			else
			{
				Directions = new ObservableCollection<Direction>();
			}
			

			if (!Directions.Contains(Direction))
			{
				Direction = null;
			}
		}

        void RefreshDirectionProfiles()
        {
            if (Direction != null)
            {
                DirectionProfiles = new ObservableCollection<DirectionProfile>(Session.DataModel.DirectionProfiles.Where(dp => dp.DirectionId == Direction.Id));
                DirectionProfile = DirectionProfiles.FirstOrDefault();
            }
            else
            {
                DirectionProfiles = new ObservableCollection<DirectionProfile>();
            }
        }

		#endregion	   

	}
}
