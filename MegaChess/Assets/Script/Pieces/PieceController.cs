using Core;
using UnityEngine;

namespace Pieces
{
    public class PieceController : Controller<PieceModel,PieceView>
    {
        private Vector2 position;
        private short side;

        public void Configure(PieceModel pieceModel, Vector2 position, short side)
        {
            base.Intialize(pieceModel);

            this.position = position;
            this.side = side;
        }
    }
}
