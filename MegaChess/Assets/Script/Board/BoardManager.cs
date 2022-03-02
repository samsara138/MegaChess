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

        // Start is called before the first frame update
        void Start()
        {
            controller = new BoardController();
            controller.Intialize(model);
            controller.configure();
            controller.CreateTiles(tileObj, BoardContainer);
            controller.CreatePieces(pieceObj);
        }

        private void OnDestroy()
        {
            controller.UnBind();
        }
    }
}