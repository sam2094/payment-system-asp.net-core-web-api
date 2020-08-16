using Microsoft.Owin.Security.DataHandler.Encoder;
using PWDTK_DOTNET451;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
	public static class Hashing
	{
		public static byte[] RandomSalt()
		{
			return PWDTK.GetRandomSalt(64);
		}
		public static byte[] Hash(byte[] salt, string password)
		{
			return PWDTK.PasswordToHash(salt, password);
		}
		public static bool Equals(byte[] salt, string password, byte[] hash)
		{
			return PWDTK.ComparePasswordToHash(salt, password, hash);
		}

		public static string GetMd5HashData(string data)
		{
			SHA256 md5 = SHA256.Create();
			byte[] hashData = md5.ComputeHash(Encoding.UTF8.GetBytes(data));

			StringBuilder returnValue = new StringBuilder();
			for (int i = 0; i < hashData.Length; i++)
			{
				returnValue.Append(hashData[i].ToString("x2"));
			}

			return returnValue.ToString();
		}

		public static string GetSha256HashData(string data)
		{
			SHA256 sha256 = SHA256.Create();
			byte[] hashData = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));

			StringBuilder returnValue = new StringBuilder();
			for (int i = 0; i < hashData.Length; i++)
			{
				returnValue.Append(hashData[i].ToString("x2"));
			}

			return returnValue.ToString();
		}
		public static string OwinBase64Encode()
		{
			byte[] key = new byte[32];
			RandomNumberGenerator.Create().GetBytes(key);
			return TextEncodings.Base64Url.Encode(key);
		}

		public static string RandomString(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
