import java.io.File;
import java.io.FileDescriptor;
import java.security.PublicKey;
import java.util.Base64;

import javax.crypto.SecretKey;

public class Encryptor {
    public static void main(String[] args){
        File publicKeyFile = new File(Constants.PUBLIC_KEY);

        File input = new File(Constants.FILE_TO_ENCRYPT);
        File output = new File(Constants.ENCRYPTED_FILE);

        byte[] content = FileUtil.readBytesFromFile(input);
        try {
            // 1. Generate AES key
            SecretKey key = AESUtil.generateAESKey();

            // 2. Encrypt file content
            byte[] encryptedContent = AESUtil.encryptFile(key, content);

            // 3. Encrypt AES key
            PublicKey publicKey = RSAUtil.getPublicKey(Base64.getDecoder().decode(FileUtil.readBytesFromFile(publicKeyFile)));
            byte[] encryptedKey = RSAUtil.encryptKey(publicKey, key.getEncoded());

            // 4. Attach encrypted key to file
            byte[] fileOutputContent = FileUtil.combineBytes(encryptedKey, encryptedContent);

            // 5. Write to output
            FileUtil.writeToFile(output, fileOutputContent);
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
}
