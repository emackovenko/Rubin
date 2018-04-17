using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Xps;
using System.Printing;
using System.Windows.Documents;

namespace University.Themes.Controls
{
    public class RaiseDocumentViewer: DocumentViewer
    {
        protected override void OnPrintCommand()
        {
            // get a print dialog, defaulted to default printer and default printer's preferences.
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;

            // get a reference to the FixedDocumentSequence for the viewer.
            FixedDocumentSequence docSeq = this.Document as FixedDocumentSequence;

            // set the default page orientation based on the desired output.
            printDialog.PrintTicket.PageOrientation = GetPageOrientationOfFirstPageOfFixedDocSeq(docSeq);

            if (printDialog.ShowDialog() == true)
            {
                // set the print ticket for the document sequence and write it to the printer.
                docSeq.PrintTicket = printDialog.PrintTicket;

                XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);
                writer.WriteAsync(docSeq, printDialog.PrintTicket);
            }
        }


        /// <summary>
        /// Gets the PageOrientation of the first page of a fixed document sequence, based on that page's dimensions.
        /// </summary>
        /// <param name="docSeq">The fixed document sequence.</param>
        /// <returns>
        /// If the first page could not be found, returns Unknown.
        /// Returns Portrait when the page width is less than the height.  
        /// Otherwise (width is greater than OR EQUAL), returns Landscape.
        /// </returns>
        private static PageOrientation GetPageOrientationOfFirstPageOfFixedDocSeq(FixedDocumentSequence docSeq)
        {
            PageOrientation orientation = PageOrientation.Unknown;

            var firstPage = GetFirstDocumentOfFixedDocSeq(docSeq).Pages[0].GetPageRoot(false);

            if (firstPage != null)
            {
                orientation = (firstPage.Width >= firstPage.Height) ? PageOrientation.Landscape : PageOrientation.Portrait;
            }

            return orientation;
        }

        /// <summary>
        /// Gets the first fixed document in a fixed document sequence, or null.
        /// </summary>
        /// <param name="docSeq">The fixed document sequence</param>
        /// <returns>The first fixed document, or null.</returns>
        private static FixedDocument GetFirstDocumentOfFixedDocSeq(FixedDocumentSequence docSeq)
        {
            FixedDocument firstDoc = null;

            if (docSeq != null && docSeq.References != null && docSeq.References.Count > 0)
            {
                DocumentReference firstDocRef = docSeq.References[0];
                firstDoc = firstDocRef.GetDocument(true);
            }

            return firstDoc;
        }
    }
}
