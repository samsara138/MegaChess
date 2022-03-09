using Core;
using Pieces;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardModel", menuName = "Mega Chess/Board Model")]
    public partial class BoardModel : Model
    {
        public int Width;
        public int Height;

        public List<TileModel> DebugTiles;

        [Space]

        public List<TileSetting> boardSettings = new List<TileSetting>();

        public void InitData()
        {
            InitStandardBoard();
        }
    }

    public partial class BoardModel
    {
        private void InitStandardBoard()
        {
            if (boardSettings == null)
            {
                boardSettings = new List<TileSetting>();
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        TileSetting setting = new TileSetting(j, i);

                        int buffer = (i + j) % 2;
                        setting.tileModel = DebugTiles[buffer];

                        boardSettings.Add(setting);
                    }
                }
            }
            else
            {
                foreach(TileSetting setting in boardSettings)
                {
                    if(setting.tileModel == null)
                    {
                        int buffer = (setting.x + setting.y) % 2;
                        setting.tileModel = DebugTiles[buffer];
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class TileSetting
    {
        public int x;
        public int y;
        public PlayerSide side;
        public PieceModel piece;
        public TileModel tileModel;

        public TileSetting(int x,int y)
        {
            this.x = x;
            this.y = y;
            side = PlayerSide.player1;
            piece = null;
            tileModel = null;
        }
    }
}