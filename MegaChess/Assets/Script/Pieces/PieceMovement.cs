using Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public static class PieceMovement
    {
        /// <summary>
        /// Get all possible move in a board
        /// </summary>
        /// <param name="piecePosition"></param>
        /// <param name="boardController"></param>
        /// <param name="pieceType"></param>
        /// <returns></returns>
        public static Dictionary<Vector2, MoveType> GetMovement(Vector2 piecePosition, BoardController boardController,PieceType pieceType)
        {
            switch (pieceType)
            {
                case PieceType.Null:
                    return null;
                case PieceType.King:
                    break;
                case PieceType.Queen:
                    break;
                case PieceType.Rook:
                    break;
                case PieceType.Bishop:
                    break;
                case PieceType.Knight:
                    break;
                case PieceType.Pawn:
                    break;
            }
            return null;
        }
    }

    public enum MoveType
    {
        //Move without any further action
        NormalMove = 1,
        //Move and kill the piece
        KillMove
    }
}