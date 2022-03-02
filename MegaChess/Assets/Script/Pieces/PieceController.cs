using Board;
using Core;
using UnityEngine;

namespace Pieces
{
    public class PieceController : Controller<PieceModel,PieceView>
    {
        private Vector2 position;
        private PlayerSide side;

        public void Configure(Vector2 position, PlayerSide side)
        {
            this.position = position;
            this.side = side;
            View.Configure(Model,side);
            View.GetClickDetector.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            Debug.Log("Click on " + Model.ModelName + " of player " + side + " at position " + position);

            ChessClickEvent data = new ChessClickEvent();
            data.gridPosition = this.position;
            EventManager.TriggerEvent(Core.EventType.ChessClickEvent, data);
        }
    }
}
