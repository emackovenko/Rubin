using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Reflection;
using System;

namespace Contingent.Themes.Controls
{
    [TemplatePart(Name = "PART_SearchControl", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class ExDataGrid: DataGrid
    {
        static ExDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExDataGrid), new FrameworkPropertyMetadata(typeof(ExDataGrid)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var btn = (Button)Template.FindName("PART_CloseButton", this);
            if (btn != null)
            {
                btn.Click += (sender, e) =>
                {
                    SearchText = "";
                    SearchTextVisibility = Visibility.Collapsed;
                };
            }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            var dg = this;
            if (dg.SelectedItem == null)
                return;
            dg.ScrollIntoView(dg.SelectedItem);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            if (!IsTextSearchEnabled)
                return;
            var tb = (TextBox)Template.FindName("PART_SearchControl", this);

            if (SearchTextVisibility != Visibility.Visible)
            {
                SearchTextVisibility = Visibility.Visible;
            }

            if (!tb.IsFocused)
            {
                tb.Focus();
                tb.SelectAll();
            }

            SearchText = tb.Text + e.Text;
        }

        public string SearchText
        {
            get
            {
                return (string)GetValue(SearchTextProperty);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetValue(SearchTextProperty, value);
                    FindRowByStringValue(value);
                }
            }
        }

        // Using a DependencyProperty as the backing store for SearchPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(ExDataGrid), new UIPropertyMetadata(null));



        public Visibility SearchTextVisibility
        {
            get { return (Visibility)GetValue(SearchTextVisibilityProperty); }
            set { SetValue(SearchTextVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchTextVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTextVisibilityProperty =
            DependencyProperty.Register("SearchTextVisibility", typeof(Visibility), typeof(ExDataGrid), 
                new UIPropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnSearchTextVisibilityChanged)));

        static void OnSearchTextVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Visibility newValue = (Visibility)e.NewValue;
            var exDataGrid = (ExDataGrid)d;
            var tb = (TextBox)exDataGrid.Template.FindName("PART_SearchControl", exDataGrid);
            tb.Visibility = newValue;
        }

        void FindRowByStringValue(string searchedValue)
        {
            if (string.IsNullOrWhiteSpace(searchedValue) || CurrentColumn == null)
            {
                return;
            }

            if (CurrentColumn is DataGridTextColumn)
            {
                string searchedText = searchedValue.ToLower();

                var col = CurrentColumn as DataGridTextColumn;

                string bindingPath = (col.Binding as Binding).Path.Path;

                foreach (var item in ItemsSource)
                {
                    // ќпредел€ем тип источника и получаем искомое свойство
                    var type = item.GetType();
                    var searchedProperty = type.GetProperty(bindingPath);
                    if (searchedProperty != null)
                    {
                        // ѕредставл€ем как строку и сравниваем
                        string currentValue = searchedProperty.GetValue(item, null).ToString().ToLower();
                        if (currentValue.StartsWith(searchedText))
                        {
                            SelectedItem = item;
                            ScrollIntoView(item, CurrentColumn);
                            break;
                        }
                    }
                }
                
            }

        }
    }
}
