using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;
using CommonMethods.TypeExtensions.exString;
using CommonMethods.TypeExtensions.exDateTime;

namespace Admission.ViewModel.Workspaces.Examinations.Editors
{
	public class EgeResultCheckProtocolEditorViewModel: ViewModelBase
	{

		#region Конструкторы

		public EgeResultCheckProtocolEditorViewModel(EntranceExaminationsCheckProtocol protocol)
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
				_protocol.Number = string.Format("{0}-ЕГЭ", maxNumber + 1);
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

		EducationForm _selectedEducationForm;

		public EducationForm SelectedEducationForm
		{
			get
			{
				return _selectedEducationForm;
			}
			set
			{
				_selectedEducationForm = value;
				UpdateResults();
				RaisePropertyChanged("SelectedEducationForm");
			}
		}

		#endregion

		#region Вспомогательные коллекции


		public ObservableCollection<EducationForm> EducationForms
		{
			get
			{
				return new ObservableCollection<EducationForm>(Session.DataModel.EducationForms);
			}
		}
		
		ObservableCollection<EgeResult> _results;

		public ObservableCollection<EgeResult> Results
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
		

		void UpdateResults()
		{
			if (SelectedEducationForm == null)
			{
				Results = null;
				return;
			}

			// Подгружаем проверенные результаты ЕГЭ, у которых не присвоен протокол проверки
			var egeResults = (from res in Session.DataModel.EgeResults
							  where res.IsChecked == true &&
								res.EntranceExaminationsCheckProtocol == null &&
								res.EgeDocument.Claim != null
							  select res).ToList();

			// Фильтруем по форме обучения
			egeResults = egeResults.Where(res => res.EgeDocument.Claim.EducationForm.Id == SelectedEducationForm.Id).ToList();
			Results = new ObservableCollection<EgeResult>(egeResults);
		}

		public List<EgeResult> GetExaminations()
		{
			return Results.ToList();
		}

		#endregion

	}
}
