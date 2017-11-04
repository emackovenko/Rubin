using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Model.Admission;
using GalaSoft.MvvmLight;
using CommonMethods.TypeExtensions.exString;

namespace Admission.ViewModel.Workspaces.Examinations.Editors
{
	public class InnerEntranceExaminationProtocolEditorViewModel: ViewModelBase
	{

		#region Конструкторы

		public InnerEntranceExaminationProtocolEditorViewModel(EntranceExaminationsCheckProtocol protocol)
		{
			_protocol = protocol;

			// Если протокол новый, то генерируем ему номер и дату
			if (_protocol.Number == null)
			{
				// Получаем протоколы по активным кампаниям
				var protocolCollection = Session.DataModel.EntranceExaminationsCheckProtocols.Where(p => p.Number != null).ToList();

				// Выбираем максимальный номер
				int maxNumber = 0;

				if (protocolCollection.Count > 0)
				{
					maxNumber = (from prot in protocolCollection
									 select int.Parse(prot.Number.WithoutLetters())).Max();
				}

				// Генерируем на его основе новый
				_protocol.Number = string.Format("{0}-СЭ", maxNumber + 1);
			}

			if (_protocol.Date == null)
			{
				_protocol.Date = DateTime.Now.Date;
			}
		}

		#endregion

		#region Обрабатываемые сущности

		EntranceExaminationsCheckProtocol _protocol;

		public EntranceExaminationsCheckProtocol Protocol
		{
			get
			{
				return _protocol;
			}
			set
			{
				_protocol = value;
				RaisePropertyChanged("Protocol");
			}
		}

		ExamSubject _selectedExaminationSubject;

		public ExamSubject SelectedExaminationSubject
		{
			get
			{
				return _selectedExaminationSubject;
			}
			set
			{
				_selectedExaminationSubject = value;
				RaisePropertyChanged("SelectedExaminationSubject");
				UpdateAvailableExaminationDates();
				UpdateResults();
			}
		}

		DateTime? _selectedExaminationDate;

		public DateTime? SelectedExaminationDate
		{
			get
			{
				return _selectedExaminationDate;
			}
			set
			{
				_selectedExaminationDate = value;
				RaisePropertyChanged("SelectedExaminationDate");
				UpdateResults();
			}
		}

		#endregion

		#region Вспомогательные коллекции

		ObservableCollection<ExamSubject> _availableExaminationSubjects;

		public ObservableCollection<ExamSubject> AvailableExaminationSubjects
		{
			get
			{
				if (_availableExaminationSubjects == null)
				{
					_availableExaminationSubjects = new ObservableCollection<ExamSubject>(Session.DataModel.ExamSubjects);
				}
				return _availableExaminationSubjects;
			}
			set
			{
				_availableExaminationSubjects = value;
				if (SelectedExaminationSubject != null && !_availableExaminationSubjects.Contains(SelectedExaminationSubject))
				{
					SelectedExaminationSubject = null;
				}
				RaisePropertyChanged("AvailableExaminationSubjects");
			}
		}

		ObservableCollection<DateTime?> _availableExaminationDates;

		public ObservableCollection<DateTime?> AvailableExaminationDates
		{
			get
			{
				return _availableExaminationDates;
			}
			set
			{
				_availableExaminationDates = value;

				if (SelectedExaminationDate != null && _availableExaminationDates.Contains(SelectedExaminationDate))
				{
					SelectedExaminationDate = null;
				}

				RaisePropertyChanged("AvailableExaminationDates");
			}
		}

		ObservableCollection<EntranceTestResult> _results;

		public ObservableCollection<EntranceTestResult> Results
		{
			get
			{
				return _results;
			}
			set
			{
				_results = value;
				RaisePropertyChanged("Results");
			}
		}

		#endregion

		#region Внутренняя логика

		void UpdateAvailableExaminationDates()
		{
			if (SelectedExaminationSubject != null)
			{
				var examDates = (from exam in Session.DataModel.EntranceTests
								 where exam.ExamSubject.Id == SelectedExaminationSubject.Id &&
								 exam.ExaminationDate != null
								 orderby exam.ExaminationDate
								 select exam.ExaminationDate).Distinct();
				AvailableExaminationDates = new ObservableCollection<DateTime?>(examDates);
			}
			else
			{
				AvailableExaminationSubjects = new ObservableCollection<ExamSubject>();
			}
		}

		void UpdateResults()
		{
			if (SelectedExaminationSubject != null && SelectedExaminationDate != null)
			{
				var results = (from result in Session.DataModel.EntranceTestResults
							   where result.EntranceTest.ExaminationDate == SelectedExaminationDate &&
							   result.EntranceTest.ExamSubject.Id == SelectedExaminationSubject.Id &&
							   result.Claim != null
							   orderby result.Claim.Entrants.FirstOrDefault().LastName,
								result.Claim.Entrants.FirstOrDefault().FirstName,
								result.Claim.Entrants.FirstOrDefault().Patronymic
							   select result).ToList();
				Results = new ObservableCollection<EntranceTestResult>(results);
			}
			else
			{
				Results = new ObservableCollection<EntranceTestResult>();
			}
		}

		public List<EntranceTest> GetExaminations()
		{
			var exams = (from result in Results
						 select result.EntranceTest).Distinct().ToList();
			return exams;
		}

		#endregion

	}
}
