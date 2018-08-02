using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

namespace Admission.ViewModel.Windows
{			
	/// <summary>
	/// Слой для авторизации
	/// </summary>
	public class Auth: ViewModelBase
	{
		string _username;
		string _password;
		RelayCommand _commandLogin;
		RelayCommand _commandExit;
										
		public string Username
		{
			get
			{
				if (_username == null)
				{
					_username = Admission.Properties.Settings.Default.LocalUsername;
				}
				return _username;
			}
			set
			{
				_username = value;
				RaisePropertyChanged("Username"); 
			}
		}
							 
		public string Password
		{
			get
			{
				return _password ?? string.Empty;
			}
			set
			{
				_password = value;
				RaisePropertyChanged("Password");
			}
		}

		/// <summary>
		/// Делегат закрытия при успешной авторизации
		/// </summary>
		public Action SuccessAuth { get; set; }

		/// <summary>
		/// Делегат при выходе без авторизации
		/// </summary>
		public Action CloseWindow { get; set; }

		public RelayCommand CommandLogin
		{
			get
			{
				if (_commandLogin == null)
				{
					_commandLogin = new RelayCommand(Login);
				}
				return _commandLogin;
			}	
		}

		public RelayCommand CommandExit
		{
			get
			{
				if (_commandExit == null)
				{
					_commandExit = new RelayCommand(Exit);
				}
				return _commandExit;
			}
		}	

        void DoScratch()
        {
            Admission.ViewModel.Export.Exporter.GeneratePackage();
        }

        void DoUserScratch()
        {
            var user = Session.DataModel.Users.First(u => u.Username == "vdudnik");
            user.PasswordHash = CommonMethods.Security.Encrypter.MD5Hash("a4tech");
            Session.DataModel.SaveChanges();
        }

		void Login()
		{
            //DoScratch();return;
			if (_username == null || _password == null)
			{
				throw new Exception("Нельзя авторизоваться с пустым логином и паролем");
			}
			System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.AppStarting;
			if (Session.Initialize(_username, _password))
			{
				Admission.Properties.Settings.Default.LocalUsername = _username;
				Admission.Properties.Settings.Default.Save();
                //DoUserScratch();
				SuccessAuth();
			}
			else
			{
				string errorMessage = "Пользователь с введёнными логином и паролем не был найден.\nПовторите попытку.";
				string errorTitle = "Ошибка при авторизации";	
				MessageBox.Show(errorMessage, errorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
		}

		void Exit()
		{
			CloseWindow();
		}	 
	}
}
