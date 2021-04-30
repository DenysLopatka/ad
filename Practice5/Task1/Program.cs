using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using System.Text.RegularExpressions;


namespace Lesson6Practice
{
    internal class Program
    {
        private static readonly Dictionary<string, string> _users = new Dictionary<string, string> { { "admin", "admin" }, { "system", "system" } };
        private static bool _isLogin;

        private static void Main()
        {
            while (true)
                ChooseAction();
        }

        private static void ChooseAction()
        {
            WriteLine("The company welcomes you!Please choose the action:\n(R) Registration, (L) Login, (LG) Logout.\n");
            var userOperation = ReadLine();

            switch (userOperation?.ToLower())
            {
                case "r": Registration(); break;
                case "l": Login(); break;
                case "lg": Logout(); break;
                default: throw new ArgumentException($"Invalid operation {userOperation}.\n");
            }
        }

        private static void Login()
        {
            if (!_isLogin)
            {
                WriteLine("Please, enter your login:\n");
                var login = ReadLine();
                if (!ValidateLogin(ref login))
                    return;

                WriteLine("Please, enter the password.\n");
                var password = ReadLine();
                if (!ValidatePassword(login, password))
                    return;                

                WriteLine($"You have been successfully logged in the system as user with login {login}.\n");
                _isLogin = true;

                if (login == "admin")
                {                    
                    AdminLogin();
                }
            }
            else
                WriteLine("You are logged in the system.\n");
            
        }

        private static bool ValidateLogin(ref string login)
        {
            
            while (!IsLoginExist(login))
            {
                WriteLine("Do you want try again? Y/N\n");
                if (ReadLine().ToLower() == "y")
                {
                    WriteLine("Please, enter your login again:\n");
                    login = ReadLine();
                }
                else
                    return false;

                
            }

            return true;
        }

        private static bool IsLoginExist(string login)
        {
            var result = _users.Keys.Any(key => key.Equals(login));

            if (!result)
                WriteLine($"User with login {login} does not exist in the system.\n");

            return result;
        }

        private static bool ValidatePassword(string login, string password)
        {
            while (!IsPasswordCorrect(login, password))
            {
                WriteLine("Do you want try again? Y/N\n");
                if (ReadLine().ToLower() == "y")
                {
                    WriteLine("Please, enter your password again:\n");
                    password = ReadLine();
                }
                else
                    return false;
            }

            return true;
        }

        private static bool IsPasswordCorrect(string login, string password)
        {
            var result = _users[login].Equals(password);

            if (!result)
                WriteLine("Password is incorrect.\n");

            return result;
        }

        private static void Registration()
        {
            if (!_isLogin)
            {
                Regex regex = new Regex(@"[a-z|A-Z|0-9]");
                WriteLine("Please, enter the login:\n");
                var login = ReadLine();
                WriteLine("Please, enter the password:\n");
                var password = ReadLine();

                if (regex.IsMatch(login) == true)
                {
                    if (!_users.TryAdd(login, password))
                        WriteLine($"User with login {login} already exists.\n");
                    else
                    {
                        WriteLine($"You have been successfully registered in the system as user with login {login}.\n");
                        Login();
                    }
                }

                else
                {
                    WriteLine("Wrong login\n");
                    ChooseAction();
                }
            }
            else
                WriteLine("You are already logged in the system.\n");
        }

        private static void Logout()
        {
            if (_isLogin)
            {
                WriteLine("You are successfully logout from the system.\n");
                _isLogin = false;
            }
            else
                WriteLine("You are not logged in the system.\n");
        }

        private static void AdminLogin()
        {
            WriteLine("Welcome Administrator! What you do want to do?\n(D)Delete User, (CL)Change login, (CP)Change password, (LG)Logout\n");
            var actionForAdmin = ReadLine();

            try
            {
                switch (actionForAdmin?.ToLower())
                {
                    case "d": DeleteUser(); break;
                    case "cl": ChangeLogin(); break;
                    case "cp": ChangePassword(); break;
                    case "lg": Logout(); break;
                    default: throw new ArgumentException($"Invalid operation {actionForAdmin}.\n");
                }
            }
            catch
            {
                WriteLine("Wrong enter. Try again\n");
                AdminLogin();
            }
        }

        private static void ChangePassword()
        {
            WriteLine("Enter the login the password of which you want to change \n");
            var login = ReadLine();
            if (_users.ContainsKey(login))
            {
                WriteLine("Enter new password: \n");
                var password = ReadLine();
                _users[login] = password;
                WriteLine("Password has been changed. \n");
            }
            else
            {
                WriteLine($"There is no user with login {login}\n");
            }
            AdminLogin();
        }

        private static void ChangeLogin()
        {
            WriteLine("Enter login you want to change: \n");
            var login = ReadLine();            
            
            if (_users.ContainsKey(login))
            {
                WriteLine("Enter new login: \n");
                var newLogin = ReadLine();
                var password = _users[login];
                _users.Add(newLogin, password);
                _users.Remove(login);
                WriteLine("Login has been changed: \n");
            }
            else
            {
                WriteLine($"There is no user with login {login}\n");
            }
            AdminLogin();

        }

        private static void DeleteUser()
        {
            
            WriteLine("Enter the login of user you want to delete: \n");
            var login = ReadLine();
            if (_users.ContainsKey(login))
            {
                if (login == "admin")
                {
                    WriteLine("You can't delete Admin account");
                    AdminLogin();
                }
                else
                {
                    _users.Remove(login);
                    WriteLine("User was succesfully deleted.\n");
                }
            }
            else
            {
                WriteLine($"There is no user with login {login}\n");
            }
            AdminLogin();


        }
    }
}
/*1. Необходимо создать программу по регистрации и авторизации пользователя. 
 * Пользователь может авторизироваться, регистрироваться, и выходит из учетки, если авторизирован. Решить с использованием Dictionary.  */