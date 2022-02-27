using Core;
using Pieces;
using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public class BoardController : Controller<BoardModel, BoardView>
    {
        private Dictionary<Vector2, PieceController> BoardData;

        private List<TileController> tiles;


        public void configure()
        {
            BoardData = new Dictionary<Vector2, PieceController>();
        }

        public void CreateTiles(GameObject tile, Transform boardTransform)
        {
            tiles = new List<TileController>();
            List<TileSetting> boardSettings = Model.boardSettings;
            float tileLength = TileModel.tileLength;

            Vector2 positionOffet = new Vector2((Model.Width - 1) * (tileLength / 2), 
                                                (Model.Height - 1) * (tileLength / 2));


            foreach (TileSetting setting in boardSettings)
            {
                Vector2 position = new Vector2(setting.x * tileLength, setting.y * tileLength);
                position -= positionOffet;

                Quaternion rotation = Quaternion.identity;

                GameObject bufferObj = GameObject.Instantiate(tile, position, rotation, boardTransform);

                TileController tileBuffer = new TileController();
                tileBuffer.Intialize(setting.tileModel);
                tileBuffer.BindView(bufferObj.GetComponent<TileView>());
                tileBuffer.Configure();
                tiles.Add(tileBuffer);
            }
        }

        public void CreatePieces()
        {

            /*
                         foreach (TileSetting setting in Model.BoardData)
            {
                if (setting.piece != null)
                {
                    Vector2 pos = new Vector2(setting.x, setting.y);

                    PieceController controller = new PieceController();
                    controller.Intialize(setting.piece);

                    BoardData.Add(pos, controller);
                }
            }
             * */
        }

    }
}