using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
	public class EgeResultEditorViewModel: ViewModelBase
	{
		#region Constructors

		public EgeResultEditorViewModel()
		{

		}

		public EgeResultEditorViewModel(EgeResult result)
		{
			_result = result;
		}

		#endregion

		#region Parent entities

		EgeResult _result;

		public EgeResult Result
		{
			get
			{
				if (_result == null)
				{
					_result = new EgeResult();
				}
				return _result;
			}

			set
			{
				_result = value;
				RaisePropertyChanged("Result");
			}
		}
		  
		#endregion
						  
		#region Additional lists

		ObservableCollection<ExamSubject> _subjects;	 

		public ObservableCollection<ExamSubject> Subjects
		{
			get
			{
				_subjects = new ObservableCollection<ExamSubject>(Session.DataModel.ExamSubjects.OrderBy(s => s.Name));	   
				return _subjects;
			}

			set
			{
				_subjects = value;
				RaisePropertyChanged("Subjects");
			}
		}
				 
		#endregion

	}
}
