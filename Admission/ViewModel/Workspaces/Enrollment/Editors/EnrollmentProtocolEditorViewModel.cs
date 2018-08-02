using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using MoreLinq;
using CommonMethods.TypeExtensions.exString;
using System.Data.Entity;

namespace Admission.ViewModel.Workspaces.Enrollment.Editors
{
	public class EnrollmentProtocolEditorViewModel: ViewModelBase
	{

		#region Конструкторы

        public EnrollmentProtocolEditorViewModel(EnrollmentProtocol protocol)
        {
			_protocol = protocol;
			_protocol.TrainingBeginDate = DateTime.Now;

			// Если протокол новый, то выставляем значения по умолчанию
			if (_protocol.Number == null)
			{
                // Генерируем номер и дату
                // Находим максимальный имеющийся номер в базе данных и в локальном кэше
                /*Session.DataModel.EnrollmentProtocols.Load();
                var protocolCollection = Session.DataModel.EnrollmentProtocols.Local.Where(p => p.EnrollmentOrder != null).ToList();

				int maxNumber = 0;
				if (protocolCollection.Count > 0)
				{
					maxNumber = (from prot in protocolCollection
								 select int.Parse(prot.Number.WithoutLetters())).Max();
				}

				_protocol.Number = string.Format("{0}-з", maxNumber + 1); */
				_protocol.Date = DateTime.Now.Date;
			}

			// Выставляем значения фильтров конкурсной группы
			if (Protocol.CompetitiveGroup != null)
			{
				SelectedDirection = Protocol.Direction;
			}
			else
			{
				if (Protocol.EnrollmentOrder != null)
				{
					RefreshDirections();
				}
			}

            // Формируем коллекцию заявлений в протоколе
            var claims = new List<Claim>();
            foreach (var ec in Protocol.EnrollmentClaims.Where(ec => ec.Claim != null))
            {
                if (!claims.Contains(ec.Claim))
                {
                    claims.Add(ec.Claim);
                }
            }
            ProtocolClaims = new ObservableCollection<Claim>(claims.OrderByDescending(c => c.TotalScore));
        }

		#endregion

		#region Обрабатываемые сущности

		EnrollmentProtocol _protocol;

        /// <summary>
        /// Собсно, сам протокол
        /// </summary>
		public EnrollmentProtocol Protocol
		{
			get
			{
				return _protocol;
			}
			set
			{
				_protocol = value;
				RefreshAvailableClaims();
				RaisePropertyChanged("Protocol");
			}
		}

        /// <summary>
        /// Конкурсная группа протокола
        /// </summary>
		CompetitiveGroup ProtocolCompetitiveGroup
		{
			get
			{
				return Protocol.CompetitiveGroup;
			}
			set
			{
				Protocol.CompetitiveGroup = value;
				RefreshAvailableClaims();
			}
		}

		Direction _selectedDirection;

        /// <summary>
        /// Фильтр конкурсной группы - направление подготовки
        /// </summary>
		public Direction SelectedDirection
		{
			get
			{
				return _selectedDirection;
			}
			set
			{
				_selectedDirection = value;
				RaisePropertyChanged("SelectedDirection");
				SetProtocolCompetitiveGroup();
			}
        }

        Claim _selectedAvailableClaim;

        /// <summary>
        /// Выбранное доступное заявление
        /// </summary>
        public Claim SelectedAvailableClaim
        {
            get
            {
                return _selectedAvailableClaim;
            }

            set
            {
                _selectedAvailableClaim = value;
                RaisePropertyChanged("SelectedAvailableClaim");
            }
        }

        Claim _selectedProtocolClaim;

        /// <summary>
        /// Выбранное доступное заявление
        /// </summary>
        public Claim SelectedProtocolClaim
        {
            get
            {
                return _selectedProtocolClaim;
            }

            set
            {
                _selectedProtocolClaim = value;
                RaisePropertyChanged("SelectedProtocolClaim");
            }
        }

        #endregion

        #region Вспомогательные коллекции

        /// <summary>
        /// Все формы обучения
        /// </summary>
		public ObservableCollection<EducationForm> EducationForms
		{
			get
			{
				return new ObservableCollection<EducationForm>(Session.DataModel.EducationForms);
			}
		}

        /// <summary>
        /// Все источники финансирования
        /// </summary>
		public ObservableCollection<FinanceSource> FinanceSources
		{
			get
			{
				return new ObservableCollection<FinanceSource>(Session.DataModel.FinanceSources);
			}
		}

        ObservableCollection<Direction> _directions;

        /// <summary>
        /// Доступные по фильтрам формы обучения и источнику финансирования направления подготовки
        /// </summary>
		public ObservableCollection<Direction> Directions
		{
			get
			{
				return _directions;
			}
			set
			{
				_directions = value;
				if (!_directions.Contains(SelectedDirection))
				{
					SelectedDirection = null;
				}
				RaisePropertyChanged("Directions");
			}
		}

		ObservableCollection<Claim> _availableClaims;

        /// <summary>
        /// Доступные для зачисления заявления в конкурсной группе
        /// </summary>
		public ObservableCollection<Claim> AvailableClaims
		{
			get
			{
				return _availableClaims;
			}
			set
			{
				_availableClaims = value;
				RaisePropertyChanged("AvailableClaims");
			}
		}

        ObservableCollection<Claim> _protocolClaims;

        /// <summary>
        /// Заявления, уже имеющиеся в протоколе
        /// </summary>
        public ObservableCollection<Claim> ProtocolClaims
        {
            get
            {
				if (_protocolClaims == null)
				{
					_protocolClaims = new ObservableCollection<Claim>(Protocol.EnrollmentClaims.Select(ec => ec.Claim));
				}
                return _protocolClaims;
            }

            set
            {
                _protocolClaims = value;
                RaisePropertyChanged("ProtocolClaims");
            }
        }

        /// <summary>
        /// Все факультеты
        /// </summary>
        public ObservableCollection<Faculty> Faculties
        {
            get
            {
                return new ObservableCollection<Faculty>(Session.DataModel.Faculties);
            }
        }

        #endregion

        #region Внешняя логика

        #region Команды

        public RelayCommand MoveSelectedAvailableClaimToProtocolCommand
        {
            get
            {
                return new RelayCommand(MoveSelectedAvailableClaimToProtocol,
                    MoveSelectedAvailableClaimToProtocolCanExecute);
            }
        }

        public RelayCommand RemoveSelectedProtocolClaimCommand
        {
            get
            {
                return new RelayCommand(RemoveSelectedProtocolClaim, 
                    RemoveSelectedProtocolClaimCanExecute);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Перемещает выбранное доступное заявление в протокол
        /// </summary>
        void MoveSelectedAvailableClaimToProtocol()
        {
            // Добавляем заявление в коллекцию заявлений в протоколе, если его там еще нет
            if (!ProtocolClaims.Contains(SelectedAvailableClaim))
            {
                ProtocolClaims.Add(SelectedAvailableClaim);
            }

            // Сортируем эту коллекцию как надо (по ебаллу)
            ProtocolClaims = new ObservableCollection<Claim>(ProtocolClaims.OrderByDescending(c => c.TotalScore));

            // Удаляем заявление из коллекции доступных
            AvailableClaims.Remove(SelectedAvailableClaim);
        }

        /// <summary>
        /// Удаляет выбранное заявление в протоколе из коллекции
        /// </summary>
        void RemoveSelectedProtocolClaim()
        {
            // Удаляем заявление из коллекции
            ProtocolClaims.Remove(SelectedProtocolClaim);

            // Обновляем доступные заявления
            RefreshAvailableClaims();
        }

        #endregion

        #region Проверки

        bool MoveSelectedAvailableClaimToProtocolCanExecute()
        {
            return SelectedAvailableClaim != null;
        }

        bool RemoveSelectedProtocolClaimCanExecute()
        {
            return SelectedProtocolClaim != null;
        }

        #endregion

        #endregion

        #region Внутренняя логика

        /// <summary>
        /// По выбранным условиям фильтра (форма обучения, источник финансирования, направление подготовки) устанавливает конкурсную группу протоколу
        /// </summary>
        void SetProtocolCompetitiveGroup()
		{
			if (Protocol.EnrollmentOrder != null &&
					SelectedDirection != null)
			{
                // Получаем конкурсные группы по указанному выше набору условий
                var compGroups = (from cg in Session.DataModel.CompetitiveGroups
                                  where cg.EducationForm.Id == Protocol.EnrollmentOrder.EducationForm.Id &&
                                    cg.FinanceSource.Id == Protocol.EnrollmentOrder.FinanceSource.Id &&
                                    cg.Direction.Id == SelectedDirection.Id
                                  select cg).ToList() ;
				if (compGroups.Count() > 0)
				{
					ProtocolCompetitiveGroup = compGroups.FirstOrDefault(cg => cg.Campaign.CampaignStatusId == 2);
					return;
				}
			}			
		}

        /// <summary>
        /// Обновляет коллекцию направлений подготовки по форме обучения и источнику финансирования
        /// </summary>
		void RefreshDirections()
		{
			if (Protocol.EnrollmentOrder != null)
			{
				// Получаем коллекцию направлений
				var directionCollection = (from cg in Session.DataModel.CompetitiveGroups
										   where cg.EducationForm.Id == Protocol.EnrollmentOrder.EducationForm.Id &&
											cg.FinanceSource.Id == Protocol.EnrollmentOrder.FinanceSource.Id
										   select cg.Direction).ToList();
				// Отсекаем дубликаты, сортируем по коду
				directionCollection = directionCollection.DistinctBy(d => d.Code).OrderBy(d => d.Code).ToList();                
				Directions = new ObservableCollection<Direction>(directionCollection);
				return;
			}
			Directions = new ObservableCollection<Direction>();
		}

        /// <summary>
        /// Обновляет коллекцию доступных для зачисления заявлений по конкурсной группе протокола
        /// </summary>
		void RefreshAvailableClaims()
		{
			if (ProtocolCompetitiveGroup != null)
			{
                // Выбираем заявления по условиям:
                // статус - новое или принято, оригинал, положительная сумма баллов
				var claims = (from condition in ProtocolCompetitiveGroup.ClaimConditions
							  where condition.Claim != null &&
							  condition.Claim.ClaimStatus.Id != 4
							  select condition.Claim).ToList();
				claims = claims.Where(c => c.TotalScore > 0).OrderByDescending(c => c.TotalScore).ToList();

                // Если заявление уже имеется в протоколе, то убираем его отсюда нахер
                var intersectClaims = claims.Intersect(ProtocolClaims).ToList();
                foreach (var claim in intersectClaims)
                {
                    claims.Remove(claim);
                }

				// Если у заявления есть приказ о зачислении и нет приказа об исключении, то убираем нахер
				var checkClaims = claims.ToList();
				foreach (var claim in checkClaims)
				{
					foreach (var ec in claim.EnrollmentClaims)
					{
						if (ec.EnrollmentExceptionOrder == null)
						{
							claims.Remove(ec.Claim);
							break;
						}
					}
				}

				AvailableClaims = new ObservableCollection<Claim>(claims.Where(c => c.Campaign.CampaignStatusId == 2));
			}
			else
			{
				AvailableClaims = new ObservableCollection<Claim>();
			}
		}

		#endregion

	}
}
