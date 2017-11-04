using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Admission.DialogService;
using Admission.ViewModel.Workspaces.Examinations.Editors;
using Admission.ViewModel.Documents;

namespace Admission.ViewModel.Workspaces.Examinations
{
	public class EntranceExaminationsProtocolsViewModel: ViewModelBase
	{

		#region Обрабатываемые сущности

		EntranceExaminationsCheckProtocol _selectedProtocol;
		
		public EntranceExaminationsCheckProtocol SelectedProtocol
		{
			get
			{
				if (_selectedProtocol == null)
				{
					if (Protocols.Count > 0)
					{
						_selectedProtocol = Protocols.First();
					}
				}
				return _selectedProtocol;
			}

			set
			{
				_selectedProtocol = value;
				RaisePropertyChanged("SelectedProtocol");
			}
		}

		public ObservableCollection<EntranceExaminationsCheckProtocol> Protocols
		{
			get
			{
				return new ObservableCollection<EntranceExaminationsCheckProtocol>
						(Session.DataModel.EntranceExaminationsCheckProtocols.OrderBy(p => p.Number));				
			}

			set
			{
				RaisePropertyChanged("Protocols");
			}
		}

		#endregion

		#region Логика

		#region Команды

		public RelayCommand AddCommand
		{
			get
			{
				return new RelayCommand(Add);
			}
		}

		public RelayCommand EditCommand
		{
			get
			{
				return new RelayCommand(Edit, EditCanExecute);
			}
		}

		public RelayCommand RemoveCommand
		{
			get
			{
				return new RelayCommand(Remove, RemoveCanExecute);
			}
		}

		public RelayCommand PrintCommand
		{
			get
			{
				return new RelayCommand(Print, PrintCanExecute);
			}
		}


		#endregion

		#region Методы

		void Add()
		{
			var protocol = new EntranceExaminationsCheckProtocol();

			var entityTypeCollection = new ObservableCollection<EntityType>
			{
				new EntityType(SelectableEntity.EgeProtocol),
				new EntityType(SelectableEntity.InnerExaminationProtocol)
			};

			switch (DialogLayer.ShowEntityTypeSelector(entityTypeCollection))
			{
				case SelectableEntity.EgeProtocol:
					{
						var vm = new EgeResultCheckProtocolEditorViewModel(protocol);
						if (DialogLayer.ShowEditor(EditingContent.EgeResultCheckProtocolEditor, vm))
						{
							Session.DataModel.EntranceExaminationsCheckProtocols.Add(protocol);
							ApplyProtocolIntoEgeResults(vm, protocol);
							Session.DataModel.SaveChanges();
							RaisePropertyChanged("Protocols");
							LogService.WriteLog(string.Format("Добавлен протокол проверки ЕГЭ: №{0} от {1} ({2})",
								protocol.Number, ((DateTime)protocol.Date).ToString("dd.MM.yyyy г."), protocol.Examination));
						}
						break;
					}
				case SelectableEntity.InnerExaminationProtocol:
					{
						var vm = new InnerEntranceExaminationProtocolEditorViewModel(protocol);
						if (DialogLayer.ShowEditor(EditingContent.InnerEntranceExaminationProtocolEditor, vm))
						{
							Session.DataModel.EntranceExaminationsCheckProtocols.Add(protocol);
							ApplyProtocolIntoInnerResults(vm, protocol);
							Session.DataModel.SaveChanges();
							RaisePropertyChanged("Protocols");
							LogService.WriteLog(string.Format("Добавлен протокол проверки ВИ: №{0} от {1} ({2})",
								protocol.Number, ((DateTime)protocol.Date).ToString("dd.MM.yyyy г."), protocol.Examination));
						}
						break;
					}
				default:
					break;
			}
		}

		void ApplyProtocolIntoInnerResults(InnerEntranceExaminationProtocolEditorViewModel vm,
			EntranceExaminationsCheckProtocol protocol)
		{
			var exams = vm.GetExaminations();
			foreach (var item in exams)
			{
				item.EntranceExaminationsCheckProtocol = protocol;
			}
		}

		void ApplyProtocolIntoEgeResults(EgeResultCheckProtocolEditorViewModel vm,
			EntranceExaminationsCheckProtocol protocol)
		{
			var exams = vm.GetExaminations();
			foreach (var item in exams)
			{
				item.EntranceExaminationsCheckProtocol = protocol;
			}
		}

		void Edit()
		{

		}

		void Remove()
		{
			if (Messenger.RemoveQuestion())
			{
				Session.DataModel.EntranceExaminationsCheckProtocols.Remove(SelectedProtocol);
				Session.DataModel.SaveChanges();
				RaisePropertyChanged("Protocols");
				LogService.WriteLog(string.Format("Удален протокол проверки ВИ: №{0} от {1} ({2})",
					SelectedProtocol.Number, ((DateTime)SelectedProtocol.Date).ToString("dd.MM.yyyy г."), SelectedProtocol.Examination));
			}
		}

		void Print()
		{
			if (SelectedProtocol.Examination != "ЕГЭ")
			{
				var doc = new InnerExaminationCheckProtocol(SelectedProtocol);
				DialogLayer.ShowDocument(doc);
			}
		}

		#endregion

		#region Проверки

		bool EditCanExecute()
		{
			return SelectedProtocol != null;
		}

		bool RemoveCanExecute()
		{
			return SelectedProtocol != null;
		}

		bool PrintCanExecute()
		{
			return SelectedProtocol != null;
		}

		#endregion

		#endregion
	}
}
