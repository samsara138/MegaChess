using Board;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Networking
{
    public class RoomManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] List<BoardModel> boardModels;
        [SerializeField] List<ChessRuleBook> ruleBooks;

        [SerializeField] GameObject panelPrefab;

        private bool IsInitialize = false;
        private int x = 10;

        public void Start()
        {
            if (NetworkManager.Instance.IsMasterClient())
            {
            }
        }

        private void ShowPanel()
        {
            GameObject panel = GameObject.Instantiate(panelPrefab, transform);
            ChessRoomSetupPanel panelControl = panel.GetComponent<ChessRoomSetupPanel>();
            panelControl.Config(boardModels);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(x);
            }else if (stream.IsReading)
            {
                int y = (int)stream.ReceiveNext();
                Debug.LogError(y);
            }
        }
    }
}