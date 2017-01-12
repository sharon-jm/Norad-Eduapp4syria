﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EA4S
{
    /// <summary>
    /// This sample class generates 32 quizzes of type "I give you a word, you say that word"
    /// </summary>
    public class SampleQuestionWithWordsProvider : IQuestionProvider
    {
        List<SampleQuestionPack> questions = new List<SampleQuestionPack>();
        string description;

        int currentQuestion;

        public SampleQuestionWithWordsProvider ()
        {
            currentQuestion = 0;

            description = "Questions description";

            for (int i = 0; i < 32; i++)
            {
                List<ILivingLetterData> correctAnswers = new List<ILivingLetterData>();
                List<ILivingLetterData> wrongAnswers = new List<ILivingLetterData>();

                LL_WordData newWordData = AppManager.I.Teacher.GetRandomTestWordDataLL();

                if (newWordData == null)
                    return;

                correctAnswers.Add(newWordData);

                // At least 4 wrong words
                while (wrongAnswers.Count < 4)
                {
                    var word = AppManager.I.Teacher.GetRandomTestWordDataLL();

                    if (!correctAnswers.Contains(word) && !wrongAnswers.Contains(word))
                    {
                        wrongAnswers.Add(word);
                    }
                }

                var currentPack = new SampleQuestionPack(newWordData, wrongAnswers, correctAnswers);
                questions.Add(currentPack);
            }
        }

        public string GetDescription()
        {
            return description;
        }

        IQuestionPack IQuestionProvider.GetNextQuestion()
        {
            currentQuestion++;

            if (currentQuestion >= questions.Count)
                currentQuestion = 0;

            return questions[currentQuestion];
        }
    }
}