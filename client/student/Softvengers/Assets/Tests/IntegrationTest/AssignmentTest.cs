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
    public class AssignmentTest
    {

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.

        string teacherEmail = "SU002@e.ntu.edu.sg";
        string teacherPassword = "SAIHOT";

        TeacherLogin teacherLogin = new TeacherLogin();

        [UnityTest]
        public IEnumerator AssignmentTestWithEnumeratorPasses()
        {
            int length = 7;

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

            string assignmentName = str_build.ToString();
            // Use the Assert class to test conditions.
            // Use yield to skip a framee


            teacherLogin.emailID = teacherEmail;
            teacherLogin.password = teacherPassword;

            GameObject assignmentTest = new GameObject();
            assignmentTest.AddComponent<Tester>();

            yield return assignmentTest.GetComponent<Tester>().TestAssignment(teacherLogin, assignmentName);

            yield return new WaitForSeconds(2.0f);

            Debug.Log("Finished Adding");

            SceneManager.LoadScene("LoginUI");
            yield return new WaitForSeconds(2.0f);
            GameObject loginController = GameObject.Find("Authenticator");
            Debug.Log(loginController);
            Assert.NotNull(loginController);

            string studentEmail = "ATUL001@e.ntu.edu.sg";
            string studentPassword = "U1922089F";

            loginController.GetComponent<LoginController>().AuthenticateDetails(studentEmail, studentPassword);
            yield return new WaitForSeconds(5.0f);

            SceneManager.LoadScene("Assignment");

            yield return new WaitForSeconds(2.0f);

            GameObject assignmentController = GameObject.FindGameObjectWithTag("Content");

            Assert.NotNull(assignmentController);

            assignmentController.GetComponent<AssignmentScene>();

            List<GameObject> assignments = assignmentController.GetComponent<AssignmentScene>().getAssignmentObjects();

            bool found = false;

            foreach (GameObject assignment in assignments)
            {
                Assert.NotNull(assignment.transform.Find("Aname"));
                Debug.Log("Name: " + assignment.transform.Find("Aname").GetComponent<Text>().text);
                if (assignment.transform.Find("Aname").GetComponent<Text>().text == assignmentName)
                    found = true;
            }

            Debug.Log("Found: " + found);
            Assert.AreEqual(true, found);

        }

    }


    public class Tester : MonoBehaviour
    {
        public IEnumerator TestAssignment(TeacherLogin teacherLogin, string assingmentName)
        {
            Debug.Log("Hello");
            System.Random rnd = new System.Random();
            Debug.Log("Created Random object");

            yield return StartCoroutine(ServerController.Post("http://localhost:5000/teacher/login", teacherLogin.stringify(),
                result =>
                {
                    Debug.Log("Logged in");
                    int assignmentID = rnd.Next(1001, 999999999);

                    Debug.Log(result);

                    SecurityToken.Token = result;

                    AssignmentCreation assignment = new AssignmentCreation();

                    assignment.tutGrp = "SCE5";
                    assignment.assignmentName = assingmentName;
                    assignment.timeLimit = 20;
                    assignment.deadline = "2022-05-01";
                    assignment.questionIDs = new List<int>() {1000};

                    AssignmentQuestion question = new AssignmentQuestion();
                    question.questionID = rnd.Next(1006, 999999999);
                    question.points = 100;
                    question.body = "Testing Assignment";
                    question.wrongOptions = new List<string> { "1", "2", "3" };
                    question.correctOption = "Correct";

                    string body = "[" + assignment.stringify() + "," +  question.stringify() + "]";


                    //Debug.Log(JsonUtility.ToJson(body));
                    Debug.Log(body);
                    Debug.Log("Creating Assignment");
                    StartCoroutine(ServerController.Post(string.Format("http://localhost:5000/teacher/assignment/{0}", assignmentID), body,
                        testResult =>
                        {
                            print(testResult);
                            
                        }
                        ));
                }));
        }
    }

    public class TeacherLogin
    {
        public string emailID;
        public string password;
        public string stringify()
        {
            return JsonUtility.ToJson(this);
        }
    }


    public class AssignmentCreation
    {
        public string tutGrp;
        public string assignmentName;
        public int timeLimit;
        public string deadline;
        public List<int> questionIDs;
        public string stringify()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public class AssignmentQuestion
    {
        public int questionID;
        public string body;
        public List<String> wrongOptions;
        public string correctOption;
        public int points;
        public string stringify()
        {
            return JsonUtility.ToJson(this);
        }
    }

}
