using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMethods.TypeExtensions.exDateTime
{
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Выполняет проверку принадлежности даты к указанному диапазону
		/// </summary>
		/// <param name="date">Проверяемая дата</param>
		/// <param name="startRangeDate">Начало диапазона (включается в проверку)</param>
		/// <param name="endRangeDate">Конец диапазона (включается в проверку)</param>
		/// <returns></returns>
		public static bool IsInRange(this DateTime date, DateTime startRangeDate, DateTime endRangeDate)
		{
			return date >= startRangeDate && date <= endRangeDate;
		}
	}
}
