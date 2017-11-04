using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Controls;
using Admission.View.Editors;
using Admission.View.InfoBoxes;
using Wizards = Admission.View.Windows.Wizards;
using Workspaces = Admission.View.Workspaces;
using System.Windows;
using CommonMethods.Documents;

namespace Admission.DialogService
{
	/// <summary>
	/// Сервис взаимодействия между ViewModel и дочерними View
	/// </summary>
	public static class DialogLayer
	{
		/// <summary>
		/// Открывает редактор в диалоговом режиме и возвращает результат закрытия окна
		/// </summary>
		/// <param name="contentType">Тип редактора</param>
		/// <param name="viewModel">Контекст данных, передаваемый в редактор</param>  
		public static bool ShowEditor(EditingContent contentType, object viewModel)
		{
			Editor editor = new Editor(GetEditingContent(contentType), viewModel);
			return editor.ShowDialog() ?? false;
		}

		/// <summary>
		/// Открывает редактор в диалоговом режиме и возвращает результат закрытия окна
		/// </summary>
		/// <param name="contentType">Тип редактора</param>
		/// <param name="viewModel">Контекст данных, передаваемый в редактор</param>
		/// <param name="validationRule">Правило валидации сущности</param>
		public static bool ShowEditor(EditingContent contentType, object viewModel, 
			EntityValidationRule validationRule)
		{
			Editor editor = new Editor(GetEditingContent(contentType), viewModel, validationRule);
			return editor.ShowDialog() ?? false;
		}
				  
		/// <summary>
		/// Открывает редактор в диалоговом режиме и возвращает результат закрытия окна
		/// </summary>
		/// <param name="contentType">Тип редактора</param>
		/// <param name="viewModel">Контекст данных, передаваемый в редактор</param> 
		/// <param name="validator">Экземпляр валидатора редактируемой сущности</param>
		public static bool ShowEditor(EditingContent contentType, object viewModel, 
			Admission.ViewModel.ValidationRules.Validators.IEntityValidator validator)
		{
			Editor editor = new Editor(GetEditingContent(contentType), viewModel, validator);
			return editor.ShowDialog() ?? false;
		}
			
		/// <summary>
		/// Возвращает контрол с содержимым редактора
		/// </summary>
		/// <param name="contentType">Тип редактирующего содержимого</param>	   
		static UserControl GetEditingContent(EditingContent contentType)
		{
			switch (contentType)
			{
				case EditingContent.RoleEditor:
					return new RoleEditor();
				case EditingContent.ClaimEditor:
					return new ClaimEditor();
				case EditingContent.IdentityDocumentEditor:
					return new IdentityDocumentEditor();
				case EditingContent.AddressSelector:
					return new AddressSelector();			
				case EditingContent.EducationOrganizationSelector:
					return new EducationOrganizationSelector();
				case EditingContent.ClaimConditionEditor:
					return new ClaimConditionEditor();
				case EditingContent.EgeDocumentsEditor:
					return new EgeDocumentsEditor();
				case EditingContent.EgeResultEditor:
					return new EgeResultEditor();
				case EditingContent.OrphanDocumentEditor:
					return new OrphanDocumentEditor();
				case EditingContent.EntranceIndividualAchievementEditor:
					return new EntranceIndividualAchievementEditor();
				case EditingContent.CountryEditor:
					return new CountryEditor();
				case EditingContent.RegionEditor:
					return new RegionEditor();
				case EditingContent.DistrictEditor:
					return new DistrictEditor();
				case EditingContent.LocalityEditor:
					return new LocalityEditor();
				case EditingContent.TownEditor:
					return new TownEditor();
				case EditingContent.StreetFromTownEditor:
					return new StreetFromTownEditor();
				case EditingContent.StreetFromLocalityEditor:
					return new StreetFromLocalityEditor();
				case EditingContent.EducationOrganizationEditor:
					return new View.Editors.EducationOrganizationEditor();								 
				case EditingContent.EducationDocumentEditor:
					return new EducationDocumentEditor();
				case EditingContent.InnerEntranceExaminationProtocolEditor:
					return new Workspaces.Examinations.Pages.Editors.InnerEntranceExaminationProtocolEditor();
				case EditingContent.EgeResultCheckProtocolEditor:
					return new Workspaces.Examinations.Pages.Editors.EgeResultCheckProtocolEditor();
				case EditingContent.EnrollmentOrderEditor:
					return new Workspaces.Enrollment.Pages.Editors.EnrollmentOrderEditor();
				case EditingContent.EnrollmentProtocolEditor:
					return new Workspaces.Enrollment.Pages.Editors.EnrollmentProtocolEditor();
				case EditingContent.EntrantContract:
					return new Workspaces.ContractForming.Pages.Editors.EntrantContractEditor();
				case EditingContent.ContragentPerson:
					return new Workspaces.ContractForming.Pages.Editors.ContragentPersonEditor();
				case EditingContent.ContragentOrganization:
					return new Workspaces.ContractForming.Pages.Editors.ContragentOrganizationEditor();
				case EditingContent.MotherCapitalCertificate:
					return new Workspaces.ContractForming.Pages.Editors.MotherCapitalCertificateEditor();
				case EditingContent.ContractIndividualPlanAgreement:
					return new Workspaces.ContractForming.Pages.Editors.ContractIndividualPlanAgreementEditor();
				default:
					throw new Exception("Указанный редактор не зарегистрирован в DialogService");
			}
		}

		/// <summary>
		/// Открывает информационное окно в обычном режиме
		/// </summary>
		/// <param name="contentType">Тип инфобокса</param>
		/// <param name="viewModel">Контекст данных, передаваемый в редактор</param>  
		public static void ShowInfoBox(InfoContent contentType, object viewModel)
		{
			InfoBox infoBox = new InfoBox(GetInfoContent(contentType), viewModel);
			infoBox.Show();
		}

		/// <summary>
		/// возвращает контрол с содержимым инфобокса
		/// </summary>
		/// <param name="contentType"></param>
		/// <returns></returns>
		static UserControl GetInfoContent(InfoContent contentType)
		{
			switch (contentType)
			{
				case InfoContent.RoleInfo:
					return new RoleInfo();
				case InfoContent.VersionHistory:
					return new VersionHistoryInfo();
				case InfoContent.FastStatistic:
					return new Admission.View.Workspaces.EntrantClaims.InfoBoxes.FastAdmissionStatistic();
				default:
					throw new Exception("Указанная информационная панель не зарегистрирована в DialogService");
			}
		}	

		/// <summary>
		/// Открывает окно в диалоговом режиме, где пользователю предлагается выбрать один из элементов коллекции, после чего возвращает выбранный элемент
		/// </summary>
		/// <param name="collection">Собсно, сама коллекция</param>
		/// <returns></returns>
		public static SelectableEntity ShowEntityTypeSelector(ObservableCollection<EntityType> collection)
		{
			ItemSelector wnd = new ItemSelector(collection);
			wnd.ShowDialog();
			return wnd.SelectedItem.Entity;	
		}

		public static bool ShowWizard(WizardContent wizard)
		{
			var window = GetWizardContent(wizard);
			return window.ShowDialog() ?? false;
		}

		static Window GetWizardContent(WizardContent wizard)
		{
			switch (wizard)
			{
				case WizardContent.TestWizard:
					return new Wizards.ExaminationSeating.ExaminationSeatingWizard();
				default:
					break;
			}
			throw new InvalidOperationException();
		}

		public static void ShowDocument(OpenXmlDocument doc)
		{
			var window = new DocumentWindow(doc);
			window.Show();
		}
	}
}
