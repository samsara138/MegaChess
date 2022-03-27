using Board;
using Networking;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChessRoomSetupPanel : MonoBehaviour
    {
        [SerializeField] private GameObject textPanelPrefab;

        [SerializeField] private Transform boardDisplayContainer;
        [SerializeField] private Transform ruleDisplayContainer;
        [SerializeField] private Button startGameButton;


        public void Config(List<BoardModel> boards, List<ChessRuleBookModel> rules, RoomManager roomManager)
        {
            foreach (BoardModel board in boards)
            {
                GameObject buffer = GameObject.Instantiate(textPanelPrefab, boardDisplayContainer);
                PanelTextButton panelTextButton = buffer.GetComponent<PanelTextButton>();
                panelTextButton.Initialize(board.ModelName, board.ModelName, roomManager.SetChessBoard, false);
            }

            foreach (ChessRuleBookModel rule in rules)
            {
                GameObject buffer = GameObject.Instantiate(textPanelPrefab, ruleDisplayContainer);
                PanelTextButton panelTextButton = buffer.GetComponent<PanelTextButton>();
                panelTextButton.Initialize(rule.ModelName, rule.ModelName, roomManager.SetRulebook, false);
            }

            startGameButton.onClick.AddListener(roomManager.StartGame);
        }
    }
}