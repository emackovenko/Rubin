using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Admission.DialogService;
using Admission.ViewModel.Workspaces.Enrollment.Editors;
using Admission.ViewModel.ValidationRules.Validators;
using Admission.ViewModel.Documents;
using CommonMethods.TypeExtensions.exString;

namespace Admission.ViewModel.Workspaces.Enrollment
{
	public class EnrollmentOrdersViewModel: ViewModelBase
	{

		#region Обрабатываемые сущности

		EnrollmentOrder _selectedOrder;

		ObservableCollection<EnrollmentOrder> _orders;

		public EnrollmentOrder SelectedOrder
		{
			get
			{
				if (_selectedOrder == null)
				{
					_selectedOrder = Orders.FirstOrDefault();
				}
				return _selectedOrder;
			}

			set
			{
				_selectedOrder = value;
				RaisePropertyChanged("SelectedOrder");
			}
		}

		public ObservableCollection<EnrollmentOrder> Orders
		{
			get
			{
				_orders = new ObservableCollection<EnrollmentOrder>(Session.DataModel.EnrollmentOrders);
				return _orders;
			}

			set
			{
				_orders = value;
				RaisePropertyChanged("Orders");
			}
		}


		#endregion

		#region Внешняя логика

		#region Команды

		public RelayCommand AddOrderCommand
		{
			get
			{
				return new RelayCommand(AddOrder);
			}
		}

		public RelayCommand EditOrderCommand
		{
			get
			{
				return new RelayCommand(EditOrder, EditOrderCanExecute);
			}
		}

		public RelayCommand PrintOrderCommand
		{
			get
			{
				return new RelayCommand(PrintOrder, PrintOrderCanExecute);
			}
		}

		#endregion

		#region Методы

		void AddOrder()
		{
			var order = new EnrollmentOrder();
			var vm = new EnrollmentOrderEditorViewModel(order);
            var validator = new EnrollmentOrderValidator(order);
			if (DialogLayer.ShowEditor(EditingContent.EnrollmentOrderEditor, vm, validator))
			{
				Session.DataModel.EnrollmentOrders.Add(order);
                RebuildEnrollment(order);
                Session.DataModel.SaveChanges();
                RaisePropertyChanged("Orders");
			}
		}

		void EditOrder()
        {
            var order = SelectedOrder;
            var vm = new EnrollmentOrderEditorViewModel(order);
            var validator = new EnrollmentOrderValidator(order);
            if (DialogLayer.ShowEditor(EditingContent.EnrollmentOrderEditor, vm, validator))
            {
                RebuildEnrollment(order);
                Session.DataModel.SaveChanges();
                RaisePropertyChanged("Orders");
            }
        }

		void PrintOrder()
		{
			var doc = new EnrollmentOrderDocument(SelectedOrder);
			DialogLayer.ShowDocument(doc);
		}


		#endregion

		#region Проверки

		bool EditOrderCanExecute()
		{
			return SelectedOrder != null;
		}

		bool PrintOrderCanExecute()
		{
			return SelectedOrder != null;
		}

        #endregion

        #endregion

        #region Внутренняя логика

        /// <summary>
        /// Перестраивает зачисление по приказу - выставляет значения номеров строк в приказе и устанавливает статусы заявления в "Зачислен в число студентов"
        /// </summary>
        /// <param name="order">Приказ о зачислении</param> 
        void RebuildEnrollment(EnrollmentOrder order)
        {
            // Счетчик
            int i = 1;

            // Проходим по протоколам в порядке возрастания номера
            foreach (var protocol in order.EnrollmentProtocols.OrderBy(p => int.Parse(p.Number.WithoutLetters())))
            {
                // Идем по новой коллекции заявлений, отсортированной по алфавиту
                var claims = (from ec in protocol.EnrollmentClaims
                              where ec.Claim != null
                              select ec.Claim).ToList();
                claims = claims.OrderBy(c => c.Person.FullName).ToList();
                foreach (var claim in claims)
                {
                    // Находим заявление в оригинальной коллекции
                    var enrollmentClaim = protocol.EnrollmentClaims.Where(ec => ec.Claim.Id == claim.Id).FirstOrDefault();

                    // Устанавливаем номер строки
                    enrollmentClaim.StringNumber = i;
                    i++;

                    // Устанавливаем статус у заявления
                    enrollmentClaim.Claim.ClaimStatusId = 3;
                }
            }

        }

        #endregion
    }
}
