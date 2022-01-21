using System;

namespace BrunoCampiol.CrossCutting.Common.Generator
{
    public class StringGenerator
    {
        public string ParagraphGenetator(int requestedWords)
        {
            if (requestedWords == 0) return String.Empty;

            Random random = new Random();
            string paragrapth = String.Empty;

            for (int i = 0; i < requestedWords - 1; i++)
            {
                paragrapth += WordGenerator(random.Next(3, 10)) + " ";
            }

            return paragrapth;
        }

        public string WordGenerator(int requestedLength)
        {
            Random rnd = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
            string[] vowels = { "a", "e", "i", "o", "u" };

            string word = "";

            if (requestedLength == 1)
            {
                word = GetRandomLetter(rnd, vowels);
            }
            else
            {
                for (int i = 0; i < requestedLength; i += 2)
                {
                    word += GetRandomLetter(rnd, consonants) + GetRandomLetter(rnd, vowels);
                }

                word = word.Replace("q", "qu").Substring(0, requestedLength); // We may generate a string longer than requested length, but it doesn't matter if cut off the excess.
            }

            return word;
        }

        private static string GetRandomLetter(Random rnd, string[] letters)
        {
            return letters[rnd.Next(0, letters.Length - 1)];
        }
    }
}
