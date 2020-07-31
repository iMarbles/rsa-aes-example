import java.io.File;
import java.security.PrivateKey;
import java.util.Arrays;
import java.util.Base64;

import javax.crypto.SecretKey;
import javax.crypto.spec.IvParameterSpec;

public class Decryptor {
   public static void main(String[] args){
       File privateKeyFile = new File(Constants.PRIVATE_KEY);

       File input = new File(Constants.ENCRYPTED_FILE);
       File output = new File(Constants.DECRYPTED_FILE);

       byte[] content = FileUtil.readBytesFromFile(input);
       try {
           // 1. First 256 is encrypted AES key
           byte[] encryptedKey = Arrays.copyOfRange(content, 0, 256);

           // 2. Next 16 bytes for IV
           byte[] ivBytes = Arrays.copyOfRange(content, 256, 272);

           // 3. Remaining bytes is encrypted file content
           byte[] fileBytes = Arrays.copyOfRange(content, 272, content.length);

           // 4. Decrypt the AES key
           PrivateKey rsaPrivate = RSAUtil.getPrivateKey(Base64.getDecoder().decode(FileUtil.readBytesFromFile(privateKeyFile)));
           byte[] aesKeyBytes = RSAUtil.decryptKey(rsaPrivate, encryptedKey);

           // 5. Decrypt file content
           SecretKey aesKey = AESUtil.getAESKey(aesKeyBytes);
           IvParameterSpec ivParams = AESUtil.getIVParams(ivBytes);
           byte[] decryptedContent = AESUtil.decryptFile(aesKey, ivParams, fileBytes);

           // 6. Write to output
           FileUtil.writeToFile(output, decryptedContent);
       } catch (Exception e) {
           e.printStackTrace();
       }
   }
}
