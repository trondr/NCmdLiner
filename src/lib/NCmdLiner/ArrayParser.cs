// File: ArrayParser.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using LanguageExt.Common;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    /// <summary>
    /// 
    /// </summary>
    public class ArrayParser : IArrayParser
    {
        #region Implementation of IArrayParser
        /// <summary>
        /// Parse array of values
        /// </summary>
        /// <param name="value">"["","","",""]" or "{"","","",""}" or "";"";"";"";"" or ""+""+""+"" or '';'';'';''</param>
        /// <returns></returns>
        public Result<string[]> Parse(string value)
        {
            value = TrimArrayString(value);
            if (string.IsNullOrEmpty(value))
            {
                return new Result<string[]>(new string[] {});
            }

            GetDelimiterAndQuote(value, out char delimiter, out var quote);
            var arrayResult = String2Array(value, delimiter, quote);
            return arrayResult;
        }

        #endregion

        #region Private methods

        /// <summary>  String 2 array. </summary>
        ///
        /// <remarks>  Trond, 05.10.2012. </remarks>
        ///
        /// <param name="value">      The value. </param>
        /// <param name="delimiter">  The delimiter. </param>
        /// <param name="quote">      The quote. </param>
        ///
        /// <returns>  . </returns>
        private static Result<string[]> String2Array(string value, char delimiter, char quote)
        {
            var resultList = new StringCollection();
            if (!value.Contains(quote.ToString()))
            {
                //Quotes is not being used, just split on delimiter
                return new Result<string[]>(value.Split(delimiter));
            }

            //Quotes is being used, use regular expression to parse the csv format.
            var delimiterString = Regex.Escape(delimiter.ToString());
            var quoteString = Regex.Escape(quote.ToString());
            var pattern = new StringBuilder();
            pattern.Append("([" + quoteString + "]{0,1})"); //Match 0 or 1 starting quote character
            pattern.Append("([^" + quoteString + "]{0,})"); //Match everything in between quote characters
            pattern.Append("\\1"); //Match 0 or 1 quote character if that was found in the first match
            pattern.Append("(" + delimiterString + "|$)"); //Match 1 delimiter or end of line

            //string pattern = string.Format("{1}([^{1}]+){1}{0}", delimiter, quote) + "{0,1}";
            var regexObj = new Regex(pattern.ToString());
            var matchResult = regexObj.Match(value);
            var expectedNexMatchIndex = 0;
            while (matchResult.Success)
            {
                if (matchResult.Index != expectedNexMatchIndex)
                {
                    return new Result<string[]>(new InvalidArrayParseException("Array format seems to be corrupt: " + value));
                }
                expectedNexMatchIndex = matchResult.Index + matchResult.Length;
#if DEBUG
                Console.WriteLine("match : " + matchResult.Value);
                Console.WriteLine("index : " + matchResult.Index);
                Console.WriteLine("length: " + matchResult.Length);
#endif
                if (matchResult.Index == value.Length && value[value.Length - 1] != delimiter)
                {
                    //Skip empty match at the end of value string
                    matchResult = matchResult.NextMatch();
                    continue;
                }
                resultList.Add(matchResult.Groups[2].Value);
                matchResult = matchResult.NextMatch();
            }
            var array = new string[resultList.Count];
            resultList.CopyTo(array, 0);
            return new Result<string[]>(array);
        }

        private static string TrimArrayString(string value)
        {
            if (value.Length == 0) return value;
            if ((value[0] == '{') && (value[value.Length - 1] == '}'))
            {
                return value.Trim('{', '}');
            }

            if ((value[0] == '[') && (value[value.Length - 1] == ']'))
            {
                return value.Trim('[', ']');
            }
            return value;
        }

        private static void GetDelimiterAndQuote(string value, out char delimiter, out char quote)
        {
            var supportedDelimiters = new[] {';', '+', '|'};
            var supportedQuotes = new[] {'\''};
            foreach (var q in supportedQuotes)
            {
                foreach (var d in supportedDelimiters)
                {
                    var compare = string.Format("{0}{1}{0}", q, d); //Example: ";"
                    if (value.Contains(compare))
                    {
                        delimiter = d;
                        quote = q;
                        return;
                    }
                }
            }
            foreach (var d in supportedDelimiters)
            {
                var compare = $"{d}"; //Example: ;
                if (value.Contains(compare))
                {
                    delimiter = d;
                    quote = '\'';
                    return;
                }
            }
            //Default values
            delimiter = ';';
            quote = '\'';
        }

        #endregion
    }
}