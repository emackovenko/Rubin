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
	public partial class InfoBox : Window
	{
		public InfoBox(UserControl content, object dataContext)
		{									
			InitializeComponent();			   
			InfoContent.Children.Add(content);
			content.DataContext = dataContext;
		}
				   
	}
}
