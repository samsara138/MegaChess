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
        private PlayerSide side;

        public void Configure(Vector2 position, PlayerSide side)
        {
            this.position = position;
            this.side = side;
            View.Configure(Model,side);
            View.ClickDetector.onClick.AddListener(OnClick);
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
            View.OnKill();
        }

        internal void MoveToPosition(TileController tileContrller)
        {
            View.MoveToPosition(tileContrller.View.gameObject.transform);
            position = tileContrller.gridPosition;
        }
    }
}
