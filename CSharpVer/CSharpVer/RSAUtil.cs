using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharpVer
{
    class RSAUtil
    {
        public static byte[] EncryptKey(byte[] publicKeyBytes, byte[] aesKeyBytes)
        {
            string stringPublicKey = Encoding.ASCII.GetString(publicKeyBytes);
            var keyBytes = Convert.FromBase64String(stringPublicKey);

            AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes);
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            RSAParameters rsaParameters = new RSAParameters();
            rsaParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned();
            rsaParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);

            return rsa.Encrypt(aesKeyBytes, false);
        }

        public static byte[] DecryptKey(byte[] privateKeyBytes, byte[] aesKeyBytes)
        {
            string stringPrivateKey = Encoding.ASCII.GetString(privateKeyBytes);
            var keyBytes = Convert.FromBase64String(stringPrivateKey);

            AsymmetricKeyParameter asymmetricKeyParameter = PrivateKeyFactory.CreateKey(keyBytes);
            RSAParameters rsaParameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)asymmetricKeyParameter);
            
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);

            return rsa.Decrypt(aesKeyBytes, false);
        }
    }
}
