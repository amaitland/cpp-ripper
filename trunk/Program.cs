using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CppRipper
{
    class Program
    {
        public void ParseFile(string file)
        {
            CppStructuralOutput output = new CppStructuralOutput();
            CppFileParser parser = new CppFileParser(output, file);
            string[] strings = output.GetStrings();
            foreach (string s in strings)
                WriteLine(s);
            WriteLine(parser.Message);
        }

        public void StartParsing()
        {
            String[] args = Environment.GetCommandLineArgs();
            if (args.Length < 2)
                throw new Exception("Expected one argument");
            string file = args[1];
            ParseFile(file);
        }

        public void Write(string s)
        {
            Console.Write(s);
        }

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public Program()
        {
            StartParsing();
            WriteLine("... press any key to continue");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            new Program();
        }
    }
}
