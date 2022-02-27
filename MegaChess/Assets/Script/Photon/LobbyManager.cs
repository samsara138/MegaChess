using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            bufferObj.GetComponent<TextMeshProUGUI>().text = pair.Key;
        }


    }

    private void Start()
    {
        CreateRoomBtn.onClick.AddListener(CreateRoom);
        JoinRoomBtn.onClick.AddListener(JoinRoom);
        cachedRoomList.Clear();

    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateRoomField.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinRoomField.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("2.Chess");
    }
}
