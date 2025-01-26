using System.Threading.Channels;
using System.Text.Json;
using System.Reflection.Metadata;
using NETQuizProject;
using System.Data;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.IO.Pipes;
using System;
namespace NETQuizProject
{
    public class Program
    {

        public static void Main()
        {

            Console.WriteLine("Welcome to the Quiz Application!");
            while (true)
            {
                Console.WriteLine("1 - Register\n2 - Login\n3 - Admin\n4 - Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Registration.Register();
                        break;

                    case "2":
                        Logined.Login();
                        break;

                    case "3":
                        Console.WriteLine("1 - Registr\n2 - Login");
                        var AdminPanelChoice = Console.ReadLine();
                        if (AdminPanelChoice == "1")
                        {
                            Admin.AdminRegister();
                        }
                        else if (AdminPanelChoice == "2")
                        {
                            Admin.AdminLogin();
                        }
                        return;

                    case "4":
                        Console.WriteLine("Exit");
                        return;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }


            }

        }
    }
}




