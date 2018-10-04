using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model.Admission
{				  
	public partial class Claim
	{	

		/// <summary>
		/// Максимальное количество баллов, начисляемое за индивидуальные достижения
		/// </summary>
		private const int MAX_INDIVIDUAL_ACHIEVEMENTS_SCORE = 10;

		/// <summary>
		/// Тип возврата документов
		/// </summary>  		
		public string DocumentsReturningType
		{
			get
			{
				if (PersonalReturning == true)
				{
					return "лично";
				}
				else
				{
					return "по почте";
				}
			}
			set
			{
				if (value == "лично")
				{
					PersonalReturning = true;
				}
				else
				{
					PersonalReturning = false;
				}
			}
		}
		
		/// <summary>
		/// Гражданство, зарегистрированное в первом из документов личности
		/// </summary>
		public string Citizenship
		{
			get
			{
				string str = string.Empty;
				if (IdentityDocuments.Count > 0)
				{
					str = IdentityDocuments.First().Citizenship.Name;
				}
				return str;
			}
		}  

		/// <summary>
		/// Определяет наличие оригинала или копии документа об образовании
		/// </summary>
		public bool IsOriginal
		{
			get
			{
				bool res = false;

				if (SchoolCertificateDocuments.Count > 0)
				{
					res = (SchoolCertificateDocuments.First().OriginalReceivedDate != null);
				}

				if (MiddleEducationDiplomaDocuments.Count > 0)
				{
					res = (MiddleEducationDiplomaDocuments.First().OriginalReceivedDate != null);
				}

				if (HighEducationDiplomaDocuments.Count > 0)
				{
					res = (HighEducationDiplomaDocuments.First().OriginalReceivedDate != null);
				}

				return res;
			}
		}

		/// <summary>
		/// Балл, начисленный за индивидуальные достижения абитуриента
		/// </summary>
		public int IndividualAchievementsScore
		{
			get
			{
				int i = 0;
				foreach (var item in EntranceIndividualAchievements)
				{
					i += item.CampaignIndividualAchievement.MaxMark ?? 0;
				}

				if (i > MAX_INDIVIDUAL_ACHIEVEMENTS_SCORE)
				{
					i = MAX_INDIVIDUAL_ACHIEVEMENTS_SCORE;
				}
				return i;
			}
		}

		/// <summary>
		/// Количество экзаменационных баллов
		/// </summary>
		public int TestScore
		{
			get
			{
				int i = 0;
				foreach (var doc in EgeDocuments)
				{
					foreach (var res in doc.EgeResults)
					{
						i += res.Value ?? 0;
					}
				}
				foreach (var res in EntranceTestResults)
				{
					i += res.Value ?? 0;					
				}
				return i;
			}
		}

		/// <summary>
		/// Общее количество баллов, учитываемое в конкурсе
		/// </summary>
		public int TotalScore
		{
			get
			{
				return TestScore + IndividualAchievementsScore;
			}
		}

		/// <summary>
		/// Форма обучения условия приёма по первому приоритету
		/// </summary>
		public EducationForm EducationForm
		{
			get
			{
				if (ClaimConditions.Count > 0)
				{
					return ClaimConditions.Where(c => c.Priority == 1).First().CompetitiveGroup.EducationForm; 
				}
				return null;
			}
		}

		/// <summary>
		/// Источник финансирования условия приёма по первому приоритету
		/// </summary>
		public FinanceSource FinanceSource
		{
			get
			{
				if (ClaimConditions.Count > 0)
				{
					return ClaimConditions.Where(c => c.Priority == 1).First().CompetitiveGroup.FinanceSource;
				}
				return null;
			}
		}

		/// <summary>
		/// Направление подготовки по первому приоритету
		/// </summary>
		public Direction FirstDirection
		{
			get
			{
				if (ClaimConditions.Where(c => c.Priority == 1).Count() > 0)
				{
					return ClaimConditions.Where(c => c.Priority == 1).First().CompetitiveGroup.Direction;
				}
				return null;
			}
		}

		/// <summary>
		/// Направление подготовки по второму приоритету
		/// </summary>
		public Direction SecondDirection
		{
			get
			{
				if (ClaimConditions.Where(c => c.Priority == 2).Count() > 0)
				{
					return ClaimConditions.Where(c => c.Priority == 2).First().CompetitiveGroup.Direction;
				}
				return null;
			}
		}

		/// <summary>
		/// Направление подготовки по тртеьему приоритету
		/// </summary>
		public Direction ThirdDirection
		{
			get
			{
				if (ClaimConditions.Where(c => c.Priority == 3).Count() > 0)
				{
					return ClaimConditions.Where(c => c.Priority == 3).First().CompetitiveGroup.Direction;
				}
				return null;
			}
		}

		public Entrant Person
		{
			get
			{
				if (Entrants.Count > 0)
				{
					return Entrants.First();
				}
				return null;
			}
		}

        public double MiddleMark
        {
            get
            {
                double result = 0.0;

                if (SchoolCertificateDocuments.Count > 0)
                {
                    result = SchoolCertificateDocuments.First().MiddleMark;
                }
                if (MiddleEducationDiplomaDocuments.Count > 0)
                {
                    result = MiddleEducationDiplomaDocuments.First().MiddleMark;
                }
                if (HighEducationDiplomaDocuments.Count > 0)
                {
                    result = HighEducationDiplomaDocuments.First().MiddleMark;
                }

                return result;
            }
        }

		public EducationLevel EducationLevel
		{
			get
			{
				EducationLevel eduLevel = null;
				if (ClaimConditions.Count > 0)
				{
					eduLevel = ClaimConditions.Where(cc => cc.Priority == 1).First().CompetitiveGroup.EducationLevel;
				}
				return eduLevel;
			}
		}

        public Campaign Campaign
        {
            get
            {
                if (ClaimConditions.Where(c => c.Priority == 1).Count() > 0)
                {
                    return ClaimConditions.Where(c => c.Priority == 1).First().CompetitiveGroup.Campaign;
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public bool IsCurrentCampaign
        {
            get
            {
                return Campaign?.YearStart == DateTime.Now.Year;
            }
        }

		#region InnerLogic

		/// <summary>
		/// Возвращает имя конкурсной группы из условий приёма по переданному приоритету условия приёма
		/// </summary>
		/// <param name="priority">Приоритет</param>
		/// <returns></returns>
		public CompetitiveGroup GetCompetitiveGroupByPriority(int priority)
		{
			// фильтруем по приоритету
			if ((from cond in ClaimConditions
				 where cond.Priority == priority
				 select cond).Count() > 0)
			{
				var condition = (from cond in ClaimConditions
								 where cond.Priority == priority
								 select cond).FirstOrDefault();
                return condition.CompetitiveGroup;
			}

			return null;
		}

        public int GetExamResultBySubjectId(int subjectId)
        {
            int result = 0;

            // Получить результат ЕГЭ по предмету
            foreach (var doc in EgeDocuments)
            {
                foreach (var res in doc.EgeResults)
                {
                    if (res.ExamSubject.Id == subjectId)
                    {
                        result = (int)res.Value;
                        return result;
                    }
                }
            }

            // Найти результат СЭ по предмету
            foreach (var res in EntranceTestResults)
            {
                if (res.EntranceTest.ExamSubject.Id == subjectId)
                {
                    result = (int)res.Value;
                    return result;
                }
            }

            return result;
        }

        #endregion


        public EnrollmentOrder EnrollmentOrder
        {
            get
            {
                return EnrollmentClaims.FirstOrDefault()?.EnrollmentProtocol.EnrollmentOrder;
            }
        }


        public EnrollmentProtocol EnrollmentProtocol
        {
            get
            {
                return EnrollmentClaims.FirstOrDefault()?.EnrollmentProtocol;
            }
        }
    }
}
