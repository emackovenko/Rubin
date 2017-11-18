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

namespace Contingent.Themes.Controls
{
	/// <summary>
	/// Логика взаимодействия для ProtectedTabItem.xaml
	/// </summary>
	public partial class ProtectedTabItem : TabItem
	{
		public ProtectedTabItem()
		{
			InitializeComponent();			
		}

		static ProtectedTabItem()
		{																						   
			PermissionNameProperty = DependencyProperty.Register("PermissionName", typeof(string), typeof(ProtectedTabItem),
				new FrameworkPropertyMetadata("Unregistered workspace", new PropertyChangedCallback(OnPermissionNamePropertyChanged)));
			TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ProtectedTabItem),
				new FrameworkPropertyMetadata("TabItem", new PropertyChangedCallback(OnTitleChanged)));
			IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ProtectedTabItem),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIconChanged)));
		}

		public string PermissionName
		{
			get
			{
				return (string)GetValue(PermissionNameProperty);
			}
			set
			{
				SetValue(PermissionNameProperty, value);
			}
		}

		public static DependencyProperty PermissionNameProperty;

		private static void OnPermissionNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
            string value = (string)e.NewValue;
            ProtectedTabItem parent = (ProtectedTabItem)d;
            parent.PermissionName = value;
            if (true) //VisibilityControl.GetWorkspacePermission(parent.PermissionName))
            {
                parent.Visibility = Visibility.Visible;
            }
            //else
            //{
            //    parent.Visibility = Visibility.Collapsed;
            //}
        }
			  
		public string Title
		{
			get
			{
				return (string)GetValue(TitleProperty);
			}
			set
			{
				SetValue(TitleProperty, value);
			}
		}	
									
		public static DependencyProperty TitleProperty;

		private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			string value = (string)e.NewValue;
			ProtectedTabItem parent = (ProtectedTabItem)d;
			parent.Title = value;
		}

		public ImageSource Icon
		{
			get
			{
				return (ImageSource)GetValue(IconProperty);
			}
			set
			{
				SetValue(IconProperty, value);
			}
		}

		public static DependencyProperty IconProperty;

		private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ImageSource icon = (ImageSource)e.NewValue;
			ProtectedTabItem parent = (ProtectedTabItem)d;
			parent.Icon = icon;
		}


	}
}
