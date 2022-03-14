using Core;
using Networking;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private BoardModel boardModel;
        [SerializeField] private ChessRuleBook ruleBook;

        [Space]

        [SerializeField] private GameObject tileObj;
        [SerializeField] private GameObject pieceObj;

        [SerializeField] private Transform boardContainer;

        private BoardController controller;

        // Start is called before the first frame update
        private void Start()
        {
            GlobalParameters.BOARD_HEIGHT = boardModel.Height;
            GlobalParameters.BOARD_WIDTH = boardModel.Width;

            controller = new BoardController();
            controller.Intialize(boardModel);
            controller.configure(ruleBook);

            controller.CreateTiles(tileObj, boardContainer);
            controller.CreatePieces(pieceObj);
            EventManager.TriggerEvent(Core.EventType.ClearTileEffectEvent);
        }


        private void OnDestroy()
        {
            if (controller != null) { controller.UnBind(); }
        }
    }
}