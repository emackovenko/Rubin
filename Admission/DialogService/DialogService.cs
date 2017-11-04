/// <summary>
/// Делегат правила валидации редактируемой сущности (проверяется перед сохранением сущности)
/// </summary>
/// <returns></returns>
public delegate bool EntityValidationRule();	
		

/// <summary>
/// Перечисление зарегистрированных редакторов для вызываемых сервисом View
/// </summary>
public enum EditingContent
{
	/// <summary>
	/// Редактор роли
	/// </summary>
	RoleEditor,
	/// <summary>
	/// Редактор заявления
	/// </summary>
	ClaimEditor,
	/// <summary>
	/// Редактор документ, удостоверяющего личность
	/// </summary>
	IdentityDocumentEditor,
	/// <summary>
	/// Окно генерирования адреса в формате КЛАДР
	/// </summary>
	AddressSelector,							 
	/// <summary>
	/// Окно выбора образовательной организации
	/// </summary>
	EducationOrganizationSelector,
	/// <summary>
	/// Редактор условия приёма
	/// </summary>
	ClaimConditionEditor,
	/// <summary>
	/// Редактор свидетельств о результатах ЕГЭ
	/// </summary>
	EgeDocumentsEditor,
	/// <summary>
	/// Редактор результата сдачи ЕГЭ
	/// </summary>
	EgeResultEditor,
	/// <summary>
	/// Редактор документа, подтверждающего сиротство
	/// </summary>
	OrphanDocumentEditor,
	/// <summary>
	/// Редактор индивидуального достижения, учитываемого в заявлении
	/// </summary>
	EntranceIndividualAchievementEditor,   
	/// <summary>
	/// Редактор страны
	/// </summary>
	CountryEditor,
	/// <summary>
	/// Редактор региона
	/// </summary>
    RegionEditor,
	/// <summary>
	/// Редактор района
	/// </summary>
	DistrictEditor,
	/// <summary>
	/// Редактор города
	/// </summary>
	TownEditor,
	/// <summary>
	/// Редактор населенного пункта
	/// </summary>
	LocalityEditor,
	/// <summary>
	/// Редактор улицы города
	/// </summary>
	StreetFromTownEditor,
	/// <summary>
	/// Редактор улицы населенного пункта
	/// </summary>
	StreetFromLocalityEditor,
	/// <summary>
	/// Редактор образвоательного учреждения
	/// </summary>
	EducationOrganizationEditor,
	/// <summary>
	/// Редактор документа об образовании
	/// </summary>
	EducationDocumentEditor,
	/// <summary>
	/// Редактор протокола проверки результатов внутренних вступительных испытаний
	/// </summary>
	InnerEntranceExaminationProtocolEditor,
	/// <summary>
	/// Редактор протокола проверки результатов ЕГЭ
	/// </summary>
	EgeResultCheckProtocolEditor,
	EnrollmentProtocolEditor,
	EnrollmentOrderEditor,
	EntrantContract,
	ContragentPerson,
	ContragentOrganization,
	MotherCapitalCertificate,
	ContractIndividualPlanAgreement
}

/// <summary>
/// Перечисление зарегистрированных информационных блоков для вызываемых сервисом View
/// </summary>
public enum InfoContent
{
	/// <summary>
	/// Информация о роли
	/// </summary>
	RoleInfo,
	/// <summary>
	/// История версий приложения
	/// </summary>
	VersionHistory,
	/// <summary>
	/// Быстрый просмотр статистики
	/// </summary>
	FastStatistic
}

/// <summary>
/// Перечисление зарегистрированных типов сущностей для последующего выбора их пользователем
/// </summary>
public enum SelectableEntity
{
	/// <summary>
	/// Сущность-несущность (если отмену нажмут)
	/// </summary>
	NullEntity,
	/// <summary>
	/// Диплом о высшем образовании
	/// </summary>
	HighEducationDiplomaDocument,
	/// <summary>
	/// Диплом о среднем профессиональном образовании
	/// </summary>
	MiddleEducationDiplomaDocument,
	/// <summary>
	/// Аттестат о среднем (полном) образовании
	/// </summary>
	SchoolCertificate,
	EgeProtocol,
	InnerExaminationProtocol
}

/// <summary>
/// Перечисление зарегистрированных окон-мастера
/// </summary>
public enum WizardContent
{
	TestWizard
}