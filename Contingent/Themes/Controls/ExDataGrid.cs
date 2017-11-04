using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            var mi = DataContext.GetType().GetMethod("EditItem");
            if (mi != null)
                mi.Invoke(DataContext, null);
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
            SearchTextVisibility = Visibility.Visible;
            //SearchText = e.Text;
            tb.Focus();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            SearchTextVisibility = Visibility.Collapsed;
        }

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
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
    }
}
