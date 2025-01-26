using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NETQuizProject
{
    internal class PersonScore
    {
        public string? Name { get; set; }
        public int Score { get; set; }
    }
    internal class QuizSettings
    {
        public string? question { get; set; }
        public List<string>? options { get; set; }
        public string? correct_answer { get; set; }



        public int id { get; set; }
        internal static void CreateQuiz()
        {
            string? choiceQuizFile = Console.ReadLine();
            string filepath = "";

            switch (choiceQuizFile)
            {
                case "1":
                    filepath = "HistoryQuiz.json";
                    break;
                case "2":
                    filepath = "BiologyQuiz.json";
                    break;
                case "3":
                    filepath = "GeographyQuiz.json";
                    break;
                case "4":
                    filepath = "MathematicsQuiz.json";
                    break;
                default:
                    Console.WriteLine("Incorrect select");
                    return;
            }

            string content = File.ReadAllText(filepath);
            var questions = JsonSerializer.Deserialize<List<QuizSettings>>(content);
            int correctCount = 0;
            Console.WriteLine("Questions 15 to 20 have 2 answers.");
           
            foreach (var question in questions)
            {
                Console.WriteLine($"{question.id}) {question.question}");

                Console.WriteLine("Options:");
                foreach (var option in question.options)
                {
                    Console.WriteLine($"- {option}");
                }

                string? userAnswer = Console.ReadLine();
                if (question.correct_answer?.ToUpper() == userAnswer?.ToUpper())
                {
                    correctCount++;
                    Console.WriteLine("True answer");
                }
                else
                {
                    Console.WriteLine($"Correct Answer: {question.correct_answer}\n");
                }
            }

         
            Console.WriteLine($"Your score: {correctCount} / {questions.Count}");
        }


    }


}



