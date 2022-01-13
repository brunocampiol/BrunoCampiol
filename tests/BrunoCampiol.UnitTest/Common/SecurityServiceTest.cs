using BrunoCampiol.CrossCutting.Common.Security;
using System;
using Xunit;

namespace BrunoCampiol.Unit.Test.Common
{
    public class SecurityServiceTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("A1B2C3D4E5F6", "bCLwXLcXJfPDWB0n+bqjWw==")]
        [InlineData("戴尔计算机存储器", "I96oxbTrK7eTtkoF9Kmg3ZB710mpuFC2+1dafQOfOJ8=")]
        [InlineData("الشمس الذهبية", "F9Zepf+rIY8Gp5xJFoqOiSjJ0op8qcX50TX8rm4az28=")]
        [InlineData("¼¶Ǳʧ!@#'~ç%&*|+*}ãşθ؏ᶎﮗźABCDEF", "o/zoGOqlFbzW14H0sirFw2PUt4tB1u+GZdN0c5XaJHyaTR87dM7C+O93jZprbQx8")]
        [InlineData("                                   ", "aHBVY5qWBEefVMpxREqPRbCpRsfX4HIVx1fcVP0J3T0hXdU2pYhFxLu4cfADgLzK")]
        [InlineData("A B C 🗺︎ ⚛ 👳 🤳 🌌 🚫 β Ѿ Ϡ Ǿ ½ ۩ 🦾 ���", "xo4lo3pV2jbazwybxylWxivlm1YlwjBChhutIu4YdQdiOUcn9YpYtkt9TlFL4R4sEN8S/ENBhnqsogdjqCHVd9IJfP906mZ0GeP1UW+YVdc=")]
        public void TestEncrypt(string decryptedText, string expectedEncrypted)
        {
            // Assemble
            IEncryptionService encryptionService = new EncryptionService();

            // Act
            string result = encryptionService.Encrypt(decryptedText);

            // Assert
            Assert.Equal(expectedEncrypted, result);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("bCLwXLcXJfPDWB0n+bqjWw==", "A1B2C3D4E5F6")]
        [InlineData("I96oxbTrK7eTtkoF9Kmg3ZB710mpuFC2+1dafQOfOJ8=", "戴尔计算机存储器")]
        [InlineData("F9Zepf+rIY8Gp5xJFoqOiSjJ0op8qcX50TX8rm4az28=", "الشمس الذهبية")]
        [InlineData("o/zoGOqlFbzW14H0sirFw2PUt4tB1u+GZdN0c5XaJHyaTR87dM7C+O93jZprbQx8", "¼¶Ǳʧ!@#'~ç%&*|+*}ãşθ؏ᶎﮗźABCDEF")]
        [InlineData("aHBVY5qWBEefVMpxREqPRbCpRsfX4HIVx1fcVP0J3T0hXdU2pYhFxLu4cfADgLzK", "                                   ")]
        [InlineData("xo4lo3pV2jbazwybxylWxivlm1YlwjBChhutIu4YdQdiOUcn9YpYtkt9TlFL4R4sEN8S/ENBhnqsogdjqCHVd9IJfP906mZ0GeP1UW+YVdc=", "A B C 🗺︎ ⚛ 👳 🤳 🌌 🚫 β Ѿ Ϡ Ǿ ½ ۩ 🦾 ���")]
        public void TestDecrypt(string encryptedText, string expectedDecrypted)
        {
            // Assemble
            IEncryptionService encryptionService = new EncryptionService();

            // Act
            string result = encryptionService.Decrypt(encryptedText);

            // Assert
            Assert.Equal(expectedDecrypted, result);
        }

        [Fact]
        public void TestEncrypt_WhenNullString()
        {
            // Assemble
            IEncryptionService encryptionService = new EncryptionService();

            // Act
            Action act = () => encryptionService.Encrypt(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void TestDecrypt_WhenNullString()
        {
            // Assemble
            IEncryptionService encryptionService = new EncryptionService();

            // Act
            Action act = () => encryptionService.Decrypt(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
