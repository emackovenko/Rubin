using Admission.DialogService;
using Admission.ViewModel.ValidationRules.Validators;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Admission.ViewModel.Documents;


namespace Admission.ViewModel.Workspaces.Enrollment.Editors
{
    public class EnrollmentOrderEditorViewModel: ViewModelBase
	{

		#region Конструкторы

		public EnrollmentOrderEditorViewModel(EnrollmentOrder order)
		{
			Order = order;
            if (Order.Date == null)
            {
                Order.Date = DateTime.Now.Date;
            }
		}

		#endregion

		#region Обрабатываемые сущности

		EnrollmentOrder _order;

		public EnrollmentOrder Order
		{
			get
			{
				return _order;
			}
			set
			{
				_order = value;
				RaisePropertyChanged("Order");
			}
		}

		EnrollmentProtocol _selectedProtocol;

		public EnrollmentProtocol SelectedProtocol
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

		ObservableCollection<EnrollmentProtocol> _protocols;

		public ObservableCollection<EnrollmentProtocol> Protocols
		{
			get
			{
				_protocols = new ObservableCollection<EnrollmentProtocol>(Order.EnrollmentProtocols);
				return _protocols;
			}

			set
			{
				_protocols = value;
				RaisePropertyChanged("Protocols");
			}
		}

		#endregion

		#region Вспомогательые коллекции
		
		public ObservableCollection<Campaign> Campaigns
		{
			get
			{
				return new ObservableCollection<Campaign>(Session.DataModel.Campaigns.Where(c => c.CampaignStatus.Id == 2));
			}
		}

		public ObservableCollection<EducationForm> EducationForms
		{
			get
			{
				return new ObservableCollection<EducationForm>(Session.DataModel.EducationForms);
			}
		}

		public ObservableCollection<EducationLevel> EducationLevels
		{
			get
			{
				return new ObservableCollection<EducationLevel>(Session.DataModel.EducationLevels);
			}
		}

		public ObservableCollection<FinanceSource> FinanceSources
		{
			get
			{
				return new ObservableCollection<FinanceSource>(Session.DataModel.FinanceSources);
			}
		}



		#endregion

		#region Внешняя логика

		#region Команды

		public RelayCommand AddProtocolCommand
		{
			get
			{
				return new RelayCommand(AddProtocol);
			}
		}

		public RelayCommand EditProtocolCommand
		{
			get
			{
				return new RelayCommand(EditProtocol, EditProtocolCanExecute);
			}
		}

		public RelayCommand RemoveProtocolCommand
		{
			get
			{
				return new RelayCommand(RemoveProtocol, RemoveProtocolCanExecute);
			}
		}

		public RelayCommand PrintProtocolCommand
		{
			get
			{
				return new RelayCommand(PrintProtocol, PrintProtocolCanExecute);
			}
		}

		#endregion

		#region Методы

		void AddProtocol()
		{
			var protocol = new EnrollmentProtocol();
			protocol.EnrollmentOrder = Order;
			var vm = new EnrollmentProtocolEditorViewModel(protocol);
            var validator = new EnrollmentProtocolValidator(protocol);
			if (DialogLayer.ShowEditor(EditingContent.EnrollmentProtocolEditor, vm, validator))
            {
				protocol.EnrollmentOrder = null;
				Order.EnrollmentProtocols.Add(protocol);
                RebuildEnrollmentProtocol(protocol, vm.ProtocolClaims);
				RaisePropertyChanged("Protocols");
				SelectedProtocol = protocol;
			}
			else
			{
				Order.EnrollmentProtocols.Remove(protocol);
			}
		}

		void EditProtocol()
		{
			if (SelectedProtocol.EnrollmentOrder == null)
			{
				SelectedProtocol.EnrollmentOrder = Order;
			}
			var vm = new EnrollmentProtocolEditorViewModel(SelectedProtocol);
            var validator = new EnrollmentProtocolValidator(SelectedProtocol);
            if (DialogLayer.ShowEditor(EditingContent.EnrollmentProtocolEditor, vm, validator))
			{
                RebuildEnrollmentProtocol(SelectedProtocol, vm.ProtocolClaims);
				RaisePropertyChanged("Protocols");
			}
		}

		void RemoveProtocol()
		{
			if (Messenger.RemoveQuestion())
			{
				Protocols.Remove(SelectedProtocol);
			}
		}

		void PrintProtocol()
		{
            var doc = new EnrollmentProtocolDocument(SelectedProtocol);
            DialogLayer.ShowDocument(doc);
		}

		#endregion

		#region Проверки

		bool EditProtocolCanExecute()
		{
			return SelectedProtocol != null;
		}

		bool RemoveProtocolCanExecute()
		{
			return SelectedProtocol != null;
		}

		bool PrintProtocolCanExecute()
		{
			return SelectedProtocol != null;
		}

		#endregion

		#endregion

		#region Внутренняя логика

        /// <summary>
        /// Перестраивает протокол, обновляя заявления в нем
        /// </summary>
        /// <param name="protocol">Протокол зачисления</param>
        /// <param name="claims">Коллекция заявлений</param>
        void RebuildEnrollmentProtocol(EnrollmentProtocol protocol, IEnumerable<Claim> claims)
        {
            // Удаляем бывшие заявления
            protocol.EnrollmentClaims.Clear();

            // Включаем выбранные заявления в протокол
            foreach (var claim in claims)
            {
                var ec = new EnrollmentClaim
                {
                    Claim = claim
                };
                protocol.EnrollmentClaims.Add(ec);
            }
        }

		#endregion

	}
}
