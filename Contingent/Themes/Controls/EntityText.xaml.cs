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
    /// Логика взаимодействия для EntityTextBox.xaml
    /// </summary>
    public partial class EntityText : UserControl
    {
        public EntityText()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(EntityText));
        

        public double EditWidth
        {
            get { return (double)GetValue(EditWidthProperty); }
            set { SetValue(EditWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditWidthProperty =
            DependencyProperty.Register("EditWidth", typeof(double), typeof(EntityText), new PropertyMetadata(0.0));
        

        public object Member
        {
            get { return (object)GetValue(MemberProperty); }
            set { SetValue(MemberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MemberProperty =
            DependencyProperty.Register("Member", typeof(object), typeof(EntityText), new PropertyMetadata(null, new PropertyChangedCallback(OnMemberChanged)));


        private static void OnMemberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {            
            if (string.IsNullOrWhiteSpace(e.NewValue as string))
            {
                (d as EntityText).text.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                (d as EntityText).text.BorderBrush = new SolidColorBrush(Color.FromRgb(178, 178, 178));
            }
        }
        
    }
}
