using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class ChallengeTest
    {

        string challengerEmail = "ATUL001@e.ntu.edu.sg";
        string challengerPassword = "U1922089F";
        string challengeeEmail = "GARG0002@e.ntu.edu.sg";
        string challengeePassword = "U1925779F";

        [UnityTest]
        public IEnumerator ChallengeTestSendAndReceive()
        {

            // Login
            SceneManager.LoadScene("LoginUI");
            yield return new WaitForSeconds(1.0f);

            GameObject loginController = GameObject.Find("Authenticator");
            Assert.NotNull(loginController);
            loginController.GetComponent<LoginController>().AuthenticateDetails(challengerEmail, challengerPassword);
            yield return new WaitForSeconds(2.0f);
            // Create challenge

            SceneManager.LoadScene("CreateChallenge");

            yield return new WaitForSeconds(1.0f);

            GameObject challengeName = GameObject.Find("ChallengeNameIF");
            Assert.NotNull(challengeName);
            int length = 6;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            System.Random random = new System.Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            challengeName.transform.GetComponent<InputField>().text = str_build.ToString();
            Debug.Log(challengeName.transform.GetComponent<InputField>().text);
            //yield return new WaitForSeconds(2.0f);
            // Play challenge
            GameObject playButton = GameObject.Find("PlayButton");
            playButton.transform.GetComponent<Button>().onClick.Invoke();
            yield return new WaitForSeconds(20.0f);

            // Send challenge
            GameObject sendChallenge = GameObject.Find("SendChallenge"); 
            sendChallenge.transform.GetComponent<Button>().onClick.Invoke();

            yield return new WaitForSeconds(5.0f);

            // Login
            SceneManager.LoadScene("LoginUI");
            yield return new WaitForSeconds(1.0f);
            loginController = GameObject.Find("Authenticator");
            Assert.NotNull(loginController);
            loginController.GetComponent<LoginController>().AuthenticateDetails(challengeeEmail, challengeePassword);
            yield return new WaitForSeconds(2.0f);


            // Check challenge exists
            SceneManager.LoadScene("ChallengeReceived");

            yield return new WaitForSeconds(1.0f);
            GameObject content = GameObject.FindGameObjectWithTag("Content");

            Assert.NotNull(content);

            bool found = false;

            List<GameObject> challengeObjects = content.GetComponent<ReceivedChallengeDisplay>().GetGameObjects();

            Assert.NotNull(challengeObjects);

            foreach(GameObject challengeObject in challengeObjects)
            {
                Assert.NotNull(challengeObject.transform.Find("Cname"));
                if (challengeObject.transform.Find("Cname").GetComponent<Text>().text == str_build.ToString())
                    found = true;
            }

            Debug.Log("Found: " + found);
            Assert.AreEqual(true, found);

        }
    }
}
