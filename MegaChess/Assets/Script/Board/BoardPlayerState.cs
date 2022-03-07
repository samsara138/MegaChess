using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class BoardPlayerState
    {
        public PlayerSide currentPlayer;
        public int currentMoveCount;
        private ChessRuleBook ruleBook;
        private int playerCount;

        public BoardPlayerState(ChessRuleBook ruleBook, int playerCount = 2)
        {
            this.ruleBook = ruleBook;
            this.playerCount = playerCount;

            currentPlayer = ruleBook.firstPlayer;
            currentMoveCount = 0;
        }

        internal void NextMove()
        {
            currentMoveCount += 1;
            if(currentMoveCount >= ruleBook.movePerPlayer)
            {
                currentPlayer += 1 % playerCount;
                currentMoveCount = 0;
            }
        }
    }
}