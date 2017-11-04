using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;

namespace Admission.ViewModel.Workspaces.EntrantClaims.Pages
{
    public class ArchiveClaimsViewModel: ViewModelBase
    {

        #region Отображаемые коллекции и элементы

        #region Поля

        Claim _selectedClaim;

        ObservableCollection<Claim> _claims;

        #endregion

        #region Свойства

        public Claim SelectedClaim
        {
            get
            {
                if (_selectedClaim == null)
                {
                    _selectedClaim = Claims.FirstOrDefault();
                }
                return _selectedClaim;
            }
            set
            {
                _selectedClaim = value;
                RaisePropertyChanged("SelectedClaim");
            }
        }

        public ObservableCollection<Claim> Claims
        {
            get
            {
                if (_claims == null)
                {
                    _claims = new ObservableCollection<Claim>(from claim in Session.DataModel.Claims
                                                              where claim.ClaimStatusId == 4
                                                              select claim);
                }
                return _claims;
            }
            set
            {
                _claims = value;
                RaisePropertyChanged("Claims");
            }
        }

        #endregion

        #endregion

        #region Логика

        #region Команды

        #endregion

        #region Методы

        #endregion

        #region Проверки

        #endregion
        
        #endregion

    }
}
