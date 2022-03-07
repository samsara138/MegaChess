using Core;
using Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    public class TileController : Controller<TileModel,TileView>
    {
        public Vector2 gridPosition;
        public PieceController childPiece;

        public void Configure(Vector2 pos)
        {
            View.Configure(Model);
            gridPosition = pos;
            EventManager.SubscribeToEvent(Core.EventType.ClearTileEffectEvent, ClearEffect);
            View.ClickDetector.onClick.AddListener(OnClickTile);
        }

        public void UnBind()
        {
            EventManager.UnsubscribeToEvent(Core.EventType.ClearTileEffectEvent, ClearEffect);
        }

        private void OnClickTile()
        {
            Debug.Log("Click tile at " + gridPosition);

            TileClickEvent data = new TileClickEvent();
            data.gridPosition = this.gridPosition;
            data.controller = this;

            EventManager.TriggerEvent(Core.EventType.TileClickEvent,data);
        }

        private void ClearEffect(object obj)
        {
            ShowEffect(MoveType.NULL);
        }

        public void ShowEffect(MoveType type)
        {
            View.ShowStepEffect(type);
        }
    }
}