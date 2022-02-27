using Core;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "PieceModel", menuName = "Mega Chess/Piece Model")]
    public class PieceModel : Model
    {
        public string PieceName;
        public Sprite Sprite;
        public PieceType PieceType;
    }

    public enum PieceType
    {
        Null = 0,
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pawn
    }
}
