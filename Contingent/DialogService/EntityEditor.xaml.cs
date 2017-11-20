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
using GalaSoft.MvvmLight;
using Model.Astu;

namespace Contingent.DialogService
{
    /// <summary>
    /// Логика взаимодействия для EntityEditor.xaml
    /// </summary>
    public partial class EntityEditor : Window
    {
        Action<Entity> _saveAction;
        Entity _editedEntity;
        Entity _backupEntity;
        bool _ignoreSaving;

        public EntityEditor(Entity entity, UserControl editingContent, ViewModelBase vm, Action<Entity> saveAction)
        {
            InitializeComponent();
            _editedEntity = entity;
            _backupEntity = _editedEntity.Clone() as Entity;
            DataContext = vm;
            _saveAction = saveAction;
            _ignoreSaving = false;
            EditingContentGrid.Children.Add(editingContent);
        }

        public EntityEditor(Entity entity, UserControl editingContent, ViewModelBase vm, bool ignoreSaving)
        {
            InitializeComponent();
            _ignoreSaving = ignoreSaving;
            _editedEntity = entity;
            _backupEntity = _editedEntity.Clone() as Entity;
            DataContext = vm;
            EditingContentGrid.Children.Add(editingContent);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
            _editedEntity.RestoreFromBackup(_backupEntity);
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_ignoreSaving)
            {
                if (_saveAction != null)
                {
                    _saveAction(_editedEntity);
                }
                else
                {
                    _editedEntity.Save();
                }
            }

            DialogResult = true;
        }
    }
}
