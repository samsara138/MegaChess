using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "ChessRuleBook", menuName = "Mega Chess/Rule Book Model")]
    public class ChessRuleBook : ScriptableObject
    {
        public int movePerPlayer;
        public PlayerSide firstPlayer;
    }
}