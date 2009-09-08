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
            StructuralCppOutput output = new StructuralCppOutput();
            CppFileParser parser = new CppFileParser(output, file);
            editMain.Text = output.ToString();
            editOutput.Text = parser.Message;
        }

        public void StartParsing()
        {
            string dir = @"C:\cygwin\home\Chr15topher\dev\lua-5.1.4\src";
            string file = dir + "\\" + "lparser.c";
            ParseFile(file);
        }

        public Form1()
        {
            InitializeComponent();
            StartParsing();
        }

    }
}
