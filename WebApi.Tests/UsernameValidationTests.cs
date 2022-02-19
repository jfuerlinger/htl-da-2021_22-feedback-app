using FeedbackApp.WebApi.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Tests
{
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
