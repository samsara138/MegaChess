using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PanelTextButton : MonoBehaviourPunCallbacks
    {
        public Button clickDetector;
        [SerializeField] private TextMeshProUGUI textMesh;

        public string id;
        private bool oneClick = true;

        public delegate void ClickAction(PanelTextButton panelTextButton);
        private ClickAction clickAction;

        public void Initialize(string displayText, string id, ClickAction clickAction,bool oneClick = true)
        {
            textMesh.text = displayText;

            this.id = id;
            this.oneClick = oneClick;
            this.clickAction = clickAction;

            clickDetector.onClick.AddListener(ClearButton);

        }

        private void ClearButton()
        {
            clickAction.Invoke(this);
            if (oneClick)
                clickDetector.onClick.RemoveAllListeners();
        }
    }
}