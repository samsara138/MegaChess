using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class PlayerInfo
    {
        public string PlayerNickName;

        public PlayerInfo()
        {
            PlayerNickName = "Default Player";
        }

        public PlayerInfo(string nickName)
        {
            PlayerNickName = nickName;
        }
    }
}