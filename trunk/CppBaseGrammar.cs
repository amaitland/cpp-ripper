using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CppRipper
{
    /// <summary>
    /// A grammar for some of the basic syntactic elements for the C++ language
    /// Inspired by:
    /// * http://www.lysator.liu.se/c/ANSI-C-grammar-y.html
    /// * http://cpp.comsci.us/etymology/literals.html   
    /// </summary>
    public class CppBaseGrammar
        : BaseGrammar
    {
        #region identifiers
        public static Rule digit = CharRange('0', '9');
        public static Rule lower_case_letter = CharRange('a', 'z');
        public static Rule upper_case_letter = CharRange('A', 'Z');
        public static Rule letter = lower_case_letter | upper_case_letter;
        public static Rule ident_first_char = CharSet("_") | letter;
        public static Rule ident_next_char = ident_first_char | digit;
        public static Rule identifier_extension = CharSeq("::") + Recursive(() => identifier);
        public static Rule identifier = Store(ident_first_char + Star(ident_next_char) + Star(identifier_extension)) + multiline_ws;
        #endregion

        #region numbers
        public static Rule octal_digit = CharRange('0', '7');
        public static Rule nonzero_digit = CharRange('1', '9');
        public static Rule hex_digit = digit | CharRange('a', 'f') | CharRange('A', 'F');
        public static Rule sign = CharSet("+-");
        #endregion numbers

        #region whitespace
        public static Rule tab = CharSeq("\t");
        public static Rule space = CharSeq(" ");
        public static Rule simple_ws = Star(tab | space);
        public static Rule eol = Opt(CharSeq("\r")) + CharSeq("\n");
        public static Rule ext_line = CharSeq("\\") + Star(simple_ws) + eol;
        public static Rule multiline_ws = Star(simple_ws | eol);
        public static Rule until_eol = Star(ext_line | AnythingBut(eol));
        public static Rule line_comment_content = until_eol;
        public static Rule line_comment = CharSeq("//") + NoFailSeq(line_comment_content + eol);
        public static Rule full_comment_content = Until(CharSeq("*/"));
        public static Rule full_comment = CharSeq("/*") + NoFailSeq(full_comment_content + CharSeq("*/"));
        public static Rule comment = Store(line_comment | full_comment);
        public static Rule ws = Star(multiline_ws | comment);
        #endregion

        #region keyword rules
        public static Rule COND = Word("?");
        public static Rule DOT = Word(".");
        public static Rule COLON = Word(":");
        public static Rule AMP = Word("&");
        public static Rule PLUS = Word("+");
        public static Rule MINUS = Word("-");
        public static Rule STAR = Word("*");
        public static Rule SLASH = Word("/");
        public static Rule MOD = Word("%");
        public static Rule NOT = Word("!");
        public static Rule TILDE = Word("~");
        public static Rule CARET = Word("^");
        public static Rule PIPE = Word("|");
        public static Rule EQ = Word("=");
        public static Rule COMMA = Word(",");
        public static Rule SIZEOF = Word("sizeof");
        public static Rule PTR_OP = Word("->");
        public static Rule INC_OP = Word("++");
        public static Rule DEC_OP = Word("--");
        public static Rule LEFT_OP = Word("<<");
        public static Rule RIGHT_OP = Word(">>");
        public static Rule LT_OP = Word("<");
        public static Rule GT_OP = Word(">");
        public static Rule LE_OP = Word("<=");
        public static Rule GE_OP = Word(">=");
        public static Rule EQ_OP = Word("==");
        public static Rule NE_OP = Word("!=");
        public static Rule AND_OP = Word("&&");
        public static Rule OR_OP = Word("||");
        public static Rule MUL_ASSIGN = Word("*=");
        public static Rule DIV_ASSIGN = Word("/=");
        public static Rule MOD_ASSIGN = Word("%=");
        public static Rule ADD_ASSIGN = Word("+=");
        public static Rule SUB_ASSIGN = Word("-=");
        public static Rule LEFT_ASSIGN = Word("<<=");
        public static Rule RIGHT_ASSIGN = Word(">>=");
        public static Rule AND_ASSIGN = Word("&=");
        public static Rule XOR_ASSIGN = Word("^=");
        public static Rule OR_ASSIGN = Word("|=");
        public static Rule TYPEDEF = Word("typedef");
        public static Rule EXTERN = Word("extern");
        public static Rule STATIC = Word("static");
        public static Rule AUTO = Word("auto");
        public static Rule REGISTER = Word("register");
        public static Rule CHAR = Word("char");
        public static Rule SHORT = Word("short");
        public static Rule INT = Word("int");
        public static Rule LONG = Word("long");
        public static Rule SIGNED = Word("signed");
        public static Rule UNSIGNED = Word("unsigned");
        public static Rule FLOAT = Word("float");
        public static Rule DOUBLE = Word("double");
        public static Rule CONST = Word("const");
        public static Rule VOLATILE = Word("volatile");
        public static Rule VOID = Word("void");
        public static Rule STRUCT = Word("struct");
        public static Rule UNION = Word("union");
        public static Rule ENUM = Word("enum");
        public static Rule ELLIPSIS = Word("...");
        public static Rule CASE = Word("case");
        public static Rule DEFAULT = Word("default");
        public static Rule IF = Word("if");
        public static Rule ELSE = Word("else");
        public static Rule SWITCH = Word("switch");
        public static Rule WHILE = Word("while");
        public static Rule DO = Word("do");
        public static Rule FOR = Word("for");
        public static Rule GOTO = Word("goto");
        public static Rule CONTINUE = Word("continue");
        public static Rule BREAK = Word("break");
        public static Rule RETURN = Word("return");
        public static Rule CLASS = Word("class");
        public static Rule TYPENAME = Word("typename");
        public static Rule TYPEID = Word("typeid");
        public static Rule TEMPLATE = Word("template");
        public static Rule PUBLIC = Word("public");
        public static Rule PROTECTED = Word("protected");
        public static Rule PRIVATE = Word("private");
        public static Rule VIRTUAL = Word("virtual");
        public static Rule OPERATOR = Word("operator");
        public static Rule USING = Word("using");
        #endregion

        #region literals
        public static Rule dot
            = CharSeq(".");
        public static Rule dbl_quote
            = CharSeq("\"");
        public static Rule quote
            = CharSeq("\'");
        public static Rule simple_escape
            = CharSeq("\\") + CharSet("abfnrtv'\"?\\");
        public static Rule octal_escape
            = CharSeq("\\") + octal_digit + Opt(octal_digit + Opt(octal_digit));
        public static Rule hex_escape
            = CharSeq("\\x") + Star(hex_digit);
        public static Rule escape_sequence
            = simple_escape
            | octal_escape
            | hex_escape;
        public static Rule c_char
            = escape_sequence | Not(quote) + Anything();
        public static Rule s_char
            = escape_sequence | Not(dbl_quote) + Anything();
        public static Rule long_suffix
            = CharSet("Ll");
        public static Rule unsigned_suffix
            = CharSet("Uu");
        public static Rule digit_sequence
            = Plus(digit);
        public static Rule exponent
            = Opt(sign) + digit_sequence;
        public static Rule exponent_prefix
            = CharSet("Ee");
        public static Rule exponent_part
            = exponent_prefix + exponent;
        public static Rule float_suffix
            = CharSet("LlFf");
        public static Rule simple_float
            = CharSeq(".") + digit_sequence
            | digit_sequence + dot + Opt(digit_sequence);
        public static Rule exponential_float
            = digit_sequence + exponent_part
            | simple_float + exponent_part;
        public static Rule unsigned_float
            = simple_float
            | exponential_float;
        public static Rule hex_prefix
            = CharSeq("0X") | CharSeq("0x");
        public static Rule hex_literal
            = hex_prefix + Plus(hex_digit);
        public static Rule octal_literal
            = CharSeq("0") + Star(octal_digit);
        public static Rule decimal_literal
            = nonzero_digit + Star(digit);
        public static Rule unsigned_literal
            = decimal_literal
            | octal_literal
            | hex_literal;
        public static Rule integer_suffix
            = long_suffix
            | unsigned_suffix
            | unsigned_suffix + long_suffix
            | long_suffix + unsigned_suffix;
        public static Rule int_literal
            = unsigned_literal + Not(dot) + Opt(integer_suffix);
        public static Rule float_literal
            = unsigned_float + Opt(float_suffix);
        public static Rule char_literal
            = Opt(CharSeq("L")) + quote + Star(c_char) + quote;
        public static Rule string_literal
            = Opt(CharSeq("L")) + dbl_quote + Star(s_char) + dbl_quote;
        public static Rule boolean_literal
            = Word("true") | Word("false");
        public static Rule literal
            = (int_literal
            | char_literal
            | float_literal
            | string_literal
            | boolean_literal)
            + NoFail(Not(ident_next_char)) + ws;
        #endregion

        #region pre-processor directives
        public static Rule pragma = Word("#") + Word("pragma") + until_eol;
        public static Rule included_file = string_literal | CharSeq("<") + Star(Not(CharSeq(">")) + Anything()) + CharSeq(">");
        public static Rule include = Word("#") + Word("include") + included_file;
        public static Rule ifdef_macro = Word("#") + Word("if") + until_eol + eol;
        public static Rule endif_macro = Word("#") + Word("endif") + until_eol + eol;
        public static Rule elif_macro = Word("#") + Word("elif") + until_eol + eol;
        public static Rule else_macro = Word("#") + Word("else") + until_eol + eol;
        #endregion

        #region symbols
        public static Rule semicolon = CharSeq(";");
        public static Rule eos = Word(";");
        #endregion

        #region rule generating functions

        /// <summary>
        /// Creates a rule that matches the rule R multiple times, delimited by commas. 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Rule CommaList(Rule r)
        {
            return r + Star(COMMA + r);
        }

        /// <summary>
        /// Creates a rule that matches a pair of rules, consuming all nested pairs within
        /// as well. 
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Rule Nested(Rule begin, Rule end)
        {
            RecursiveRule recursive = new RecursiveRule(() => { return Nested(begin, end); });
            return begin + NoFailSeq(Star(recursive | Not(end) + Not(begin) + Anything()) + end);
        }

        /// <summary>
        /// Creates a rule that matches a pair of character sequences, consuming all nested pairs 
        /// within as well. For example: "Nested("{", "}"));
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Rule Nested(string begin, string end)
        {
            return Nested(CharSeq(begin), CharSeq(end));
        }

        /// <summary>
        /// Creates a SkipRule that matches a sequence of characters, like CharSeq. It also assures that no other letters follows 
        /// and will also eats whitespace. No node is created.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Rule Word(string s) 
        {
            return CharSeq(s) + Not(ident_next_char) + ws;
        }
        #endregion

        /// <summary>
        /// Constructor: initializes the public static Rule fields.
        /// </summary>
        public CppBaseGrammar()
        {
            InitializeRules<CppBaseGrammar>();
        }
    }
}
