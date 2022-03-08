using Core;
using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private BoardModel boardModel;
        [SerializeField] private GameObject tileObj;
        [SerializeField] private GameObject pieceObj;
        [SerializeField] private Transform boardContainer;
        [SerializeField] private ChessRuleBook ruleBook;

        private BoardController controller;

        public static int boardWidth;
        public static int boardHeight;


        // Start is called before the first frame update
        void Start()
        {
            boardWidth = boardModel.Width;
            boardHeight = boardModel.Height;

            controller = new BoardController();
            controller.Intialize(boardModel);
            controller.configure(ruleBook);

            controller.CreateTiles(tileObj, boardContainer);
            controller.CreatePieces(pieceObj);
            EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);

        }

        private void OnDestroy()
        {
            if(controller != null) { controller.UnBind(); }
        }
    }
}