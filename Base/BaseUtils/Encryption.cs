using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Base.BaseUtils
{
    public class Encryption
    {

        public static string EncryptString(string str, byte[] key, byte[] vec)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);

            Rijndael alg = Rijndael.Create();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(key, vec), CryptoStreamMode.Write);
            cs.Write(strBytes, 0, strBytes.Length);
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string str, byte[] key, byte[] vec)
        {
            byte[] encrypted = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(key, vec), CryptoStreamMode.Write);
            cs.Write(encrypted, 0, encrypted.Length);
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }


    }
}