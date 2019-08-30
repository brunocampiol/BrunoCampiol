namespace BrunoCampiol.Common.Security
{
    /// <summary>
    /// Provides a service for encrypt / decrypt string values
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Given a string value, encrypts it's value
        /// </summary>
        /// <param name="value">The string value to be encrypted</param>
        /// <returns>Encrypted string value</returns>
        string Encrypt(string value);

        /// <summary>
        /// Given a encrypted string value, decrypt it's value
        /// </summary>
        /// <param name="value">String previously encrypted</param>
        /// <returns>Decrypted string value</returns>
        string Decrypt(string value);
    }
}
