using Core;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextPanelHandler : MonoBehaviour
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