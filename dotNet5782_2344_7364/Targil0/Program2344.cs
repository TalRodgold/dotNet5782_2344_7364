using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome2344();
            welcome7364();
            Console.ReadKey();
        }

        private static void welcome2344()
        {
            Console.Write("Enter your name: ");
            string user_name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", user_name);
        }

        static partial void welcome7364();
    }
}
