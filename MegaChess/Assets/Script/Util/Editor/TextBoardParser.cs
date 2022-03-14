using Board;
using Pieces;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tile;
using UnityEditor;
using UnityEngine;

namespace Util
{
    [CreateAssetMenu(fileName = "BoardParser", menuName = "Mega Chess/Board Parser")]
    public class TextBoardParser : ScriptableObject
    {
        [SerializeField] private TextAsset TextFile;
        [SerializeField] private string OutputPath = "Assets/Data/BoardSettings";
        [SerializeField] private string OutputName = "TestBoard";

        [Space]

        [SerializeField] private PieceModel KingModel;
        [SerializeField] private PieceModel QueenModel;
        [SerializeField] private PieceModel RookModel;
        [SerializeField] private PieceModel BishopModel;
        [SerializeField] private PieceModel KnightModel;
        [SerializeField] private PieceModel PawnModel;

        [Space]

        [SerializeField] private TileModel WhiteTile;
        [SerializeField] private TileModel BlackTile;

        public void ParseData()
        {
            Debug.Log("Parsing data");
            BoardModel model = (BoardModel)ScriptableObject.CreateInstance(typeof(BoardModel));
            model.ModelName = OutputName;

            string[] lines = TextFile.text.Split('\n');

            int width = int.Parse(lines[0].Split(' ')[0]);
            int height = int.Parse(lines[0].Split(' ')[1]);

            model.Width = width;
            model.Height = height;

            Debug.LogFormat("Board size: Width {0} - Height {1}", width, height);

            int y;

            for (short iy = 1; iy < lines.Length; iy++)
            {
                y = height - iy;
                string[] line = lines[iy].Split(' ');

                for(short x = 0; x < line.Length; x++)
                {
                    TileSetting setting = SetupTile(x, y, line[x]);
                    model.boardSettings.Add(setting);
                }
            }

            model.DebugTiles = new List<TileModel>();
            model.DebugTiles.Add(BlackTile);
            model.DebugTiles.Add(WhiteTile);
            model.InitData();

            SaveAsset(model);
            AssetDatabase.Refresh();
        }

        private TileSetting SetupTile(int x, int y,string data)
        {
            TileSetting buffer = new TileSetting(x, y);

            if(data == "**")
                return buffer;


            switch (data[0])
            {
                case '1':
                    buffer.side = PlayerSide.player1;
                    break;
                case '2':
                    buffer.side = PlayerSide.player2;
                    break;
                case '3':
                    buffer.side = PlayerSide.player3;
                    break;
                case '4':
                    buffer.side = PlayerSide.player4;
                    break;
            }

            switch (data[1])
            {
                case 'K':
                    buffer.piece = KingModel;
                    break;
                case 'Q':
                    buffer.piece = QueenModel;
                    break;
                case 'R':
                    buffer.piece = RookModel;
                    break;
                case 'B':
                    buffer.piece = BishopModel;
                    break;
                case 'k':
                    buffer.piece = KnightModel;
                    break;
                case 'P':
                    buffer.piece = PawnModel;
                    break;
            }

            //TODO tile stuff
            return buffer;
        }

        private void SaveAsset(BoardModel obj)
        {
            string p = Path.Combine(OutputPath, OutputName + ".asset");
            AssetDatabase.CreateAsset(obj, p);
        }
    }
}
