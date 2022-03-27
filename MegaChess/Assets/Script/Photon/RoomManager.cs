using Board;
using Photon.Pun;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Networking
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {

        [SerializeField] private BoardManager boardManager;


        [SerializeField] private GameObject panelPrefab;

        public BoardModel boardSetting = null;
        public ChessRuleBookModel ruleSetting = null;

        [Space]

        [SerializeField] private List<BoardModel> boardModels;
        [SerializeField] private List<ChessRuleBookModel> ruleBooks;

        public void Start()
        {
            boardSetting = boardModels[0];
            ruleSetting = ruleBooks[0];

            if (NetworkManager.Instance.IsMasterClient())
            {
                ShowPanel();
            }
        }

        private void ShowPanel()
        {
            GameObject panel = GameObject.Instantiate(panelPrefab, transform);
            ChessRoomSetupPanel panelControl = panel.GetComponent<ChessRoomSetupPanel>();
            panelControl.Config(boardModels, ruleBooks, this);
        }

        public void StartGame()
        {
            boardManager.boardModel = boardSetting;
            boardManager.ruleBook = ruleSetting;
            boardManager.Setup();
            Destroy(gameObject);
        }

        public void SetChessBoard(PanelTextButton button)
        {
            foreach(BoardModel board in boardModels)
            {
                if(string.Equals(board.ModelName, button.id))
                {
                    boardSetting = board;
                    return;
                }
            }
        }

        public void SetRulebook(PanelTextButton button)
        {
            foreach (ChessRuleBookModel rule in ruleBooks)
            {
                if (string.Equals(rule.ModelName, button.id))
                {
                    ruleSetting = rule;
                    return;
                }
            }
        }
    }
}