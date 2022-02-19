using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Persistence;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WebApi.Tests
{
    [TestClass]
    public class PwValidationTests
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
}