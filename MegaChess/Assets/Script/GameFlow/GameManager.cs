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
        LobbyMenu,
        GameMenu
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
                    HandleInitState();
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

        private void HandleInitState()
        {
            networkManager.ConnectToServer();
        }

        private void HandleLobbyState()
        {
            SceneManager.LoadScene("1.Lobby");
        }

        private void HandleGameState()
        {
            PhotonNetwork.LoadLevel("2.Chess");
            StartCoroutine("WaitForLoad");
        }

        private IEnumerator WaitForLoad()
        {
            while (PhotonNetwork.LevelLoadingProgress < 1f)
            {
                yield return new WaitForEndOfFrame();
            }
            OnLevelLoad();
        }

        private void OnLevelLoad()
        {
        }

        private void Log(string log)
        {
            Debug.Log("<b><color=#00FFFF>GameManager</color></b> " + log);
        }
    }
}