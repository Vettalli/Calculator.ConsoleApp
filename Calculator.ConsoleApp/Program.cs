using System;
using Calculator.Application;

namespace Calculator.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ICalculate calculate = new CalculatorApplication();
            IFile fromFile = new CalcFromFile();

            Console.WriteLine("Please, choose how would you like to enter your math expression:\nWrite 1 if you want to right expression right in console\nWrite 2 if you want to select the file with expressions");

            try
            {
                string userChoice;
                string path;

                while (true)
                {
                    userChoice = Console.ReadLine();

                    if (userChoice == "1" || userChoice == "2")
                    {
                        break;
                    }
                }
                
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("Please, write your expression");
                        Console.WriteLine(calculate.Calculate(Console.ReadLine()));
                        break;
                    case "2":
                    {
                        if (args?.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
                        {
                            path = args[0];
                        }
                        else
                        {
                            path = Console.ReadLine();
                        }
                        
                        fromFile.CalculateExpressions(path);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
