using Board;
using System;
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
                    return CalculateKnightMovement(piecePosition, boardController, pieceType);
                    break;
                case PieceType.Pawn:
                    break;
            }
            return null;
        }

        private static Dictionary<Vector2, MoveType> CalculateKnightMovement(Vector2 piecePosition,BoardController boardController, PieceType pieceType)
        {
            Dictionary<Vector2, MoveType> result = new Dictionary<Vector2, MoveType>();

            Vector2 testPosition = new Vector2(piecePosition.x, piecePosition.y);
            testPosition.x += 2;
            testPosition.y += 1;
            result.Add(testPosition, MoveType.NormalMove);

            testPosition = new Vector2(piecePosition.x, piecePosition.y);
            testPosition.x += 1;
            testPosition.y += 2;
            result.Add(testPosition, MoveType.KillMove);

            return result;
        }
    }

    public enum MoveType
    {
        NULL = 0,
        //Move without any further action
        NormalMove = 1,
        //Move and kill the piece
        KillMove
    }
}