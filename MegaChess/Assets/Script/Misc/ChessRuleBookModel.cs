using Core;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "ChessRuleBookModel", menuName = "Mega Chess/Rule Book Model")]
    public class ChessRuleBookModel : Model
    {
        public int movePerPlayer;
        public PlayerSide firstPlayer;
    }
}