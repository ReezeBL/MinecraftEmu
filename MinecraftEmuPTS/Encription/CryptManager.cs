using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Extension;
using Org.BouncyCastle.X509.Store;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.IO;


namespace MinecraftEmuPTS.Encription
{
    class CryptManager
    {
        static private byte[] PublicKey;
        static private byte[] SecretKey;

        static public void DecodePublicKey(byte[] data)
        {
            PublicKey = data;
        }
        static byte[] CreateSecretKey()
        {
            CipherKeyGenerator keygen = new CipherKeyGenerator();
            keygen.Init(new KeyGenerationParameters(new SecureRandom(), 16));
            SecretKey = keygen.GenerateKey();
            return SecretKey;
        }

        public static byte[] EncryptWithPublicKey(byte[] data)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters RSAKeyInfo = new RSAParameters();
            byte[] Exponent = {1,0,1};
            RSAKeyInfo.Modulus = PublicKey;
            RSAKeyInfo.Exponent = Exponent;
            rsa.ImportParameters(RSAKeyInfo);
            return rsa.Encrypt(data, false);
        }

        static public String GetServerIdHash(String ServerId)
        {
            return JavaHexDigest(ServerId);
        }

        public static Stream encryptStream(Stream stream)
        {
            BufferedBlockCipher output = new BufferedBlockCipher(new CfbBlockCipher(new AesFastEngine(), 8));
            output.Init(true, new ParametersWithIV(new KeyParameter(SecretKey), SecretKey, 0, 16));
            BufferedBlockCipher input = new BufferedBlockCipher(new CfbBlockCipher(new AesFastEngine(), 8));
            input.Init(false, new ParametersWithIV(new KeyParameter(SecretKey), SecretKey, 0, 16));
            return new CipherStream(stream, output, input);
        }


        private static string JavaHexDigest(string data)
        {
            var sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
            bool negative = (hash[0] & 0x80) == 0x80;
            if (negative) // check for negative hashes
                hash = TwosCompliment(hash);
            // Create the string and trim away the zeroes
            string digest = GetHexString(hash).TrimStart('0');
            if (negative)
                digest = "-" + digest;
            return digest;
        }

        private static string GetHexString(byte[] p)
        {
            string result = string.Empty;
            for (int i = 0; i < p.Length; i++)
                result += p[i].ToString("x2"); // Converts to hex string
            return result;
        }
        private static byte[] TwosCompliment(byte[] p) // little endian
        {
            int i;
            bool carry = true;
            for (i = p.Length - 1; i >= 0; i--)
            {
                p[i] = (byte)~p[i];
                if (carry)
                {
                    carry = p[i] == 0xFF;
                    p[i]++;
                }
            }
            return p;
        }
    }

}
