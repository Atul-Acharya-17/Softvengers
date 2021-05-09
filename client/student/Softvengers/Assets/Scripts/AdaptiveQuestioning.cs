using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveQuestioning : MonoBehaviour
{

    private static int correctThreshold = 6;
    private static int incorrectThreshold = 3;


    public static int GetNextQuestionDifficultyOnCorrect(int planetLevel, int currentQuestionLevel, ref int correctStreak, ref int incorrectStreak)
    {
        if (currentQuestionLevel == planetLevel)
        {
            correctStreak++;
            incorrectStreak = 0;
            if (correctStreak == correctThreshold && currentQuestionLevel < 2)
            {
                currentQuestionLevel++;
            }
        }
        else if (currentQuestionLevel < planetLevel)
        {
            incorrectStreak--;
            currentQuestionLevel++;
        }

        return currentQuestionLevel;
    }

    public static int GetNextQuestionDifficultyOnWrong(int planetLevel, int currentQuestionLevel, ref int correctStreak, ref int incorrectStreak)
    {
        if (currentQuestionLevel == planetLevel)
        {
            incorrectStreak++;
            correctStreak = 0;
            if (incorrectStreak == incorrectThreshold && currentQuestionLevel > 0)
            {
                currentQuestionLevel--;
            }
        }
        else if (currentQuestionLevel > planetLevel)
        {
            correctStreak--;
            currentQuestionLevel--;
        }

        return currentQuestionLevel;
    }

}
