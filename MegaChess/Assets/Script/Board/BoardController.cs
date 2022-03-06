using Core;
using Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public class MovementBuffer
    {
        public PieceController pieceController;
        private Dictionary<Vector2, MoveType> movements;

        public MovementBuffer(PieceController controller, Dictionary<Vector2, MoveType> moves)
        {
            pieceController = controller;
            movements = moves;
        }

        public bool TryGetMove(Vector2 gridPostition, out MoveType moveType)
        {
            moveType = MoveType.NULL;
            if (movements.ContainsKey(gridPostition))
            {
                moveType = movements[gridPostition];
                return true;
            }
            return false;
        }
    }

    public class BoardController : Controller<BoardModel, BoardView>
    {
        private Dictionary<Vector2, PieceController> PieceData;
        private Dictionary<Vector2, TileController> TilesData;

        private MovementBuffer moveBuffer;

        public int Height => Model.Height;
        public int Width => Model.Width;

        public void configure()
        {
            PieceData = new Dictionary<Vector2, PieceController>();
            EventManager.SubscribeToEvent(Core.EventType.ChessClickEvent, HandlePieceClick);
            EventManager.SubscribeToEvent(Core.EventType.TileClickEvent, HandleTileClick);
        }

        public void UnBind()
        {
            EventManager.UnsubscribeToEvent(Core.EventType.ChessClickEvent, HandlePieceClick);
            foreach (TileController tileController in TilesData.Values)
            {
                tileController.UnBind();
            }
        }

        private void HandlePieceClick(object obj)
        {
            ChessClickEvent data = obj as ChessClickEvent;
            EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);

            if (moveBuffer == null)
            {
                Dictionary<Vector2, MoveType> movements = PieceMovement.GetMovement(data.gridPosition, this, data.controller.Model.PieceType);
                foreach (KeyValuePair<Vector2, MoveType> pair in movements)
                {
                    TilesData[pair.Key].ShowEffect(pair.Value);
                }
                moveBuffer = new MovementBuffer(data.controller, movements);
            }
            else
            {
                MoveType moveType;
                if (moveBuffer.TryGetMove(data.gridPosition, out moveType))
                {
                    if (moveType == MoveType.KillMove)
                    {
                        PieceData[data.gridPosition].OnKill();

                        PieceController buffer = moveBuffer.pieceController;
                        PieceData.Remove(moveBuffer.pieceController.position);
                        PieceData[data.gridPosition] = buffer;

                        moveBuffer = null;
                    }
                }
            }
        }

        private void HandleTileClick(object obj)
        {
            TileClickEvent data = obj as TileClickEvent;

            MoveType moveType;
            if (moveBuffer.TryGetMove(data.gridPosition, out moveType))
            {
                if(moveType == MoveType.NormalMove)
                {
                    PieceController buffer = moveBuffer.pieceController;

                    buffer.MoveToPosition(data.controller);

                    PieceData.Remove(moveBuffer.pieceController.position);
                    PieceData[data.gridPosition] = buffer;

                    moveBuffer = null;
                    EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
                }
            }
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
                Vector2 position = BoardHelperFunctions.GetRealPosition(gridPosition);
                GameObject bufferObj = GameObject.Instantiate(tileObj, position, Quaternion.identity, boardTransform);

                TileController tileBuffer = new TileController();
                tileBuffer.Intialize(setting.tileModel);
                tileBuffer.BindView(bufferObj.GetComponent<TileView>());
                tileBuffer.Configure(gridPosition);
                TilesData.Add(gridPosition, tileBuffer);
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
                    Vector2 position = BoardHelperFunctions.GetRealPosition(gridPosition);

                    Transform tileTransform = TilesData[gridPosition].View.transform;
                    GameObject bufferObj = GameObject.Instantiate(pieceObj, position,
                            Quaternion.identity, tileTransform);

                    PieceController pieceBuffer = new PieceController();
                    pieceBuffer.Intialize(setting.piece);
                    pieceBuffer.BindView(bufferObj.GetComponent<PieceView>());
                    pieceBuffer.Configure(gridPosition, setting.side);
                    PieceData.Add(gridPosition, pieceBuffer);
                }
            }
        }

    }
}