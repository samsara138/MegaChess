using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public static class BoardHelperFunctions
    {
        public static Vector2 GetRealPosition(Vector2 postion)
        {
            int Width = BoardManager.boardWidth;
            int Height = BoardManager.boardHeight;
            float tileLength = TileModel.tileLength;

            Vector2 position = new Vector2(postion.x * tileLength, postion.y * tileLength);
            Vector2 positionOffet = new Vector2((Width - 1) * (tileLength / 2),
                                               (Height - 1) * (tileLength / 2));
            position -= positionOffet;
            return position;
        }
    }
}