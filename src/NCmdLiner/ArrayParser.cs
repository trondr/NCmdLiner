// File: ArrayParser.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    /// <summary>
    /// 
    /// </summary>
    internal class ArrayParser : IArrayParser
    {
        #region Implementation of IArrayParser
        /// <summary>
        /// Parse array of values
        /// </summary>
        /// <param name="value">"["","","",""]" or "{"","","",""}" or "";"";"";"";"" or ""+""+""+"" or '';'';'';''</param>
        /// <returns></returns>
        public string[] Parse(string value)
        {
            value = TrimArrayString(value);
            if (string.IsNullOrEmpty(value))
            {
                return new string[] {};
            }
            char delimiter;
            char quote;
            GetDelimiterAndQuote(value, out delimiter, out quote);
            string[] array = String2Array(value, delimiter, quote);
            return array;
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
        private static string[] String2Array(string value, char delimiter, char quote)
        {
            StringCollection resultList = new StringCollection();
            if (!value.Contains(quote.ToString(CultureInfo.InvariantCulture)))
            {
                //Quotes is not beeing used, just split on delimiter
                return value.Split(new[] {delimiter});
            }

            //Quotes is beeing used, use regular expression to parse the csv format.
            string delimiterString = Regex.Escape(delimiter.ToString(CultureInfo.InvariantCulture));
            string quoteString = Regex.Escape(quote.ToString(CultureInfo.InvariantCulture));
            StringBuilder pattern = new StringBuilder();
            pattern.Append("([" + quoteString + "]{0,1})"); //Match 0 or 1 starting quote character
            pattern.Append("([^" + quoteString + "]{0,})"); //Match everything in between quote charchters
            pattern.Append("\\1"); //Match 0 or 1 quote charchter if that was found in the first match
            pattern.Append("(" + delimiterString + "|$)"); //Match 1 delimter or end of line

            //string pattern = string.Format("{1}([^{1}]+){1}{0}", delimiter, quote) + "{0,1}";
            Regex regexObj = new Regex(pattern.ToString());
            Match matchResult = regexObj.Match(value);
            int expectedNexMatchIndex = 0;
            while (matchResult.Success)
            {
                if (matchResult.Index != expectedNexMatchIndex)
                {
                    throw new InvalidArrayParseException("Array format seems to be corrupt: " + value);
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
            string[] array = new string[resultList.Count];
            resultList.CopyTo(array, 0);
            return array;
        }

        private static string TrimArrayString(string value)
        {
            if (value.Length == 0) return value;
            if ((value[0] == '{') && (value[value.Length - 1] == '}'))
            {
                return value.Trim(new[] {'{', '}'});
            }

            if ((value[0] == '[') && (value[value.Length - 1] == ']'))
            {
                return value.Trim(new[] {'[', ']'});
            }
            return value;
        }

        private static void GetDelimiterAndQuote(string value, out char delimiter, out char quote)
        {
            char[] supportedDelimiters = new[] {';', '+', '|'};
            char[] supportedQuotes = new[] {'\''};
            foreach (char q in supportedQuotes)
            {
                foreach (char d in supportedDelimiters)
                {
                    string compare = string.Format("{0}{1}{0}", q, d); //Example: ";"
                    if (value.Contains(compare))
                    {
                        delimiter = d;
                        quote = q;
                        return;
                    }
                }
            }
            foreach (char d in supportedDelimiters)
            {
                string compare = string.Format("{0}", d); //Example: ;
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