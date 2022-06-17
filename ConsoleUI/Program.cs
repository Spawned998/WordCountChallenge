using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        private static string[] tests = new string[]
        {
            @"The test of the 
            best way to handle

multiple lines,   extra spaces and more.",
            @"Using the starter app, create code that will 
loop through the strings and identify the total 
character count, the number of characters
excluding whitespace (including line returns), and the
number of words in the string. Finally, list each word, ensuring it
is a valid word."
        };

        /* 
            First string (tests[0]) Values:
            Total Words: 14
            Total Characters: 89
            Character count (minus line returns and spaces): 60
            Most used word: the (2 times)
            Most used character: e (10 times)

            Second string (tests[1]) Values:
            Total Words: 45
            Total Characters: 276
            Character count (minus line returns and spaces): 230
            Most used word: the (6 times)
            Most used character: t (24 times)
        */

        static void Main(string[] args)
        {
            List<string> wordsList = new List<string>();

            int totalCharacterCount = 0;
            int numCharsExcludingWhiteSpaceLineReturns = 0;

            string mostUsedWord = "";
            int mostUsedWordCount = 0;

            char mostUsedChar;
            int mostUsedCharCount = 0;

            int stringLength = 0;

            char[] ignoreForTotalChars = {'\n'};
            char[] ignoreForNumberOfChars = { ' ', '\n', '\r' };
            char[] ignoreForNumberOfWords = { ' ', '.', ',', '\n', '\r' };

            string stringToTest = tests[1];

            stringLength = stringToTest.Length;

            for (int i = 0; i < stringLength; i++)
            {
                if (IsValidChar(stringToTest[i], ignoreForTotalChars))
                {
                    totalCharacterCount++;
                }

                if (IsValidChar(stringToTest[i], ignoreForNumberOfChars))
                {
                    numCharsExcludingWhiteSpaceLineReturns++;
                }
            }

            Console.WriteLine($"Total character count: {totalCharacterCount}");
            Console.WriteLine($"Total characters excluding whitespace & line returns: {numCharsExcludingWhiteSpaceLineReturns}");

            wordsList = WordCount(stringToTest, ignoreForNumberOfWords);

            Console.WriteLine($"Total words in sentence: {wordsList.Count}");

            foreach (var word in wordsList)
            {
                Console.WriteLine(word);
            }

            (mostUsedWord, mostUsedWordCount) = WordFrequency(wordsList);
            (mostUsedChar, mostUsedCharCount) = CharacterFrequency(wordsList);

            Console.WriteLine($"Most used word is \"{mostUsedWord}\" used {mostUsedWordCount} time(s).");


            Console.WriteLine($"Most used char is \"{mostUsedChar}\" used {mostUsedCharCount} time(s).");


            Console.ReadLine();
        }


        public static bool IsValidChar(char charToTest, char[] charSetToTest)
        {
            foreach (var character in charSetToTest)
            {
                if(charToTest == character)
                {
                    return false;
                }
            }

            return true;
        }


        public static List<string> WordCount(string stringToTest, char[] charsToIgnore)
        {
            List<string> output = new List<string>();
            string word = "";

            int stringLength = stringToTest.Length;

            for (int i = 0; i < stringLength; i++)
            {
                if (IsValidChar(stringToTest[i], charsToIgnore) == false)
                {
                    if(word.Length > 0)
                    {
                        output.Add(word);
                        word = "";
                    }
                }
                else
                {
                    word += stringToTest[i];
                }

            }

            return output;
        }


        public static (string, int) WordFrequency(List<string> wordList)
        {
            Dictionary<string, int> wordLibrary = new Dictionary<string, int>();

            string mostUsedWord = "";
            int numTimesUsed = 0;

            foreach (string word in wordList)
            {
                //Does the word(to lower) exist in the library?
                if(wordLibrary.ContainsKey(word.ToLower()))
                {
                    //Yes, increase count by 1
                    wordLibrary[word.ToLower()]++;
                }

                else
                {
                    //Else, add word to library and set count to 1
                    wordLibrary.Add(word.ToLower(), 1);
                }
            }

            foreach (var entry in wordLibrary)
            {
                if(entry.Value > numTimesUsed)
                {
                    numTimesUsed = entry.Value;
                    mostUsedWord = entry.Key;
                }
            }

            return (mostUsedWord, numTimesUsed);

        }


        public static (char, int) CharacterFrequency(List<string> wordList)
        {

            char mostUsedChar = 'a';
            int numTimesUsed = 0;
            Dictionary<char, int> characterLibrary = new Dictionary<char, int>();

            //Iterate through each word in wordList
            foreach (string word in wordList)
            {

                char[] wordToTest = word.ToLower().ToCharArray();

                foreach (char item in wordToTest)
                {
                    if(characterLibrary.ContainsKey(item))
                    {
                        characterLibrary[item]++;
                    }
                    else
                    {
                        characterLibrary.Add(item, 1);
                    }
                }

                foreach (var entry in characterLibrary)
                {
                    if(entry.Value > numTimesUsed)
                    {
                        mostUsedChar = entry.Key;
                        numTimesUsed = entry.Value;
                    }
                }
            }

            return(mostUsedChar, numTimesUsed);

        }
    }
}
