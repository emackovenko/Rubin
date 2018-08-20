using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonMethods.Documents;
using GemBox.Document;
using CommonMethods.TypeExtensions.exGemBox;
using CommonMethods.TypeExtensions.exDateTime;
using Model.Admission;
using ResourceLibrary.Documents;
using GemBox.Document.Tables;

namespace Admission.ViewModel.Documents
{
    public class ExaminationRegister : OpenXmlDocument
    {
        public ExaminationRegister(ExamSubject _subject, DateTime _date,
            string _classroom, int _linesCount, int _placesCount, int _variantsCount,
            EducationForm _educationForm, FinanceSource _financeSource)
        {
            DocumentType = OpenXmlDocumentType.Document;
            examSubject = _subject;
            date = _date;
            classroom = _classroom;
            linesCount = _linesCount;
            placesCount = _placesCount;
            variantsCount = _variantsCount;
            educationForm = _educationForm;
            financeSource = _financeSource;
        }

        ExamSubject examSubject;
        DateTime date;
        string classroom;
        int linesCount;
        int placesCount;
        int variantsCount;
        EducationForm educationForm;
        FinanceSource financeSource;

        public override void CreatePackage(string fileName)
        {
            var doc = DocumentModel.Load(DocumentTemplate.ExtractDoc("ExaminationRegister"));


            // Подготовка стилей
            var simpleText = new CharacterFormat
            {
                Bold = false,
                Size = 12.0
            };
            var underlinedText12 = new CharacterFormat
            {
                UnderlineColor = Color.Black,
                UnderlineStyle = UnderlineType.Single,
                Size = 12.0
            };
            var underlinedText14 = new CharacterFormat
            {
                UnderlineColor = Color.Black,
                UnderlineStyle = UnderlineType.Single,
                Size = 14.0
            };
            var paragraphStyle = (ParagraphStyle)Style.CreateStyle(StyleTemplateType.Title, doc);
            paragraphStyle.ParagraphFormat = new ParagraphFormat
            {
                Alignment = HorizontalAlignment.Center,
                SpaceBefore = 0.0,
                LeftIndentation = 0.0,
                SpecialIndentation = 0.0
            };
            paragraphStyle.CharacterFormat = new CharacterFormat
            {
                FontName = "Times New Roman",
                Size = 12.0,
                FontColor = Color.Black
            };
            doc.Styles.Add(paragraphStyle);

            var paragraphStyleLeft = (ParagraphStyle)Style.CreateStyle(StyleTemplateType.Title, doc);
            paragraphStyleLeft.Name = "style222";
            paragraphStyleLeft.ParagraphFormat = new ParagraphFormat
            {
                Alignment = HorizontalAlignment.Left,
                SpaceBefore = 0.0,
                LeftIndentation = 0.0,
                SpecialIndentation = 0.0
            };
            paragraphStyleLeft.CharacterFormat = new CharacterFormat
            {
                FontName = "Times New Roman",
                Size = 12.0,
                FontColor = Color.Black
            };
            doc.Styles.Add(paragraphStyleLeft);


            doc.InsertToBookmark("SubjectName", examSubject.Name, underlinedText14);
            doc.InsertToBookmark("ExaminationDate", date.Format(), underlinedText12);
            doc.InsertToBookmark("Classroom", classroom, underlinedText12);
            doc.InsertToBookmark("Variants", GetVariantsString(), simpleText);

            // рассчитываем рассадку вариантов по местам


            // оформляем табличу
            // получаем список абитуриентов
            // выбираем результаты, подходящие по фильтру
            var entrantsTests = Session.DataModel.EntranceTests.Where(et => et.ExaminationDate == date && et.ExamSubjectId == examSubject.Id).ToList();

            if (educationForm != null)
            {
                entrantsTests = entrantsTests.Where(et => et.EducationFormId == educationForm.Id).ToList();
            }
            if (financeSource != null)
            {
                entrantsTests = entrantsTests.Where(et => et.FinanceSourceId == financeSource.Id).ToList();
            }

            if (entrantsTests.Count <= 0)
            {
                System.Windows.MessageBox.Show("Не найдено ни одного экзамена по заданным параметрам. Пожалуйста, проверьте данные и попробуйте заново.");
                doc.Content.Delete();
                doc.Save(fileName);
                return;
            }

            var entrantsTestResults = entrantsTests.SelectMany(et => et.EntranceTestResult).ToList();
            var claimList = entrantsTestResults.Select(etr => etr.Claim).ToList();

            // рандомно раскидываем 
            var rnd = new Random();
            claimList = claimList.OrderBy(c => rnd.Next()).ToList();

            // по каждому абитриенту заполняем таблицу
            doc.InsertToBookmark("CommonCount", claimList.Count.ToString(), simpleText);
            var table = doc.GetChildElements(true, ElementType.Table).Cast<Table>().FirstOrDefault();
            int i = 1;

            //Матрица рассадки и счетчики для нее
            var seatingMatrix = GenerateVariantsSeating();
            int x = 0, y = 0;

            foreach (var claim in claimList)
            {
                // создаем строку и в нее создаем ячейки
                var row = new TableRow(doc);

                var numberCell = new TableCell(doc, new Paragraph(doc, i.ToString())
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyle
                    }
                });
                numberCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(numberCell);

                var entrantNameCell = new TableCell(doc, new Paragraph(doc, claim.Person.FullName)
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyleLeft
                    }
                });
                entrantNameCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(entrantNameCell);

                var seriesCell = new TableCell(doc, new Paragraph(doc, claim.IdentityDocuments.OrderByDescending(id => id.Date).First().Series)
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyle
                    }
                });
                seriesCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(seriesCell);

                var idNumberCell = new TableCell(doc, new Paragraph(doc, claim.IdentityDocuments.OrderByDescending(id => id.Date).First().Number)
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyle
                    }
                });
                idNumberCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(idNumberCell);

                var emptyCell = new TableCell(doc, new Paragraph(doc, ConvertDigitToLetter(x + 1))
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyle
                    }
                });
                emptyCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(emptyCell);

                emptyCell = new TableCell(doc, new Paragraph(doc, (y + 1).ToString())
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyle
                    }
                });
                emptyCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(emptyCell);

                emptyCell = new TableCell(doc, new Paragraph(doc, seatingMatrix[y,x].ToString())
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyle
                    }
                });
                emptyCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(emptyCell);
                                
                emptyCell = new TableCell(doc, new Paragraph(doc, string.Empty)
                {
                    ParagraphFormat = new ParagraphFormat
                    {
                        Style = paragraphStyle
                    }
                });
                emptyCell.CellFormat.VerticalAlignment = VerticalAlignment.Center;
                row.Cells.Add(emptyCell);
                row.Cells.Add(emptyCell.Clone(true));
                row.Cells.Add(emptyCell.Clone(true));

                table.Rows.Add(row);
                i++;
                x++;
                if (x >= linesCount)
                {
                    x = 0;
                }
                y++;
                if (y >= placesCount)
                {
                    y = 0;
                }
            }

            doc.Save(fileName);
        }

        string GetVariantsString()
        {
            string str = string.Empty;
            for (int i = 1; i <= variantsCount; i++)
            {
                str += string.Format(" {0}, ", i);
            }
            str = str.TrimEnd(' ');
            str = str.TrimEnd(',');
            return str;
        }

        public int[,] GenerateVariantsSeating()
        {

            // генерируем матрицу размера [кол-во рядов]x[кол-во мест в ряду] и заполняем нулями
            var seatingMatrix = new int[placesCount, linesCount];
            for (int i = 0; i < placesCount; i++)
            {
                for (int j = 0; j < linesCount; j++)
                {
                    seatingMatrix[i, j] = 0;
                }
            }
            int v = 1;
            for (int i = 0; i < linesCount; i++)
            {
                int y = 0;
                int x = i;
                while (x >= 0 && y < placesCount)
                {
                    seatingMatrix[y, x] = v;
                    x--;
                    y++;
                }
                v++;
                if (v > variantsCount)
                {
                    v = 1;
                }
            }
            
            for (int i = 0; i < placesCount; i++)
            {
                int y = i;
                int x = linesCount - 1;
                while (x >= 0 && y < placesCount)
                {
                    seatingMatrix[y, x] = v;
                    x--;
                    y++;
                }
                v++;
                if (v > variantsCount)
                {
                    v = 1;
                }
            }

          
            return seatingMatrix;
        }

        string ConvertDigitToLetter(int digit)
        {
            var dict = new Dictionary<int, string>();
            dict.Add(1, "A");
            dict.Add(2, "Б");
            dict.Add(3, "В");
            dict.Add(4, "Г");
            dict.Add(5, "Д");
            dict.Add(6, "Е");
            dict.Add(7, "Ж");
            dict.Add(8, "З");
            dict.Add(9, "И");
            dict.Add(10, "К");
            dict.Add(11, "Л");
            dict.Add(12, "М");
            dict.Add(13, "Н");
            dict.Add(14, "О");
            dict.Add(15, "П");
            return dict.First(v => v.Key == digit).Value;
        }


    }
}
