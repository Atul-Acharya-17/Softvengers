using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AdaptiveQuestioningTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AdaptiveQuestioningEasyToMedium()
        {
            int planetDifficulty = 0;
            int questionDifficulty = 0;
            int correctStreak = 5;
            int incorrectStreak = 0;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnCorrect(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);

            Assert.AreEqual(1, nextQuestionDifficulty);
        }

        [Test]
        public void AdaptiveQuestioningMediumToEasy()
        {
            int planetDifficulty = 1;
            int questionDifficulty = 1;
            int correctStreak = 0;
            int incorrectStreak = 2;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnWrong(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);

            Assert.AreEqual(0, nextQuestionDifficulty);
        }

        [Test]
        public void AdaptiveQuestioningMediumToHard()
        {
            int planetDifficulty = 1;
            int questionDifficulty = 1;
            int correctStreak = 5;
            int incorrectStreak = 0;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnCorrect(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);

            Assert.AreEqual(2, nextQuestionDifficulty);
        }

        [Test]
        public void AdaptiveQuestioningHardToMedium()
        {
            int planetDifficulty = 2;
            int questionDifficulty = 2;
            int correctStreak = 0;
            int incorrectStreak = 2;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnWrong(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);

            Assert.AreEqual(1, nextQuestionDifficulty);
        }

        [Test]
        public void AdaptiveQuestioningEasyToMediumOnMedium()
        {
            int planetDifficulty = 1;
            int questionDifficulty = 0;
            int correctStreak = 0;
            int incorrectStreak = 3;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnCorrect(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);
            Assert.AreEqual(1, nextQuestionDifficulty);
        }

        [Test]
        public void AdaptiveQuestioningMediumToEasyOnEasy()
        {
            int planetDifficulty = 0;
            int questionDifficulty = 1;
            int correctStreak = 6;
            int incorrectStreak = 0;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnWrong(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);
            Assert.AreEqual(0, nextQuestionDifficulty);
        }

        [Test]
        public void AdaptiveQuestioningHardToMediumOnMedium()
        {
            int planetDifficulty = 1;
            int questionDifficulty = 2;
            int correctStreak = 6;
            int incorrectStreak = 0;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnWrong(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);

            Assert.AreEqual(1, nextQuestionDifficulty);
        }

        [Test]
        public void AdaptiveQuestioningMediumToHardOnHard()
        {
            int planetDifficulty = 2;
            int questionDifficulty = 1;
            int correctStreak = 3;
            int incorrectStreak = 0;

            int nextQuestionDifficulty = AdaptiveQuestioning.GetNextQuestionDifficultyOnCorrect(planetDifficulty, questionDifficulty, ref correctStreak, ref incorrectStreak);

            Assert.AreEqual(2, nextQuestionDifficulty);
        }

    }
}
