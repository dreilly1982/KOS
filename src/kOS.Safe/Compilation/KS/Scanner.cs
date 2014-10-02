// Generated by TinyPG v1.3 available at www.codeproject.com

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace kOS.Safe.Compilation.KS
{
    #region Scanner

    public partial class Scanner
    {
        public string Input;
        public int StartPos = 0;
        public int EndPos = 0;
        public string CurrentFile;
        public int CurrentLine;
        public int CurrentColumn;
        public int CurrentPosition;
        public List<Token> Skipped; // tokens that were skipped
        public Dictionary<TokenType, Regex> Patterns;

        private Token LookAheadToken;
        private List<TokenType> Tokens;
        private List<TokenType> SkipList; // tokens to be skipped
        private readonly TokenType FileAndLine;

        public Scanner()
        {
            Regex regex;
            Patterns = new Dictionary<TokenType, Regex>();
            Tokens = new List<TokenType>();
            LookAheadToken = null;
            Skipped = new List<Token>();

            SkipList = new List<TokenType>();
            SkipList.Add(TokenType.WHITESPACE);
            SkipList.Add(TokenType.COMMENTLINE);

            regex = new Regex(@"(\+|-)");
            Patterns.Add(TokenType.PLUSMINUS, regex);
            Tokens.Add(TokenType.PLUSMINUS);

            regex = new Regex(@"\*");
            Patterns.Add(TokenType.MULT, regex);
            Tokens.Add(TokenType.MULT);

            regex = new Regex(@"/");
            Patterns.Add(TokenType.DIV, regex);
            Tokens.Add(TokenType.DIV);

            regex = new Regex(@"\^");
            Patterns.Add(TokenType.POWER, regex);
            Tokens.Add(TokenType.POWER);

            regex = new Regex(@"e");
            Patterns.Add(TokenType.E, regex);
            Tokens.Add(TokenType.E);

            regex = new Regex(@"not");
            Patterns.Add(TokenType.NOT, regex);
            Tokens.Add(TokenType.NOT);

            regex = new Regex(@"and");
            Patterns.Add(TokenType.AND, regex);
            Tokens.Add(TokenType.AND);

            regex = new Regex(@"or");
            Patterns.Add(TokenType.OR, regex);
            Tokens.Add(TokenType.OR);

            regex = new Regex(@"true|false");
            Patterns.Add(TokenType.TRUEFALSE, regex);
            Tokens.Add(TokenType.TRUEFALSE);

            regex = new Regex(@"<>|>=|<=|=|>|<");
            Patterns.Add(TokenType.COMPARATOR, regex);
            Tokens.Add(TokenType.COMPARATOR);

            regex = new Regex(@"set");
            Patterns.Add(TokenType.SET, regex);
            Tokens.Add(TokenType.SET);

            regex = new Regex(@"to");
            Patterns.Add(TokenType.TO, regex);
            Tokens.Add(TokenType.TO);

            regex = new Regex(@"if");
            Patterns.Add(TokenType.IF, regex);
            Tokens.Add(TokenType.IF);

            regex = new Regex(@"else");
            Patterns.Add(TokenType.ELSE, regex);
            Tokens.Add(TokenType.ELSE);

            regex = new Regex(@"until");
            Patterns.Add(TokenType.UNTIL, regex);
            Tokens.Add(TokenType.UNTIL);

            regex = new Regex(@"lock");
            Patterns.Add(TokenType.LOCK, regex);
            Tokens.Add(TokenType.LOCK);

            regex = new Regex(@"unlock");
            Patterns.Add(TokenType.UNLOCK, regex);
            Tokens.Add(TokenType.UNLOCK);

            regex = new Regex(@"print");
            Patterns.Add(TokenType.PRINT, regex);
            Tokens.Add(TokenType.PRINT);

            regex = new Regex(@"at");
            Patterns.Add(TokenType.AT, regex);
            Tokens.Add(TokenType.AT);

            regex = new Regex(@"on");
            Patterns.Add(TokenType.ON, regex);
            Tokens.Add(TokenType.ON);

            regex = new Regex(@"toggle");
            Patterns.Add(TokenType.TOGGLE, regex);
            Tokens.Add(TokenType.TOGGLE);

            regex = new Regex(@"wait");
            Patterns.Add(TokenType.WAIT, regex);
            Tokens.Add(TokenType.WAIT);

            regex = new Regex(@"when");
            Patterns.Add(TokenType.WHEN, regex);
            Tokens.Add(TokenType.WHEN);

            regex = new Regex(@"then");
            Patterns.Add(TokenType.THEN, regex);
            Tokens.Add(TokenType.THEN);

            regex = new Regex(@"off");
            Patterns.Add(TokenType.OFF, regex);
            Tokens.Add(TokenType.OFF);

            regex = new Regex(@"stage");
            Patterns.Add(TokenType.STAGE, regex);
            Tokens.Add(TokenType.STAGE);

            regex = new Regex(@"clearscreen");
            Patterns.Add(TokenType.CLEARSCREEN, regex);
            Tokens.Add(TokenType.CLEARSCREEN);

            regex = new Regex(@"add");
            Patterns.Add(TokenType.ADD, regex);
            Tokens.Add(TokenType.ADD);

            regex = new Regex(@"remove");
            Patterns.Add(TokenType.REMOVE, regex);
            Tokens.Add(TokenType.REMOVE);

            regex = new Regex(@"log");
            Patterns.Add(TokenType.LOG, regex);
            Tokens.Add(TokenType.LOG);

            regex = new Regex(@"break");
            Patterns.Add(TokenType.BREAK, regex);
            Tokens.Add(TokenType.BREAK);

            regex = new Regex(@"preserve");
            Patterns.Add(TokenType.PRESERVE, regex);
            Tokens.Add(TokenType.PRESERVE);

            regex = new Regex(@"declare");
            Patterns.Add(TokenType.DECLARE, regex);
            Tokens.Add(TokenType.DECLARE);

            regex = new Regex(@"parameter");
            Patterns.Add(TokenType.PARAMETER, regex);
            Tokens.Add(TokenType.PARAMETER);

            regex = new Regex(@"switch");
            Patterns.Add(TokenType.SWITCH, regex);
            Tokens.Add(TokenType.SWITCH);

            regex = new Regex(@"copy");
            Patterns.Add(TokenType.COPY, regex);
            Tokens.Add(TokenType.COPY);

            regex = new Regex(@"from");
            Patterns.Add(TokenType.FROM, regex);
            Tokens.Add(TokenType.FROM);

            regex = new Regex(@"rename");
            Patterns.Add(TokenType.RENAME, regex);
            Tokens.Add(TokenType.RENAME);

            regex = new Regex(@"volume");
            Patterns.Add(TokenType.VOLUME, regex);
            Tokens.Add(TokenType.VOLUME);

            regex = new Regex(@"file");
            Patterns.Add(TokenType.FILE, regex);
            Tokens.Add(TokenType.FILE);

            regex = new Regex(@"delete");
            Patterns.Add(TokenType.DELETE, regex);
            Tokens.Add(TokenType.DELETE);

            regex = new Regex(@"edit");
            Patterns.Add(TokenType.EDIT, regex);
            Tokens.Add(TokenType.EDIT);

            regex = new Regex(@"run");
            Patterns.Add(TokenType.RUN, regex);
            Tokens.Add(TokenType.RUN);

            regex = new Regex(@"compile");
            Patterns.Add(TokenType.COMPILE, regex);
            Tokens.Add(TokenType.COMPILE);

            regex = new Regex(@"list");
            Patterns.Add(TokenType.LIST, regex);
            Tokens.Add(TokenType.LIST);

            regex = new Regex(@"reboot");
            Patterns.Add(TokenType.REBOOT, regex);
            Tokens.Add(TokenType.REBOOT);

            regex = new Regex(@"shutdown");
            Patterns.Add(TokenType.SHUTDOWN, regex);
            Tokens.Add(TokenType.SHUTDOWN);

            regex = new Regex(@"for");
            Patterns.Add(TokenType.FOR, regex);
            Tokens.Add(TokenType.FOR);

            regex = new Regex(@"unset");
            Patterns.Add(TokenType.UNSET, regex);
            Tokens.Add(TokenType.UNSET);

            regex = new Regex(@"batch");
            Patterns.Add(TokenType.BATCH, regex);
            Tokens.Add(TokenType.BATCH);

            regex = new Regex(@"deploy");
            Patterns.Add(TokenType.DEPLOY, regex);
            Tokens.Add(TokenType.DEPLOY);

            regex = new Regex(@"\(");
            Patterns.Add(TokenType.BRACKETOPEN, regex);
            Tokens.Add(TokenType.BRACKETOPEN);

            regex = new Regex(@"\)");
            Patterns.Add(TokenType.BRACKETCLOSE, regex);
            Tokens.Add(TokenType.BRACKETCLOSE);

            regex = new Regex(@"\{");
            Patterns.Add(TokenType.CURLYOPEN, regex);
            Tokens.Add(TokenType.CURLYOPEN);

            regex = new Regex(@"\}");
            Patterns.Add(TokenType.CURLYCLOSE, regex);
            Tokens.Add(TokenType.CURLYCLOSE);

            regex = new Regex(@"\[");
            Patterns.Add(TokenType.SQUAREOPEN, regex);
            Tokens.Add(TokenType.SQUAREOPEN);

            regex = new Regex(@"\]");
            Patterns.Add(TokenType.SQUARECLOSE, regex);
            Tokens.Add(TokenType.SQUARECLOSE);

            regex = new Regex(@",");
            Patterns.Add(TokenType.COMMA, regex);
            Tokens.Add(TokenType.COMMA);

            regex = new Regex(@":");
            Patterns.Add(TokenType.COLON, regex);
            Tokens.Add(TokenType.COLON);

            regex = new Regex(@"in");
            Patterns.Add(TokenType.IN, regex);
            Tokens.Add(TokenType.IN);

            regex = new Regex(@"#");
            Patterns.Add(TokenType.ARRAYINDEX, regex);
            Tokens.Add(TokenType.ARRAYINDEX);

            regex = new Regex(@"all");
            Patterns.Add(TokenType.ALL, regex);
            Tokens.Add(TokenType.ALL);

            regex = new Regex(@"[a-z_][a-z0-9_]*");
            Patterns.Add(TokenType.IDENTIFIER, regex);
            Tokens.Add(TokenType.IDENTIFIER);

            regex = new Regex(@"[0-9]+");
            Patterns.Add(TokenType.INTEGER, regex);
            Tokens.Add(TokenType.INTEGER);

            regex = new Regex(@"[0-9]*\.[0-9]+");
            Patterns.Add(TokenType.DOUBLE, regex);
            Tokens.Add(TokenType.DOUBLE);

            regex = new Regex(@"@?\""(\""\""|[^\""])*\""");
            Patterns.Add(TokenType.STRING, regex);
            Tokens.Add(TokenType.STRING);

            regex = new Regex(@"\.");
            Patterns.Add(TokenType.EOI, regex);
            Tokens.Add(TokenType.EOI);

            regex = new Regex(@"^$");
            Patterns.Add(TokenType.EOF, regex);
            Tokens.Add(TokenType.EOF);

            regex = new Regex(@"\s+");
            Patterns.Add(TokenType.WHITESPACE, regex);
            Tokens.Add(TokenType.WHITESPACE);

            regex = new Regex(@"//[^\n]*\n?");
            Patterns.Add(TokenType.COMMENTLINE, regex);
            Tokens.Add(TokenType.COMMENTLINE);


        }

        public void Init(string input)
        {
            Init(input, "");
        }

        public void Init(string input, string fileName)
        {
            this.Input = input;
            StartPos = 0;
            EndPos = 0;
            CurrentFile = fileName;
            CurrentLine = 1;
            CurrentColumn = 1;
            CurrentPosition = 0;
            LookAheadToken = null;
        }

        public Token GetToken(TokenType type)
        {
            Token t = new Token(this.StartPos, this.EndPos);
            t.Type = type;
            return t;
        }

         /// <summary>
        /// executes a lookahead of the next token
        /// and will advance the scan on the input string
        /// </summary>
        /// <returns></returns>
        public Token Scan(params TokenType[] expectedtokens)
        {
            Token tok = LookAhead(expectedtokens); // temporarely retrieve the lookahead
            LookAheadToken = null; // reset lookahead token, so scanning will continue
            StartPos = tok.EndPos;
            EndPos = tok.EndPos; // set the tokenizer to the new scan position
            CurrentLine = tok.Line + (tok.Text.Length - tok.Text.Replace("\n", "").Length);
            CurrentFile = tok.File;
            return tok;
        }

        /// <summary>
        /// returns token with longest best match
        /// </summary>
        /// <returns></returns>
        public Token LookAhead(params TokenType[] expectedtokens)
        {
            int i;
            int startpos = StartPos;
            int endpos = EndPos;
            int currentline = CurrentLine;
            string currentFile = CurrentFile;
            Token tok = null;
            List<TokenType> scantokens;


            // this prevents double scanning and matching
            // increased performance
            if (LookAheadToken != null 
                && LookAheadToken.Type != TokenType._UNDETERMINED_ 
                && LookAheadToken.Type != TokenType._NONE_) return LookAheadToken;

            // if no scantokens specified, then scan for all of them (= backward compatible)
            if (expectedtokens.Length == 0)
                scantokens = Tokens;
            else
            {
                scantokens = new List<TokenType>(expectedtokens);
                scantokens.AddRange(SkipList);
            }

            do
            {

                int len = -1;
                TokenType index = (TokenType)int.MaxValue;
                string input = Input.Substring(startpos);

                tok = new Token(startpos, endpos);

                for (i = 0; i < scantokens.Count; i++)
                {
                    Regex r = Patterns[scantokens[i]];
                    Match m = r.Match(input);
                    if (m.Success && m.Index == 0 && ((m.Length > len) || (scantokens[i] < index && m.Length == len )))
                    {
                        len = m.Length;
                        index = scantokens[i];  
                    }
                }

                if (index >= 0 && len >= 0)
                {
                    tok.EndPos = startpos + len;
                    tok.Text = Input.Substring(tok.StartPos, len);
                    tok.Type = index;
                }
                else if (tok.StartPos == tok.EndPos)
                {
                    if (tok.StartPos < Input.Length)
                        tok.Text = Input.Substring(tok.StartPos, 1);
                    else
                        tok.Text = "EOF";
                }

                // Update the line and column count for error reporting.
                tok.File = currentFile;
                tok.Line = currentline;
                if (tok.StartPos < Input.Length)
                    tok.Column = tok.StartPos - Input.LastIndexOf('\n', tok.StartPos);

                if (SkipList.Contains(tok.Type))
                {
                    startpos = tok.EndPos;
                    endpos = tok.EndPos;
                    currentline = tok.Line + (tok.Text.Length - tok.Text.Replace("\n", "").Length);
                    currentFile = tok.File;
                    Skipped.Add(tok);
                }
                else
                {
                    // only assign to non-skipped tokens
                    tok.Skipped = Skipped; // assign prior skips to this token
                    Skipped = new List<Token>(); //reset skips
                }

                // Check to see if the parsed token wants to 
                // alter the file and line number.
                if (tok.Type == FileAndLine)
                {
                    var match = Patterns[tok.Type].Match(tok.Text);
                    var fileMatch = match.Groups["File"];
                    if (fileMatch.Success)
                        currentFile = fileMatch.Value;
                    var lineMatch = match.Groups["Line"];
                    if (lineMatch.Success)
                        currentline = int.Parse(lineMatch.Value);
                }
            }
            while (SkipList.Contains(tok.Type));

            LookAheadToken = tok;
            return tok;
        }
    }

    #endregion

    #region Token

    public enum TokenType
    {

            //Non terminal tokens:
            _NONE_  = 0,
            _UNDETERMINED_= 1,

            //Non terminal tokens:
            Start   = 2,
            instruction_block= 3,
            instruction= 4,
            set_stmt= 5,
            if_stmt = 6,
            until_stmt= 7,
            lock_stmt= 8,
            unlock_stmt= 9,
            print_stmt= 10,
            on_stmt = 11,
            toggle_stmt= 12,
            wait_stmt= 13,
            when_stmt= 14,
            onoff_stmt= 15,
            stage_stmt= 16,
            clear_stmt= 17,
            add_stmt= 18,
            remove_stmt= 19,
            log_stmt= 20,
            break_stmt= 21,
            preserve_stmt= 22,
            declare_stmt= 23,
            switch_stmt= 24,
            copy_stmt= 25,
            rename_stmt= 26,
            delete_stmt= 27,
            edit_stmt= 28,
            run_stmt= 29,
            compile_stmt= 30,
            filevol_name= 31,
            list_stmt= 32,
            reboot_stmt= 33,
            shutdown_stmt= 34,
            for_stmt= 35,
            unset_stmt= 36,
            batch_stmt= 37,
            deploy_stmt= 38,
            arglist = 39,
            expr    = 40,
            and_expr= 41,
            compare_expr= 42,
            arith_expr= 43,
            multdiv_expr= 44,
            factor  = 45,
            suffix  = 46,
            function= 47,
            array   = 48,
            atom    = 49,
            sci_number= 50,
            number  = 51,
            varidentifier= 52,

            //Terminal tokens:
            PLUSMINUS= 53,
            MULT    = 54,
            DIV     = 55,
            POWER   = 56,
            E       = 57,
            NOT     = 58,
            AND     = 59,
            OR      = 60,
            TRUEFALSE= 61,
            COMPARATOR= 62,
            SET     = 63,
            TO      = 64,
            IF      = 65,
            ELSE    = 66,
            UNTIL   = 67,
            LOCK    = 68,
            UNLOCK  = 69,
            PRINT   = 70,
            AT      = 71,
            ON      = 72,
            TOGGLE  = 73,
            WAIT    = 74,
            WHEN    = 75,
            THEN    = 76,
            OFF     = 77,
            STAGE   = 78,
            CLEARSCREEN= 79,
            ADD     = 80,
            REMOVE  = 81,
            LOG     = 82,
            BREAK   = 83,
            PRESERVE= 84,
            DECLARE = 85,
            PARAMETER= 86,
            SWITCH  = 87,
            COPY    = 88,
            FROM    = 89,
            RENAME  = 90,
            VOLUME  = 91,
            FILE    = 92,
            DELETE  = 93,
            EDIT    = 94,
            RUN     = 95,
            COMPILE = 96,
            LIST    = 97,
            REBOOT  = 98,
            SHUTDOWN= 99,
            FOR     = 100,
            UNSET   = 101,
            BATCH   = 102,
            DEPLOY  = 103,
            BRACKETOPEN= 104,
            BRACKETCLOSE= 105,
            CURLYOPEN= 106,
            CURLYCLOSE= 107,
            SQUAREOPEN= 108,
            SQUARECLOSE= 109,
            COMMA   = 110,
            COLON   = 111,
            IN      = 112,
            ARRAYINDEX= 113,
            ALL     = 114,
            IDENTIFIER= 115,
            INTEGER = 116,
            DOUBLE  = 117,
            STRING  = 118,
            EOI     = 119,
            EOF     = 120,
            WHITESPACE= 121,
            COMMENTLINE= 122
    }

    public class Token
    {
        private string file;
        private int line;
        private int column;
        private int startpos;
        private int endpos;
        private string text;
        private object value;

        // contains all prior skipped symbols
        private List<Token> skipped;

        public string File { 
            get { return file; } 
            set { file = value; }
        }

        public int Line { 
            get { return line; } 
            set { line = value; }
        }

        public int Column {
            get { return column; } 
            set { column = value; }
        }

        public int StartPos { 
            get { return startpos;} 
            set { startpos = value; }
        }

        public int Length { 
            get { return endpos - startpos;} 
        }

        public int EndPos { 
            get { return endpos;} 
            set { endpos = value; }
        }

        public string Text { 
            get { return text;} 
            set { text = value; }
        }

        public List<Token> Skipped { 
            get { return skipped;} 
            set { skipped = value; }
        }
        public object Value { 
            get { return value;} 
            set { this.value = value; }
        }

        [XmlAttribute]
        public TokenType Type;

        public Token()
            : this(0, 0)
        {
        }

        public Token(int start, int end)
        {
            Type = TokenType._UNDETERMINED_;
            startpos = start;
            endpos = end;
            Text = ""; // must initialize with empty string, may cause null reference exceptions otherwise
            Value = null;
        }

        public void UpdateRange(Token token)
        {
            if (token.StartPos < startpos) startpos = token.StartPos;
            if (token.EndPos > endpos) endpos = token.EndPos;
        }

        public override string ToString()
        {
            if (Text != null)
                return Type.ToString() + " '" + Text + "'";
            else
                return Type.ToString();
        }
    }

    #endregion
}
