using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public enum PlayerSide
    {
        player1 = 0,
        player2,
        player3,
        player4
    }

    public class PlayerInfo
    {
        public string name;
        public PlayerSide side;

        public PlayerInfo(int number)
        {
            name = "player" + number.ToString();
        }
    }

    public class PlayerState
    {
        PlayerInfo[] infos;

        public PlayerState(int playerCount)
        {
            infos = new PlayerInfo[playerCount];
            for(short i = 0; i < infos.Length; i++)
            {
                infos[i] = new PlayerInfo(i);
            }
        }
    }
}