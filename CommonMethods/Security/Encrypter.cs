using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonMethods.Security
{
	public class Encrypter
	{
		/// <summary>
		/// Возвращает MD5-хэш строки (спизжено с MSDN)
		/// </summary>					   
		/// <param name="input">Собсно, сама строка</param>
		/// <returns></returns>
		public static string MD5Hash(string input)
		{
			MD5 md5Hasher = MD5.Create();
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString().ToUpper();
		}

	}
}
