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
        private Vector2 gridPosition;
        public void Configure(Vector2 pos)
        {
            View.Configure(Model);
            gridPosition = pos;
            EventManager.SubscribeToEvent(Core.EventType.ClearTileEffectEvent, ClearEffect);
        }

        public void UnBind()
        {
            EventManager.UnsubscribeToEvent(Core.EventType.ClearTileEffectEvent, ClearEffect);
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