using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Reflection;

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
            if (dg.SelectedItem == null) return;
            dg.ScrollIntoView(dg.SelectedItem);
            var dgRow = (DataGridRow)dg.ItemContainerGenerator.ContainerFromItem(dg.SelectedItem);
            if (dgRow == null) return;
            dgRow.Focus();
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
                tb.Focus();
                tb.SelectAll();
            }
            SearchTextVisibility = Visibility.Visible;
            SearchText = tb.Text + e.Text;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            SearchTextVisibility = Visibility.Collapsed;
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
            DependencyProperty.Register("SearchTextVisibility", typeof(Visibility), typeof(ExDataGrid), new UIPropertyMetadata(Visibility.Collapsed));

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
                            var cellInfo = new DataGridCellInfo(item, CurrentColumn);
                            SelectedCells.Clear();
                            SelectedCells.Add(cellInfo);
                            ScrollIntoView(item, CurrentColumn);
                            break;
                        }
                    }
                }
                
            }

        }
    }
}
