using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ResourceLibrary.Properties;

namespace ResourceLibrary.Documents
{
	public static class DocumentTemplate
	{
		/// <summary>
		/// Имя каталога шаблонов
		/// </summary>
		static string templatePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) 
			+ "\\Rubin\\Templates";

		/// <summary>
		/// Справочник шаблонов [имя шаблона - шаблон из ресурсов]
		/// </summary>
		static Dictionary<string, byte[]> templateDictionary = new Dictionary<string, byte[]>
		{
			{"EntrantClaim", Resources.EntrantClaim },
			{"EnrollmentAgreementClaim", Resources.EnrollmentAgreementClaim },
			{"TitlePage", Resources.TitlePage },
			{"HostelClaim", Resources.HostelClaim },
			{ "InventoryList", Resources.InventoryList },
			{"IndividualAchievementsProtocol", Resources.IndividualAchievementsProtocol },
			{"Voucher", Resources.Voucher },
			{"MissingDocument", Resources.MissingDocument },
			{"EntrantClaimMiddleEducation", Resources.EntrantClaimMiddleEducation },
            {"GeneralizedEntrantList", Resources.EntrantList },
            {"AdmissionStatistic", Resources.AdmissionStatistic },
			{"PublicEntrantList", Resources.PublicEntrantList},
			{"InnerExaminationCheckProtocol", Resources.InnerExaminationCheckProtocol },
            {"EnrollmentProtocol", Resources.EnrollmentProtocol },
			{"EnrollmentOrder", Resources.EnrollmentOrder },
			{"ExaminationStatement", Resources.ExaminationStatement },
			{"EnrollmentOrderStatement", Resources.EnrollmentOrderStatement },
			{"EntrantContract", Resources.EntrantContract },
			{"EnrollmentDisagreementClaim", Resources.EnrollmentDisagreementClaim },
			{"ExaminationStatementMiddleEducation", Resources.ExaminationStatementMiddleEducation },
			{"ContractIndividualPlanAgreement", Resources.ContractIndividualPlanAgreement }
		};

        /// <summary>
        /// Извлекает шаблон из файла ресурсов в папку данных приложения и возвращает полное имя файла-шаблона
        /// </summary>
        /// <param name="templateName">Имя шаблона</param>
        public static string ExtractDoc(string templateName)
        {
            //получаем шаблон
            byte[] template = null;
            if (!templateDictionary.TryGetValue(templateName, out template))
            {
                throw new Exception("Шаблон с заданным именем не найден в библиотеке");
            }


            //подготовка директории	и шаблона
            //имя шаблона задано с текущим временем на случай возникновения ошибки
            string fileName = templatePath + "\\" + templateName + " - " + DateTime.Now.ToString("dd.MM.yyyy hh-mm-ss") + ".dotx";
            Directory.CreateDirectory(templatePath);

            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
            }

            //собсно, извлечение шаблона
            File.WriteAllBytes(fileName, template);

            return fileName;
        }


        /// <summary>
        /// Извлекает шаблон из файла ресурсов в папку данных приложения и возвращает полное имя файла-шаблона
        /// </summary>
        /// <param name="templateName">Имя шаблона</param>
        public static string ExtractXls(string templateName)
        {
            //получаем шаблон
            byte[] template = null;
            if (!templateDictionary.TryGetValue(templateName, out template))
            {
                throw new Exception("Шаблон с заданным именем не найден в библиотеке");
            }


            //подготовка директории	и шаблона
            //имя шаблона задано с текущим временем на случай возникновения ошибки
            string fileName = templatePath + "\\" + templateName + " - " + DateTime.Now.ToString("dd.MM.yyyy hh-mm-ss") + ".xlsx";
            Directory.CreateDirectory(templatePath);

            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
            }

            //собсно, извлечение шаблона
            File.WriteAllBytes(fileName, template);

            return fileName;
        }
    }
}
