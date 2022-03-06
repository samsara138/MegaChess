using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;

namespace Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private BoardModel model;
        [SerializeField] private GameObject tileObj;
        [SerializeField] private GameObject pieceObj;
        [SerializeField] private Transform BoardContainer;

        [SerializeField] private List<TileModel> availableTiles;

        private BoardController controller;

        public static int boardWidth;
        public static int boardHeight;


        // Start is called before the first frame update
        void Start()
        {
            controller = new BoardController();
            controller.Intialize(model);
            controller.configure();
            controller.CreateTiles(tileObj, BoardContainer);
            controller.CreatePieces(pieceObj);

            boardWidth = controller.Width;
            boardHeight = controller.Height;
        }

        private void OnDestroy()
        {
            controller.UnBind();
        }
    }
}