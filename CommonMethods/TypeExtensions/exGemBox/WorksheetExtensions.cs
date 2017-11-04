using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = GemBox.Spreadsheet;

namespace CommonMethods.TypeExtensions.exGemBox
{
    public static class WorksheetExtensions
    {

        /// <summary>
        /// Находит текст в документе и заменяет его, если найдено. ВНИМАНИЕ! К искомой строке добавляются квадратные скобки []
        /// </summary>
        /// <param name="ws">Лист Excel</param>
        /// <param name="findStr">Искомый текст</param>
        /// <param name="replaceStr">Заменяющий текст</param>
        public static void FindAndReplaceText(this Excel.ExcelWorksheet ws, string findStr, string replaceStr)
        {
            string str = string.Format("[{0}]", findStr);
            int rowIndex, columnIndex;
            if (ws.Cells.FindText(str, true, true, out rowIndex, out columnIndex))
            {
                ws.Cells[rowIndex, columnIndex].Value = replaceStr;
            }
        }
    }
}
