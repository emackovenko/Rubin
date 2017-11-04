using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Admission.DialogService
{
	/// <summary>
	/// Предоставляет тип сущности, которая в последующем будет создаваться
	/// </summary>
	public class EntityType
	{

		/// <summary>
		/// Предоставляет экземпляр класса EntityType, готовый к употреблению
		/// </summary>											 
		/// <param name="entity">Тип сущности, зарегистрированный в DialogService</param>
		public EntityType(SelectableEntity entity)
		{					  
			Entity = entity;
		}	 
												   
		public SelectableEntity Entity { get; private set; }
							   
		/// <summary>
		/// Предоставляет строковое представление экземпляра
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			switch (Entity)
			{
				case SelectableEntity.NullEntity:
					{
						return string.Empty;
					}
				case SelectableEntity.HighEducationDiplomaDocument:
					{
						return "Диплом о высшем профессиональном образовании";
					}
				case SelectableEntity.MiddleEducationDiplomaDocument:
					{
						return "Диплом о серднем профессиональном образовании";
					}
				case SelectableEntity.SchoolCertificate:
					{
						return "Аттестат о среднем (полном) образовании";
					}
				case SelectableEntity.EgeProtocol:
					return "Протокол проверки результатов ЕГЭ";
				case SelectableEntity.InnerExaminationProtocol:
					return "Протокол проверки результатов внутреннего экзамена";
				default:
					{
						return string.Empty;
					}
			}
		}

	}
}
