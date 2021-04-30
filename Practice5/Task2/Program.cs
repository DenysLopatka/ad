using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Симуляция очереди к врачу начата.");

            Hospital();

            Console.WriteLine("Симуляция очереди к врачу закончена.");
        }
        static void Hospital()
        {
            var QueueToDoctor = new Queue<string>();
            QueueToDoctor.Enqueue("Ivan");
            QueueToDoctor.Enqueue("Vasiliy");
            QueueToDoctor.Enqueue("Georgiy");
            QueueToDoctor.Enqueue("Lydmila");
            QueueToDoctor.Enqueue("Maria");

            var rand = new Random();
            var timeWithDoctor = rand.Next(5000, 15001);

            while (QueueToDoctor.Count != 0)
            {
                var nextHuman = QueueToDoctor.Dequeue();
                Console.WriteLine($"{nextHuman} зашел к доктору");
                Thread.Sleep(timeWithDoctor);
                Console.WriteLine($"{nextHuman} вышел от доктора");
            }
            return;
        }
    }
}


/*2. Организовать очередь на прием к доктору с помощью Queue, 
 * чтобы пациент проводил у доктора от 5 до 15 секунд рандомно. Кол-во пациентов должно быть не менее 5-ти. Все действия должны выводиться в консоль.*/