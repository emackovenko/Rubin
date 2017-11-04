using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using System.IO;
using System.Xml.Serialization;
using Model.Admission;

namespace Admission.ViewModel
{
	[Serializable]
	public class LogService
	{

		static string _logPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\" + 
			Application.ResourceAssembly.GetName().Name;

		DateTime _time;
		string _stackTrace;
		string _message;
		string _source;
		string _class;
		string _method;
		List<string> _parameters;

		public LogService()
		{ }

		public DispatcherUnhandledExceptionEventHandler Handler
		{
			get
			{
				return OnUnhandledException;
			}
		}

		public string Time
		{
			get
			{
				return _time.ToString("dd.MM.yyyy HH:mm:ss");
			}
			set
			{
				_time = DateTime.Parse(value);
			}	  
		}

		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}

		public string Source
		{
			get
			{
				return _source;
			}	
			set
			{
				_source = value;
			}
		}

		public string Class
		{
			get
			{
				return _class;
			} 
			set
			{
				_class = value;
			} 
		}

		public string Method
		{
			get
			{
				return _method;
			}
			set
			{
				_method = value;
			}	
		}

		public List<string> Parameters
		{
			get
			{
				return _parameters;
			}
			set
			{
				_parameters = value;
			}	 
		}

		public string StackTrace
		{
			get
			{
				return _stackTrace;
			}	
			set
			{
				_stackTrace = value;
			} 
		}

		void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			var exception = e.Exception;
			if (exception.InnerException != null)
			{
				exception = exception.InnerException;
			}
			_time = DateTime.Now;
			_stackTrace = exception.StackTrace;
			_message = exception.Message;
			_source = exception.Source;
			_class = exception.TargetSite.ReflectedType.Name;
			_method = exception.TargetSite.Name;
			var parameters = exception.TargetSite.GetParameters();
			_parameters = new List<string>();
			foreach (var p in parameters)
			{
				_parameters.Add(p.ToString());
			}
			WriteErrorLog();
			string errorMessage = string.Format("При работе программы произошла необрабатываемая ошибка.\n\nТекст ошибки:\n{0}", exception.Message);
			MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;

		}

		void WriteErrorLog()
		{
			string fileName = _logPath + "\\errors.xml";

			if (!System.IO.Directory.Exists(_logPath))
			{
				System.IO.Directory.CreateDirectory(_logPath);
			}

			using (Stream saver = new FileStream(fileName, FileMode.Append, FileAccess.Write))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(LogService));
				serializer.Serialize(saver, this);
			}
					   
		}	

		/// <summary>
		/// Записывает отчетную информацию в журнал операций, добавляя текущее время
		/// </summary>
		/// <param name="logMessage">Текст отчетного сообщения</param>
		public static void WriteLog(string logMessage)
		{
			using (var context = new AdmissionDatabase())
			{
				var log = new Log
				{
					Date = DateTime.Now.Date,
					Time = DateTime.Now.TimeOfDay,
					UserId = Session.CurrentUser.Id,
					Message = logMessage
				};
				context.Logs.Add(log);
				context.SaveChanges();
			}
		}		
	}
}
