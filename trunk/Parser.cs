using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CppRipper
{
    public class CppFileParser
    {
        static CppStructuralGrammar grammar = new CppStructuralGrammar();

        private string message;

        public string Message { get { return message; } }

        public CppFileParser(IAstPrinter printer, string file)
        {
            Rule parse_rule = grammar.file;

            string text = File.ReadAllText(file);
            printer.Clear();
            ParserState state = new ParserState(text);

            try
            {
                if (!parse_rule.Match(state))
                {
                    message = "Failed to parse file " + file;
                }
                else
                {
                    if (state.AtEndOfInput())
                    {
                        message = "Successfully parsed file";
                    }
                    else
                    {
                        message = "Failed to read end of input";
                    }
                }
                    
            }
            catch (ParsingException e)
            {
                state.ForceCompletion();
                message = e.Message;
            }

            printer.PrintNode(state.GetRoot(), 0);
        }
    }

    public class CppFileSetParser
    {
        public DirectoryInfo di;

        public CppFileSetParser(IAstPrinter printer, string sDir)
        {
            di = new DirectoryInfo(sDir);
            foreach (FileInfo fi in di.GetFiles("*.c;*.cpp;*.h;*.hpp;"))
            {
                CppFileParser fp = new CppFileParser(printer, fi.Name);
            }
        }
    }
}

