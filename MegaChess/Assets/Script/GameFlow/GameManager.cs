using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;
using Networking;

namespace GameFlow
{
    public enum GameState
    {
        InitState,
        ConnectingState,
        LobbyMenu,
        GameMenu
    }

    public enum GameScenes
    {
        MainMenu = 0,
        BootStrap,
        Lobby,
        Chess
    }

    public class GameManager : MonoBehaviour
    {
        private GameState State;

        private NetworkManager networkManager;
        private PlayerInfo playerInfo;

        #region Singleton behaviour

        private static GameManager instance;
        public static GameManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        #endregion

        private void Start()
        {
            networkManager = NetworkManager.Instance;
            playerInfo = new PlayerInfo();

            UpdateGameState(GameState.InitState);
        }

        public void UpdateGameState(GameState newState)
        {
            Log("Change State: " + newState);
            instance.State = newState;
            switch (newState)
            {
                case GameState.InitState:
                    break;
                case GameState.ConnectingState:
                    HandleConnectingState();
                    break;
                case GameState.LobbyMenu:
                    HandleLobbyState();
                    break;
                case GameState.GameMenu:
                    HandleGameState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void HandleConnectingState()
        {
            SceneManager.LoadScene((int)GameScenes.BootStrap);
            networkManager.ConnectToServer();
        }

        private void HandleLobbyState()
        {
            SceneManager.LoadScene((int)GameScenes.Lobby);
        }

        private void HandleGameState()
        {
            PhotonNetwork.LoadLevel((int)GameScenes.Chess);
        }

        private void Log(string msg)
        {
            if(GlobalParameters.GAME_MANAGER_DEBUG)
                Debug.Log("<b><color=#00FFFF>GameManager</color></b> " + msg);
        }
    }
}