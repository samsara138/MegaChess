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
            int Width = GlobalParameters.BOARD_WIDTH;
            int Height = GlobalParameters.BOARD_HEIGHT;
            float tileLength = GlobalParameters.TILE_LENGTH;

            Vector2 realPosition = new Vector2(postion.x * tileLength, postion.y * tileLength);
            Vector2 positionOffet = new Vector2((Width - 1) * (tileLength / 2),
                                               (Height - 1) * (tileLength / 2));

            realPosition -= positionOffet;
            return realPosition;
        }
    }
}