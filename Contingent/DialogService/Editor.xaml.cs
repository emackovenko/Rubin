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

namespace Contingent.DialogService
{
	/// <summary>
	/// Логика взаимодействия для Editor.xaml
	/// </summary>
	public partial class Editor : Window
	{		 
		public Editor(UserControl content, object dataContext)
		{									
			InitializeComponent();			   
			EditingContent.Children.Add(content);
			content.DataContext = dataContext;
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true; 
		}
	}
}
