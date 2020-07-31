using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSharpVer
{
    class Encryptor
    {
        public static void Main()
        {
            byte[] content = FileUtil.ReadBytesFromFile(Constants.FILE_TO_ENCRYPT);

            // 1. Generate AES key
            AesManaged aes = new AesManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.GenerateKey();
            aes.GenerateIV();

            byte[] aesKey = aes.Key;
            byte[] ivParams = aes.IV;

            Console.WriteLine(Convert.ToBase64String(aesKey));
            Console.WriteLine(Convert.ToBase64String(ivParams));

            // 2. Encrypt file content
            byte[] encryptedContent = AESUtil.EncryptFile(aesKey, ivParams, content);
           
            // 3. Encrypt AES key
            byte[] publicKey = FileUtil.ReadBytesFromFile(Constants.PUBLIC_KEY);
            byte[] encryptedKey = RSAUtil.EncryptKey(publicKey, aesKey);

            // 4. Attach encrypted key to file
            byte[] fileOutputContent = FileUtil.combineBytes(encryptedKey, encryptedContent);

            // 5. Write to output
            FileUtil.WriteToFile(Constants.ENCRYPTED_FILE, fileOutputContent);

            Console.ReadLine();
        }
    }
}
