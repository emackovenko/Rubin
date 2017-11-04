using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
	public class OrphanDocumentEditorViewModel: ViewModelBase
	{

		public OrphanDocumentEditorViewModel()
		{

		}

		public OrphanDocumentEditorViewModel(OrphanDocument document)
		{
			_doc = document;
		}


		OrphanDocument _doc;

		public OrphanDocument Document
		{
			get
			{
				return _doc;
			}
			set
			{
				_doc = value;
				RaisePropertyChanged("Document");
			}
		}

		public ObservableCollection<OrphanDocumentType> DocumentTypes
		{
			get
			{
				return new ObservableCollection<OrphanDocumentType>(Session.DataModel.OrphanDocumentTypes);
			}  
		}


	}
}
