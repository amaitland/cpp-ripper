using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CppRipper
{
    /// <summary>
    /// This grammar is used for parsing C/C++ files into declarations, including comments.
    /// The goal is to get a high-level structure of the language, and understand 
    /// where the comments relate to items.
    /// </summary>
    class CppFullGrammar
        : CppBaseGrammar
    {
        public CppFullGrammar()
        {
            InitializeRules<CppStructuralGrammar>();
        }

        public static Rule Delimiter(string s)
        {
            return CharSeq(s) + multiline_ws;
        }

        public static Rule DelimitedGroup(string begin, Rule r, string close)
        {
            return CharSeq(begin) + multiline_ws + Star(r) + CharSeq(close) + multiline_ws; 
        }

        public static Rule symbol = Store(Not(CharSeq("/*") | CharSeq("//")) + CharSet("~!@%^&*-+=|:<>.?/,") + multiline_ws);
        public static Rule template_decl = Store(TEMPLATE + NoFail(Nested("<", ">"))) + ws;
        public static Rule typedef_decl = Store(comment + TYPEDEF + multiline_ws);
        public static Rule class_decl = Store(CLASS + Opt(identifier));
        public static Rule struct_decl = Store(STRUCT + Opt(identifier));
        public static Rule union_decl = Store(UNION + Opt(identifier));
        public static Rule enum_decl = Store(ENUM + Opt(identifier) + bracketed_group);
        public static Rule label = Store(identifier + ws + COLON);
        public static Rule comment_set = Store(Star(comment + multiline_ws) + multiline_ws);
        public static Rule same_line_comment = simple_ws + comment;
        public static Rule pp_directive = CharSeq("#") + NoFailSeq(ws + identifier + until_eol + eol);
        public static Rule type_decl = Store(comment_set + Opt(template_decl) + (class_decl | struct_decl | union_decl | enum_decl));
        public static Rule bracketed_group = Store(DelimitedGroup("[", Recursive(() => node), "]"));
        public static Rule paran_group = Store(DelimitedGroup("(", Recursive(() => node), ")"));
        public static Rule brace_group = Store(DelimitedGroup("{", Recursive(() => node), "}"));

        public static Rule node = Store(
            bracketed_group
            | paran_group
            | brace_group
            | type_decl
            | typedef_decl
            | literal
            | symbol
            | label
            | identifier);        
    }
}
