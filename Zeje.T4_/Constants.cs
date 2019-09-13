﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Zeje.T4_
{
    public class Constants
    {
        /// <summary>
        /// 
        /// </summary>
        public static Regex rxCleanUp = new Regex(@"[^\w\d_]", RegexOptions.Compiled);
        /// <summary>
        /// 
        /// </summary>
        public static string[] cs_keywords = 
        { 
            "abstract",
            "event",
            "new",
            "struct",
            "as",
            "explicit",
            "null", 
            "switch",
            "base",
            "extern",
            "object",
            "this",
            "bool",
            "false",
            "operator",
            "throw",
            "break",
            "finally",
            "out",
            "true",
            "byte",
            "fixed",
            "override",
            "try",
            "case",
            "float",
            "params",
            "typeof",
            "catch",
            "for",
            "private", 
            "uint", 
            "char", 
            "foreach", 
            "protected", 
            "ulong", 
            "checked", 
            "goto", 
            "public", 
            "unchecked", 
            "class", 
            "if", 
            "readonly", 
            "unsafe", 
            "const", 
            "implicit", 
            "ref", 
            "ushort", 
            "continue", 
            "in", 
            "return", 
            "using", 
            "decimal", 
            "int", 
            "sbyte", 
            "virtual", 
            "default", 
            "interface", 
            "sealed", 
            "volatile", 
            "delegate", 
            "internal", 
            "short", 
            "void", 
            "do", 
            "is", 
            "sizeof", 
            "while", 
            "double", 
            "lock", 
            "stackalloc", 
            "else", 
            "long", 
            "static", 
            "enum", 
            "namespace", 
            "string" 
        };
    }
}
