using System.Security.Cryptography;
using System.Text;

namespace Qfx.Cinema.Web.Extensions
{
    public static class AesDecryptor
    {
        public static string DecryptPayload(this string encryptedData)
        {
            string keyHex = "ad1a72a9fcc24435fe3a91c95a15a47b0d1534b41e7ec573a60f812a25248132";
            string ivHex = "0522e7c6cea95599e39e4ddfe92c801e";
            if (string.IsNullOrEmpty(keyHex) || string.IsNullOrEmpty(ivHex))
                return null;

            byte[] key = Convert.FromHexString(keyHex);
            byte[] iv = Convert.FromHexString(ivHex);
            byte[] encryptedBytes = Convert.FromHexString(encryptedData.Trim());

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
