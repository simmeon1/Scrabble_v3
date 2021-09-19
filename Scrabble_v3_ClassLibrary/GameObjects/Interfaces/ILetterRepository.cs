using Scrabble_v3_ClassLibrary.DataObjects;
using System.Collections.Generic;

namespace Scrabble_v3_ClassLibrary.GameObjects.Interfaces
{
    public interface ILetterRepository
    {
        bool LetterIsValid(string letter);
    }
}