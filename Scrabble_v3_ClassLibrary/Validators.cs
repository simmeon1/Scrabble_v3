using System;

namespace Scrabble_v3_ClassLibrary
{
    public static class Validators
    {
        public const string LETTER_IS_NULL = "Letter is null.";
        public const string LETTER_MORE_THAN_ONE_CHARACTERS = "Letter is more than 1 characters.";
        public const string SCORE_BELOW_ZERO = "Score is below 0.";
        public static void ValidateLetter(string letter)
        {
            if (letter == null) throw new Exception(LETTER_IS_NULL);
            if (letter.Length > 1) throw new Exception(LETTER_MORE_THAN_ONE_CHARACTERS);
        }
        
        public static void ValidateScore(int score)
        {
            if (score < 0) throw new Exception(SCORE_BELOW_ZERO);
        }
    }
}
