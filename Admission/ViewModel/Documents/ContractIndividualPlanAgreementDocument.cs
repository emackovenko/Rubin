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
	public class ContractIndividualPlanAgreementDocument : OpenXmlDocument
	{
		public ContractIndividualPlanAgreementDocument(ContractIndividualPlanAuxAgreement agreement)
		{
			_agreement = agreement;
			DocumentType = OpenXmlDocumentType.Document;
		}

		ContractIndividualPlanAuxAgreement _agreement;

		public override void CreatePackage(string fileName)
		{
			var document = DocumentModel.Load(DocumentTemplate.ExtractDoc("ContractIndividualPlanAgreement"));

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
			string yearPrice = string.Format("{0} {1}", _agreement.YearPrice,
				RusCurrency.Str((double)_agreement.YearPrice));
			string fullPrice = string.Format("{0} {1}", _agreement.FullPrice,
				RusCurrency.Str((double)_agreement.FullPrice));
			
			// Вставляем текст на закладки
			document.InsertToBookmark("Number", _agreement.Number, underlinedBoldText);
			document.InsertToBookmark("Date", ((DateTime)_agreement.Date).ToString("«dd» MMMM yyyy г."));
			document.InsertToBookmark("YearPrice", yearPrice, underlinedBoldText);
			document.InsertToBookmark("FullPrice", fullPrice, underlinedBoldText);
			document.InsertToBookmark("YearPrice1", yearPrice, underlinedBoldText);		
			document.InsertToBookmark("AgentInfo", GetAgentInfo());
			document.InsertToBookmark("EntrantInfo", GetEntrantInfo());
			document.InsertToBookmark("EntrantName", _agreement.EntrantContract.Entrant.FullName, boldText);
			document.InsertToBookmark("AgentName", _agreement.EntrantContract.PayerName, boldText);
			document.InsertToBookmark("TrainingPeriod", _agreement.TrainingPeriod.AsPeriod(), underlinedText);
			document.InsertToBookmark("FullTrainingPeriod", _agreement.EntrantContract.TrainingPeriod.AsPeriod(), underlinedText);
			document.InsertToBookmark("ContractNumber1", _agreement.EntrantContract.Number, underlinedBoldText);
			document.InsertToBookmark("ContractDate1", ((DateTime)_agreement.EntrantContract.Date).ToString("dd.MM.yyyy г."), underlinedBoldText);
			document.InsertToBookmark("ContractNumber2", _agreement.EntrantContract.Number, underlinedText);
			document.InsertToBookmark("ContractDate2", ((DateTime)_agreement.EntrantContract.Date).ToString("dd.MM.yyyy г."), underlinedText);
			document.InsertToBookmark("ContractNumber3", _agreement.EntrantContract.Number, underlinedText);
			document.InsertToBookmark("ContractDate3", ((DateTime)_agreement.EntrantContract.Date).ToString("dd.MM.yyyy г."), underlinedText);
			document.InsertToBookmark("ContractNumber4", _agreement.EntrantContract.Number, underlinedText);
			document.InsertToBookmark("ContractDate4", ((DateTime)_agreement.EntrantContract.Date).ToString("dd.MM.yyyy г."), underlinedText);

			document.Save(fileName);
		}

		string GetAgentInfo()
		{
			string str = string.Empty;
			if (_agreement.EntrantContract.ContragentPerson != null)
			{
				var agent = _agreement.EntrantContract.ContragentPerson;
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
				var agent = _agreement.EntrantContract.ContragentOrganization;
				str = string.Format("{0}\n{1}\nИНН:{2}\nКПП:{3}\nр\\с {4}\nБанк {5}\nБИК {6}\nТелефон:{7}",
					agent.Name, agent.Address.MailString, agent.Inn, agent.Kpp, agent.PayNumber, agent.BankName,
					agent.BankBik, agent.PhoneNumber);
			}
			return str;
		}

		string GetEntrantInfo()
		{
			var entrant = _agreement.EntrantContract.Entrant;
			string doc = string.Format("{4}: {0} {1} выдан {2}, {3}",
				entrant.Claim.IdentityDocuments.First().Series,
				entrant.Claim.IdentityDocuments.First().Number, 
				entrant.Claim.IdentityDocuments.First().Organization,
				((DateTime)entrant.Claim.IdentityDocuments.First().Date).ToString("dd.MM.yyyy г."),
				entrant.Claim.IdentityDocuments.First().IdentityDocumentType.Name);
			string str = string.Format("{0}\n{1}\nАдрес: {2}\n\n{3}\nТелефон:{4}",
				entrant.FullName, ((DateTime)entrant.Claim.IdentityDocuments.First().BirthDate).ToString("dd.MM.yyyy г."), 
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
