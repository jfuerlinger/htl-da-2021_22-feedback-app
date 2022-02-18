using FeedbackApp.WebApi.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApi.Tests
{
    [TestClass]
    public class PasswordValidationTests
    {
        [TestMethod]
        public void PwMeetRequirements_ShouldReturnTrue()
        {
            string password = "Stefano12";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsTrue(isValid, "Password should be correct");
        }

        [TestMethod]
        public void PwBlank_ShouldReturnFalse()
        {
            string password = "";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsFalse(isValid, "Password should be incorrect");
        }

        [TestMethod]
        public void PwBlankWithSpace_ShouldReturnFalse()
        {
            string password = " ";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsFalse(isValid, "Password should be incorrect");
        }

        [TestMethod]
        public void PwMinLenght_ShouldReturnTrue()
        {
            string password = "Stef12"; // Length 6
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsTrue(isValid, "Password should be correct");
        }

        [TestMethod]
        public void PwUnderMinLength_ShouldReturnFalse()
        {
            string password = "Stef1"; // Length 5
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsFalse(isValid, "Password should be incorrect");
        }

        [TestMethod]
        public void PwLong_ShouldReturnTrue()
        {
            string password = "ok9qJXlyk5pnUadmpSxvKhEiCNYQNtE0"; // Length 32
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsTrue(isValid, "Password should be correct");
        }

        [TestMethod]
        public void PwSpaceContained_ShouldReturnFalse()
        {
            string password = "Stefano 12";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsFalse(isValid, "Password should be incorrect");
        }

        [TestMethod]
        public void PwWithoutUpper_ShouldReturnFalse()
        {
            string password = "stefano12";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsFalse(isValid, "Password should be incorrect");
        }

        [TestMethod]
        public void PwWithoutLower_ShouldReturnFalse()
        {
            string password = "STEFANO12";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsFalse(isValid, "Password should be incorrect");
        }

        [TestMethod]
        public void PwWithoutDigit_ShouldReturnFalse()
        {
            string password = "stefano";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsFalse(isValid, "Password should be incorrect");
        }

        [TestMethod]
        public void PwWithSpecialChar_ShouldReturnTrue()
        {
            string password = "Stefano1-#+@'<>;.*~?ß$&%/()!";
            bool isValid = AuthenticateValidations.CheckPwRequirements(password);
            Assert.IsTrue(isValid, "Password should be correct");
        }
    }

    [TestClass]
    public class UsernameValidationTests
    {
        [TestMethod]
        public void UsernameCorrect_ShouldReturnTrue()
        {
            string username = "Stefano35";
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsTrue(isValid, "Username should be correct");
        }

        [TestMethod]
        public void UsernameMinLength_ShouldReturnTrue()
        {
            string username = "stef25"; // Length 6
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsTrue(isValid, "Username should be correct");
        }

        [TestMethod]
        public void UsernameTooShort_ShouldReturnFalse()
        {
            string username = "stef1"; // Length5
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsFalse(isValid, "Username should be incorrect");
        }

        public void UsernameBlank_ShouldReturnFalse()
        {
            string username = "";
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsFalse(isValid, "Blank-Username should be incorrect");
        }

        [TestMethod]
        public void UsernameBlankWithSpace_ShouldReturnFalse()
        {
            string username = " ";
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsFalse(isValid, "BlankSpace-Username should be incorrect");
        }

        [TestMethod]
        public void UsernameMaxLength_ShouldReturnTrue()
        {
            string username = "Einzelhandelsverkaufspreis"; // Length 26
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsTrue(isValid, "Username should be correct");
        }

        [TestMethod]
        public void UsernameTooLong_ShouldReturnFalse()
        {
            string username = "Einzelhandelsverkaufspreis1"; // Length 27
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsFalse(isValid, "Username should be incorrect");
        }

        [TestMethod]
        public void UsernameContainSpecialChar1_ShouldReturnFalse()
        {
            string username = "stefano12@+-._";
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsFalse(isValid, "Username should be incorrect");
        }

        [TestMethod]
        public void UsernameContainSpecialChar2_ShouldReturnFalse()
        {
            string username = "stefano12!$%&/()=?'\\;";
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsFalse(isValid, "Username should be incorrect");
        }

        [TestMethod]
        public void UsernameContainSpace_ShouldReturnFalse()
        {
            string username = "stef ano";
            bool isValid = AuthenticateValidations.CheckUsernameRequirements(username);
            Assert.IsFalse(isValid, "Username should be incorrect");
        }
    }
}