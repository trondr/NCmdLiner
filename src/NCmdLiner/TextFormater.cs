// File: TextFormater.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NCmdLiner
{
    public class TextFormater
    {
        public string Justify(string line, int width)
        {
            if (string.IsNullOrEmpty(line)) throw new ArgumentNullException("line");
            if (width <= 0) throw new ArgumentException("Width must be greater than 0.", "width");

            line = Straighten(line);
            int numberOfSpacesToInsert = width - line.Length;
            string[] wordArray = line.Split(' ');
            if (wordArray.Length == 1)
            {
                return line;
            }
            if (wordArray.Length == 2)
            {
                return line;
            }
            if (line.Length >= width) return line; //Do not justify line if longer than the requested width
            if (line.Length < width/2)
                return line; //Do not justify line if shorter that half of the requested width         

            //Create an array of the spaces and justify by adding spaces with highest weight on the middle of the line.
            int numberOfSpaces = wordArray.Length - 1;
            SpaceWeight[] spaceArray = new SpaceWeight[numberOfSpaces];
            InitSpaceArray(spaceArray);
            for (int i = 0; i < numberOfSpacesToInsert; i++)
            {
                AddSpace(spaceArray);
            }

            //Rebuild the line with the justified spaces.
            StringBuilder justifiedLine = new StringBuilder();
            for (int i = 0; i < wordArray.Length; i++)
            {
                justifiedLine.Append(wordArray[i]);
                if (i < wordArray.Length - 1)
                {
                    justifiedLine.Append(spaceArray[i].Spaces);
                }
            }
            return justifiedLine.ToString();
        }

        private void AddSpace(SpaceWeight[] spaceArray)
        {
            if (spaceArray.Length == 1)
            {
                spaceArray[0].Spaces += " ";
                return;
            }
            //Find space with highest weight (i.e closer to the center and not allready expanded)
            double maxWeight = 0;
            int maxWeigthIndex = spaceArray.Length - 1;
            for (int i = spaceArray.Length - 1; i >= 0; i--)
            {
                if (spaceArray[i].Weight > maxWeight)
                {
                    maxWeight = spaceArray[i].Weight;
                    maxWeigthIndex = i;
                }
            }
            spaceArray[maxWeigthIndex].Spaces += " ";
        }

        private void InitSpaceArray(SpaceWeight[] spaceArray)
        {
            for (int i = 0; i < spaceArray.Length; i++)
            {
                spaceArray[i] = new SpaceWeight {Spaces = " ", DistanceFromEnd = Math.Min(i, spaceArray.Length - 1 - i)};
            }
        }

        private class SpaceWeight
        {
            public string Spaces { get; set; }
            public int DistanceFromEnd { get; set; }

            public double Weight
            {
                get { return (double) DistanceFromEnd + 10/(double) Spaces.Length; }
            }
        }

        public string Straighten(string line)
        {
            if (line == null) throw new ArgumentNullException("line");
            return Regex.Replace(line.Trim(), @"\s+", " ", RegexOptions.Singleline);
            //Replace any double spaces, tabs, new line characters with a single space.
        }

        public List<string> BreakIntoLines(string description, int width)
        {
            List<string> lines = new List<string>();
            description = Straighten(description);
            string[] wordArray = description.Split(' ');
            StringBuilder line = new StringBuilder();
            line.Length = 0;
            for (int i = 0; i < wordArray.Length; i++)
            {
                string word = wordArray[i];
                if (line.Length + 1 + word.Length < width)
                {
                    //It is room for the word on the line, append it
                    line.Append(word + " ");
                }
                else
                {
                    //no room for the word on the line, save line, start a new one and and the word to the new one
                    if (line.Length > 0) lines.Add(line.ToString().TrimEnd());
                    line.Length = 0;
                    line.Append(word + " ");
                }
                if (i == wordArray.Length - 1)
                {
                    lines.Add(line.ToString().TrimEnd());
                }
            }
            return lines;
        }
    }
}