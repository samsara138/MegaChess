using Board;
using Core;
using System;
using Tile;
using UnityEngine;

namespace Pieces
{
    public class PieceController : Controller<PieceModel,PieceView>
    {
        public Vector2 position;
        public PlayerSide side;
        public TileController parentTile;

        public void Configure(Vector2 position, PlayerSide side, TileController parentTile)
        {
            this.position = position;
            this.side = side;
            View.Configure(Model,side);
            View.ClickDetector.onClick.AddListener(OnClick);
            this.parentTile = parentTile;
            parentTile.childPiece = this;
        }

        public void OnClick()
        {
            Debug.Log("Click on " + Model.ModelName + " of player " + side + " at position " + position);

            ChessClickEvent data = new ChessClickEvent();
            data.gridPosition = this.position;
            data.controller = this;

            EventManager.TriggerEvent(Core.EventType.ChessClickEvent, data);
        }

        public void OnKill()
        {
            parentTile.childPiece = null;
            View.OnKill();
        }

        internal void MoveToPosition(TileController tileContrller)
        {
            parentTile.childPiece = null;
            parentTile = tileContrller;
            parentTile.childPiece = this;

            View.MoveToPosition(parentTile.View.gameObject.transform);
            position = tileContrller.gridPosition;
        }
    }
}
