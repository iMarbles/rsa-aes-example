using System;

namespace CSharpVer
{
    class Constants
    {
        private static String FOLDER = "../../Output/";

        public static String PRIVATE_KEY = FOLDER + "private.txt";
        public static String PUBLIC_KEY = FOLDER + "public.txt";
        public static String FILE_TO_ENCRYPT = FOLDER + "test.txt";
        public static String ENCRYPTED_FILE = FOLDER + "csharp_encrypt_test.txt";
        public static String DECRYPTED_FILE = FOLDER + "csharp_decrypt_test.txt";
    }
}
