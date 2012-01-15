using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace TestsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile, outputFile;
            Console.Write("Write input file (with extension part): ");
            inputFile = Console.ReadLine();
            Console.Write("Write input file (with extension part): ");
            outputFile = Console.ReadLine();
            if (inputFile != string.Empty && outputFile != string.Empty && inputFile.Contains('.'))
            {
                TestGenerator generator = new TestGenerator(inputFile, outputFile);
                int variants, questions;
                bool mix;
                Console.Write("Write number of variants to generate: ");
                variants = Convert.ToInt32(Console.ReadLine());
                Console.Write("Write number of question per variant: ");
                questions = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Would you like to mix answers? (y/n) ");
                char c = Console.ReadKey().KeyChar;
                if (c=='y'||c=='Y')
                    mix = true;
                else if (c=='n'||c=='N')
                    mix = false;
                else throw new ArgumentException("I said you should say yes or no!");

                generator.GenerateTest(variants, questions, mix);
                //Process.Start(generator.OutputFileName);
            }
            else {
                Console.WriteLine("Wrong put. Extension part is missed");
            }
        }
    }
}