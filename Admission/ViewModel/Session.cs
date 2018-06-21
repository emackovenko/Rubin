using System;					  
using System.Linq;
using System.Data;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using Model.Admission;
using CommonMethods.Security;

namespace Admission.ViewModel
{
	/// <summary>
	/// Хранит текущую сессию пользователя.
	/// При повторном создании сессии выбросится исключение.
	/// </summary>
	internal static class Session
	{
		static User _currentUser;
		static DateTime _accessTime;
		static AdmissionDatabase _data;	  

		
		/// <summary>
		/// Возвращает текущего пользователя приложения
		/// </summary>
		public static User CurrentUser
		{
			get
			{
				if (_currentUser == null)
				{
					throw new Exception("Авторизация не была пройдена");
				}
				return _currentUser;
			}
		}

		/// <summary>
		/// Предоставляет контекст модели базы данных, если авторизация не была пройдена, бросается исключение.
		/// </summary>
		public static AdmissionDatabase DataModel
		{
			get
			{
				if (_data == null)
				{
					throw new Exception("Контекст базы данных недоступен.");
				}
				return _data;
			}
		}

		/// <summary>
		/// Возвращает true при наличии пользователя и инициализирует сессию, открывая контекст доступа к данным
		/// </summary>
		/// <param name="username">Имя пользователя</param>
		/// <param name="password">Пароль</param>
		public static bool Initialize(string username, string password)
		{	
			if (_currentUser != null)
			{
				throw new InvalidOperationException("Сеанс был инициализирован ранее");
			}
			try
			{               
                password = Encrypter.MD5Hash(password);
                var context = new AdmissionDatabase();
                var result = (from user in context.Users
                              where user.Username == username && user.PasswordHash == password
                              select user).Single();
                _currentUser = result;
                _accessTime = DateTime.Now;
                _data = context;
                return true;
			}
			catch (Exception)
			{
				_currentUser = null;
				_data = null;
				return false;
			}
		}			 

		/// <summary>
		/// Повторно загружает измененную сущность из базы данных
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		public static void RefreshEntityItem<TEntity>(TEntity entity) 
			where TEntity: class
		{
			if (_data == null || _currentUser == null)
			{
				throw new Exception("Сессия не была инициализирована ранее.");
			}
			_data.Entry<TEntity>(entity).Reload();
		}
				  
		/// <summary>
		/// Перезагружает контекст модели данных, подгружая обновленные данные извне
		/// </summary>
		public static void RefreshAll()
		{
			if (_data == null || _currentUser == null)
			{
				throw new Exception("Сессия не была инициализирована ранее.");
			}
			foreach (var entity in _data.ChangeTracker.Entries())
			{
				entity.Reload();
			}
		}
		  
	}
}
