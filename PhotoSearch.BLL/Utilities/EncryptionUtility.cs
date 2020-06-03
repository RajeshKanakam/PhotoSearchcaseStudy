using System.Security.Cryptography;

namespace PhotoSearch.BLL.Utilities
{
    public static class EncryptionUtility
    {
        /// <summary>
        /// Method to Encode given string to Base64
        /// </summary>
        /// <param name="stringText"></param>
        /// <returns></returns>
        public static string Base64Encode(string stringText)
        {
            var stringTextBytes = System.Text.Encoding.UTF8.GetBytes(stringText);
            return System.Convert.ToBase64String(stringTextBytes);
        }

        /// <summary>
        /// Method to Decode given string from Base64
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string DecodeBase64(string encryptedString)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encryptedString);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes); 
        }
    }
}
