using Core;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ChatPanelHandler : MonoBehaviour
    {
        [SerializeField] private GameObject textPanelLine;

        private void OnEnable()
        {
            EventManager.SubscribeToEvent(Core.EventType.NewTextMessageEvent, OnNewTextMessageHandle);
        }

        private void OnDisable()
        {
            EventManager.UnsubscribeToEvent(Core.EventType.NewTextMessageEvent, OnNewTextMessageHandle);
        }

        private void OnNewTextMessageHandle(object obj)
        {
            TextData data = obj as TextData;

            GameObject bufferObj = PhotonNetwork.Instantiate(textPanelLine.name, Vector3.zero, Quaternion.identity);
            bufferObj.transform.SetParent(transform);
            TextMeshProUGUI textObj = bufferObj.GetComponent<TextMeshProUGUI>();
            textObj.fontSize = 18;
            textObj.color = Color.black;
            textObj.text = data.text;
        }
    }
}