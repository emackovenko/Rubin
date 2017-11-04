using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Document;

namespace CommonMethods.TypeExtensions.exGemBox
{
	public static class DocumentExtensions
	{
		/// <summary>
		/// Вставляет текст на закладку
		/// </summary>
		/// <param name="document"></param>
		/// <param name="bookmarkName">Имя закладки</param>
		/// <param name="text">Вставляемый текст</param>
		public static void InsertToBookmark(this DocumentModel document, string bookmarkName, string text)
		{
			var bookmark = document.Bookmarks.Where(b => b.Name == bookmarkName).FirstOrDefault();

			if (bookmark == null)
			{
				throw new Exception("Не найдена закладка в шаблоне документа");
			}

			var range = bookmark.GetContent(false);
			range.LoadText(text);
		}

		/// <summary>
		/// Вставляет текст на закладку с указанным стилем
		/// </summary>
		/// <param name="document"></param>
		/// <param name="bookmarkName">Имя закладки</param>
		/// <param name="text">Вставляемый текст</param>
		/// <param name="format">Формат текста</param>
		public static void InsertToBookmark(this DocumentModel document, string bookmarkName, string text, CharacterFormat format)
		{
			var bookmark = document.Bookmarks.Where(b => b.Name == bookmarkName).FirstOrDefault();

			if (bookmark == null)
			{
				throw new Exception("Не найдена закладка в шаблоне документа");
			}

			var range = bookmark.GetContent(false);
			range.LoadText(text, format);
		}
	}
}
