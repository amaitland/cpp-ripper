using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CppRipper
{
    public partial class Form1 : Form
    {
        public void ParseFile(string file)
        {
            CppStructuralOutput output = new CppStructuralOutput();
            CppFileParser parser = new CppFileParser(output, file);
            editMain.Lines = output.GetStrings();
            editOutput.Text = parser.Message;
        }

        public void StartParsing()
        {
            String[] args = Environment.GetCommandLineArgs();
            if (args.Length < 2)
                throw new Exception("Expected one argument");
            string file = args[1];
            ParseFile(file);
        }

        public Form1()
        {
            InitializeComponent();
            StartParsing();
        }

    }
}
