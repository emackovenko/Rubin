using System;					   
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using ResourceLibrary.Documents;

namespace CommonMethods.Documents
{
	/// <summary>
	/// Базовый класс для документов Microsoft Word
	/// </summary>
	public abstract class WordDocument : IDocument
	{	  
		Word.Application _application;
		Word.Document _document;

        /// <summary>
        /// Открывает документ в приложении Microsoft Word, после чего возвращает управление
        /// </summary>
        /// <param name="fileName">Имя открываемого файла</param>
        public static void OpenDocument(string fileName)
        {
            //офис требует объектных параметров, так что делаем обертки
            object missingObj = Missing.Value;
            object trueObj = true;
            object falseObj = false;
            object templateNameObj = fileName;

            //если вылетим здесь, то нужно закрыть приложение ворда, чтоб не висел лишний процесс
            var _application = new Word.Application();
            try
            {
                var _document = _application.Documents.Add(ref templateNameObj, ref missingObj,
                    ref missingObj, ref missingObj);
                _application.Visible = true;
                _document = null;
                _application = null;
            }
            catch (Exception)
            {
                if (_application != null)
                {
                    _application.Quit(ref missingObj, ref missingObj, ref missingObj);
                    _application = null;
                }
                throw new Exception("Приложение Microsoft Office Word недоступно.");
            }
        }

		/// <summary>
		/// Предоставляет экземпляр документа Microsoft Word, созданного в памяти по заданному шаблону
		/// </summary>
		/// <param name="templateName">Полное имя шаблона в формате dot/dotx</param>
		protected WordDocument(string templateName)
		{
			var fileName = DocumentTemplate.ExtractDoc(templateName);

			//офис требует объектных параметров, так что делаем обертки
			object missingObj = Missing.Value;
			object trueObj = true;
			object falseObj = false;
			object templateNameObj = fileName;		  

			//если вылетим здесь, то нужно закрыть приложение ворда, чтоб не висел лишний процесс
			try
			{							
				_application = new Word.Application();
				_document = _application.Documents.Add(ref templateNameObj, ref missingObj,
					ref missingObj, ref missingObj);
			}
			catch (Exception)
			{
				if (_document != null)
				{							 
					_document.Close(ref falseObj, ref missingObj, ref missingObj);
					_document = null;
				}
				if (_application != null)
				{
					 _application.Quit(ref missingObj, ref missingObj, ref missingObj);
					_application = null;
				}			   
			}
		}

		/// <summary>
		/// Структура [заменяемое значение-заменяющее значение]
		/// </summary>
		protected struct DocumentField
		{
			/// <summary>
			/// Имя заменяемого значения
			/// </summary>
			public string Name;

			/// <summary>
			/// Заменяющее значение
			/// </summary>
			public string Value;
		}

		/// <summary>
		/// Заполненный значениями список закладок
		/// </summary>
		protected List<DocumentField> BookmarkFields = new List<DocumentField>();

		/// <summary>
		/// Выводит на экран приложение Word с открытым документом
		/// </summary>
		public void Show()
		{
			if (_application != null)
			{	 
				_application.Visible = true;  
			}
		}

		/// <summary>
		/// Заменяет все вхождения строки в документе на указанный текст
		/// </summary>
		/// <param name="findStr">Искомая строка (макс. 255 симв.)</param>
		/// <param name="replaceStr">Заменяющая строка (макс. 255 симв.)</param>
		protected void ReplaceString(string findStr, string replaceStr)
		{
			if (_document == null || _application == null)
			{
				return;
			}

			// обьектные строки для Word
			object strToFindObj = findStr;
			object replaceStrObj = replaceStr;	  
			object missingObj = Missing.Value;

			// диапазон документа Word
			Word.Range wordRange;
			//тип поиска и замены
			object replaceTypeObj;
			replaceTypeObj = Word.WdReplace.wdReplaceAll;
			// обходим все разделы документа
			for (int i = 1; i <= _document.Sections.Count; i++)
			{
				// берем всю секцию диапазоном
				wordRange = _document.Sections[i].Range;

				/*
				Обходим редкий глюк в Find, ПРИЗНАННЫЙ MICROSOFT, метод Execute на некоторых машинах вылетает 
				с ошибкой "Заглушке переданы неправильные данные / Stub received bad data"  
				Подробности: http://support.microsoft.com/default.aspx?scid=kb;en-us;313104
				// выполняем метод поиска и  замены обьекта диапазона ворд
				wordRange.Find.Execute(ref strToFindObj, ref wordMissing, ref wordMissing, 
				ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, ref wordMissing, 
				ref wordMissing, ref replaceStrObj, ref replaceTypeObj, ref wordMissing, ref wordMissing, 
				ref wordMissing, ref wordMissing);
				*/

				Word.Find wordFindObj = wordRange.Find;
				object[] wordFindParameters = new object[15] { strToFindObj, missingObj, missingObj, missingObj, missingObj,
					missingObj, missingObj, missingObj, missingObj, replaceStrObj, replaceTypeObj, missingObj, missingObj,
					missingObj, missingObj };

				wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
			}  
		}			  

		/// <summary>
		/// Вставляет текст на место, где указана закладка
		/// </summary>
		/// <param name="bookmarkName">Имя закладки</param>
		/// <param name="insertedStr">Вставляемый текст</param>
		protected void InsertIntoBookmark(string bookmarkName, string insertedStr)
		{
			if (_document == null || _application == null)
			{
				return;
			}


			object bookmarkNameObj = bookmarkName;
			Word.Range range = _document.Bookmarks.get_Item(ref bookmarkNameObj).Range;
			range.Text = insertedStr;	 
		}

		/// <summary>
		/// Заполняет шаблон по свойству BookmarkFields
		/// </summary>
		protected void FillByBookmarks()
		{
			if (_document == null || _application == null)
			{
				return;
			}


			foreach (var field in BookmarkFields)
			{
				InsertIntoBookmark(field.Name, field.Value ?? "———");
			}
		}

		/// <summary>
		/// Сохраняет текущий документ на диск
		/// </summary>
		/// <param name="fileName">Полное имя файла для сохранения</param>
		public void Save(string fileName, bool closeAfterSave = true)
		{
			if (_document == null || _application == null)
			{
				return;
			}

			object missingObj = System.Reflection.Missing.Value;
			object trueObj = true;
			object falseObj = false;
			object pathToSaveObj = fileName;

			_document.SaveAs(ref pathToSaveObj, Word.WdSaveFormat.wdFormatDocument,
				ref missingObj, ref missingObj, ref missingObj, ref missingObj,
				ref missingObj, ref missingObj, ref missingObj, ref missingObj, 
				ref missingObj, ref missingObj, ref missingObj, ref missingObj, 
				ref missingObj, ref missingObj);

			if (closeAfterSave)
			{	 
				_document.Close(ref falseObj, ref missingObj, ref missingObj);
				_document = null; 
				_application.Quit(ref missingObj, ref missingObj, ref missingObj);
				_application = null;
			}
		}

		/// <summary>
		/// Вставляет содержимое файла *.doc/*.docx в текущий документ с новой страницы 
		/// </summary>
		/// <param name="fileName">Полное имя вставляемого файла</param>
		public void InsertFromFile(string fileName)
		{
			if (_document == null || _application == null)
			{
				return;
			}

			object missingObj = Missing.Value;
			object whatObj = Word.WdGoToItem.wdGoToLine;
			object whichObj = Word.WdGoToDirection.wdGoToLast;	 
			
			var range = _document.GoTo(ref whatObj, ref whichObj, ref missingObj, ref missingObj);	
			range.InsertFile(fileName);
		}

		/// <summary>
		/// Вставляет разрыв страницы в конец документа
		/// </summary>
		public void InsertBreak()
		{
			if (_document == null || _application == null)
			{
				return;
			}

			object missingObj = Missing.Value;
			object whatObj = Word.WdGoToItem.wdGoToLine;
			object whichObj = Word.WdGoToDirection.wdGoToLast;

			var range = _document.GoTo(ref whatObj, ref whichObj, ref missingObj, ref missingObj);
			range.InsertBreak(Word.WdBreakType.wdPageBreak);
		}

        /// <summary>
        /// Предоставляет доступ к интерфейсу открытого документа
        /// </summary>
        /// <returns></returns>
        [Obsolete("Использование данного метода нарушает инкапсуляцию внутренного объекта и ведет к использованию неуправляемого кода во внутренней логике приложения.")]
        protected Word.Document GetDocument()
        {
            return _document;
        }
	}
}
