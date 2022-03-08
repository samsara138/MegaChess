using Core;
using Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public class BoardController : Controller<BoardModel, BoardView>
    {
        public Dictionary<Vector2, PieceController> PieceData;
        private Dictionary<Vector2, TileController> TilesData;

        public int Height => Model.Height;
        public int Width => Model.Width;

        public void configure()
        {
            PieceData = new Dictionary<Vector2, PieceController>();
            EventManager.SubscribeToEvent(Core.EventType.ChessClickEvent, HandleClick);
        }

        public void UnBind()
        {
            EventManager.UnsubscribeToEvent(Core.EventType.ChessClickEvent, HandleClick);
            foreach(TileController tileController in TilesData.Values)
            {
                tileController.UnBind();
            }
        }

        private void HandleClick(object obj)
        {
            EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
            ChessClickEvent data = obj as ChessClickEvent;

            Dictionary<Vector2, MoveType> movements = PieceMovement.GetMovement(data.gridPosition, this, data.pieceController);
            foreach(KeyValuePair<Vector2,MoveType> pair in movements)
            {
                TilesData[pair.Key].ShowEffect(pair.Value);
            }
        }

        private Vector2 GetRealPosition(Vector2 postion)
        {
            float tileLength = TileModel.tileLength;
            Vector2 position = new Vector2(postion.x * tileLength, postion.y * tileLength);
            Vector2 positionOffet = new Vector2((Width - 1) * (tileLength / 2),
                                               (Height - 1) * (tileLength / 2));
            position -= positionOffet;
            return position;
        }

        /// <summary>
        /// Spawn the tile grid
        /// </summary>
        /// <param name="tileObj"></param>
        /// <param name="boardTransform"></param>
        public void CreateTiles(GameObject tileObj, Transform boardTransform)
        {
            TilesData = new Dictionary<Vector2, TileController>();
            List<TileSetting> boardSettings = Model.boardSettings;

            foreach (TileSetting setting in boardSettings)
            {
                Vector2 gridPosition = new Vector2(setting.x, setting.y);
                Vector2 position = GetRealPosition(gridPosition);
                GameObject bufferObj = GameObject.Instantiate(tileObj, position, Quaternion.identity, boardTransform);

                TileController tileBuffer = new TileController();
                tileBuffer.Intialize(setting.tileModel);
                tileBuffer.BindView(bufferObj.GetComponent<TileView>());
                tileBuffer.Configure(gridPosition);
                TilesData.Add(gridPosition,tileBuffer);
            }
        }

        public void CreatePieces(GameObject pieceObj)
        {
            List<TileSetting> boardSettings = Model.boardSettings;
            PieceData = new Dictionary<Vector2, PieceController>();

            foreach (TileSetting setting in boardSettings)
            {
                if (setting.piece != null)
                {
                    Vector2 gridPosition = new Vector2(setting.x, setting.y);
                    Vector2 position = GetRealPosition(gridPosition);

                    Transform tileTransform = TilesData[gridPosition].View.transform;
                    GameObject bufferObj = GameObject.Instantiate(pieceObj, position, 
                            Quaternion.identity, tileTransform);

                    PieceController pieceBuffer = new PieceController();
                    pieceBuffer.Intialize(setting.piece);
                    pieceBuffer.BindView(bufferObj.GetComponent<PieceView>());
                    pieceBuffer.Configure(gridPosition,setting.side);
                    PieceData.Add(gridPosition, pieceBuffer);
                }
            }
        }

    }
}