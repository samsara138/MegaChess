using Board;
using Core;
using System;
using Tile;
using UnityEngine;

namespace Pieces
{
    public class PieceController : Controller<PieceModel,PieceView>
    {
        public Vector2 gridPosition;
        public PlayerSide side;
        public TileController parentTile;

        public bool moved;

        public void Configure(Vector2 position, PlayerSide side, TileController parentTile)
        {
            this.gridPosition = position;
            this.side = side;
            View.Configure(Model,side);
            View.ClickDetector.onClick.AddListener(OnClick);
            this.parentTile = parentTile;
            parentTile.childPiece = this;
            moved = false;
        }

        public void OnClick()
        {
            TextData textData = new TextData();
            textData.text = ("Click on " + Model.ModelName + " of player " + side + " at position " + gridPosition);
            EventManager.TriggerEvent(Core.EventType.NewTextMessageEvent, textData);

            ChessClickEvent data = new ChessClickEvent();
            data.gridPosition = this.gridPosition;
            data.pieceController = this;

            EventManager.TriggerEvent(Core.EventType.ChessClickEvent, data);
        }

        public void OnKilled()
        {
            parentTile.childPiece = null;
            View.OnKill();
        }

        public void MoveToPosition(TileController tileContrller, PieceController killedPiece = null)
        {
            parentTile.childPiece = null;
            parentTile = tileContrller;
            parentTile.childPiece = this;

            View.MoveToPosition(parentTile.View.gameObject.transform, killedPiece);
            gridPosition = tileContrller.gridPosition;

            moved = true;
        }
    }
}
