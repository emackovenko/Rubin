using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace CommonMethods.Documents
{
	/// <summary>
	/// Предоставляет экземпляр документа Microsoft Excel, созданного в памяти
	/// </summary>
	public abstract class ExcelDocument : IDocument
	{	 
		public ExcelDocument()
		{
			try
			{	
				_application = new Excel.Application();
				_workbook = _application.Workbooks.Add();
				_worksheet = (Excel.Worksheet)_workbook.Worksheets.get_Item(1);	 
			}
			catch (Exception)
			{
				if (_application != null)
				{
					_application.Quit();
					_application = null;
				}
			}

		}

        /// <summary>
        /// Открывает документ в приложении Microsoft Excel, после чего возвращает управление
        /// </summary>
        /// <param name="fileName">Имя открываемого файла</param>
        public static void OpenDocument(string fileName)
        {
            var _application = new Excel.Application();
            var _workbook = _application.Workbooks.Add(fileName);
            try
            {
                var _worksheet = (Excel.Worksheet)_workbook.Worksheets.get_Item(1);
            }
            catch (Exception)
            {
                if (_application != null)
                {
                    _application.Quit();
                    _application = null;
                }
            }
        }

        Excel.Application _application;
		Excel.Workbook _workbook;
		Excel.Worksheet _worksheet;

		public void Show()
		{
			_application.Visible = true;
			_application.UserControl = true;
		}

		protected void InputIntoCell(int rowIndex, int columnIndex, string value)
		{
			try
			{
				object rowIndexObj = rowIndex;
				object columnIndexObj = columnIndex;
				object valueObj = value;
				_worksheet.Cells[rowIndexObj, columnIndexObj] = valueObj; 
				// автоматическая ширина столбца и высота строки
				_worksheet.get_Range(_worksheet.Cells[rowIndexObj, columnIndexObj], _worksheet.Cells[rowIndexObj, columnIndexObj]).EntireColumn.AutoFit();
				_worksheet.get_Range(_worksheet.Cells[rowIndexObj, columnIndexObj], _worksheet.Cells[rowIndexObj, columnIndexObj]).EntireRow.AutoFit();
			}
			catch (Exception)
			{	   

			}
		}

		protected void MergeCellRange(string beginCell, string endCell)
		{
			object beginCellObj = beginCell;
			object endCellObj = endCell;

			Excel.Range _range = (Excel.Range)_worksheet.get_Range(beginCellObj, endCellObj).Cells;
			_range.Merge(Type.Missing);
		}

	}
}
