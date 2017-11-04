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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admission.View.Workspaces.EntrantClaims.Pages
{
	/// <summary>
	/// Логика взаимодействия для ClaimList.xaml
	/// </summary>
	public partial class ClaimList : Page
	{
		public ClaimList()
		{
			InitializeComponent();
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// на MVVM я на мгновение плюнул к хуям - отдельный контрол не помогает
			claimList.ScrollIntoView(claimList.SelectedItem);
		}
	}
}
