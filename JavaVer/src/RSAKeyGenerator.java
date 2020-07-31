import java.io.File;
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.NoSuchAlgorithmException;
import java.util.Base64;

public class RSAKeyGenerator {
    public static void main(String[] args) throws NoSuchAlgorithmException {
        KeyPairGenerator generator = KeyPairGenerator.getInstance("RSA");
        generator.initialize(2048);
        KeyPair kpg = generator.generateKeyPair();

        File privateKey = new File(Constants.PRIVATE_KEY);
        File publicKey = new File(Constants.PUBLIC_KEY);

        FileUtil.writeToFile(privateKey, Base64.getEncoder().encode(kpg.getPrivate().getEncoded()));
        FileUtil.writeToFile(publicKey, Base64.getEncoder().encode(kpg.getPublic().getEncoded()));
    }
}
