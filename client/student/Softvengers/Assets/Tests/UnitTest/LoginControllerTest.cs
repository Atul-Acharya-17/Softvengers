using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LoginControllerTest
    {
        private string emailID;
        private string passWord;

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator LoginControllerTestValidLogin()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            emailID = "ATUL001@e.ntu.edu.sg";
            passWord = "U1922089F";

            var gameObject = new GameObject();
            gameObject.AddComponent<LoginController>();
            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, passWord);

            yield return new WaitForSeconds(2.0f);
            Debug.Log(SecurityToken.Email);
            Assert.AreEqual(SecurityToken.Email, emailID);
        }

        [UnityTest]
        public IEnumerator LoginControllerTestInvalidPassword()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            emailID = "ATUL001@e.ntu.edu.sg";
            passWord = "ABCDEFGHI";

            var gameObject = new GameObject();
            gameObject.AddComponent<LoginController>();
            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, passWord);

            yield return new WaitForSeconds(2.0f);
            Assert.AreEqual(SecurityToken.Email, "");
        }

        [UnityTest]
        public IEnumerator LoginControllerTestInvalidEmailAndPassword()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            emailID = "ATUL@e.ntu.edu.sg";
            passWord = "ABCDGFEJF";

            var gameObject = new GameObject();
            gameObject.AddComponent<LoginController>();
            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, passWord);

            yield return new WaitForSeconds(2.0f);
            Assert.AreEqual(SecurityToken.Email, "");
        }

        [UnityTest]
        public IEnumerator LoginControllerTestInvalidEmail()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            emailID = "ATUL@e.ntu.edu.sg";
            passWord = "U1922089F";

            var gameObject = new GameObject();
            gameObject.AddComponent<LoginController>();
            gameObject.GetComponent<LoginController>().AuthenticateDetails(emailID, passWord);

            yield return new WaitForSeconds(2.0f);
            Assert.AreEqual(SecurityToken.Email, "");
        }
    }
}

