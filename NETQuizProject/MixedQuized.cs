using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NETQuizProject
{
    public class Question
    {
        public int id { get; set; }
        public string? question { get; set; }
        public List<string>? options { get; set; }
        public string? correct_answer { get; set; }
    }

    public class MixedQuized
    {
        public static void MixQuiz()
        {
            string[] filepaths = new string[]
            {
            "MathematicsQuiz.json",
            "BiologyQuiz.json",
            "GeographyQuiz.json",
            "HistoryQuiz.json"
            };

            var allQuestions = new List<Question>();


            foreach (var filepath in filepaths)
            {
                string content = File.ReadAllText(filepath);
                var questions = JsonSerializer.Deserialize<List<Question>>(content);
                allQuestions.AddRange(questions);
            }

            var availableQuestions = allQuestions.Where(q => q.id < 15).ToList();
            Random random = new Random();
            var shuffledQuestions = availableQuestions.OrderBy(q => random.Next()).Take(20).ToList();


            Console.WriteLine("Quiz Questions:");
            int questionNumber = 1;
            int correctAnswer = 0;
            foreach (var question in shuffledQuestions)
            {

                Console.WriteLine($"{questionNumber}) {question.question}");
                for (int i = 0; i < question.options.Count; i++)
                {
                    Console.WriteLine(question.options[i]);
                }
                string? answer = Console.ReadLine();
                if (question.correct_answer.ToUpper() == answer.ToUpper())
                {
                    correctAnswer++;
                    Console.WriteLine("True answer");
                }
                else
                {
                    Console.WriteLine($"Correct answer {question.correct_answer}");
                }
                Console.WriteLine();
                questionNumber++;

            }
            Console.WriteLine($"Your score 20 / {correctAnswer}");
        }
    }
}





