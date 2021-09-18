﻿using Scrabble_v3_ClassLibrary.DataObjects;
using System.Collections.Generic;

namespace Scrabble_v3_ClassLibrary.GameObjects.Interfaces
{
    public interface IBoardTileArrayCreator
    {
        BoardTileDto[][] GetBoardTileArray(IEnumerable<BoardTileDto> tiles);
    }
}