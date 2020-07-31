using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CSharpVer
{
    class Decryptor
    {
        public static void Main()
        {
            byte[] content = FileUtil.ReadBytesFromFile(Constants.ENCRYPTED_FILE);

            // 1. First 256 is encrypted AES key
            byte[] encryptedKey = content.Take(256).ToArray();

            // 2. Next 16 bytes for IV
            byte[] ivBytes = content.Skip(256).Take(16).ToArray();

            // 3. Remaining bytes is encrypted file content 
            byte[] fileBytes = content.Skip(encryptedKey.Length + ivBytes.Length).ToArray();

            // 4. Decrypt the AES key
            byte[] privateKey = FileUtil.ReadBytesFromFile(Constants.PRIVATE_KEY);
            byte[] aesKeyBytes = RSAUtil.DecryptKey(privateKey, encryptedKey);

            // 5. Decrypt file content
            byte[] decryptedContent = AESUtil.DecryptFile(aesKeyBytes, ivBytes, fileBytes);

            // 6. Write to output
            FileUtil.WriteToFile(Constants.DECRYPTED_FILE, decryptedContent);

            Console.ReadLine();
        }
    }
}
