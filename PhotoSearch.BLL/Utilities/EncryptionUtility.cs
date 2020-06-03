using System.Security.Cryptography;

namespace PhotoSearch.BLL.Utilities
{
    public static class EncryptionUtility
    {
        public static string DecodeBase64(string encryptedString)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encryptedString);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes); 
        }
    }
}
