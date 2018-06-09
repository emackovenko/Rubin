using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Model.Admission;

namespace Admission.ViewModel.Editors
{
	public class IdentityDocumentEditorViewModel: ViewModelBase
	{	 
		public IdentityDocumentEditorViewModel(IdentityDocument document)
		{
			_document = document;
		}

		IdentityDocument _document;
		public IdentityDocument Document
		{
			get
			{
				return _document;
			}

			set
			{
				_document = value;
				RaisePropertyChanged("Document");
			}
		}



		#region Вспомогательные коллекции

		ObservableCollection<IdentityDocumentType> _documentTypes;

		public ObservableCollection<IdentityDocumentType> DocumentTypes
		{
			get
			{
				if (_documentTypes == null)
				{
					_documentTypes = new ObservableCollection<IdentityDocumentType>(Session.DataModel.IdentityDocumentTypes.ToList());
				}
				return _documentTypes;
			}

			set
			{
				_documentTypes = value;
				RaisePropertyChanged("DocumentTypes");
			}
		}											  


		ObservableCollection<Citizenship> _citizenships;

		public ObservableCollection<Citizenship> Citizenships
		{
			get
			{
				if (_citizenships == null)
				{
					_citizenships = new ObservableCollection<Citizenship>(Session.DataModel.Citizenships.ToList());
				}
				return _citizenships;
			}

			set
			{
				_citizenships = value;
				RaisePropertyChanged("Citizenships");
			}
		}

		#endregion

	}
}
