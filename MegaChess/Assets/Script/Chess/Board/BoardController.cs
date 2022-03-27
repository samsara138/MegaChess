using Core;
using ExitGames.Client.Photon;
using Networking;
using Photon.Pun;
using Photon.Realtime;
using Pieces;
using System;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public class BoardController : Controller<BoardModel, BoardView>, IOnEventCallback
    {
        public Dictionary<Vector2, PieceController> PieceData;
        private Dictionary<Vector2, TileController> TilesData;

        private MovementBuffer moveBuffer;

        public BoardPlayerState playerState;

        public int Height => Model.Height;
        public int Width => Model.Width;

        public void configure(ChessRuleBook ruleBook)
        {
            PieceData = new Dictionary<Vector2, PieceController>();
            EventManager.SubscribeToEvent(Core.EventType.ChessClickEvent, HandlePieceClick);
            EventManager.SubscribeToEvent(Core.EventType.TileClickEvent, HandleTileClick);
            playerState = new BoardPlayerState(ruleBook);
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void UnBind()
        {
            EventManager.UnsubscribeToEvent(Core.EventType.ChessClickEvent, HandlePieceClick);
            foreach (TileController tileController in TilesData.Values)
            {
                tileController.UnBind();
            }
            PhotonNetwork.RemoveCallbackTarget(this);

        }

        #region Game Events

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
                            Vector2 origLoc = moveBuffer.pieceController.gridPosition;
                            Vector2 destLoc = data.gridPosition;
                            KillMoveEvent moveEvent = new KillMoveEvent(origLoc, destLoc);
                            moveEvent.Invoke();
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
                        Vector2 origLoc = moveBuffer.pieceController.gridPosition;
                        Vector2 destLoc = data.controller.gridPosition;
                        NormalMoveEvent moveEvent = new NormalMoveEvent(origLoc, destLoc);
                        moveEvent.Invoke();
                    }
                }
            }
        }

        #endregion

        #region Photon Events

        public void OnEvent(EventData photonEvent)
        {
            PunEvent eventType = (PunEvent)(int)photonEvent.Code;
            switch (eventType)
            {
                case PunEvent.NormalMoveEvent:
                    NormalMoveEvent normalMoveData = new NormalMoveEvent(photonEvent);
                    ExecuteNormalMove(normalMoveData);
                    break;
                case PunEvent.KillMoveEvent:
                    KillMoveEvent killMoveData = new KillMoveEvent(photonEvent);
                    ExecuteKillMove(killMoveData);
                    break;
            }
        }

        private void ExecuteKillMove(MoveEventData data)
        {
            PieceController buffer = PieceData[data.origLoc];
            PieceData.Remove(data.origLoc);
            buffer.MoveToPosition(PieceData[data.destLoc].parentTile, PieceData[data.destLoc]);
            PieceData[data.destLoc] = buffer;
            moveBuffer = null;
            EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
            playerState.NextMove();
        }

        private void ExecuteNormalMove(MoveEventData data)
        {
            PieceController buffer = PieceData[data.origLoc];
            PieceData.Remove(data.origLoc);
            buffer.MoveToPosition(TilesData[data.destLoc]);
            PieceData[data.destLoc] = buffer;
            moveBuffer = null;
            EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
            playerState.NextMove();
        }

        #endregion

        #region Creating board

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
                    GameObject bufferObj = GameObject.Instantiate(pieceObj, position, Quaternion.identity, tileTransform);

                    PieceController pieceBuffer = new PieceController();
                    pieceBuffer.Intialize(setting.piece);
                    pieceBuffer.BindView(bufferObj.GetComponent<PieceView>());
                    pieceBuffer.Configure(gridPosition, setting.side, TilesData[gridPosition]);
                    PieceData.Add(gridPosition, pieceBuffer);
                }
            }
        }

        #endregion
    }
}
