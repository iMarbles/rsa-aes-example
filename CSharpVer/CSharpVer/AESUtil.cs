using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CSharpVer
{
    class AESUtil
    {
        public static byte[] EncryptFile(byte[] aesKey, byte[] ivParams, byte[] content)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesKey;
                aesAlg.IV = ivParams;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(Encoding.ASCII.GetString(content));
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            return FileUtil.combineBytes(ivParams, encrypted);
        }

        public static byte[] DecryptFile(byte[] aesKey, byte[] ivParams, byte[] fileContent)
        {
            string res = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesKey;
                aesAlg.IV = ivParams;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream ms = new MemoryStream(fileContent))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(cs))
                        {
                            res = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }



            return Encoding.ASCII.GetBytes(res);
        }
    }
}
