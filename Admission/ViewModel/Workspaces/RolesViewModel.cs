using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Admission.DialogService;
using System.Data.Entity;
using System.Windows;

namespace Admission.ViewModel.Workspaces
{
	public class RolesViewModel: ViewModelBase 
	{	
		Role _selectedRole;
		public Role SelectedRole
		{
			get
			{
				if (_selectedRole == null)
				{
					_selectedRole = new Role();
				}
				return _selectedRole;
			}

			set
			{
				_selectedRole = value;
				RaisePropertyChanging("SelectedRole");
			}
		}  
						  
		ObservableCollection<Role> _roleList;
		public ObservableCollection<Role> RoleList
		{
			get
			{
				if (_roleList == null)
				{							 
					_roleList = new ObservableCollection<Role>(Session.DataModel.Roles.ToList());  
				}
				return _roleList;
			}

			set
			{
				_roleList = value;
				RaisePropertyChanging("RoleList");
			}
		}			  

		RelayCommand _newRoleCommand;
		public ICommand NewRoleCommand
		{
			get
			{
				if (_newRoleCommand == null)
				{
					_newRoleCommand = new RelayCommand(AddRole);
				}
				return _newRoleCommand;
			}
		}

		RelayCommand _editRoleCommand;
		public ICommand EditRoleCommand
		{
			get
			{
				if (_editRoleCommand == null)
				{
					_editRoleCommand = new RelayCommand(EditRole, CanEdit);
				}
				return _editRoleCommand;
			}
		}		 

		RelayCommand _removeRoleCommand;  
		public ICommand RemoveRoleCommand
		{
			get
			{
				if (_removeRoleCommand == null)
				{
					_removeRoleCommand = new RelayCommand(RemoveRole);
				}
				return _removeRoleCommand;
			}
		}

		RelayCommand _showRoleInfoCommand;
		public ICommand ShowRoleInfoCommand
		{
			get
			{
				if (_showRoleInfoCommand == null)
				{
					_showRoleInfoCommand = new RelayCommand(ShowInfo);
				}
				return _showRoleInfoCommand;
			}
		}

		RelayCommand _updateListCommand;
		public ICommand UpdateListCommand
		{
			get
			{
				if (_updateListCommand == null)
				{
					_updateListCommand = new RelayCommand(UpdateList);
				}
				return _updateListCommand;
			}
		}
											  
		void AddRole()
		{
			_selectedRole = new Role();
			if (DialogLayer.ShowEditor(EditingContent.RoleEditor, this))
			{
				Session.DataModel.Roles.Add(_selectedRole);
				Session.DataModel.SaveChanges();
				_roleList.Add(_selectedRole);
			}
			else
			{
				_selectedRole = _roleList.Last();
			}
		}	 
		
		void EditRole()
		{
			if (DialogLayer.ShowEditor(EditingContent.RoleEditor, this))
			{
				Session.DataModel.SaveChanges();
			}
		}

		bool CanEdit()
		{
			return _selectedRole != null;
		}

		void RemoveRole()
		{
			Admission.ViewModel.Export.Exporter.GeneratePackage();
			//if (MessageBox.Show("Вы действительно хотите удалить эту запись?\nОтменить данное действие будет невозможно.", 
			//	"Подтвердите действие", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			//{
			//	Session.DataModel.Roles.Remove(_selectedRole);
			//	_roleList.Remove(_selectedRole);
			//	Session.DataModel.SaveChanges();
			//}
		}

		void ShowInfo()
		{					
			DialogLayer.ShowInfoBox(InfoContent.RoleInfo, this);
		}

		public enum FOO
		{
			a,b,c,d
		}

		public class TCL
		{
			public string Title { get; set; }
			public FOO Type { get; set; }
			public override string ToString()
			{
				return Title;
			}
		}

		void Test()
		{
			ObservableCollection<EntityType> collection = new ObservableCollection<EntityType>();
			collection.Add(new EntityType(SelectableEntity.HighEducationDiplomaDocument));
			collection.Add(new EntityType(SelectableEntity.MiddleEducationDiplomaDocument));
			collection.Add(new EntityType(SelectableEntity.SchoolCertificate));

			var res = DialogLayer.ShowEntityTypeSelector(collection);
			switch (res)
			{
				case SelectableEntity.HighEducationDiplomaDocument:
					{
						MessageBox.Show("institute");
						break;
					}						
				case SelectableEntity.MiddleEducationDiplomaDocument:
					{
						MessageBox.Show("spo");
						break;
					}
				case SelectableEntity.SchoolCertificate:
					{
						MessageBox.Show("school");
						break;
					}
				default:
					break;
			}

		}


		void UpdateList()
		{
			Test();
			Session.RefreshAll();
			_roleList = null;
			RaisePropertyChanged("RoleList");
		}
	}
}
