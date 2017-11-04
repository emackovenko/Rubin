using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Xps.Packaging;
using Word = GemBox.Document;
using Excel = GemBox.Spreadsheet;

namespace CommonMethods.Documents
{
    /// <summary>
    /// Предоставляет базовый функционал документа в формате OpenXML
    /// </summary>
    public abstract class OpenXmlDocument
    {
        string _documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            + "\\" + Assembly.GetCallingAssembly().GetName().Name + "\\Documents";

        /// <summary>
        /// Тип документа
        /// </summary>
        public OpenXmlDocumentType DocumentType { get; protected set; }

        string _originalFileName;

        /// <summary>
        /// Имя файла в оригинальном формате
        /// </summary>
        public string OriginalFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_originalFileName))
                {
                    switch (DocumentType)
                    {
                        case OpenXmlDocumentType.Spreadsheet:
                            {
                                if (!Directory.Exists(_documentsDirectory + "\\Excel"))
                                {
                                    Directory.CreateDirectory(_documentsDirectory + "\\Excel");
                                }
                                _originalFileName = string.Format("{0}\\Excel\\{1}{2}.xlsx", 
                                     _documentsDirectory, this.GetHashCode(), DateTime.Now.GetHashCode());
                                break;
                            }
                        case OpenXmlDocumentType.Document:
                            {
                                if (!Directory.Exists(_documentsDirectory + "\\Word"))
                                {
                                    Directory.CreateDirectory(_documentsDirectory + "\\Word");
                                }
                                _originalFileName = string.Format("{0}\\Word\\{1}{2}.docx", 
                                    _documentsDirectory, this.GetHashCode(), DateTime.Now.GetHashCode());
                                break;
                            }
                        default:
                            throw new InvalidOperationException("Возникли непонятки с типом документа OpenXml");
                    }
                    CreatePackage(_originalFileName);
                }
                return _originalFileName;
            }
        }
        
        string _xpsFileName;

        /// <summary>
        /// Имя файла в формате XPS
        /// </summary>
        private string XpsFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_xpsFileName))
                {
                    if (!Directory.Exists(_documentsDirectory + "\\XPS"))
                    {
                        Directory.CreateDirectory(_documentsDirectory + "\\XPS");
                    }
                    if (DocumentType == OpenXmlDocumentType.Document)
                    {
                        var doc = Word.DocumentModel.Load(OriginalFileName);
                        string fileName = string.Format("{0}\\XPS\\{1}{2}.xps", 
                            _documentsDirectory, this.GetHashCode(), DateTime.Now.GetHashCode());
                        doc.Save(fileName, Word.SaveOptions.XpsDefault);
                        _xpsFileName = fileName;
                    }
                    if (DocumentType == OpenXmlDocumentType.Spreadsheet)
                    {
                        var doc = Excel.ExcelFile.Load(OriginalFileName);
                        string fileName = string.Format("{0}\\XPS\\{1}{2}.xps",
                            _documentsDirectory, this.GetHashCode(), DateTime.Now.GetHashCode());
                        doc.Save(fileName, Excel.SaveOptions.XpsDefault);
                        _xpsFileName = fileName;
                    }
                }
                return _xpsFileName;
            }
        }

        XpsDocument _xpsDocument;

        /// <summary>
        /// Предоставляет документ в формате XPS, открытый в режиме чтения
        /// </summary>
        public XpsDocument XpsDocument
        {
            get
            {
                if (_xpsDocument == null)
                {
                    _xpsDocument = new XpsDocument(XpsFileName, FileAccess.Read);
                }
                return _xpsDocument;
            }
        }

        /// <summary>
        /// Открывает документ в приложении Microsoft Office, после чего возвращает управление
        /// </summary>
        public void ShowInMicrosoftApp()
        {
            switch (DocumentType)
            {
                case OpenXmlDocumentType.Spreadsheet:
                    {
                        ExcelDocument.OpenDocument(OriginalFileName);
                        return;
                    }
                case OpenXmlDocumentType.Document:
                    {
                        WordDocument.OpenDocument(OriginalFileName);
                        return;
                    }
                default:
                    throw new InvalidOperationException("Возникли непонятки с типом документа OpenXml");
            }
        }

        /// <summary>
        /// Контракт на создание документа
        /// </summary>
        public abstract void CreatePackage(string fileName);


    }

    /// <summary>
    /// Перечисление типов документов в формате OpenXML
    /// </summary>
    public enum OpenXmlDocumentType
    {
        /// <summary>
        /// Электронная таблица
        /// </summary>
        Spreadsheet,
        /// <summary>
        /// Текстовый документ
        /// </summary>
        Document
    }
}
