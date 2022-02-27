using Core;
using Pieces;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardModel", menuName = "Mega Chess/Board Model")]
    public class BoardModel : Model
    {
        public int Width;
        public int Height;

        public List<TileModel> DebugTiles;

        [Space]

        public List<TileSetting> boardSettings = new List<TileSetting>();

        public void InitData()
        {
            boardSettings = new List<TileSetting>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    TileSetting setting = new TileSetting(i, j);

                    int buffer = (i + j) % 2;
                    setting.tileModel = DebugTiles[buffer];

                    boardSettings.Add(setting);
                }
            }
        }
    }

    [System.Serializable]
    public class TileSetting
    {
        public int x;
        public int y;
        public PieceModel piece;
        public TileModel tileModel;

        public TileSetting(int x,int y)
        {
            this.x = x;
            this.y = y;
            piece = null;
            tileModel = null;
        }
    }
}