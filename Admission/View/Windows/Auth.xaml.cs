/************************************************************************************
 * 
 * Здесь будет дохера логики, но это авторизация, работы с данными немного, в принципе,
 * так что обойдемся без ViewModel.
 * 
 * Плюсы: безопасно, инкапсулировано от всей системы (не считая точки входа в приложение)
 * 
 * Минусы: нарушаем MVVM
 * 
 ***********************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;	 

namespace Admission.View.Windows
{
	/// <summary>
	/// Логика взаимодействия для Auth.xaml
	/// </summary>
	public partial class Auth : Window
	{
		public Auth()
		{
			InitializeComponent();
			var viewModel = new ViewModel.Windows.Auth();
			DataContext = viewModel;
			if (viewModel.SuccessAuth == null)
			{
				viewModel.SuccessAuth = new Action(() => { DialogResult = true; });
			}

			if (viewModel.CloseWindow == null)
			{
				viewModel.CloseWindow = new Action(() => { DialogResult = false; });
			}
            pwdBox.Focus();
		}
		
    }
}
