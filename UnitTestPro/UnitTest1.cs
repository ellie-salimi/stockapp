using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NYSEPro;

namespace UnitTestPro
{
   
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_login_Admin()
        {
            User adminUser = new User();
            // Arrange
            string username = "admin";
            string password = "admin123";
            string expected = "Admin";

            // Act
            string actual = adminUser.login(username, password);

            // Assert

            Assert.AreEqual(expected, actual);
        }
    }
}
