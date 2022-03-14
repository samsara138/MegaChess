using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RoomPanelHandler : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button clickDetector;
        [SerializeField] private TextMeshProUGUI textMesh;

        private string roonName;

        public void Initialize(string roonName)
        {
            this.roonName = roonName;
            textMesh.text = roonName;
            clickDetector.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            PhotonNetwork.JoinRoom(roonName);
            clickDetector.onClick.RemoveAllListeners();
        }
    }
}