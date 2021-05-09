using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ResetPasswordTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ResetPasswordTestCorrectConfirmation()
        {
            string newPassword = "ASGHY156as&^";
            string confirmationPassword = "ASGHY156as&^";

            GameObject gameObject = new GameObject();
            gameObject.AddComponent<ResetPasswordHandler>();
            bool result = gameObject.GetComponent<ResetPasswordHandler>().CheckConfirmationPassword(newPassword, confirmationPassword);

            Assert.AreEqual(true, result);
            // Use the Assert class to test conditions
        }

        [Test]
        public void ResetPasswordTestWrongConfirmation()
        {
            string newPassword = "ASGHY156as&^";
            string confirmationPassword = "ASGHY156asH^";

            GameObject gameObject = new GameObject();
            gameObject.AddComponent<ResetPasswordHandler>();
            bool result = gameObject.GetComponent<ResetPasswordHandler>().CheckConfirmationPassword(newPassword, confirmationPassword);

            Assert.AreEqual(false, result);
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ResetPasswordTestValidUpdate()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            string oldPassword = "U1922089F";
            string newPassword = "U1922089F";

            // Login to get SecurityToken
            // Then UpdateDataBase
            // Then Login and Assert SecurityToken.Email
            string emailID = "ATUL001@e.ntu.edu.sg";
           

            var gameObject = new GameObject();
            gameObject.AddComponent<LoginController>();
            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, oldPassword);

            yield return new WaitForSeconds(2.0f);

            gameObject.AddComponent<ResetPasswordHandler>();
            gameObject.GetComponent<ResetPasswordHandler>().UpdateDatabase(oldPassword, newPassword);

            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, newPassword);

            yield return new WaitForSeconds(2.0f);

            Assert.AreEqual(emailID, SecurityToken.Email);
        }

        [UnityTest]
        public IEnumerator ResetPasswordTestInvalidUpdate()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            string correctPassword = "U1922089F";
            string oldPassword = "AVDGE5Hheu*";
            string newPassword = "AHBEDU43I89SXK(&";

            // Login to get SecurityToken
            // Then UpdateDataBase
            // Then Login and Assert SecurityToken.Email
            string emailID = "ATUL001@e.ntu.edu.sg";


            var gameObject = new GameObject();
            gameObject.AddComponent<LoginController>();
            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, correctPassword);

            yield return new WaitForSeconds(2.0f);

            gameObject.AddComponent<ResetPasswordHandler>();
            gameObject.GetComponent<ResetPasswordHandler>().UpdateDatabase(oldPassword, newPassword);

            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, newPassword);

            yield return new WaitForSeconds(2.0f);

            Assert.AreEqual("", SecurityToken.Email);
        }
    }
}
