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

namespace Admission.DialogService
{
	/// <summary>
	/// Логика взаимодействия для Editor.xaml
	/// </summary>
	public partial class Editor : Window
	{		 
		EntityValidationRule _validate;

		Admission.ViewModel.ValidationRules.Validators.IEntityValidator _validator;

		public Editor(UserControl content, object dataContext)
		{									
			InitializeComponent();			   
			EditingContent.Children.Add(content);
			content.DataContext = dataContext;
		}

		public Editor(UserControl content, object dataContext, EntityValidationRule validationRule)
			: this(content, dataContext)
		{
			_validate = validationRule;
		}
							
		public Editor(UserControl content, object dataContext, Admission.ViewModel.ValidationRules.Validators.IEntityValidator validator)
			: this(content, dataContext)
		{
			_validator = validator;
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			// сначала проверяем по валидатору 
			if (_validator != null)
			{
				if (_validator.IsValid)
				{
					DialogResult = true;
				}
				else
				{
					if (_validator.ErrorList.Count > 0)
					{
						// преобразуем список ошибок в одну строку
						string str = "При проверке введённых данных были обнаружены следующие ошибки:\n\n";
						foreach (var error in _validator.ErrorList)
						{
							str += string.Format("\t— {0}\n", error);
						}
						MessageBox.Show(str, "Исправьте ошибки в данных", 
							MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
			else
			{
				// если его нет, то проверяем по правилу валидации
				// если нет и второго, то херачим тру, закрывая окно 
				if (_validate == null)
				{
					DialogResult = true;
				}
				else
				{
					if (_validate())
					{
						DialogResult = true;
					}
				}
			} 
		}
	}
}
