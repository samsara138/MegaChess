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

        private MovementBuffer moveBuffer;

        public BoardPlayerState playerState;
        private ChessRuleBook ruleBook;

        public int Height => Model.Height;
        public int Width => Model.Width;

        public void configure(ChessRuleBook ruleBook)
        {
            PieceData = new Dictionary<Vector2, PieceController>();
            EventManager.SubscribeToEvent(Core.EventType.ChessClickEvent, HandlePieceClick);
            EventManager.SubscribeToEvent(Core.EventType.TileClickEvent, HandleTileClick);
            playerState = new BoardPlayerState(ruleBook);
            this.ruleBook = ruleBook;
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

            if (data.pieceController.side == playerState.currentPlayer)
            {
                EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
                Dictionary<Vector2, MoveType> movements = PieceMovement.GetMovement(data.gridPosition, this, data.pieceController);
                TilesData[data.gridPosition].ShowEffect(MoveType.CurrentPosition);

                foreach (KeyValuePair<Vector2, MoveType> pair in movements)
                {
                    TilesData[pair.Key].ShowEffect(pair.Value);
                }
                moveBuffer = new MovementBuffer(data.pieceController, movements);
            }
            else
            {
                if (moveBuffer != null)
                {
                    MoveType moveType;
                    if (moveBuffer.TryGetMove(data.gridPosition, out moveType))
                    {
                        if (moveType == MoveType.KillMove)
                        {
                            PieceData[data.gridPosition].OnKilled();

                            PieceController buffer = moveBuffer.pieceController;
                            PieceData.Remove(moveBuffer.pieceController.position);

                            buffer.MoveToPosition(PieceData[data.gridPosition].parentTile);
                            PieceData[data.gridPosition] = buffer;

                            moveBuffer = null;
                            EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
                            playerState.NextMove();
                        }
                    }
                }
            }
        }

        private void HandleTileClick(object obj)
        {
            TileClickEvent data = obj as TileClickEvent;

            MoveType moveType;
            if (moveBuffer != null)
            {
                if (moveBuffer.TryGetMove(data.gridPosition, out moveType))
                {
                    if (moveType == MoveType.NormalMove)
                    {
                        PieceController buffer = moveBuffer.pieceController;

                        PieceData.Remove(moveBuffer.pieceController.position);
                        PieceData[data.gridPosition] = buffer;
                        buffer.MoveToPosition(data.controller);

                        moveBuffer = null;
                        EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
                        playerState.NextMove();
                    }
                }
            }
        }

        #region creating board

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
                    pieceBuffer.Configure(gridPosition, setting.side, TilesData[gridPosition]);
                    PieceData.Add(gridPosition, pieceBuffer);
                }
            }
        }

        #endregion creating board
    }
}
