using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Astu;
using GalaSoft.MvvmLight;

namespace Contingent.ViewModel.Workspaces.Students
{
    public class IdentityDocumentViewModel: ViewModelBase
    {
        public IdentityDocumentViewModel(IdentityDocument document)
        {
            Document = document;
        }


        private IdentityDocument _document;

        public IdentityDocument Document
        {
            get { return _document; }
            set { _document = value; RaisePropertyChanged("Document"); }
        }

        public IEnumerable<string> DocOrgans
        {
            get
            {
                return Astu.IdentityDocuments.Select(d => d.Organization).OrderBy(s => s).Distinct();
            }
        }


        public IEnumerable<string> BirthPlaces
        {
            get
            {
                return Astu.IdentityDocuments.Select(d => d.BirthPlace).OrderBy(s => s).Distinct();
            }
        }


        public IEnumerable<IdentityDocumentType> IdentityDocumentTypes
        {
            get
            {
                return Astu.IdentityDocumentTypes.OrderBy(idt => idt.Name);
            }
        }


        public IEnumerable<Citizenship> Citizenships
        {
            get
            {
                return Astu.Citizenships.OrderBy(idt => idt.Name);
            }
        }



    }
}
