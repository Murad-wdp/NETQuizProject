using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NETQuizProject
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection.Metadata;
    using System.Text.Json;

    internal class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime BirthDate { get; set; }
    }

    internal class Registration
    {
        internal static void Register()
        {
            string filepath = "person.json";


            var users = new List<User>();
            if (File.Exists(filepath))
            {
                string existingData = File.ReadAllText(filepath);
                if (!string.IsNullOrWhiteSpace(existingData))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();
                }
            }


            Console.Write("Username: ");
            var username = Console.ReadLine();
            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("This username is already in use. Please choose a different one.");
                return;
            }


            Console.Write("Password: ");
            var password = Console.ReadLine();

            Console.WriteLine("Birthday (dd-mm-yyyy): ");
            DateTime birthDate;
            while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
            {
                Console.WriteLine("Invalid Date format. Please enter the date in dd-mm-yyyy format:");
            }

            User newUser = new User
            {
                Username = username,
                Password = password,
                BirthDate = birthDate
            };


            users.Add(newUser);


            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, jsonString);

            Console.WriteLine("User registered successfully!");
        }

        internal static void ChangeSettings()
        {
            string filepath = "person.json";

            
            var users = ReadUsersFromJson(filepath);

           
            Console.Write("Enter username to update: ");
            string username = Console.ReadLine() ?? string.Empty;

          
            var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
              
                
                Console.Write("Enter new username: ");
                string newUsername = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine() ?? string.Empty;

                user.Username = newUsername;
                user.Password = newPassword;

               
                SaveUsersToJson(filepath, users);


                Console.WriteLine("User username and password updated successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

      
        static List<User> ReadUsersFromJson(string filepath)
        {
            if (File.Exists(filepath))
            {
                string jsonContent = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<User>>(jsonContent) ?? new List<User>();
            }
            else
            {
                Console.WriteLine("File not found.");
                return new List<User>();
            }
        }

        
        static void SaveUsersToJson(string filepath, List<User> users)
        {
            string jsonContent = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, jsonContent);
        }
    }
}

        

