using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Admission;
using Admission.DialogService;

namespace Admission.ViewModel.Editors
{
	public class EgeDocumentsEditorViewModel: ViewModelBase
	{	 
		#region Constructors
			 		
		public EgeDocumentsEditorViewModel(Claim claim)
		{
			_claim = claim;
		}

		#endregion

		#region Parent entities

		Claim _claim;

		EgeDocument _selectedDocument;

		public EgeDocument SelectedDocument
		{
			get
			{
				if (_selectedDocument == null)
				{
					_selectedDocument = _claim.EgeDocuments.FirstOrDefault();
				}
				return _selectedDocument;
			}  
			set
			{
				_selectedDocument = value;
				RaisePropertyChanged("SelectedDocument");
				RaisePropertyChanged("Results");
			}
		}		  

		ObservableCollection<EgeDocument> _documents;

		public ObservableCollection<EgeDocument> Documents
		{
			get
			{
				if (_documents == null)
				{
					_documents = new ObservableCollection<EgeDocument>(_claim.EgeDocuments);
				}
				return _documents;
			}

			set
			{
				_documents = value;
				RaisePropertyChanged("Documents");
			}
		}	  
				

		EgeResult _selectedResult;

		public EgeResult SelectedResult
		{
			get
			{
				if (_selectedResult == null)
				{
					_selectedResult = SelectedDocument?.EgeResults.FirstOrDefault();
				}
				return _selectedResult;
			}	   
			set
			{
				_selectedResult = value;
				RaisePropertyChanged("SelectedResult");
			}
		}

		ObservableCollection<EgeResult> _results;

		public ObservableCollection<EgeResult> Results
		{
			get
			{
				if (_selectedDocument != null)
				{
					_results = new ObservableCollection<EgeResult>(SelectedDocument.EgeResults);
				}					
				return _results;
			}
			set
			{
				_results = value;
				RaisePropertyChanged("Results");
			}
		}
		   
		#endregion

		#region Commands

		#region Commands 

		public RelayCommand AddDocumentCommand
		{
			get
			{
				return new RelayCommand(AddDocument);
			}
		}

		public RelayCommand RemoveDocumentCommand
		{
			get
			{
				return new RelayCommand(RemoveDocument, RemoveDocumentCanExecute);
			}
		}

		public RelayCommand AddResultCommand
		{
			get
			{
				return new RelayCommand(AddResult, AddResultCanExecute);
			}
		}

		public RelayCommand EditResultCommand
		{
			get
			{
				return new RelayCommand(EditResult, EditResultCanExecute);
			}
		}

		public RelayCommand RemoveResultCommand
		{
			get
			{
				return new RelayCommand(RemoveResult, RemoveDocumentCanExecute);
			}
		}

		#endregion

		#region Methods	 
			
		void AddDocument()
		{
			var newDoc = new EgeDocument();
			_claim.EgeDocuments.Add(newDoc);
			Documents.Add(newDoc);
			SelectedDocument = newDoc;
		}

		void RemoveDocument()
		{
			if (Messenger.RemoveQuestion())
			{
				_claim.EgeDocuments.Remove(_selectedDocument);
				Documents.Remove(_selectedDocument);
			}
		}

		void AddResult()
		{
			var newResult = new EgeResult();
			if (DialogLayer.ShowEditor(EditingContent.EgeResultEditor,
				new EgeResultEditorViewModel(newResult)))
			{
				SelectedDocument.EgeResults.Add(newResult);
				RaisePropertyChanged("Results");
			}
		}

		void EditResult()
		{
			DialogLayer.ShowEditor(EditingContent.EgeResultEditor,
				new EgeResultEditorViewModel(_selectedResult));
			RaisePropertyChanged("Results");	   
		}

		void RemoveResult()
		{
			if (Messenger.RemoveQuestion())
			{
				SelectedDocument.EgeResults.Remove(SelectedResult);
				Results.Remove(SelectedResult);
				RaisePropertyChanged("Results");
			}
		}

		#endregion

		#region CheckMethods

		bool RemoveDocumentCanExecute()
		{
			return _selectedDocument != null;
		}

		bool AddResultCanExecute()
		{
			return _selectedDocument != null;
		}

		bool EditResultCanExecute()
		{
			return _selectedResult != null;
		}

		bool RemoveResultCanExecute()
		{
			return _selectedResult != null;
		}

		#endregion

		#endregion

	}
}
