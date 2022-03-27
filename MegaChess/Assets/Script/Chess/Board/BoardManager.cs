using Core;
using UnityEngine;

namespace Board
{
    public class BoardManager : MonoBehaviour
    {
        public BoardModel boardModel;
        public ChessRuleBookModel ruleBook;

        [Space]

        [SerializeField] private GameObject tileObj;
        [SerializeField] private GameObject pieceObj;

        [SerializeField] private Transform boardContainer;

        private BoardController controller;

        // Start is called before the first frame update
        public void Setup()
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