using GameFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MainMenuHandler : MonoBehaviour
    {
        public void OnClickStartGame()
        {
            GameManager.Instance.UpdateGameState(GameState.ConnectingState);
        }
    }
}