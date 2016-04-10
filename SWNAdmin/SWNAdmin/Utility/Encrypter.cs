using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SWNAdmin.Utility
{
    public class Encryption
    {
        private readonly byte[] _iv;
        private readonly byte[] _key;

        public Encryption( string key )
        {
            var bytes = new byte[16];
            Encoding.ASCII.GetBytes(key, 0, Math.Min(key.Length, 16), bytes, 0);
            _key = bytes;
            _iv = Convert.FromBase64String("2sFIzEmmg1Q=");
        }

        public string EncryptStringToBytes( string plainText )
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                return "";
            }

            var encrypted = new byte[]
            {
            };
            // Create an TripleDESCryptoServiceProvider object
            // with the specified key and _iv.
            using (var tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = _key;
                tdsAlg.IV = _iv;

                // Create a decrytor to perform the stream transform.
                if (tdsAlg.Key == null)
                {
                    return Convert.ToBase64String(encrypted);
                }
                var encryptor = tdsAlg.CreateEncryptor(tdsAlg.Key, tdsAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
        }

        public string DecryptStringFromBytes( string cipherText )
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                return "";
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create an TripleDESCryptoServiceProvider object
            // with the specified key and _iv.
            using (var tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = _key;
                tdsAlg.IV = _iv;

                // Create a decrytor to perform the stream transform.
                if (tdsAlg.Key == null)
                {
                    return null;
                }
                var decryptor = tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV);

                // Create the streams used for decryption.
                try
                {
                    using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    return "";
                }
            }

            return plaintext;
        }
    }
}