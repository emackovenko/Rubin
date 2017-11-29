using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonMethods.Documents;
using Model.Astu;
using ResourceLibrary.Documents;
using GemBox.Document;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exDateTime;
using CommonMethods.TypeExtensions.exString;

namespace Contingent.ViewModel.Documents
{
    /// <summary>
    /// Справка об обучении
    /// </summary>
    public class LearningTicketDocument : OpenXmlDocument
    {
        public LearningTicketDocument(Student student)
        {
            _student = student;
            DocumentType = OpenXmlDocumentType.Document;
        }

        Student _student;

        public override void CreatePackage(string fileName)
        {
            var doc = DocumentModel.Load(DocumentTemplate.ExtractDoc("LearningTicket"));

            doc.InsertToBookmark("StudentName", _student.FullName);
            doc.InsertToBookmark("StudentBirthDate", _student.BirthDate.Format("dd MMMM yyyy г."));
            doc.InsertToBookmark("DeanName", _student.Faculty.DeanName);

            var orders = _student.Orders.OrderBy(o => o.Date);
            string str = string.Empty;

            foreach (var o in orders)
            {
                str += o.DocumentLabel;
                str += "\n";
            }

            doc.InsertToBookmark("StudentOrders", str);

            doc.Save(fileName);
        }
    }
}
