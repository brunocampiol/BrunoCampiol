using System;
using System.IO;
using System.Security.Cryptography;

namespace BrunoCampiol.Common.Security
{
    /// <summary>
    /// Concrete class for encrypt/decrypt string values using Rijndael algorithm
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private byte[] _bytesKey;
        private byte[] _bytesIV;

        /// <summary>
        /// Initializes a new instance of the EncryptionService class
        /// </summary>
        public EncryptionService()
        {
            _bytesKey = Convert.FromBase64String(EncryptionSettings.Key);
            _bytesIV = Convert.FromBase64String(EncryptionSettings.IV);
        }

        /// <summary>
        /// Encrypts a string value using Rijndael algorithm
        /// </summary>
        /// <param name="value">The string value to be encrypted</param>
        /// <returns>Encrypted string value</returns>
        public string Encrypt(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value == String.Empty) return String.Empty;

            return AESRijndaelEncrypt(value, _bytesKey, _bytesIV);
        }

        /// <summary>
        /// Decrypts a string value using Rijndael algorithm
        /// </summary>
        /// <param name="value">String previously encrypted</param>
        /// <returns>Decrypted string value</returns>
        public string Decrypt(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value == String.Empty) return String.Empty;

            return AESRijndaelDecrypt(value, _bytesKey, _bytesIV);
        }

        private string AESRijndaelDecrypt(string value, byte[] Key, byte[] IV)
        {
            string plainValue = String.Empty;

            byte[] cipherText = Convert.FromBase64String(value);
            using (var aesAlg = new RijndaelManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainValue = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plainValue;
        }

        private string AESRijndaelEncrypt(string value, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (var aesAlg = new RijndaelManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // TODO: [Security Meeting] Rafael will research how to remove the usage of "MemoryStream", 
                // if that does not work, we need to talk again with the security Team.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(value);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }
    }
}
