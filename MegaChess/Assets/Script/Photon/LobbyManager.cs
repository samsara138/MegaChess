using Networking;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button CreateRoomBtn;
    [SerializeField] private TMP_InputField CreateRoomField;

    [SerializeField] private Button JoinRoomBtn;
    [SerializeField] private TMP_InputField JoinRoomField;

    [SerializeField] private Transform PanelTransform;
    [SerializeField] private GameObject RoomPanel;


    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    private List<GameObject> rooms = new List<GameObject>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }

        foreach(GameObject obj in rooms)
        {
            Destroy(obj);
        }

        foreach(KeyValuePair<string,RoomInfo> pair in cachedRoomList)
        {
            GameObject bufferObj = GameObject.Instantiate(RoomPanel, PanelTransform);
            RoomPanelHandler panel = bufferObj.GetComponent<RoomPanelHandler>();
            panel.Initialize(pair.Key);
        }
    }

    private void Start()
    {
        CreateRoomBtn.onClick.AddListener(OnClickCreateRoom);
        JoinRoomBtn.onClick.AddListener(OnClickJoinRoom);
        cachedRoomList.Clear();

    }

    private void OnClickCreateRoom()
    {
        NetworkManager.Instance.CreateRoom(CreateRoomField.text);
    }

    private void OnClickJoinRoom()
    {
        NetworkManager.Instance.JoinRoom(JoinRoomField.text);
    }
}
