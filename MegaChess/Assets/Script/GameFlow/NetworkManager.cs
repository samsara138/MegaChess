using Core;
using GameFlow;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        #region Singleton behaviour

        private static NetworkManager instance;

        public static NetworkManager Instance
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
            }
        }

        #endregion

        #region General

        public bool IsMasterClient()
        {
            if (PhotonNetwork.InRoom)
                return PhotonNetwork.IsMasterClient;
            else
                return false;
        }

        #endregion

        #region Init Connection

        // Start is called before the first frame update
        public void ConnectToServer()
        {
            Log("Connecting to server");
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Log("Connected to server");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Log("Joined lobby");
            GameManager.Instance.UpdateGameState(GameState.LobbyMenu);
        }

        #endregion

        #region Lobby

        public void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        #endregion

        #region In Game

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Log("I'm da master");
            }

            PhotonNetwork.LocalPlayer.NickName = "Build Gamer";
            Log("Joined room " + PhotonNetwork.CurrentRoom.Name);
            GameManager.Instance.UpdateGameState(GameState.GameMenu);

            /*
            Log("Joined Room");
            Log("My user ID: " + PhotonNetwork.LocalPlayer.UserId);
            Log("Total Player: " + PhotonNetwork.CountOfPlayersInRooms.ToString());
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                Log("Player in room: " + player.UserId);
            }
            */
        }


        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Log("Player join: " + newPlayer.NickName);

            TextData data = new TextData();
            data.textList = new List<string>();

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                data.textList.Add("Player in room: " + player.UserId);
            }

            EventManager.TriggerEvent(Core.EventType.PlayerJoinRoomEvent, data);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Log(otherPlayer.NickName + " left the room");
        }

        #endregion

        private void Log(string msg)
        {
            if(GlobalParameters.NETWORK_MANAGER_DEBUG)
                Debug.Log("<b><color=#FF5CFF>NetworkManager</color></b> " + msg);
        }
    }
}
