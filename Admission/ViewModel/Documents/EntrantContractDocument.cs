using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Document;
using Model.Admission;
using CommonMethods.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exNumeric;
using CommonMethods.TypeExtensions.exString;
using ResourceLibrary.Documents;

namespace Admission.ViewModel.Documents
{
	public class EntrantContractDocument : OpenXmlDocument
	{
		public EntrantContractDocument(EntrantContract contract)
		{
			_contract = contract;
			DocumentType = OpenXmlDocumentType.Document;
		}

		EntrantContract _contract;

		public override void CreatePackage(string fileName)
		{
			var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("EntrantContract"));

			// Подготовка стилей
			var underlinedText = new CharacterFormat
			{
				UnderlineColor = Color.Black,
				UnderlineStyle = UnderlineType.Single
			};
			var underlinedBoldText = new CharacterFormat
			{
				UnderlineColor = Color.Black,
				UnderlineStyle = UnderlineType.Single,
				Bold = true
			};
			var boldText = new CharacterFormat
			{
				Bold = true
			};

			// Готовим текст
			string yearPrice = string.Format("{0} {1}", _contract.YearPrice,
				RusCurrency.Str((double)_contract.YearPrice));
			string fullPrice = string.Format("{0} {1}", _contract.FullPrice,
				RusCurrency.Str((double)_contract.FullPrice));
			string shhets = _contract.ContragentType.Id == 1 ? "2" : "3";

			// Вставляем текст на закладки
			document.InsertToBookmark("Number", _contract.Number, underlinedBoldText);
			document.InsertToBookmark("Date", ((DateTime)_contract.Date).ToString("«dd» MMMM yyyy г."));
			document.InsertToBookmark("YearPrice", yearPrice, underlinedBoldText);
			document.InsertToBookmark("FullPrice", fullPrice, underlinedBoldText);
			document.InsertToBookmark("YearPrice1", yearPrice, underlinedBoldText);
			document.InsertToBookmark("SheetCount", shhets);
			document.InsertToBookmark("AgentInfo", GetAgentInfo());
			document.InsertToBookmark("EntrantInfo", GetEntrantInfo());
			document.InsertToBookmark("EntrantName", _contract.Entrant.FullName, boldText);
			document.InsertToBookmark("AgentName", _contract.PayerName, boldText);
			document.InsertToBookmark("CompetitiveGroup", 
				string.Format("{0} {1}, {2}, {3} форма обучения",
				_contract.Entrant.Claim.FirstDirection.Code,
				_contract.Entrant.Claim.FirstDirection.Name,
				_contract.Entrant.Claim.GetCompetitiveGroupByPriority(1).EducationLevel.Name,
				_contract.Entrant.Claim.EducationForm.Name), boldText);
			string educationLevel = _contract.Entrant.Claim.EducationLevel.Id == 1 ? "высшего образования" : "среднего профессионального образования";
			string diplomaType = _contract.Entrant.Claim.EducationLevel.Id == 1 ? "диплом о высшем образовании" : "диплом о среднем профессиональном образовании";
			document.InsertToBookmark("DiplomaType", diplomaType, underlinedText);
			document.InsertToBookmark("EducationLevel", educationLevel, underlinedText);
			document.InsertToBookmark("TrainingTime", _contract.TrainingPeriod.AsPeriod(), underlinedText);

			document.Save(fileName);
		}

		string GetAgentInfo()
		{
			string str = string.Empty;
			if (_contract.ContragentPerson != null)
			{
				var agent = _contract.ContragentPerson;
				str = string.Format("{0}\n{1}\nАдрес: {2}\n\n{3}\nТелефон:{4}",
					agent.FullName, ((DateTime)agent.BirthDate).ToString("dd.MM.yyyy г."),
					agent.Address.ViewAddress,
					string.Format("{4} \n{0} {1} выдан {2}, {3}", agent.DocumentSeries,
					 agent.DocumentNumber, agent.DocumentOrganization, ((DateTime)agent.DocumentDate).ToString("dd.MM.yyyy г."),
					agent.IdentityDocumentType.Name), 
					agent.PhoneNumber);
				if (!string.IsNullOrWhiteSpace(agent.Email))
				{
					str += string.Format("\nE-mail:{0}", agent.Email);
				}
			}
			else
			{
				var agent = _contract.ContragentOrganization;
				str = string.Format("{0}\n{1}\nИНН:{2}\nКПП:{3}\nр\\с {4}\nБанк {5}\nБИК {6}\nТелефон:{7}",
					agent.Name, agent.Address.MailString, agent.Inn, agent.Kpp, agent.PayNumber, agent.BankName,
					agent.BankBik, agent.PhoneNumber);
			}
			return str;
		}

		string GetEntrantInfo()
		{
			var entrant = _contract.Entrant;
			string doc = string.Format("{4}: {0} {1} выдан {2}, {3}",
				entrant.Claim.IdentityDocuments.First().Series,
				entrant.Claim.IdentityDocuments.First().Number, 
				entrant.Claim.IdentityDocuments.First().Organization,
				((DateTime)entrant.Claim.IdentityDocuments.First().Date).ToString("dd.MM.yyyy г."),
				entrant.Claim.IdentityDocuments.First().IdentityDocumentType.Name);
			string str = string.Format("{0}\n{1}\nАдрес: {2}\n\n{3}\nТелефон:{4}",
				entrant.FullName, ((DateTime)entrant.BirthDate).ToString("dd.MM.yyyy г."), 
				entrant.Address.MailString, 
				doc, entrant.Phone);
			if (!string.IsNullOrWhiteSpace(entrant.Email))
			{
				str += string.Format("\nE-mail:{0}", entrant.Email);
			}
			return str;
		}
	}
}
