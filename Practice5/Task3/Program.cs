using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Task3
{
    internal class Program
    {
        private static Dictionary<string, string> _namesWithNumbers = new Dictionary<string, string> {};
        
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                ChooseAction();
            }
        }

        private static void ChooseAction()
        {
            WriteLine("Что хотите сделать? \nДобавить новый номер (Д)/Удалить существующий номер(У)/Обновить номер пользователя(О)/Вывести все номера(В) \n");
            var action = ReadLine();

            try
            {
                switch (action?.ToLower())
                {
                    case "д": AddNumber(); break;
                    case "у": DeleteNumber(); break;
                    case "о": Refresh(); break;
                    case "в": ShowNumbers(); break;
                    default: throw new ArgumentException($"Нет такого дейсвия {action}.");
                }
            }
            catch
            {
                WriteLine("Неправильный ввод. посторите попытку.");
            }
        }

        private static void ShowNumbers()
        {
            foreach(KeyValuePair<string,string> KeyValue in _namesWithNumbers)
            {
                WriteLine(KeyValue.Key + " - " + KeyValue.Value);
            }
        }

        private static void Refresh()
        {
            WriteLine("Введите имя, которое хотите обновить: ");
            var name = ReadLine();
            if (_namesWithNumbers.ContainsKey(name))
            {
                WriteLine("Что хотите изменить? \nИмя(И)/Номер телефона(Н)");
                var refreshAction = ReadLine().ToLower();
                if (refreshAction == "и")
                {
                    ChangeName(name);
                    WriteLine("Имя успешно изменено\n");
                }
                else if (refreshAction == "н")
                {
                    ChangeNumber(name);
                }
            }
            else
            {
                WriteLine("Такого имени нету в телефонной книжке\n");
            }
        }

        private static void ChangeNumber(string name)
        {
            WriteLine("Введите новый номер телефона:");
            var number = ReadLine();
            _namesWithNumbers[name] = number;
        }

        private static void ChangeName(string name)
        {
            WriteLine("Введите новое имя:");
            var newName = ReadLine();
            var number = _namesWithNumbers[name];
            _namesWithNumbers.Add(newName, number);
            _namesWithNumbers.Remove(name);
            
        }

        private static void DeleteNumber()
        {
            WriteLine("Введите имя, которое хотите удалить: ");
            var name = ReadLine();
            if (_namesWithNumbers.ContainsKey(name))
            {
                _namesWithNumbers.Remove(name);
                WriteLine("Имя и номер телефона успешно удалены.\n");
            }
            else
            {
                WriteLine("Такого имени нету в телефонной книжке\n");
            }
        }

        private static void AddNumber()
        {
            WriteLine("Введите имя");
            var name = ReadLine();
            WriteLine("Введите номер телефона");
            var number = ReadLine();

            if (!_namesWithNumbers.TryAdd(name, number))
                WriteLine($"Пользователь с именем {name} уже есть в телефонной книге.\n");
            else
            {
                WriteLine($"Имя {name} и номер телефона {number} успешно добавлены.\n");                
            }            
        }
    }
}
/*3. Создать телефонную книгу, в которую пользователь может добавлять контакты, удалять контакты, обновлять контакты, и выводить все контакты в виде имен и их номеров телефонов, 
 * либо выводить какой-то один номер для определенно контакта.Необходимо использовать Dictionary при решении данной задачи. Учесть все негативные сценарии в связи с работой с Dictionary. 
 * Программа должна работать циклично.*/