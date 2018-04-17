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

namespace University.Themes.Controls
{
	/// <summary>
	/// Логика взаимодействия для WorkspaceMenuItem.xaml
	/// </summary>
	public partial class WorkspaceMenuItem : UserControl
	{
		public WorkspaceMenuItem()
		{
			InitializeComponent();
		}

		static WorkspaceMenuItem()
		{
			TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WorkspaceMenuItem),
				new FrameworkPropertyMetadata("Menu Item Title", new PropertyChangedCallback(OnTitleChanged)));	  
			URLProperty = DependencyProperty.Register("URL", typeof(Uri), typeof(WorkspaceMenuItem),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(OnURLChanged)));
		}

		public static DependencyProperty TitleProperty;
		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			string newTitle = (string)e.NewValue;
			WorkspaceMenuItem item = (WorkspaceMenuItem)d;
			item.Title = newTitle;
		}
																										 
		public static DependencyProperty URLProperty;
		public Uri URL
		{
			get { return (Uri)GetValue(URLProperty); }
			set { SetValue(URLProperty, value); }
		}
		private static void OnURLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Uri url = (Uri)e.NewValue;
			WorkspaceMenuItem item = (WorkspaceMenuItem)d;
			item.URL = url;
		}


	}
}
