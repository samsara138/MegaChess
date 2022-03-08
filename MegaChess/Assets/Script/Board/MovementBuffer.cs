using Pieces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class MovementBuffer
    {
        public PieceController pieceController;
        private Dictionary<Vector2, MoveType> movements;

        public MovementBuffer(PieceController controller, Dictionary<Vector2, MoveType> moves)
        {
            Config(controller, moves);
        }

        public void Config(PieceController controller, Dictionary<Vector2, MoveType> moves)
        {
            pieceController = controller;
            movements = moves;
        }

        public bool TryGetMove(Vector2 gridPostition, out MoveType moveType)
        {
            moveType = MoveType.NULL;
            if (movements.ContainsKey(gridPostition))
            {
                moveType = movements[gridPostition];
                return true;
            }
            return false;
        }

        public void Clear()
        {
            pieceController = null;
            movements = new Dictionary<Vector2, MoveType>();
        }
    }
}
