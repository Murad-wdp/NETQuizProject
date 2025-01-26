using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NETQuizProject
{
    internal class Admin
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDate { get; set; }

        public Admin() { }

        private Admin(string? username, string? password, DateTime? birdhDate)
        {
            Username = username;
            Password = password;
            BirthDate = birdhDate;
        }


        //Admin Registration
        internal static void AdminRegister()
        {
            string filepath = "admin.json";
            string existingData = File.ReadAllText(filepath);
            if (!string.IsNullOrWhiteSpace(existingData))
            {
                Console.WriteLine("Admin already registered. No need to register again.");
                return;
            }
            else
            {
                Console.Write("Username: ");
                var username = Console.ReadLine();

                Console.Write("Password: ");
                var password = Console.ReadLine();

                Console.WriteLine("Birthday (dd-mm-yyyy): ");
                DateTime birthDate;
                while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    Console.WriteLine("Invalid Date format. Please enter the date in dd-mm-yyyy format:");
                }
                Admin admin = new Admin
                {
                    Username = username,
                    Password = password,
                    BirthDate = birthDate
                };
                List<Admin> admins = new List<Admin>();
                if (!string.IsNullOrWhiteSpace(existingData))
                {
                    admins = JsonSerializer.Deserialize<List<Admin>>(existingData) ?? new List<Admin>();
                }

                admins.Add(admin);

                string jsonString = JsonSerializer.Serialize(admins, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filepath, jsonString);

                Console.WriteLine("Admin registered successfully!");
            }
        }

        //Admin Login
        internal static void AdminLogin()
        {
            string filepath = "admin.json";
            string existingData = File.ReadAllText(filepath);


            if (string.IsNullOrWhiteSpace(existingData))
            {
                Console.WriteLine("No admin found. Please register first.");
                return;
            }

            string jsonString = File.ReadAllText(filepath);

            Console.Write("Enter Username: ");
            var loginUsername = Console.ReadLine();

            Admin? person = null;

            if (jsonString.TrimStart().StartsWith("["))
            {
                var people = JsonSerializer.Deserialize<List<Admin>>(jsonString);
                person = people?.FirstOrDefault(p =>
                    p.Username.Equals(loginUsername, StringComparison.OrdinalIgnoreCase));
            }

            if (person != null)
            {
                Console.Write("Enter Password: ");
                var loginPassword = Console.ReadLine();

                if (person.Password == loginPassword)
                {
                    Console.WriteLine($"Login successful! Welcome,Admin: {person.Username}.");
                    Console.WriteLine("1 - Create Quiz\n2 - Change Quiz");
                    var QuizSettingsChoice = Console.ReadLine();
                    switch (QuizSettingsChoice)
                    {
                        case "1":
                            CreateQuiz();
                            break;

                        case "2":
                            ChangeQuiz();
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("No password found!");
                }
            }
            else
            {
                Console.WriteLine("No admin username found!");
            }
        }



        internal static void CreateQuiz()
        {

            Console.WriteLine("Choose quiz type to create (HistoryQuiz, MathematicsQuiz, BiologyQuiz,GeographyQuiz): ");
            string? quizType = Console.ReadLine();

            string filepath = $"{quizType}.json";
            List<QuizSettings> quizzes = new List<QuizSettings>();

            if (File.Exists(filepath))
            {
                string existingData = File.ReadAllText(filepath);
                quizzes = JsonSerializer.Deserialize<List<QuizSettings>>(existingData) ?? new List<QuizSettings>();
            }

            Console.WriteLine("Enter quiz details:");


            Console.Write("Enter number of questions: ");
            int numberOfQuestions;
            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions) || numberOfQuestions <= 0)
            {
                Console.WriteLine("Please enter a valid number of questions.");
            }

            for (int i = 0; i < numberOfQuestions; i++)
            {
                Console.WriteLine($"Enter details for question {i + 1}:");


                Console.Write("Question: ");
                string? questionText = Console.ReadLine();


                List<string> options = new List<string>();
                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"Option {j + 1}: ");
                    options.Add(Console.ReadLine());
                }


                Console.Write("Enter correct answer (A, B, C, or D): ");
                string? correctAnswer = Console.ReadLine()?.ToUpper();


                QuizSettings quiz = new QuizSettings
                {
                    id = quizzes.Count + 1,
                    question = questionText,
                    options = options,
                    correct_answer = correctAnswer
                };

                quizzes.Add(quiz);
            }


            string jsonString = JsonSerializer.Serialize(quizzes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, jsonString);

            Console.WriteLine("Quiz created successfully!");
        }




        internal static void ChangeQuiz()
        {
            Console.WriteLine("Choose quiz type to change (History, Math, etc.): ");
            string? quizType = Console.ReadLine();

            string? filepath = $"{quizType}.json";
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Quiz file not found. Please create a quiz first.");
                return;
            }

            string? existingData = File.ReadAllText(filepath);
            List<QuizSettings> quizzes = JsonSerializer.Deserialize<List<QuizSettings>>(existingData) ?? new List<QuizSettings>();

            Console.WriteLine("Enter the ID of the quiz question you want to change:");
            int questionId;
            while (!int.TryParse(Console.ReadLine(), out questionId) || !quizzes.Any(q => q.id == questionId))
            {
                Console.WriteLine("Invalid ID. Please enter a valid quiz ID.");
            }

            QuizSettings quizToChange = quizzes.First(q => q.id == questionId);

            Console.WriteLine($"Current question: {quizToChange.question}");
            Console.Write("Enter new question (leave empty to keep current): ");
            string? newQuestion = Console.ReadLine();
            if (!string.IsNullOrEmpty(newQuestion))
            {
                quizToChange.question = newQuestion;
            }


            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Current Option {i + 1}: {quizToChange.options[i]}");
                Console.Write($"Enter new option {i + 1} (leave empty to keep current): ");
                string? newOption = Console.ReadLine();
                if (!string.IsNullOrEmpty(newOption))
                {
                    quizToChange.options[i] = newOption;
                }
            }

            Console.WriteLine($"Current correct answer: {quizToChange.correct_answer}");
            Console.Write("Enter new correct answer (A, B, C, D, leave empty to keep current): ");
            string? newCorrectAnswer = Console.ReadLine()?.ToUpper();
            if (!string.IsNullOrEmpty(newCorrectAnswer))
            {
                quizToChange.correct_answer = newCorrectAnswer;
            }


            string? jsonString = JsonSerializer.Serialize(quizzes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, jsonString);

            Console.WriteLine("Quiz updated successfully!");
        }
    }
}