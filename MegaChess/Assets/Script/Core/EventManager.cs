using Pieces;
using System;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Core
{
    public enum EventType
    {
        ChessClickEvent = 0,
        TileClickEvent,
        ClearTileEffectEvent,
        NewTextMessageEvent,
        PlayerJoinRoomEvent
    }

    public class ChessClickEvent
    {
        public Vector2 gridPosition;
        public PieceController pieceController;
    }

    public class TileClickEvent
    {
        public Vector2 gridPosition;
        public TileController controller;
    }

    public class TextData
    {
        public List<string> textList;
        public string text;
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
            if (EventSystem.ContainsKey(type))
                EventSystem[type]?.Invoke(data);
            else
                Debug.LogWarning("No subscriber for " + type);
        }
    }
}
