﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace xeno_rat_client
{
    class Encryption
    {

        /// <summary>
        /// Encrypts the input data using the provided key and returns the encrypted result.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        /// <param name="Key">The key used for encryption.</param>
        /// <returns>The encrypted data using the specified key.</returns>
        /// <remarks>
        /// This method encrypts the input data using the Advanced Encryption Standard (AES) algorithm with a specified key and initialization vector (IV).
        /// It creates an instance of AES algorithm, sets the key and IV, and then creates an encryptor using the specified key and IV.
        /// The input data is then encrypted using the encryptor and the resulting encrypted data is returned.
        /// </remarks>
        public static byte[] Encrypt(byte[] data, byte[] Key)
        {
            byte[] encrypted;
            byte[] IV = new byte[16];
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                        csEncrypt.FlushFinalBlock();
                        encrypted = msEncrypt.ToArray();
                    }
                }
                encryptor.Dispose();
            }
            return encrypted;
        }

        /// <summary>
        /// Decrypts the input data using the provided key and returns the decrypted result.
        /// </summary>
        /// <param name="data">The data to be decrypted.</param>
        /// <param name="Key">The key used for decryption.</param>
        /// <returns>The decrypted data.</returns>
        /// <remarks>
        /// This method decrypts the input data using the provided key and returns the decrypted result.
        /// It uses the AES algorithm with a 16-byte initialization vector (IV) and creates a decryptor using the specified key and IV.
        /// The decrypted data is returned as a byte array.
        /// </remarks>
        public static byte[] Decrypt(byte[] data, byte[] Key)
        {
            byte[] IV = new byte[16];
            byte[] decrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        decrypted = ms.ToArray();
                    }
                }
                decryptor.Dispose();
            }
            return decrypted;
        }
    }
}
