using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NETQuizProject
{


    internal class Logined
    {
        internal static void Login()
        {
            string filepath = "person.json";

            if (!File.Exists(filepath) || new FileInfo(filepath).Length == 0)
            {
                Console.WriteLine("No users found. Please register first.");
                return;
            }

            string jsonString = File.ReadAllText(filepath);

            Console.Write("Enter Username: ");
            var loginUsername = Console.ReadLine();

            User? person = null;

            if (jsonString.TrimStart().StartsWith("["))
            {
                var people = JsonSerializer.Deserialize<List<User>>(jsonString);
                person = people?.FirstOrDefault(p =>
                    p.Username.Equals(loginUsername, StringComparison.OrdinalIgnoreCase));
            }

            if (person != null)
            {
                Console.Write("Enter Password: ");
                var loginPassword = Console.ReadLine();

                if (person.Password == loginPassword)
                {
                    Console.WriteLine($"Login successful! Welcome, {person.Username}.");
                    Console.WriteLine("1 - Start a new quiz\n2 - Change settings\n3 - Logout account");
                    var choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Console.WriteLine("1 - Select a field of knowledge\n2 - choose a mixed field of knowledge");
                        var quizChoice = Console.ReadLine();
                        switch (quizChoice)
                        {
                            case "1":
                                Console.WriteLine("1 - History\n2 - Biology\n3 - Geography\n4 - Mathematics");
                                QuizSettings.CreateQuiz();
                                break;

                            case "2":
                                MixedQuized.MixQuiz();
                                break;


                        }

                    }
                    switch (choice)
                    {

                        case "2":
                            Registration.ChangeSettings();
                            break;

                        case "3":
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Incorrect password!");
                }
            }
            else
            {
                Console.WriteLine("Username not found!");
            }
        }

    }
}




