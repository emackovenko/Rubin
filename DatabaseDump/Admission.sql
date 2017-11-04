--
-- Скрипт сгенерирован Devart dbForge Studio for MySQL, Версия 7.2.76.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 20.07.2017 11:50:20
-- Версия сервера: 5.5.50-38.0
-- Версия клиента: 4.1
--


--
-- Описание для базы данных Admission
--
DROP DATABASE IF EXISTS Admission;
CREATE DATABASE Admission
CHARACTER SET utf8
COLLATE utf8_general_ci;

-- 
-- Отключение внешних ключей
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Установить режим SQL (SQL mode)
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Установка кодировки, с использованием которой клиент будет посылать запросы на сервер
--
SET NAMES 'utf8';

-- 
-- Установка базы данных по умолчанию
--
USE Admission;

--
-- Описание для таблицы BudgetLevels
--
CREATE TABLE BudgetLevels (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Уровни бюджета'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы CampaignStatuses
--
CREATE TABLE CampaignStatuses (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Статусы приёмной кампании'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы CampaignTypes
--
CREATE TABLE CampaignTypes (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 6
AVG_ROW_LENGTH = 3276
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Типы приёмной кампании'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы Citizenships
--
CREATE TABLE Citizenships (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(100) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(5) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 3
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Справочник гражданства';

--
-- Описание для таблицы ClaimStatuses
--
CREATE TABLE ClaimStatuses (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Код в справочнике ФИС',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 5
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Статусы заявлений';

--
-- Описание для таблицы Classrooms
--
CREATE TABLE Classrooms (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(10) DEFAULT NULL COMMENT 'Наименование (номер)',
  LineCount int(11) DEFAULT NULL COMMENT 'Количество рядов',
  PlacesInLineCount int(11) DEFAULT NULL COMMENT 'Количество мест в каждом ряду',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 3
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Аудитории для экзаменов и консультаций';

--
-- Описание для таблицы Commands
--
CREATE TABLE Commands (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  Description varchar(255) DEFAULT NULL COMMENT 'Описание',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Защищённые команды, используемые в приложении'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы Countries
--
CREATE TABLE Countries (
  Id int(2) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(50) DEFAULT NULL COMMENT 'Наименование',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 8
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Географические объекты из КЛАДР первого уровня (государства)';

--
-- Описание для таблицы Directions
--
CREATE TABLE Directions (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Code varchar(10) DEFAULT NULL COMMENT 'Код направления подготовки',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ShortName varchar(255) DEFAULT NULL COMMENT 'Краткое наименование',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id),
  UNIQUE INDEX Code (Code)
)
ENGINE = INNODB
AUTO_INCREMENT = 13
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Направления подготовки'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы Districts
--
CREATE TABLE Districts (
  Id int(2) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(50) DEFAULT NULL COMMENT 'Наименование',
  Prefix varchar(10) DEFAULT NULL COMMENT 'Префикс',
  MailIndex varchar(6) DEFAULT NULL COMMENT 'Почтовый индекс',
  RegionId int(11) DEFAULT NULL COMMENT 'Идентификатор родительского обекта',
  ParentLevel int(11) DEFAULT 2 COMMENT 'Уровень родительского объекта',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 1889
AVG_ROW_LENGTH = 89
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Географические объекты из КЛАДР второго уровня (районы субъектов)';

--
-- Описание для таблицы EducationForms
--
CREATE TABLE EducationForms (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  RegistrationNumberMemberPart varchar(10) DEFAULT NULL COMMENT 'Значение, вставляемое в регистрационный номер заявления',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Формы обучения'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы EducationLevels
--
CREATE TABLE EducationLevels (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 3
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Уровни образования'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы EducationProgramTypes
--
CREATE TABLE EducationProgramTypes (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Виды образовательных программ';

--
-- Описание для таблицы ExamSubjects
--
CREATE TABLE ExamSubjects (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(100) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(5) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 6
AVG_ROW_LENGTH = 3276
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Дисциплины для сдачи экзаменов';

--
-- Описание для таблицы FinanceSources
--
CREATE TABLE FinanceSources (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  NameInDocument varchar(255) DEFAULT NULL COMMENT 'Строковое представление в документе',
  ExportCode varchar(10) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  SortNumber int(11) DEFAULT NULL COMMENT 'Порядковый номер сортировки',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 5
AVG_ROW_LENGTH = 4096
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Источники финансирования'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы ForeignLanguages
--
CREATE TABLE ForeignLanguages (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Иностранные языки';

--
-- Описание для таблицы Genders
--
CREATE TABLE Genders (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(10) DEFAULT NULL COMMENT 'Наименование',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 3
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Справочник полов';

--
-- Описание для таблицы IdentityDocumentTypes
--
CREATE TABLE IdentityDocumentTypes (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(100) DEFAULT NULL COMMENT 'Наименование',
  NameInDocument varchar(255) DEFAULT NULL COMMENT 'Выводимое имя в документе',
  ExportCode varchar(5) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 6
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Типы документов, удостоверяющих личность';

--
-- Описание для таблицы IndividualAchievementCategories
--
CREATE TABLE IndividualAchievementCategories (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(100) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(5) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 17
AVG_ROW_LENGTH = 1170
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Категории индивидуальных достижений';

--
-- Описание для таблицы Localities
--
CREATE TABLE Localities (
  Id int(2) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(50) DEFAULT NULL COMMENT 'Наименование',
  Prefix varchar(10) DEFAULT NULL COMMENT 'Префикс',
  MailIndex varchar(6) DEFAULT NULL COMMENT 'Почтовый индекс',
  DistrictID int(11) DEFAULT NULL COMMENT 'Идентификатор родительского объекта',
  ParentLevel int(11) DEFAULT 3 COMMENT 'Уровень родительского объекта',
  PRIMARY KEY (Id),
  INDEX IDX_cladr_locality_District (DistrictID)
)
ENGINE = INNODB
AUTO_INCREMENT = 159626
AVG_ROW_LENGTH = 69
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Географические объекты из КЛАДР четвертого уровня (населенные пункты, райцентры, села и т.д.)';

--
-- Описание для таблицы OrphanDocumentTypes
--
CREATE TABLE OrphanDocumentTypes (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  ExportCode varchar(5) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 13
AVG_ROW_LENGTH = 2340
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Типы документов, подтверждающих сиротство';

--
-- Описание для таблицы PreviousEducationLevels
--
CREATE TABLE PreviousEducationLevels (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Уровни образования, полученного ранее';

--
-- Описание для таблицы Regions
--
CREATE TABLE Regions (
  Id int(2) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(30) DEFAULT NULL COMMENT 'Наименование',
  Prefix varchar(10) DEFAULT NULL COMMENT 'Префикс',
  MailIndex varchar(6) DEFAULT NULL COMMENT 'Почтовый индекс',
  CountryId int(11) DEFAULT NULL COMMENT 'Идентификатор государства',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 111
AVG_ROW_LENGTH = 190
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Географические объекты из КЛАДР первого уровня (субъекты государств)';

--
-- Описание для таблицы Roles
--
CREATE TABLE Roles (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Роли пользователей'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы Streets
--
CREATE TABLE Streets (
  Id int(2) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(50) DEFAULT NULL COMMENT 'Наименование',
  Prefix varchar(10) DEFAULT NULL COMMENT 'Префикс',
  MailIndex varchar(6) DEFAULT NULL COMMENT 'Почтовый индекс',
  ParentID int(11) DEFAULT NULL COMMENT 'Идентификатор родительского объекта',
  ParentLevel int(11) DEFAULT 3 COMMENT 'Уровень родительского объекта',
  PRIMARY KEY (Id),
  INDEX indParID (ParentID),
  INDEX indParLev (ParentLevel)
)
ENGINE = INNODB
AUTO_INCREMENT = 852954
AVG_ROW_LENGTH = 68
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Географические объекты из КЛАДР пятого уровня (улицы)';

--
-- Описание для таблицы Towns
--
CREATE TABLE Towns (
  Id int(2) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(50) DEFAULT NULL COMMENT 'Наименование',
  Prefix varchar(10) DEFAULT NULL COMMENT 'Префикс',
  MailIndex varchar(6) DEFAULT NULL COMMENT 'Почтовый индекс',
  RegionId int(11) DEFAULT NULL COMMENT 'Идентификатор родительского объекта',
  ParentLevel int(11) DEFAULT 2 COMMENT 'Уровень родительского объекта',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 517
AVG_ROW_LENGTH = 201
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Географические объекты второго уровня (города)';

--
-- Описание для таблицы Versions
--
CREATE TABLE Versions (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Version varchar(15) DEFAULT NULL COMMENT 'Версия',
  ChangeList text DEFAULT NULL COMMENT 'Список изменений',
  Date date DEFAULT NULL COMMENT 'Дата выпуска обновления',
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 13
AVG_ROW_LENGTH = 3276
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'История версий приложения';

--
-- Описание для таблицы Workspaces
--
CREATE TABLE Workspaces (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  Description varchar(255) DEFAULT NULL COMMENT 'Описание',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name)
)
ENGINE = INNODB
AUTO_INCREMENT = 8
AVG_ROW_LENGTH = 2340
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Рабочие области приложения'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы Addresses
--
CREATE TABLE Addresses (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  CountryId int(11) DEFAULT NULL COMMENT 'Идентификатор государства (1 уровень)',
  RegionId int(11) DEFAULT NULL COMMENT 'Идентификатор субъекта (2 уровень)',
  DistrictId int(11) DEFAULT NULL COMMENT 'Идентификатор района (3 уровень)',
  TownId int(11) DEFAULT NULL COMMENT 'Идентификатор города (3 уровень)',
  LocalityId int(11) DEFAULT NULL COMMENT 'Идентификатор населенного пункта (4 уровень)',
  StreetId int(11) DEFAULT NULL COMMENT 'Идентификатор улицы (5 уровень)',
  BuildingNumber varchar(5) DEFAULT NULL COMMENT 'Номер строения',
  FlatNumber varchar(5) DEFAULT NULL COMMENT 'Номер квартиры',
  PRIMARY KEY (Id),
  CONSTRAINT FK_Addresses_Countries_Id FOREIGN KEY (CountryId)
  REFERENCES Countries (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Addresses_Districts_Id FOREIGN KEY (DistrictId)
  REFERENCES Districts (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Addresses_Localities_Id FOREIGN KEY (LocalityId)
  REFERENCES Localities (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Addresses_Regions_Id FOREIGN KEY (RegionId)
  REFERENCES Regions (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Addresses_Streets_Id FOREIGN KEY (StreetId)
  REFERENCES Streets (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Addresses_Towns_Id FOREIGN KEY (TownId)
  REFERENCES Towns (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 535
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Адреса объектов в формате КЛАДР';

--
-- Описание для таблицы Campaigns
--
CREATE TABLE Campaigns (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Название',
  YearStart year(4) DEFAULT NULL COMMENT 'Год начала',
  YearEnd year(4) DEFAULT NULL COMMENT 'Год окончания',
  CampaignStatusId int(11) DEFAULT NULL COMMENT 'Идентификатор статуса приёмной кампании',
  CampaignTypeId int(11) DEFAULT NULL COMMENT 'Идентификатор типа приёмной кампании',
  PRIMARY KEY (Id),
  CONSTRAINT FK_Capmaigns_CampaignStatuses_Id FOREIGN KEY (CampaignStatusId)
  REFERENCES CampaignStatuses (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Capmaigns_CampaignTypes_Id FOREIGN KEY (CampaignTypeId)
  REFERENCES CampaignTypes (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 2
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Приёмные кампании'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы Claims
--
CREATE TABLE Claims (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Number varchar(15) DEFAULT NULL COMMENT 'Номер заявления',
  RegistrationDate date DEFAULT NULL COMMENT 'Дата регистрации заявления',
  IsHostelNeed tinyint(1) DEFAULT 0 COMMENT 'Признак необходимости общежития',
  ClaimStatusId int(11) DEFAULT NULL COMMENT 'Идентификатор статуса заявления',
  StatusComment varchar(255) DEFAULT NULL COMMENT 'Коментарий к статусу заявления (опционально)',
  PersonalReturning tinyint(1) DEFAULT 1 COMMENT 'Возврат документов лично абитуриенту в случае не поступления (иначе по почте)',
  ReturnDate date DEFAULT NULL COMMENT 'Дата отправки в архив (возврата документов)',
  PRIMARY KEY (Id),
  UNIQUE INDEX Number (Number),
  CONSTRAINT FK_Claims_ClaimStatuses_Id FOREIGN KEY (ClaimStatusId)
  REFERENCES ClaimStatuses (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 258
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Заявления абитуриентов';

--
-- Описание для таблицы CommandPermissions
--
CREATE TABLE CommandPermissions (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  RoleId int(11) DEFAULT NULL COMMENT 'Идентификатор роли',
  CommandId int(11) DEFAULT NULL COMMENT 'Идентификатор команды',
  PRIMARY KEY (Id),
  UNIQUE INDEX RoleId (RoleId, CommandId),
  CONSTRAINT FK_CommandPermissions_Commands_Id FOREIGN KEY (CommandId)
  REFERENCES Commands (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CommandPermissions_Roles_Id FOREIGN KEY (RoleId)
  REFERENCES Roles (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Разрешения на выполнение защищенных комманд для ролей'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы EducationOrganizationTypes
--
CREATE TABLE EducationOrganizationTypes (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  PreviousEducationLevelId int(11) DEFAULT NULL COMMENT 'Идентификатор уровня полученного образования',
  PRIMARY KEY (Id),
  CONSTRAINT FK_EducationOrganizationTypes_PreviousEducationLevels_Id FOREIGN KEY (PreviousEducationLevelId)
  REFERENCES PreviousEducationLevels (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 10
AVG_ROW_LENGTH = 1820
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Типы образовательных учреждений';

--
-- Описание для таблицы Users
--
CREATE TABLE Users (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Username varchar(50) DEFAULT NULL COMMENT 'Логин',
  PasswordHash varchar(255) DEFAULT NULL COMMENT 'Хэш-функция пароля',
  LastName varchar(50) DEFAULT NULL COMMENT 'Фамилия',
  FirstName varchar(50) DEFAULT NULL COMMENT 'Имя',
  Patronymic varchar(50) DEFAULT NULL COMMENT 'Отчество',
  Phone varchar(21) DEFAULT NULL COMMENT 'Номер телефона',
  Email varchar(100) DEFAULT NULL COMMENT 'Адрес электронной почты',
  RoleId int(11) DEFAULT NULL COMMENT 'Идентификатор роли',
  PRIMARY KEY (Id),
  UNIQUE INDEX Email (Email),
  UNIQUE INDEX Phone (Phone),
  UNIQUE INDEX Username (Username),
  CONSTRAINT FK_Users_Roles_Id FOREIGN KEY (RoleId)
  REFERENCES Roles (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 11
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Пользователи'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы WorkspacePermissions
--
CREATE TABLE WorkspacePermissions (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  RoleId int(11) DEFAULT NULL COMMENT 'Идентификатор роли',
  WorkspaceId int(11) DEFAULT NULL COMMENT 'Идентификатор рабочей области',
  PRIMARY KEY (Id),
  UNIQUE INDEX RoleId (RoleId, WorkspaceId),
  CONSTRAINT FK_WorkspacePermissions_Roles_Id FOREIGN KEY (RoleId)
  REFERENCES Roles (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_WorkspacePermissions_Workspaces_Id FOREIGN KEY (WorkspaceId)
  REFERENCES Workspaces (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 16
AVG_ROW_LENGTH = 2340
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Разрешения на отображение рабочих областей для ролей'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы AdmissionVolume
--
CREATE TABLE AdmissionVolume (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  CampaignId int(11) DEFAULT NULL COMMENT 'Идентификатор приёмной кампании',
  EducationLevelId int(11) DEFAULT NULL COMMENT 'Идентификатор уровня образования',
  DirectionId int(11) DEFAULT NULL COMMENT 'Идентификатор направления подготовки',
  NumberBudgetO int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест очной формы',
  NumberBudgetZ int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест заочной формы',
  NumberBudgetOZ int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест очно-заочной формы',
  NumberPaidO int(11) DEFAULT NULL COMMENT 'Количество платных мест очной формы',
  NumberPaidZ int(11) DEFAULT NULL COMMENT 'Количество платных мест заочной формы',
  NumberPaidOZ int(11) DEFAULT NULL COMMENT 'Количество платных мест очно-заочной формы',
  NumberQuotaO int(11) DEFAULT NULL COMMENT 'Количество льготных мест очной формы',
  NumberQuotaZ int(11) DEFAULT NULL COMMENT 'Количество льготных мест заочной формы',
  NumberQuotaOZ int(11) DEFAULT NULL COMMENT 'Количество льготных мест очно-заочной формы',
  NumberTargetO int(11) DEFAULT NULL COMMENT 'Количество целевых мест очной формы',
  NumberTargetZ int(11) DEFAULT NULL COMMENT 'Количество целевых мест заочной формы',
  NumberTargetOZ int(11) DEFAULT NULL COMMENT 'Количество целевых мест очно-заочной формы',
  PRIMARY KEY (Id),
  UNIQUE INDEX CampaignId (CampaignId, EducationLevelId, DirectionId),
  CONSTRAINT FK_AdmissionVolume_Capmaigns_Id FOREIGN KEY (CampaignId)
  REFERENCES Campaigns (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_AdmissionVolume_Directions_Id FOREIGN KEY (DirectionId)
  REFERENCES Directions (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_AdmissionVolume_EducationLevels_Id FOREIGN KEY (EducationLevelId)
  REFERENCES EducationLevels (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Объем приёма по направлению подготовки'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы CampaignIndividualAchievements
--
CREATE TABLE CampaignIndividualAchievements (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  CampaignId int(11) DEFAULT NULL COMMENT 'Идентификатор приёмной кампании',
  Name varchar(100) DEFAULT NULL COMMENT 'Наименование',
  IndividualAchievementCategoryId int(11) DEFAULT NULL COMMENT 'Идентификатор категории индивидуального достижения',
  MaxMark int(11) DEFAULT NULL COMMENT 'Максимальный балл, начисляемый за индивидуальное достижение',
  ExportCode varchar(5) DEFAULT NULL COMMENT 'Идентификатор в справочнике базы данных ФИС',
  PRIMARY KEY (Id),
  UNIQUE INDEX Name (Name),
  CONSTRAINT FK_CampaignIndividualAchievements_Campaigns_Id FOREIGN KEY (CampaignId)
  REFERENCES Campaigns (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CIndividualAchievements_IndividualAchievementCategories_Id FOREIGN KEY (IndividualAchievementCategoryId)
  REFERENCES IndividualAchievementCategories (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 25
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Индивидуальные достижения, учитываемые в приёмной кампании';

--
-- Описание для таблицы CompetitiveGroups
--
CREATE TABLE CompetitiveGroups (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  CampaignId int(11) DEFAULT NULL COMMENT 'Идентификатор приёмной кампании',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование конкурса',
  EducationLevelId int(11) DEFAULT NULL COMMENT 'Идентификатор уровня образования',
  FinanceSourceId int(11) DEFAULT NULL COMMENT 'Идентификатор источника финасирования',
  EducationFormId int(11) DEFAULT NULL COMMENT 'Идентификатор формы обучения',
  DirectionId int(11) DEFAULT NULL COMMENT 'Идентификатор направления подготовки',
  IsAdditional tinyint(1) DEFAULT 0 COMMENT 'Признак конкурса для дополнительного набора',
  RegistrationNumberMemberPart varchar(10) DEFAULT NULL COMMENT 'Значение, вставляемое в регистрационный номер заявления',
  AdmissionFirstStageEndDate date DEFAULT NULL COMMENT 'Дата окончания приёма документов для желающих быть зачисленными на первом этапе',
  AdmissionSecondStageEndDate date DEFAULT NULL COMMENT 'Дата окончания приёма документов для желающих быть зачисленными на втором этапе',
  AdmissionThirdStageEndDate date DEFAULT NULL COMMENT 'Дата окончания приёма документов для желающих быть зачисленными на третьем этапе',
  EducationProgramTypeId int(11) DEFAULT NULL COMMENT 'Идентификатор типа образовательной программы',
  ExportCode varchar(255) DEFAULT NULL COMMENT 'Код для экспорта в ФИС ЕГЭ и Приёма',
  PlaceCount int(11) DEFAULT NULL COMMENT 'Количество мест для приёма',
  PRIMARY KEY (Id),
  CONSTRAINT FK_CompetitiveGroups_Capmaigns_Id FOREIGN KEY (CampaignId)
  REFERENCES Campaigns (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CompetitiveGroups_Directions_Id FOREIGN KEY (DirectionId)
  REFERENCES Directions (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CompetitiveGroups_Education FOREIGN KEY (EducationProgramTypeId)
  REFERENCES EducationProgramTypes (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CompetitiveGroups_EducationForms_Id FOREIGN KEY (EducationFormId)
  REFERENCES EducationForms (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CompetitiveGroups_EducationLevels_Id FOREIGN KEY (EducationLevelId)
  REFERENCES EducationLevels (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CompetitiveGroups_FinanceSources_Id FOREIGN KEY (FinanceSourceId)
  REFERENCES FinanceSources (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 36
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Конкурсные группы'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы EducationOrganizations
--
CREATE TABLE EducationOrganizations (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  Name varchar(255) DEFAULT NULL COMMENT 'Наименование',
  EducationOrganizationTypeId int(11) DEFAULT NULL COMMENT 'Идентификатор типа образовательного учреждения',
  AddressId int(11) DEFAULT NULL COMMENT 'Идентификатор адреса (нас. пункта)',
  PRIMARY KEY (Id),
  CONSTRAINT FK_EducationOrganizations_Addresses_Id FOREIGN KEY (AddressId)
  REFERENCES Addresses (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_EducationOrganizations_EducationOrganizationTypes_Id FOREIGN KEY (EducationOrganizationTypeId)
  REFERENCES EducationOrganizationTypes (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 123
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Образовательные учреждения';

--
-- Описание для таблицы EgeDocuments
--
CREATE TABLE EgeDocuments (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор документа, приложенного к заявлению',
  OriginalReceivedDate date DEFAULT NULL COMMENT 'Дата предоставления оригинала',
  Number varchar(20) DEFAULT NULL COMMENT 'Номер документа',
  Date date DEFAULT NULL COMMENT 'Дата выдачи документа',
  Year year(4) DEFAULT NULL COMMENT 'Год выдачи',
  PRIMARY KEY (Id),
  CONSTRAINT FK_EgeDocuments_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 101
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Свидетельства о результатах ЕГЭ';

--
-- Описание для таблицы EntranceTests
--
CREATE TABLE EntranceTests (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  CampaignId int(11) DEFAULT NULL COMMENT 'Идентификатор приёмной кампании',
  EducationFormId int(11) DEFAULT NULL COMMENT 'Идентификатор формы обучения',
  FinanceSourceId int(11) DEFAULT NULL COMMENT 'Идентификатор источника финансирования',
  ExamSubjectId int(11) DEFAULT NULL COMMENT 'Идентификатор дисциплины',
  RegistrationDateRangeBegin date DEFAULT NULL COMMENT 'Начало диапазона дат, доступных для регистрации на экзамен',
  RegistrationDateRangeEnd date DEFAULT NULL COMMENT 'Конец диапазона дат, доступных для регистрации на экзамен',
  ConsultationDate date DEFAULT NULL COMMENT 'Дата консультации',
  ConsultationTime time DEFAULT NULL COMMENT 'Время консультации',
  ExaminationDate date DEFAULT NULL COMMENT 'Дата экзамена',
  ExaminationTime time DEFAULT NULL COMMENT 'Время экзамена',
  ReserveDate date DEFAULT NULL COMMENT 'Дата резервного дня',
  ReserveTime time DEFAULT NULL COMMENT 'Время резервного дня',
  PRIMARY KEY (Id),
  CONSTRAINT FK_EntranceTests_Campaigns_Id FOREIGN KEY (CampaignId)
  REFERENCES Campaigns (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_EntranceTests_EducationForms_Id FOREIGN KEY (EducationFormId)
  REFERENCES EducationForms (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_EntranceTests_ExamSubjects_Id FOREIGN KEY (ExamSubjectId)
  REFERENCES ExamSubjects (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_EntranceTests_FinanceSources_Id FOREIGN KEY (FinanceSourceId)
  REFERENCES FinanceSources (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 19
AVG_ROW_LENGTH = 1365
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Вступительные испытания, проводимые ВУЗом самостоятельно';

--
-- Описание для таблицы Entrants
--
CREATE TABLE Entrants (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  LastName varchar(50) DEFAULT NULL COMMENT 'Фамилия',
  FirstName varchar(50) DEFAULT NULL COMMENT 'Имя',
  Patronymic varchar(50) DEFAULT NULL COMMENT 'Отчество',
  GenderId int(11) DEFAULT 1 COMMENT 'Идентификатор пола',
  CustomInformation varchar(255) DEFAULT NULL COMMENT 'Дополнительные сведения, предоставленные абитуриентом',
  Email varchar(100) DEFAULT NULL COMMENT 'Электронный адрес',
  AddressId int(11) DEFAULT NULL COMMENT 'Идентификатор адреса регистрации',
  Phone varchar(21) DEFAULT NULL COMMENT 'Номер телефона (стационарный)',
  MobilePhone varchar(21) DEFAULT NULL COMMENT 'Номер телефона (мобильный)',
  FatherName varchar(255) DEFAULT NULL COMMENT 'Ф.И.О. отца',
  FatherPhone varchar(21) DEFAULT NULL COMMENT 'Номер телефона отца',
  FatherJob varchar(255) DEFAULT NULL COMMENT 'Место работы отца',
  MotherName varchar(255) DEFAULT NULL COMMENT 'Ф.И.О. матери',
  MotherPhone varchar(21) DEFAULT NULL COMMENT 'Номер телефона матери',
  MotherJob varchar(255) DEFAULT NULL COMMENT 'Место работы матери',
  JobPost varchar(255) DEFAULT NULL COMMENT 'Работа - Должность.',
  JobOrganization varchar(255) DEFAULT NULL COMMENT 'Работа - Организация',
  JobStage decimal(2, 1) DEFAULT NULL COMMENT 'Работа - Стаж',
  ForeignLanguageId int(11) DEFAULT NULL COMMENT 'Идентификатор изучаемого иностранного языка',
  PRIMARY KEY (Id),
  UNIQUE INDEX ClaimId (ClaimId),
  CONSTRAINT FK_Entrants_Addresses_Id FOREIGN KEY (AddressId)
  REFERENCES Addresses (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Entrants_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Entrants_ForeignLanguages_Id FOREIGN KEY (ForeignLanguageId)
  REFERENCES ForeignLanguages (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_Entrants_Genders_Id FOREIGN KEY (GenderId)
  REFERENCES Genders (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 276
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Абитуриенты';

--
-- Описание для таблицы IdentityDocuments
--
CREATE TABLE IdentityDocuments (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор документа, приложенного к заявлению',
  OriginalReceivedDate date DEFAULT NULL COMMENT 'Дата предоставления оригинала',
  Series varchar(20) DEFAULT NULL COMMENT 'Серия документа',
  Number varchar(20) DEFAULT NULL COMMENT 'Номер документа',
  Date date DEFAULT NULL COMMENT 'Дата выдачи документа',
  SubdivisionCode varchar(15) DEFAULT NULL COMMENT 'Код подразделения',
  Organization varchar(255) DEFAULT NULL COMMENT 'Организация, выдавшая документ',
  IdenityDocumentTypeId int(11) DEFAULT NULL COMMENT 'Идентификатор типа документа, удостоверяющего личность',
  CitizenshipId int(11) DEFAULT NULL COMMENT 'Идентификатор гражданства',
  BirthDate date DEFAULT NULL COMMENT 'Дата рождения',
  BirthPlace varchar(255) DEFAULT NULL COMMENT 'Место рождения',
  PRIMARY KEY (Id),
  CONSTRAINT FK_IdentityDocuments_Citizenships_Id FOREIGN KEY (CitizenshipId)
  REFERENCES Citizenships (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_IdentityDocuments_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_IdentityDocuments_IdentityDocumentTypes_Id FOREIGN KEY (IdenityDocumentTypeId)
  REFERENCES IdentityDocumentTypes (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 245
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Документы, удостоверяющие личность';

--
-- Описание для таблицы OrphanDocuments
--
CREATE TABLE OrphanDocuments (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  OriginalReceivedDate date DEFAULT NULL COMMENT 'Дата предоставления оригинала',
  OrphanDocumentTypeId int(11) DEFAULT NULL COMMENT 'Идентификатор типа документа, подтверждающего сиротство',
  Series varchar(20) DEFAULT NULL COMMENT 'Серия документа',
  Number varchar(20) DEFAULT NULL COMMENT 'Номер документа',
  Date date DEFAULT NULL COMMENT 'Дата выдачи документа',
  Organization varchar(255) DEFAULT NULL COMMENT 'Организация, выдавшая документ',
  PRIMARY KEY (Id),
  CONSTRAINT FK_OrphanDocuments_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_OrphanDocuments_OrphanDocumentTypes_Id FOREIGN KEY (OrphanDocumentTypeId)
  REFERENCES OrphanDocumentTypes (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 16
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Документы, подтверждающие сиротство';

--
-- Описание для таблицы OtherRequiredDocuments
--
CREATE TABLE OtherRequiredDocuments (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  Fluorography tinyint(1) DEFAULT NULL COMMENT 'Флюорография',
  MedicinePermission tinyint(1) DEFAULT NULL COMMENT 'Медицинская справка',
  Photos tinyint(1) DEFAULT NULL COMMENT 'Фотографии',
  Certificate tinyint(1) DEFAULT NULL COMMENT 'Сертификат о прививках',
  PRIMARY KEY (Id),
  UNIQUE INDEX UK_OtherRequiredDocuments_Clai (ClaimId),
  CONSTRAINT FK_OtherRequiredDocuments_Clai FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 254
AVG_ROW_LENGTH = 73
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Другие истребуемые документы';

--
-- Описание для таблицы ClaimConditions
--
CREATE TABLE ClaimConditions (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  CompetitiveGroupId int(11) DEFAULT NULL COMMENT 'Идентификатор конкурсной группы',
  Priority int(11) DEFAULT 1 COMMENT 'Приоритет зачисления',
  EnrollmentAgreementDate date DEFAULT NULL COMMENT 'Дата заявления о согласии на зачисление',
  EnrollmentDisclaimerDate date DEFAULT NULL COMMENT 'Дата заявления об отказе на зачисление',
  PRIMARY KEY (Id),
  UNIQUE INDEX ClaimId (ClaimId, CompetitiveGroupId),
  UNIQUE INDEX ClaimId_2 (ClaimId, Priority),
  CONSTRAINT FK_ClaimConditions_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_ClaimConditions_CompetitiveGroups_Id FOREIGN KEY (CompetitiveGroupId)
  REFERENCES CompetitiveGroups (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 415
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Условия приёма';

--
-- Описание для таблицы CompetitionEntranceTests
--
CREATE TABLE CompetitionEntranceTests (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  CompetitiveGroupId int(11) DEFAULT NULL COMMENT 'Идентификатор конкурсной группы',
  EntranceTestId int(11) DEFAULT NULL COMMENT 'Идентификатор вступительного испытания, проводимого ВУЗом самостоятельно',
  MinScore int(11) DEFAULT NULL COMMENT 'Минимальное количество баллов',
  Priority int(11) DEFAULT NULL COMMENT 'Приоритет вступительного испытания',
  PRIMARY KEY (Id),
  CONSTRAINT FK_CompetitionEntranceTests_CompetitiveGroups_Id FOREIGN KEY (CompetitiveGroupId)
  REFERENCES CompetitiveGroups (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_CompetitionEntranceTests_EntranceTests_Id FOREIGN KEY (EntranceTestId)
  REFERENCES EntranceTests (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 626
AVG_ROW_LENGTH = 2730
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Вступительные испытания, проводимые ВУЗом самостоятельно, распределённые по конкурсным группам';

--
-- Описание для таблицы CompetitiveGroupItems
--
CREATE TABLE CompetitiveGroupItems (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  CompetitiveGroupId int(11) DEFAULT NULL COMMENT 'Идентификатор конкурсной группы',
  NumberBudgetO int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест очной формы',
  NumberBudgetZ int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест заочной формы',
  NumberBudgetOZ int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест очно-заочной формы',
  NumberPaidO int(11) DEFAULT NULL COMMENT 'Количество платных мест очной формы',
  NumberPaidZ int(11) DEFAULT NULL COMMENT 'Количество платных мест заочной формы',
  NumberPaidOZ int(11) DEFAULT NULL COMMENT 'Количество платных мест очно-заочной формы',
  NumberQuotaO int(11) DEFAULT NULL COMMENT 'Количество льготных мест очной формы',
  NumberQuotaZ int(11) DEFAULT NULL COMMENT 'Количество льготных мест заочной формы',
  NumberQuotaOZ int(11) DEFAULT NULL COMMENT 'Количество льготных мест очно-заочной формы',
  NumberTargetO int(11) DEFAULT NULL COMMENT 'Количество целевых мест очной формы',
  NumberTargetZ int(11) DEFAULT NULL COMMENT 'Количество целевых мест заочной формы',
  NumberTargetOZ int(11) DEFAULT NULL COMMENT 'Количество целевых мест очно-заочной формы',
  PRIMARY KEY (Id),
  CONSTRAINT FK_CompetitiveGroupItems_CompetitiveGroups_Id FOREIGN KEY (CompetitiveGroupId)
  REFERENCES CompetitiveGroups (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 31
AVG_ROW_LENGTH = 546
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Места для приёма в конкурсной группе'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы ConsultationsLocations
--
CREATE TABLE ConsultationsLocations (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  EntranceTestId int(11) DEFAULT NULL COMMENT 'Идентификатор экзамена',
  ClassroomId int(11) DEFAULT NULL COMMENT 'Идентификатор аудитории',
  PRIMARY KEY (Id),
  CONSTRAINT FK_ConsultationsLocations_Clas FOREIGN KEY (ClassroomId)
  REFERENCES Classrooms (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_ConsultationsLocations_Entr FOREIGN KEY (EntranceTestId)
  REFERENCES EntranceTests (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 21
AVG_ROW_LENGTH = 819
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Журнал записи консультаций на аудиторию';

--
-- Описание для таблицы DistributedAdmissionVolume
--
CREATE TABLE DistributedAdmissionVolume (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  AdmissionVolumeId int(11) DEFAULT NULL COMMENT 'Идентификатор объёма приёма по направлению подготовки',
  BudgetLevelId int(11) DEFAULT NULL COMMENT 'Идентификатор уровня финансирования',
  NumberBudgetO int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест очной формы',
  NumberBudgetZ int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест заочной формы',
  NumberBudgetOZ int(11) DEFAULT NULL COMMENT 'Количество бюджетных мест очно-заочной формы',
  NumberQuotaO int(11) DEFAULT NULL COMMENT 'Количество льготных мест очной формы',
  NumberQuotaZ int(11) DEFAULT NULL COMMENT 'Количество льготных мест заочной формы',
  NumberQuotaOZ int(11) DEFAULT NULL COMMENT 'Количество льготных мест очно-заочной формы',
  NumberTargetO int(11) DEFAULT NULL COMMENT 'Количество целевых мест очной формы',
  NumberTargetZ int(11) DEFAULT NULL COMMENT 'Количество целевых мест заочной формы',
  NumberTargetOZ int(11) DEFAULT NULL COMMENT 'Количество целевых мест очно-заочной формы',
  PRIMARY KEY (Id),
  UNIQUE INDEX AdmissionVolumeId (AdmissionVolumeId, BudgetLevelId),
  CONSTRAINT FK_DistributedAdmissionVolume_AdmissionVolume_Id FOREIGN KEY (AdmissionVolumeId)
  REFERENCES AdmissionVolume (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_DistributedAdmissionVolume_BudgetLevels_Id FOREIGN KEY (BudgetLevelId)
  REFERENCES BudgetLevels (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Объем приёма, распределённый по уровню бюджета'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы EgeResults
--
CREATE TABLE EgeResults (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  EgeDocumentId int(11) DEFAULT NULL COMMENT 'Идентификатор свидетельства о результатах ЕГЭ',
  ExamSubjectId int(11) DEFAULT NULL COMMENT 'Идентификатор дисциплины',
  Value int(11) DEFAULT NULL COMMENT 'Балл',
  IsChecked tinyint(1) DEFAULT 0 COMMENT 'Флаг проверки в БД ФИС ГИА и приёма',
  PRIMARY KEY (Id),
  CONSTRAINT FK_EgeResults_EgeDocuments_Id FOREIGN KEY (EgeDocumentId)
  REFERENCES EgeDocuments (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_EgeResults_ExamSubjects_Id FOREIGN KEY (ExamSubjectId)
  REFERENCES ExamSubjects (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 268
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Дисциплины для сдачи экзаменов';

--
-- Описание для таблицы EntranceIndividualAchievements
--
CREATE TABLE EntranceIndividualAchievements (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  CampaignIndividualAchievementId int(11) DEFAULT NULL COMMENT 'Идентификатор индивидуального достижения, указанного в приёмной кампании',
  PRIMARY KEY (Id),
  UNIQUE INDEX ClaimId (ClaimId, CampaignIndividualAchievementId),
  CONSTRAINT FK_EntranceIndividualAchievem2 FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_EntranceIndividualAchieveme FOREIGN KEY (CampaignIndividualAchievementId)
  REFERENCES CampaignIndividualAchievements (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 42
AVG_ROW_LENGTH = 5461
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Индивидуальные достижения, учитываемые в заявлении';

--
-- Описание для таблицы EntranceTestResults
--
CREATE TABLE EntranceTestResults (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  EntranceTestId int(11) DEFAULT NULL COMMENT 'Идентификатор вступительного испытания',
  Value int(11) DEFAULT NULL COMMENT 'Балл',
  PrimaryMark int(11) DEFAULT 0 COMMENT 'Первичный балл',
  Variant varchar(3) DEFAULT NULL COMMENT 'Номер варианта',
  ClassroomId int(11) DEFAULT NULL COMMENT 'Идентификатор аудитории',
  TableLineNumber int(11) DEFAULT NULL COMMENT 'Номер ряда в аудитории',
  TablePlaceNumber int(11) DEFAULT NULL COMMENT 'Номер парты в ряду',
  PRIMARY KEY (Id),
  UNIQUE INDEX ClaimId (ClaimId, EntranceTestId),
  CONSTRAINT FK_EntranceTestResults_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_EntranceTestResults_EntranceTests_Id FOREIGN KEY (EntranceTestId)
  REFERENCES EntranceTests (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 451
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Результаты вступительных испытаний, проводимых ВУЗом самостоятельно';

--
-- Описание для таблицы ExaminationsLocations
--
CREATE TABLE ExaminationsLocations (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  EntranceTestId int(11) DEFAULT NULL COMMENT 'Идентификатор экзамена',
  ClassroomId int(11) DEFAULT NULL COMMENT 'Идентификатор аудитории',
  PRIMARY KEY (Id),
  CONSTRAINT FK_ExaminationsLocations_Class FOREIGN KEY (ClassroomId)
  REFERENCES Classrooms (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_ExaminationsLocations_Entra FOREIGN KEY (EntranceTestId)
  REFERENCES EntranceTests (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 37
AVG_ROW_LENGTH = 455
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Журнал записи экзаменов на аудиторию';

--
-- Описание для таблицы HighEducationDiplomaDocuments
--
CREATE TABLE HighEducationDiplomaDocuments (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  OriginalReceivedDate date DEFAULT NULL COMMENT 'Дата предоставления оригинала',
  Series varchar(20) DEFAULT NULL COMMENT 'Серия документа',
  Number varchar(20) DEFAULT NULL COMMENT 'Номер документа',
  RegistrationNumber varchar(30) DEFAULT NULL COMMENT 'Регистрационный номер',
  Date date DEFAULT NULL COMMENT 'Дата выдачи документа',
  SubdivisionCode varchar(15) DEFAULT NULL COMMENT 'Код подразделения',
  EducationOrganizationId int(11) DEFAULT NULL COMMENT 'Идентификатор организации, выдавшей документ',
  GraduationYear year(4) DEFAULT NULL COMMENT 'Год окончания учебного заведения',
  FiveCount int(11) DEFAULT 0 COMMENT 'Количество оценок "5"',
  FourCount int(11) DEFAULT 0 COMMENT 'Количество оценок "4"',
  ThreeCount int(11) DEFAULT 0 COMMENT 'Количество оценок "3"',
  PRIMARY KEY (Id),
  CONSTRAINT FK_HighEducationDiplomaDocuments_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_HighEducationDiplomaDocuments_EducationOrganizations_Id FOREIGN KEY (EducationOrganizationId)
  REFERENCES EducationOrganizations (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 6
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Дипломы о высшем образовании';

--
-- Описание для таблицы MiddleEducationDiplomaDocuments
--
CREATE TABLE MiddleEducationDiplomaDocuments (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  OriginalReceivedDate date DEFAULT NULL COMMENT 'Дата предоставления оригинала',
  Series varchar(20) DEFAULT NULL COMMENT 'Серия документа',
  Number varchar(20) DEFAULT NULL COMMENT 'Номер документа',
  RegistrationNumber varchar(30) DEFAULT NULL COMMENT 'Регистрационный номер',
  Date date DEFAULT NULL COMMENT 'Дата выдачи документа',
  SubdivisionCode varchar(15) DEFAULT NULL COMMENT 'Код подразделения',
  EducationOrganizationId int(11) DEFAULT NULL COMMENT 'Идентификатор организации, выдавшей документ',
  GraduationYear year(4) DEFAULT NULL COMMENT 'Год окончания учебного заведения',
  FiveCount int(11) DEFAULT 0 COMMENT 'Количество оценок "5"',
  FourCount int(11) DEFAULT 0 COMMENT 'Количество оценок "4"',
  ThreeCount int(11) DEFAULT 0 COMMENT 'Количество оценок "3"',
  PRIMARY KEY (Id),
  CONSTRAINT FK_MiddleEducationDiplomaDocuments_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_MiddleEducationDiplomaDocuments_EducationOrganizations_Id FOREIGN KEY (EducationOrganizationId)
  REFERENCES EducationOrganizations (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 119
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Дипломы о среднем профессиональном образовании';

--
-- Описание для таблицы SchoolCertificateDocuments
--
CREATE TABLE SchoolCertificateDocuments (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления',
  OriginalReceivedDate date DEFAULT NULL COMMENT 'Дата предоставления оригинала',
  Series varchar(20) DEFAULT NULL COMMENT 'Серия документа',
  Number varchar(20) DEFAULT NULL COMMENT 'Номер документа',
  Date date DEFAULT NULL COMMENT 'Дата выдачи документа',
  SubdivisionCode varchar(15) DEFAULT NULL COMMENT 'Код подразделения',
  EducationOrganizationId int(11) DEFAULT NULL COMMENT 'Идентификатор организации, выдавшей документ',
  GraduationYear year(4) DEFAULT NULL COMMENT 'Год окончания учебного заведения',
  FiveCount int(11) DEFAULT 0 COMMENT 'Количество оценок "5"',
  FourCount int(11) DEFAULT 0 COMMENT 'Количество оценок "4"',
  ThreeCount int(11) DEFAULT 0 COMMENT 'Количество оценок "3"',
  PRIMARY KEY (Id),
  CONSTRAINT FK_SchoolCertificateDocuments_Claims_Id FOREIGN KEY (ClaimId)
  REFERENCES Claims (Id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT FK_SchoolCertificateDocuments_EducationOrganizations_Id FOREIGN KEY (EducationOrganizationId)
  REFERENCES EducationOrganizations (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 105
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Аттестаты о среднем (полном) общем образовании';

--
-- Описание для таблицы EnrollmentAgreementClaims
--
CREATE TABLE EnrollmentAgreementClaims (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  ClaimConditionId int(11) DEFAULT NULL COMMENT 'Идентификатор условия приёма в заявлении',
  Date date DEFAULT NULL COMMENT 'Дата регистрации заявления',
  PRIMARY KEY (Id),
  CONSTRAINT FK_EnrollmentAgreementClaims_ClaimConditions_Id FOREIGN KEY (ClaimConditionId)
  REFERENCES ClaimConditions (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Заявления о согласии на зачисление';

--
-- Описание для таблицы EnrollmentDisagreementClaims
--
CREATE TABLE EnrollmentDisagreementClaims (
  Id int(11) NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор',
  EnrollmentAgreementClaimId int(11) DEFAULT NULL COMMENT 'Идентификатор заявления о согласии на зачисление',
  Date date DEFAULT NULL COMMENT 'Дата регистрации заявления',
  PRIMARY KEY (Id),
  CONSTRAINT FK_EnrollmentDisagreementClaims_EnrollmentAgreementClaims_Id FOREIGN KEY (EnrollmentAgreementClaimId)
  REFERENCES EnrollmentAgreementClaims (Id) ON DELETE SET NULL ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8
COLLATE utf8_general_ci
COMMENT = 'Заявления о согласии на зачисление';

DELIMITER $$

--
-- Описание для процедуры ClearBlankRecords
--
CREATE PROCEDURE ClearBlankRecords ()
COMMENT 'ВНИМАНИЕ!!! Удаляет все несвязанные ни с одним заявлением записи.'
BEGIN
  DELETE
    FROM ClaimConditions
  WHERE ClaimId IS NULL;
  DELETE
    FROM EgeDocuments
  WHERE ClaimId IS NULL;
  DELETE
    FROM EgeResults
  WHERE EgeDocumentId IS NULL;
  DELETE
    FROM EnrollmentAgreementClaims
  WHERE ClaimConditionId IS NULL;
  DELETE
    FROM EnrollmentDisagreementClaims
  WHERE EnrollmentAgreementClaimId IS NULL;
  DELETE
    FROM EntranceTestResults
  WHERE ClaimId IS NULL;
  DELETE
    FROM Entrants
  WHERE ClaimId IS NULL;
  DELETE
    FROM HighEducationDiplomaDocuments
  WHERE ClaimId IS NULL;
  DELETE
    FROM IdentityDocuments
  WHERE ClaimId IS NULL;
  DELETE
    FROM EntranceIndividualAchievements
  WHERE ClaimId IS NULL;
  DELETE
    FROM MiddleEducationDiplomaDocuments
  WHERE ClaimId IS NULL;
  DELETE
    FROM OrphanDocuments
  WHERE ClaimId IS NULL;
  DELETE
    FROM OtherRequiredDocuments
  WHERE ClaimId IS NULL;
  DELETE
    FROM SchoolCertificateDocuments
  WHERE ClaimId IS NULL;
END
$$

--
-- Описание для функции GetAddress
--
CREATE FUNCTION GetAddress (AddressId int)
RETURNS varchar(555) charset utf8
DETERMINISTIC
COMMENT 'Возвращает адрес в формате КЛАДР'
BEGIN
  RETURN (SELECT
      CONCAT_WS(', ',
      c.Name,
      CONCAT(r.Name, ' ', r.Prefix),
      CONCAT(d.Name, ' ', d.Prefix),
      CONCAT(t.Prefix, '. ', t.Name),
      CONCAT(l.Prefix, '. ', l.Name),
      CONCAT(s.Prefix, '. ', s.Name),
      CONCAT('д. ', a.BuildingNumber),
      CONCAT('кв. ', a.FlatNumber)) AS Address
    FROM Addresses a
      LEFT JOIN Countries c
        ON c.Id = a.CountryId
      LEFT JOIN Regions r
        ON r.Id = a.RegionId
      LEFT JOIN Districts d
        ON a.DistrictId = d.Id
      LEFT JOIN Towns t
        ON a.TownId = t.Id
      LEFT JOIN Localities l
        ON a.LocalityId = l.Id
      LEFT JOIN Streets s
        ON a.StreetId = s.Id
    WHERE a.Id = AddressId
    LIMIT 1);
END
$$

--
-- Описание для функции GetEtrantTestMark
--
CREATE FUNCTION GetEtrantTestMark (claimId int)
RETURNS int(11)
BEGIN
  DECLARE result int;
  DECLARE innerRes int;
  DECLARE egeRes int;
  SET result = 0;
  SET innerRes = result + (SELECT
      SUM(etr.Value)
    FROM EntranceTestResults etr
    WHERE etr.ClaimId = claimId);
  SET egeRes = result + (SELECT
      SUM(er.Value)
    FROM EgeResults er
      INNER JOIN EgeDocuments ed
        ON er.EgeDocumentId = ed.Id
    WHERE ed.ClaimId = claimId);

  IF innerRes IS NULL THEN
    SET innerRes = 0;
  END IF;
  IF egeRes IS NULL THEN
    SET egeRes = 0;
  END IF;
  SET result = innerRes + egeRes;
  RETURN result;
END
$$

--
-- Описание для функции GetEtrantTotalMark
--
CREATE FUNCTION GetEtrantTotalMark (claimId int)
RETURNS int(11)
BEGIN
  DECLARE result int;
  DECLARE iaMark int;
  DECLARE innerRes int;
  DECLARE egeRes int;
  SET result = 0;
  SET innerRes = result + (SELECT
      SUM(etr.Value)
    FROM EntranceTestResults etr
    WHERE etr.ClaimId = claimId);
  SET egeRes = result + (SELECT
      SUM(er.Value)
    FROM EgeResults er
      INNER JOIN EgeDocuments ed
        ON er.EgeDocumentId = ed.Id
    WHERE ed.ClaimId = claimId);
  SET iaMark = (SELECT
      SUM(cia.MaxMark)
    FROM EntranceIndividualAchievements eia
      INNER JOIN CampaignIndividualAchievements cia
        ON eia.CampaignIndividualAchievementId = cia.Id
    WHERE eia.ClaimId = claimId);
  IF iaMark IS NULL THEN
    SET iaMark = 0;
  END IF;
  IF innerRes IS NULL THEN
    SET innerRes = 0;
  END IF;
  IF egeRes IS NULL THEN
    SET egeRes = 0;
  END IF;
  IF iaMark > 10 THEN
    SET iaMark = 10;
  END IF;
  SET result = innerRes + egeRes + iaMark;
  RETURN result;
END
$$

--
-- Описание для функции GetMailAddress
--
CREATE FUNCTION GetMailAddress (AddressId int)
RETURNS varchar(500) charset utf8
DETERMINISTIC
COMMENT 'Возвращает почтовый адрес в формате КЛАДР'
BEGIN

  DECLARE ind varchar(6);
  SET ind = (SELECT
      IF(
      ##condition
      LENGTH(s.MailIndex) = 6,
      ##operation
      s.MailIndex,


      ##other IF
      IF(
      ##condition
      LENGTH(l.MailIndex) = 6,
      ##operation
      l.MailIndex,


      ##other IF
      IF(
      ##condition
      LENGTH(d.MailIndex) = 6,
      ##operation
      d.MailIndex,


      ##other IF
      IF(
      ##condition
      LENGTH(t.MailIndex) = 6,
      ##operation
      t.MailIndex,


      ##other IF
      IF(
      ##condition
      LENGTH(r.MailIndex) = 6,
      ##operation
      r.MailIndex,
      NULL
      )
      )
      )
      )
      )
    FROM Addresses a
      LEFT JOIN Countries c
        ON c.Id = a.CountryId
      LEFT JOIN Regions r
        ON r.Id = a.RegionId
      LEFT JOIN Districts d
        ON a.DistrictId = d.Id
      LEFT JOIN Towns t
        ON a.TownId = t.Id
      LEFT JOIN Localities l
        ON a.LocalityId = l.Id
      LEFT JOIN Streets s
        ON a.StreetId = s.Id
    WHERE a.Id = AddressId
    LIMIT 1);

  RETURN CONCAT_WS(' ', ind, GetAddress(AddressId));

END
$$

--
-- Описание для функции IsOriginal
--
CREATE FUNCTION IsOriginal (claimId int)
RETURNS tinyint(1)
BEGIN
  DECLARE school int;
  DECLARE middle int;
  DECLARE high int;
  DECLARE result bool;
  SET result = FALSE;
  SET school = (SELECT
      COUNT(*)
    FROM SchoolCertificateDocuments scd
    WHERE scd.claimId = claimId
    AND scd.OriginalReceivedDate IS NOT NULL);
  SET middle = (SELECT
      COUNT(*)
    FROM MiddleEducationDiplomaDocuments medd
    WHERE medd.claimId = claimId
    AND medd.OriginalReceivedDate IS NOT NULL);
  SET high = (SELECT
      COUNT(*)
    FROM HighEducationDiplomaDocuments hedd
    WHERE hedd.claimId = claimId
    AND hedd.OriginalReceivedDate IS NOT NULL);
  IF (school > 0) THEN
    SET result = TRUE;
  END IF;
  IF (middle > 0) THEN
    SET result = TRUE;
  END IF;
  IF (high > 0) THEN
    SET result = TRUE;
  END IF;
  RETURN result;
END
$$

DELIMITER ;

--
-- Описание для представления EntrantList
--
CREATE OR REPLACE
VIEW EntrantList
AS
SELECT
  `ef`.`Name` AS `EducationForm`,
  CONCAT_WS(' ', `d`.`Code`, `d`.`Name`) AS `Direction`,
  CONCAT_WS(' ', `e`.`LastName`, `e`.`FirstName`, `e`.`Patronymic`) AS `EntrantName`,
  `c`.`Number` AS `RegistrationNumber`,
  IF(`IsOriginal`(`c`.`Id`), 'оригинал', 'копия') AS `OriginalOrCopy`,
  `fs`.`Name` AS `Category`,
  `cc`.`Priority` AS `Priority`,
  `GetEtrantTestMark`(`c`.`Id`) AS `TestMark`
FROM ((((((`Claims` `c`
  JOIN `Entrants` `e`
    ON ((`c`.`Id` = `e`.`ClaimId`)))
  JOIN `ClaimConditions` `cc`
    ON ((`c`.`Id` = `cc`.`ClaimId`)))
  JOIN `CompetitiveGroups` `cg`
    ON ((`cc`.`CompetitiveGroupId` = `cg`.`Id`)))
  JOIN `EducationForms` `ef`
    ON ((`cg`.`EducationFormId` = `ef`.`Id`)))
  JOIN `FinanceSources` `fs`
    ON ((`cg`.`FinanceSourceId` = `fs`.`Id`)))
  JOIN `Directions` `d`
    ON ((`cg`.`DirectionId` = `d`.`Id`)))
WHERE (`c`.`ClaimStatusId` IN (1, 2))
ORDER BY `fs`.`SortNumber`, `GetEtrantTestMark`(`c`.`Id`) DESC, `c`.`RegistrationDate`, CONCAT_WS(' ', `e`.`LastName`, `e`.`FirstName`, `e`.`Patronymic`);

--
-- Описание для представления TotalStatisticReport
--
CREATE OR REPLACE
VIEW TotalStatisticReport
AS
SELECT
  `cg`.`EducationFormId` AS `EducationFormId`,
  CONCAT_WS(' ', `d`.`Code`, `d`.`Name`) AS `Direction`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpBudget`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 3)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpQuota`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 2)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpExtrabudget`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`FinanceSourceId` <> 3)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpCommon`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 1)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `BudgetFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 1)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `BudgetOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 3)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `QuotaFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 3)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `QuotaOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 2)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `ExtrabudgetFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 2)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `ExtrabudgetOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `CommonFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `CommonOriginalClaimCount`
FROM (`CompetitiveGroups` `cg`
  JOIN `Directions` `d`
    ON ((`cg`.`DirectionId` = `d`.`Id`)))
GROUP BY `cg`.`EducationFormId`,
         CONCAT_WS(' ', `d`.`Code`, `d`.`Name`)
ORDER BY `cg`.`EducationLevelId`, CONCAT_WS(' ', `d`.`Code`, `d`.`Name`);

--
-- Описание для представления TotalStatisticReport_DailyForm
--
CREATE OR REPLACE
VIEW TotalStatisticReport_DailyForm
AS
SELECT
  CONCAT_WS(' ', `d`.`Code`, `d`.`Name`) AS `Direction`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpBudget`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 3)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpQuota`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 2)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpExtrabudget`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`FinanceSourceId` <> 3)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpCommon`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 1)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `BudgetFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 1)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `BudgetOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 3)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `QuotaFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 3)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `QuotaOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 2)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `ExtrabudgetFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 2)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `ExtrabudgetOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `CommonFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `CommonOriginalClaimCount`
FROM (`CompetitiveGroups` `cg`
  JOIN `Directions` `d`
    ON ((`cg`.`DirectionId` = `d`.`Id`)))
WHERE (`cg`.`EducationFormId` = 1)
GROUP BY CONCAT_WS(' ', `d`.`Code`, `d`.`Name`)
ORDER BY `cg`.`EducationLevelId`, CONCAT_WS(' ', `d`.`Code`, `d`.`Name`);

--
-- Описание для представления TotalStatisticReport_NotDailyForm
--
CREATE OR REPLACE
VIEW TotalStatisticReport_NotDailyForm
AS
SELECT
  CONCAT_WS(' ', `d`.`Code`, `d`.`Name`) AS `Direction`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpBudget`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 3)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpQuota`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`FinanceSourceId` = 2)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpExtrabudget`,
  IFNULL((SELECT
      SUM(`cgb`.`PlaceCount`)
    FROM `CompetitiveGroups` `cgb`
    WHERE ((`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`FinanceSourceId` <> 3)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))), 0) AS `KcpCommon`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 1)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `BudgetFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 1)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `BudgetOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 3)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `QuotaFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 3)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `QuotaOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 2)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `ExtrabudgetFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cgb`.`FinanceSourceId` = 2)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `ExtrabudgetOriginalClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`))) AS `CommonFactClaimCount`,
  (SELECT
      COUNT(0)
    FROM ((`ClaimConditions` `cc`
      JOIN `CompetitiveGroups` `cgb`
        ON ((`cc`.`CompetitiveGroupId` = `cgb`.`Id`)))
      JOIN `Claims` `c`
        ON (((`cc`.`ClaimId` = `c`.`Id`)
        AND (`c`.`ClaimStatusId` IN (1, 2)))))
    WHERE ((`cc`.`ClaimId` IS NOT NULL)
    AND (`cc`.`Priority` = 1)
    AND (`cgb`.`DirectionId` = `cg`.`DirectionId`)
    AND (`cgb`.`EducationFormId` = `cg`.`EducationFormId`)
    AND (`IsOriginal`(`cc`.`ClaimId`) IS TRUE))) AS `CommonOriginalClaimCount`
FROM (`CompetitiveGroups` `cg`
  JOIN `Directions` `d`
    ON ((`cg`.`DirectionId` = `d`.`Id`)))
WHERE (`cg`.`EducationFormId` = 2)
GROUP BY CONCAT_WS(' ', `d`.`Code`, `d`.`Name`)
ORDER BY `cg`.`EducationLevelId`, CONCAT_WS(' ', `d`.`Code`, `d`.`Name`);

-- 
-- Восстановить предыдущий режим SQL (SQL mode)
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;