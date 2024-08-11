using System.Security.Cryptography;
using System.Text;
using System;
using System.Configuration;

namespace MidMarket.Business.Services
{
    public class EncriptacionService
    {
        #region Keys AES
        private readonly static string IV = ConfigurationManager.AppSettings["AES_KeyIV"]; //16 chars = 128 bytes
        private readonly static string key = ConfigurationManager.AppSettings["AES_Key"];  // 32 chars = 256 bytes
        #endregion

        #region Hash SHA256
        public static string Hash(string texto)
        {
            UnicodeEncoding codificar = new UnicodeEncoding();
            byte[] textobyte = codificar.GetBytes(texto);
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] tablaBytes = sha256.ComputeHash(textobyte);
            string textoCifrado = Convert.ToBase64String(tablaBytes).ToString();
            return textoCifrado;
        }
        #endregion

        #region Algoritmo Encriptación AES
        public static string EncriptarAES(string decrypted)
        {
            byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(decrypted);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Key = ASCIIEncoding.ASCII.GetBytes(key);
            encdec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);

            byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
            icrypt.Dispose();

            return Convert.ToBase64String(enc);
        }

        public static string DesencriptarAES(string encrypted)
        {
            if (encrypted.Length < 24) return "0"; // Esto lo hago por si alteran la tabla USUARIO y me ponen un campo que tenga menos de 24 caracteres.
            byte[] encbytes = Convert.FromBase64String(encrypted);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Key = ASCIIEncoding.ASCII.GetBytes(key);
            encdec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateDecryptor(encdec.Key, encdec.IV);

            byte[] dec = icrypt.TransformFinalBlock(encbytes, 0, encbytes.Length);
            icrypt.Dispose();

            return ASCIIEncoding.ASCII.GetString(dec);
        }
        #endregion
    }
}
