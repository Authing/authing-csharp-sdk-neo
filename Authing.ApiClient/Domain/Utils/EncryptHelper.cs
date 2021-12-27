using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Authing.ApiClient.Domain.Utils
{
    public static class EncryptHelper
    {
        /// <summary>
        /// RAS加密
        /// </summary>
        /// <param name="clearText">要加密的信息</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string RsaEncryptWithPublic(string clearText, string publicKey)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(clearText);

            var encryptEngine = new Pkcs1Encoding(new RsaEngine());

            using (var txtreader = new StringReader(publicKey))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();

                encryptEngine.Init(true, keyParameter);
            }

            var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
            return encrypted;
        }

        public static string SHA256Hash(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            SHA256 shaM = new SHA256Managed();
            var hashBytes = shaM.ComputeHash(data);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
