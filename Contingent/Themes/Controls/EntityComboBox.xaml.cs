using System;
using System.Collections;
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
    /// Логика взаимодействия для EntityComboBoxBox.xaml
    /// </summary>
    public partial class EntityComboBox : UserControl
    {
        public EntityComboBox()
        {
            InitializeComponent();
        }
		
        

        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IEnumerable), typeof(EntityComboBox), new PropertyMetadata());



        public string DisplayPath
        {
            get { return (string)GetValue(DisplayPathProperty); }
            set { SetValue(DisplayPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayPathProperty =
            DependencyProperty.Register("DisplayPath", typeof(string), typeof(EntityComboBox), new PropertyMetadata("Name"));




        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(EntityComboBox));

        



        public double EditWidth
        {
            get { return (double)GetValue(EditWidthProperty); }
            set { SetValue(EditWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditWidthProperty =
            DependencyProperty.Register("EditWidth", typeof(double), typeof(EntityComboBox), new PropertyMetadata(0.0));

        


        public object Member
        {
            get { return (object)GetValue(MemberProperty); }
            set { SetValue(MemberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MemberProperty =
            DependencyProperty.Register("Member", typeof(object), typeof(EntityComboBox));
        


    }
}
