# RSA_AES_Example
RSA2048 and AES256 as keys for Java and C#.

Codes used allows encryption and decryption between both languages.

## Usage
* Change the Constants.java/Constants.cs to the file path in use
* Run Encryptor class to encrypt file (**FILE_TO_ENCRYPT**)
* Encrypted file: **ENCRYPTED_FILE**
* Run Decryptor class to decrypt file (**ENCRYPTED_FILE**)
* Decrypted file: **DECRYPTED_FILE**

Note: RSA keys can be generated using the RSAKeyGenerator class provided in the Java version.

## Logic
The expected input/output for file bytes:  
[**ENCRYPTED_AES_KEY**] [**IV BYTES**] [**ENCRYPTED FILE CONTENT**]

### Encrypting
1. Generate an AES key
2. Encrypt the file content using the generated AES key (IV is also attached to the file bytes)
3. Encrypt the AES key with RSA public key 
4. Combine encrypted AES key with encrypted file content

### Decrypting
1. Breakdown file bytes into the three portions (256 AES, 16 IV, Remaining bytes for file content)
2. Decrypt AES key with RSA private key
3. Decrypt file content using decrypted AES key with IV
