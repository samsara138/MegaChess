using Pieces;
using System;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Core
{
    public class ChessClickEvent
    {
        public Vector2 gridPosition;
        public PieceController controller;
    }

    public class TileClickEvent
    {
        public Vector2 gridPosition;
        public TileController controller;
    }

    public static class EventManager
    {
        private static Dictionary<EventType, Action<object>> EventSystem = new Dictionary<EventType, Action<object>>();

        public static void SubscribeToEvent(EventType type, Action<object> func)
        {
            if (!EventSystem.ContainsKey(type))
            {
                EventSystem[type] = new Action<object>(func);
            }
            else
            {
                EventSystem[type] += func;
            }
        }

        public static void UnsubscribeToEvent(EventType type, Action<object> func)
        {
            if (EventSystem.ContainsKey(type))
            {
                EventSystem[type] -= func;
            }
        }

        public static void TriggerEvent(EventType type, object data = null)
        {
            EventSystem[type]?.Invoke(data);
        }

    }

    public enum EventType
    {
        ChessClickEvent = 0,
        TileClickEvent,
        ClearTileEffectEvent
    }
}