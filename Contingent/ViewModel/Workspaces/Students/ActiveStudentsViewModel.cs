﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Astu;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Contingent.DialogService;

namespace Contingent.ViewModel.Workspaces.Students
{
    public class ActiveStudentsViewModel: ViewModelBase
    {
        #region Fields

        Student _selectedStudent;

        ObservableCollection<Student> _students;

        #endregion


        #region Properties

        public Student SelectedStudent
        {
            get
            {
                if (_selectedStudent == null)
                {
                    _selectedStudent = Students.FirstOrDefault();
                }
                return _selectedStudent;
            }
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged("SelectedStudent");
            }
        }

        public ObservableCollection<Student> Students
        {
            get
            {
                var activeStatuses = new string[] {"0001", "0002", "0007", "0008"};
                var students = Astu.Students.Where(s => activeStatuses.Contains(s.StatusId)).OrderBy(s => s.Course).OrderBy(s => s.Name).OrderBy(s => s.Group?.Name);
                return new ObservableCollection<Student>(students);
            }
        }

        #endregion


        #region Logic

        #region Commands

        public RelayCommand AddStudentCommand
        {
            get
            {
                return new RelayCommand(AddStudent);
            }
        }

        public RelayCommand EditStudentCommand
        {
            get
            {
                return new RelayCommand(EditStudent, EditStudentCanExecute);
            }
        }

        public RelayCommand RefreshListCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        #endregion

        #region Methods

        void AddStudent()
        {
            var student = new Student();
            var vm = new StudentViewViewModel(student);
            EditorInvoker.ShowEditor(student, StudentChangesSaving);
        }

        void StudentChangesSaving(Entity student)
        {
            Astu.Students.Add(student as Student);
            Astu.Save();
            RaisePropertyChanged("Students");
            SelectedStudent = student as Student;
        }

        void EditStudent()
        {
            EditorInvoker.ShowEditor(SelectedStudent, StudentChangesSaving);
        }

        void RefreshList()
        {
            Astu.Students.Reset();
            RaisePropertyChanged("Students");
        }

        #endregion

        #region Checks

        bool EditStudentCanExecute()
        {
            return SelectedStudent != null;
        }

        #endregion

        #endregion
    }
}
