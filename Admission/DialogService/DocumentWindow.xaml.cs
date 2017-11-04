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
using CommonMethods.Documents;

namespace Admission.DialogService
{
	/// <summary>
	/// Логика взаимодействия для DocumentWindow.xaml
	/// </summary>
	public partial class DocumentWindow : Window
	{
		public DocumentWindow(OpenXmlDocument doc)
		{
			InitializeComponent();
			docArea.Document = doc;
		}
	}
}
