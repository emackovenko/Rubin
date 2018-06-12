using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Admission;
using GemBox.Document;
using CommonMethods.Documents;
using ResourceLibrary.Documents;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exDateTime;

namespace Admission.ViewModel.Documents
{
    internal class DocumentClaim : OpenXmlDocument
    {
        public DocumentClaim(Claim claim)
        {
            _claim = claim;
            DocumentType = OpenXmlDocumentType.Document;
        }

        Claim _claim;

        public override void CreatePackage(string fileName)
        {
            // загружаем документ 
            var doc = DocumentModel.Load(DocumentTemplate.ExtractDoc("EntrantClaim"));

            // подготовка стилей
            var boldChar = new CharacterFormat
            {
                FontName = "Times New Roman",
                Size = 10.0,
                Bold = true,
                FontColor = Color.Black
            };

            var simpleChar = new CharacterFormat
            {
                FontName = "Times New Roman",
                Size = 10.0,
                Bold = false,
                FontColor = Color.Black
            };
            var uboldChar = new CharacterFormat
            {
                FontName = "Times New Roman",
                Size = 10.0,
                Bold = true,
                FontColor = Color.Black,
                UnderlineColor = Color.Black
            };

            var usimpleChar = new CharacterFormat
            {
                FontName = "Times New Roman",
                Size = 10.0,
                Bold = false,
                FontColor = Color.Black,
                UnderlineColor = Color.Black
            };

            // вставляем текст на закладки
            doc.InsertToBookmark("Number", _claim.Number, boldChar);
            doc.InsertToBookmark("EntrantName", _claim.Person.FullName, boldChar);

            var idDoc = _claim.IdentityDocuments.First();

            doc.InsertToBookmark("BirthDate", idDoc.BirthDate.Format(), boldChar);
            doc.InsertToBookmark("Citizenship", idDoc.Citizenship.Name, boldChar);
            doc.InsertToBookmark("IdentityDocumentRequisites"
                , string.Format("{0} {1} №{2}", idDoc.IdentityDocumentType.Name, idDoc.Series, idDoc.Number)
                , boldChar);
            doc.InsertToBookmark("IdentityDocumentIssueData"
                , string.Format("{0} {1}", idDoc.Organization, idDoc.Date.Format())
                , boldChar);
            doc.InsertToBookmark("Address", _claim.Person.Address.MailString, boldChar);

            string eduDocRequsites = string.Empty;
            if (_claim.SchoolCertificateDocuments.Count > 0)
            {
                var eduDoc = _claim.SchoolCertificateDocuments.First();
                eduDocRequsites = string.Format("аттестат о среднем (полном) образовании {0} №{1}, дата выдачи: {2}",
                    eduDoc.Series, eduDoc.Number, eduDoc.Date.Format());
            }
            if (_claim.MiddleEducationDiplomaDocuments.Count > 0)
            {
                var eduDoc = _claim.MiddleEducationDiplomaDocuments.First();
                eduDocRequsites = string.Format("диплом о среднем профессиональном образовании {0} №{1}, дата выдачи: {2}",
                    eduDoc.Series, eduDoc.Number, eduDoc.Date.Format());
            }
            if (_claim.HighEducationDiplomaDocuments.Count > 0)
            {
                var eduDoc = _claim.HighEducationDiplomaDocuments.First();
                eduDocRequsites = string.Format("диплом о высшем образовании {0} №{1}, дата выдачи: {2}",
                    eduDoc.Series, eduDoc.Number, eduDoc.Date.Format());
            }


            // здесь запутанно добавляю условия приема
            var range = doc.Bookmarks.FirstOrDefault(b => b.Name == "ContestItems").GetContent(false);
            
            if (_claim.FinanceSource?.Id == 3)
            {
                // формирую строки
                string str1 = "Прошу допустить к участию в конкурсе";
                string str11 = " по направлению подготовки (специальности) ";

                string directionView = string.Format("{0}, программа бакалавриата, \"{1}\", {2} форма обучения ",
                    _claim.ClaimConditions.First(cc => cc.Priority == 1).CompetitiveGroup.Direction.Name,
                    _claim.ClaimConditions.First(cc => cc.Priority == 1).CompetitiveGroup.Direction.DirectionProfiles.FirstOrDefault().Name,
                    _claim.ClaimConditions.First(cc => cc.Priority == 1).CompetitiveGroup.EducationForm.Name);

                string str2 = "в рамках особой квоты на основании следующих документов: ";

                string reasonView = string.Format("{0} {1} №{2} от {3}.",
                    _claim.OrphanDocuments.FirstOrDefault()?.OrphanDocumentType.Name,
                    _claim.OrphanDocuments.FirstOrDefault()?.Series,
                    _claim.OrphanDocuments.FirstOrDefault()?.Number,
                    _claim.OrphanDocuments.FirstOrDefault()?.Date);

                string finalString = string.Format("{0}{1}{2}{3}",
                    str1, directionView, str2, reasonView);
                
                var paragraph = new Paragraph(doc,
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, str1)
                    {
                        CharacterFormat = boldChar.Clone()
                    },
                    new Run(doc, str11)
                    {
                        CharacterFormat = simpleChar.Clone()
                    },
                    new Run(doc, directionView)
                    {
                        CharacterFormat = boldChar.Clone()
                    },
                    new Run(doc, str2)
                    {
                        CharacterFormat = simpleChar.Clone()
                    },
                    new Run(doc, reasonView)
                    {
                        CharacterFormat = boldChar.Clone()
                    }
                    );
                paragraph.ParagraphFormat = new ParagraphFormat
                {
                    Alignment = HorizontalAlignment.Justify
                };
                range.Start.InsertRange(paragraph.Content);
            }
            else
            {
                string str1 = "Прошу допустить к участию в общем конкурсе ";
                string str2 = "по следующим направлениям подготовки (специальностям):";
                var paragraph1 = new Paragraph(doc,
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, str1)
                    {
                        CharacterFormat = boldChar.Clone()
                    },
                    new Run(doc, str2)
                    {
                        CharacterFormat = simpleChar.Clone()
                    })
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Alignment = HorizontalAlignment.Justify,
                        SpaceBefore = 0.0,
                        SpaceAfter = 0.0,
                        NoSpaceBetweenParagraphsOfSameStyle = true
                    }
                };
                range.Start.InsertRange(paragraph1.Content);
                
                foreach (var cond in _claim.ClaimConditions)
                {
                    string str = string.Format("{0}, программа бакалавриата, \"{1}\", {2} форма обучения;",
                       cond.CompetitiveGroup.Direction.Name,
                       cond.CompetitiveGroup.Direction.DirectionProfiles.FirstOrDefault().Name,
                       cond.CompetitiveGroup.EducationForm.Name);
                    var p = new Paragraph(doc,
                        new SpecialCharacter(doc, SpecialCharacterType.Tab),
                        new Run(doc, str)
                        {
                            CharacterFormat = boldChar.Clone()
                        })
                    {
                        ParagraphFormat = new ParagraphFormat
                        {
                            Alignment = HorizontalAlignment.Left,
                            SpaceBefore = 0.0,
                            SpaceAfter = 0.0,
                            NoSpaceBetweenParagraphsOfSameStyle = true
                        }
                    };
                    range.End.InsertRange(p.Content);
                }

                string str3 = "на места, финансируемые за счет средств федерального бюджета. ";
                string str4 = "В случае непрохождения по конкурсу (либо в случае отсутствия мест, финансируемых за счёт средств федерального бюджета) на указанные направления подготовки (специальности) прошу допустить к участию в конкурсе на места по договорам с оплатой стоимости обучения юридическими и (или) физическими лицами.";

                var paragraph3 = new Paragraph(doc,
                    new Run(doc, str3)
                    {
                        CharacterFormat = boldChar.Clone()
                    },
                    new Run(doc, str4)
                    {
                        CharacterFormat = simpleChar.Clone()
                    })
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Alignment = HorizontalAlignment.Justify,
                        SpaceBefore = 0.0,
                        SpaceAfter = 0.0,
                        NoSpaceBetweenParagraphsOfSameStyle = true,
                        
                    }
                };
                range.End.InsertRange(paragraph3.Content);
            }

            var testsData = doc.Bookmarks.FirstOrDefault(b => b.Name == "TestResultData").GetContent(false);
            if (_claim.EntranceTestResults.Count > 0)
            {
                string str1 = "На основании того, что отношусь к ";
                string str2 = "лицам, получившим ранее СПО/НПО/ВО, прошу допустить к следующим общеобразовательным вступительным испытаниям, проводимым РИИ АлтГТУ самостоятельно:";
                string str3 = string.Empty;
                foreach (var etr in _claim.EntranceTestResults)
                {
                    str3 += string.Format("{0}, ", etr.EntranceTest.ExamSubject.Name);
                }
                str3 = str3.Remove(str3.Length - 1, 1) + ".";
                var p1 = new Paragraph(doc,
                        new SpecialCharacter(doc, SpecialCharacterType.Tab),
                        new Run(doc, str1)
                        {
                            CharacterFormat = simpleChar.Clone()
                        },
                        new Run(doc, str2)
                        {
                            CharacterFormat = boldChar.Clone()
                        })
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Alignment = HorizontalAlignment.Justify,
                        SpaceBefore = 0.0,
                        SpaceAfter = 0.0,
                        NoSpaceBetweenParagraphsOfSameStyle = true
                    }
                };
                testsData.Start.InsertRange(p1.Content);
                var p2 = new Paragraph(doc,
                        new SpecialCharacter(doc, SpecialCharacterType.Tab),
                        new Run(doc, str3)
                        {
                            CharacterFormat = boldChar.Clone()
                        })
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Alignment = HorizontalAlignment.Left,
                        SpaceBefore = 0.0,
                        SpaceAfter = 0.0,
                        NoSpaceBetweenParagraphsOfSameStyle = true
                    }
                };
                testsData.End.InsertRange(p2.Content);
            }
            if (_claim.EgeDocuments.Count > 0)
            {
                string str1 = "Прошу зачесть результаты ЕГЭ в качестве результатов следующих вступительных испытаний:";
               
                string str3 = string.Empty;
                foreach (var ed in _claim.EgeDocuments)
                {
                    foreach (var er in ed.EgeResults)
                    {
                        str3 += string.Format("{0} - {1} баллов; ", er.ExamSubject, er.Value);
                    }
                }
                str3 = str3.Remove(str3.Length - 1, 1) + ".";
                var p1 = new Paragraph(doc,
                        new SpecialCharacter(doc, SpecialCharacterType.Tab),
                        new Run(doc, str1)
                        {
                            CharacterFormat = boldChar.Clone()
                        })
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Alignment = HorizontalAlignment.Justify,
                        SpaceBefore = 0.0,
                        SpaceAfter = 0.0,
                        NoSpaceBetweenParagraphsOfSameStyle = true
                    }
                };
                testsData.Start.InsertRange(p1.Content);
                var p2 = new Paragraph(doc,
                        new SpecialCharacter(doc, SpecialCharacterType.Tab),
                        new Run(doc, str3)
                        {
                            CharacterFormat = boldChar.Clone()
                        })
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Alignment = HorizontalAlignment.Left,
                        SpaceBefore = 0.0,
                        SpaceAfter = 0.0,
                        NoSpaceBetweenParagraphsOfSameStyle = true
                    }
                };
                testsData.End.InsertRange(p2.Content);
            }

            doc.InsertToBookmark("EducationDocumentRequisites", eduDocRequsites, boldChar);

            string prerogativeRight = _claim.FinanceSource.Id == 3 ? "имею" : "не имею";        
            doc.InsertToBookmark("PrerogativeRight", prerogativeRight, boldChar);

            string hostelNeeding = _claim.IsHostelNeed.Value ? "нуждаюсь" : "не нуждаюсь";
            doc.InsertToBookmark("HostelNeeding", hostelNeeding, boldChar);
            doc.InsertToBookmark("IndividualAchievements", "отсутствуют", boldChar);
            doc.InsertToBookmark("DocumentsReturning", _claim.DocumentsReturningType, boldChar);
            doc.InsertToBookmark("OperatorName",
                string.Format("{0} {1}.{2}.", 
                Session.CurrentUser.LastName,
                Session.CurrentUser.FirstName[0], Session.CurrentUser.Patronymic[0])
                , usimpleChar);
            doc.InsertToBookmark("Date", _claim.RegistrationDate.Format(), uboldChar);

            // сохраняем документ
            doc.Save(fileName);
        }
    }
}
