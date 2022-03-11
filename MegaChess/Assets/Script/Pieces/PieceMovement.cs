using Board;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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


        public static Dictionary<Vector2, MoveType> GetMovement(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {

            switch (pieceController.Model.PieceType)
            {
                case PieceType.Null:
                    return null;
                case PieceType.King:
                    return CalcKing(piecePosition, boardController, pieceController);
                case PieceType.Queen:
                    return CalcQueen(piecePosition, boardController, pieceController);
                case PieceType.Rook:
                    return CalcRook(piecePosition, boardController, pieceController);
                case PieceType.Bishop:
                    return CalcBishop(piecePosition, boardController, pieceController);
                case PieceType.Knight:
                    return CalcKnight(piecePosition, boardController, pieceController);
                case PieceType.Pawn:
                    return CalcPawn(piecePosition, boardController, pieceController);
            }
            return null;
        }

        private static Dictionary<Vector2, MoveType> CalcPawn(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            Dictionary<Vector2, MoveType> result = new Dictionary<Vector2, MoveType>();

            Vector2 directionAdj = Vector2.one;
            directionAdj.y = pieceController.side == PlayerSide.player1 ? 1 : -1;


            Vector2 b1 = piecePosition + new Vector2(0, 1) * directionAdj;
            if (!boardController.PieceData.ContainsKey(b1))
            {
                result.Add(b1, MoveType.NormalMove);
                if (!pieceController.moved)
                {
                    Vector2 b2 = piecePosition + new Vector2(0, 2) * directionAdj;
                    if (!boardController.PieceData.ContainsKey(b2))
                    {
                        result.Add(b2, MoveType.NormalMove);
                    }
                }
            }

            b1 = new Vector2(piecePosition.x, piecePosition.y) + new Vector2(1, 1) * directionAdj;
            if(boardController.PieceData.ContainsKey(b1) && boardController.PieceData[b1].side != pieceController.side)
            {
                result.Add(b1, MoveType.KillMove);
            }

            b1 = new Vector2(piecePosition.x, piecePosition.y) + new Vector2(-1, 1) * directionAdj;
            if (boardController.PieceData.ContainsKey(b1) && boardController.PieceData[b1].side != pieceController.side)
            {
                result.Add(b1, MoveType.KillMove);
            }

            return result;
        }

        private static Dictionary<Vector2, MoveType> CalcKnight(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            Dictionary<Vector2, MoveType> result = new Dictionary<Vector2, MoveType>();
            List<Vector2> positions = new List<Vector2>();
            positions.Add(new Vector2(piecePosition.x + 1, piecePosition.y + 2));
            positions.Add(new Vector2(piecePosition.x + 1, piecePosition.y - 2));
            positions.Add(new Vector2(piecePosition.x - 1, piecePosition.y + 2));
            positions.Add(new Vector2(piecePosition.x - 1, piecePosition.y - 2));
            positions.Add(new Vector2(piecePosition.x + 2, piecePosition.y + 1));
            positions.Add(new Vector2(piecePosition.x - 2, piecePosition.y + 1));
            positions.Add(new Vector2(piecePosition.x + 2, piecePosition.y - 1));
            positions.Add(new Vector2(piecePosition.x - 2, piecePosition.y - 1));

            foreach(Vector2 position in positions)
            {
                if (canMove(position, boardController))
                {
                    if (boardController.PieceData.ContainsKey(position))
                    {
                        if (boardController.PieceData[position].side != pieceController.side)
                        {
                            result.Add(position, MoveType.KillMove);

                        }
                    }
                    else
                    {
                        result.Add(position, MoveType.NormalMove);
                    }
                }
            }
            return result;
        }

        private static Dictionary<Vector2, MoveType> CalcRook(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            return moveHorizontal(piecePosition, boardController, pieceController);
        }

        private static Dictionary<Vector2, MoveType> CalcBishop(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            return moveDiagonal(piecePosition, boardController, pieceController);
        }

        private static Dictionary<Vector2, MoveType> CalcQueen(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            Dictionary <Vector2, MoveType> dict1 = moveDiagonal(piecePosition, boardController, pieceController);
            Dictionary <Vector2, MoveType> dict2 = moveHorizontal(piecePosition, boardController, pieceController);
            foreach(KeyValuePair<Vector2, MoveType> entry in dict1)
            {
                dict2.Add(entry.Key, entry.Value);
            }
            return dict2;
        }

        private static Dictionary<Vector2, MoveType> CalcKing(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            Dictionary<Vector2, MoveType> result = new Dictionary<Vector2, MoveType>();
            Vector2 testp = new Vector2(piecePosition.x, piecePosition.y);
            List<Vector2> positions = new List<Vector2>();


            // upper row
            testp.y++;
            for (int i = (int)piecePosition.x - 1; i <= (int)piecePosition.x + 1; i++)
            {
                testp.x = i;
                positions.Add(testp);
            }

            // lower row
            testp.y = testp.y - 2;
            for (int i = (int)piecePosition.x - 1; i <= piecePosition.x + 1; i++)
            {
                testp.x = i;
                positions.Add(testp);
            }

            // left and right
            testp.y = piecePosition.y;
            for (int i = (int)piecePosition.x - 1; i <= piecePosition.x + 1; i++)
            {
                testp.x = i;
                positions.Add(testp);
            }

            foreach (Vector2 position in positions)
            {
                if (canMove(position, boardController))
                {
                    if (boardController.PieceData.ContainsKey(position))
                    {
                        if (boardController.PieceData[position].side != pieceController.side)
                        {
                            result.Add(position, MoveType.KillMove);

                        }
                    }
                    else
                    {
                        result.Add(position, MoveType.NormalMove);
                    }
                }
            }
            return result;
        }
        private static bool canMove(Vector2 dest, BoardController boardcontroller)
        {
            /*
             * check if destination is valid, return false if
             * 1) dest out of bounds
             * 2) dest blocked by pieces on the same side
             */

            if (dest.x >= boardcontroller.Width || dest.x < 0 || dest.y >= boardcontroller.Height || dest.y < 0)
            {
                return false;
            }
            return true;
        }
        private static Dictionary<Vector2, MoveType> moveHorizontal(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            // initialize dict
            Dictionary<Vector2, MoveType> result = new Dictionary<Vector2, MoveType>();

            // initialize test position
            Vector2 testp = new Vector2(piecePosition.x, piecePosition.y);

            // check the vertical column (x doesn't change)
            for (int i = (int)piecePosition.y + 1; i < boardController.Height; i++)
            {
                testp.y = i;
                if (canMove(testp, boardController))
                {
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);
                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }

            for (int i = (int)piecePosition.y - 1; i >= 0; i--)
            {
                testp.y = i;
                if (canMove(testp, boardController))
                {
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);

                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }

            testp.y = piecePosition.y;

            for (int i = (int)piecePosition.x + 1; i < boardController.Width; i++)
            {
                testp.x = i;
                if (canMove(testp, boardController))
                {
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);

                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }

            for (int i = (int)piecePosition.x - 1; i >= 0; i--)
            {
                testp.x = i;
                if (canMove(testp, boardController))
                {
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);


                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }
            return result;
        }
        private static Dictionary<Vector2, MoveType> moveDiagonal(Vector2 piecePosition, BoardController boardController, PieceController pieceController)
        {
            // method to move pieces on diagonals
            // initialize
            Dictionary<Vector2, MoveType> result = new Dictionary<Vector2, MoveType>();
            Vector2 testp = new Vector2(piecePosition.x, piecePosition.y);

            // check the upper right diagonal
            for (int i = (int)piecePosition.x + 1; i < boardController.Width; i++)
            {
                testp.x = i;
                testp.y++;
                if(testp.y >= boardController.Height) { break; }
                if (canMove(testp, boardController))
                {
                    // check if destination has pieces
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);
                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }
            // reset the test position to piece position
            testp.x = piecePosition.x;
            testp.y = piecePosition.y;


            // lower right diagonal
            for (int i = (int)piecePosition.x + 1; i < boardController.Width; i++)
            {
                testp.x = i;
                testp.y--;
                if (testp.y < 0) { break; }
                if (canMove(testp, boardController))
                {
                    // check if destination has pieces
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);
                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }
            testp.x = piecePosition.x;
            testp.y = piecePosition.y;
            // upper left
            for (int i = (int)piecePosition.x - 1; i >= 0; i--)
            {
                testp.x = i;
                testp.y++;
                if (testp.y >= boardController.Height) { break; }
                if (canMove(testp, boardController))
                {
                    // check if destination has pieces
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);
                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }
            testp.x = piecePosition.x;
            testp.y = piecePosition.y;
            // lower left
            for (int i = (int)piecePosition.x - 1; i >= 0; i--)
            {
                testp.x = i;
                testp.y--;
                if (testp.y < 0) { break; }
                if (canMove(testp, boardController))
                {
                    // check if destination has pieces
                    if (boardController.PieceData.ContainsKey(testp))
                    {
                        if (boardController.PieceData[testp].side != pieceController.side)
                        {
                            result.Add(testp, MoveType.KillMove);
                        }
                        break;
                    }
                    else
                    {
                        result.Add(testp, MoveType.NormalMove);
                    }
                }
            }

            return result;
        }
    }

    public enum MoveType
    {
        NULL = 0,
        //Move without any further action
        NormalMove = 1,
        //Move and kill the piece
        KillMove,
        CurrentPosition
    }
}
