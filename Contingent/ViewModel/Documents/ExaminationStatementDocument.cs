using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using Model.Astu;
using ResourceLibrary.Documents;
using GemBox.Document;

namespace Contingent.ViewModel.Documents
{
    public class ExaminationStatementDocument : OpenXmlDocument
    {
        Group _group;

        public ExaminationStatementDocument(Group group)
        {
            _group = group;
            DocumentType = OpenXmlDocumentType.Document;
        }

        public override void CreatePackage(string fileName)
        {
            var doc = DocumentModel.Load(DocumentTemplate.ExtractDoc("ExaminationStatement"));
            doc.InsertToBookmark("DirectionName", _group.EducationPlan?.Direction?.Name);
            doc.InsertToBookmark("GroupName", _group.Name);
            doc.Save(fileName);
        }
    }
}
