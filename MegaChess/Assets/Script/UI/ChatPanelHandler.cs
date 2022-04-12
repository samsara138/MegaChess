using Core;
using ExitGames.Client.Photon;
using Networking;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ChatPanelHandler : MonoBehaviour, IOnEventCallback
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

        #region PhotonEvent

        public void OnEvent(EventData photonEvent)
        {
            PunEvent eventType = (PunEvent)(int)photonEvent.Code;
            switch (eventType)
            {
                case PunEvent.ShowChatEvent:
                    ShowChatEventData data = new ShowChatEventData(photonEvent);
                    HandleChatEvent(data);
                    break;
            }
        }

        private void HandleChatEvent(ShowChatEventData data)
        {
            throw new NotImplementedException();
        }

        #endregion

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