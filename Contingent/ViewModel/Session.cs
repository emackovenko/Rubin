using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonMethods.Security;
using Model.Astu;
using Contingent.Properties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Data.OracleClient;
using System.Security.Cryptography;
using System.Data.Common;

namespace Contingent.ViewModel
{
    /// <summary>
    /// Пользовательская сессия (Singleton)
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Приватный конструктор для синглтона
        /// </summary>
        private Session()
        {
            Id = System.Guid.NewGuid().ToString();
        }



        private static readonly Session instance = new Session();
        
        /// <summary>
        /// Возвращает экземпляр текущей сессии
        /// </summary>
        public static Session GetInstance()
        {
            return instance;
        }
        



        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Соединение с БД
        /// </summary>
        public DbConnection DbConnection
        {
            get
            {
                return Astu.DbConnection;
            }
        }
        

        /// <summary>
        /// Пытается авторизоваться и возвращает результат авторизации
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        public bool Auth(string username, string password)
        {
            // загружаем параметры из настроек
            string dbHost = Settings.Default.DbHost;
            int dbPort = Settings.Default.DbPort;
            string dbServiceName = Settings.Default.DbServiceName;

            string connectionString = string.Format("Data Source={0};User Id={1};Password={2};Unicode=True;",
                dbServiceName, username, password);

            // создаем коннект и пытаемся инициализировать модель
            var connection = new OracleConnection(connectionString.ToString());

            // если получилось
           // var authResult = ModelAuthAsync(connection).GetAwaiter().GetResult();
            if (Astu.Auth(connection))
            {
                // сохраняем параметры подключения
                Settings.Default.Save();
                Username = username;
                PasswordHash = Encrypter.MD5Hash(password);

                // шлем сообщение об успешной авторизации
                return true;
            }
            // если не получилось
            else
            {
                // шлем сообщение о хреновой авторизации
                return false;
            }
        }

        
    }
}
